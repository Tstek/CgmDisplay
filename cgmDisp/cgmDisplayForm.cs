using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using System.Windows.Forms.DataVisualization.Charting;
using System.Linq;
using System.Drawing;

//Icons made by 'Pixel perfect' from www.flaticon.com

namespace cgmDisp
{
    public partial class cgmDisplayForm : Form
    {
        public cgmDisplayForm()
        {
            InitializeComponent();
            try
            {
                string configJson = File.ReadAllText(".//nightscoutConfig.json");

                NightscoutConfig config = JsonConvert.DeserializeObject<NightscoutConfig>(configJson);
                nightscout = new NightscoutAPI(config.baseUrl, config.token);
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format("{0}\n{1}", "Error: Could not read nightscoutConfig.json", e.Message));
            }

            bgGraph.MouseDown += PanelBackGround_MouseDown;
            panelBackGround.MouseDown += PanelBackGround_MouseDown;
            panelBackGround.Select();

            trendArrows.Add("Flat", "→");
            trendArrows.Add("SingleUp", "↑");
            trendArrows.Add("SingleDown", "↓");
            trendArrows.Add("FortyFiveUp", "↗");
            trendArrows.Add("FortyFiveDown", "↘");
            trendArrows.Add("DoubleUp", "↑↑");
            trendArrows.Add("DoubleDown", "↓↓");

            //initial values

            string resp = nightscout.GetLatest(36); //36 data points should be 3hrs
            if (!string.IsNullOrWhiteSpace(resp))
            {
                CgmEntry[] entries = JsonConvert.DeserializeObject<CgmEntry[]>(resp);
                BgValues = new List<int>();
                BgTimes = new List<DateTime>();

                for (int i = entries.Length - 1; i >= 0; i--)//points are in reverse order
                {
                    BgValues.Add(entries[i].sgv);
                    BgTimes.Add(DateTimeOffset.Parse(entries[i].dateString).LocalDateTime);
                }
                SetGraph(bgGraph, BgTimes, BgValues);
            }

            _updateThread = new Thread(GetVals);
            _updateThread.IsBackground = true;
            _updateThread.Start();
        }
        private List<int> BgValues;
        private List<DateTime> BgTimes;
        private void Form1_Load(object sender, EventArgs e)
        {
            if (nightscout == null)
            {
                this.Close();
            }
        }

        NightscoutAPI nightscout = null;
        private Dictionary<string, string> trendArrows = new Dictionary<string, string>();
        private Thread _updateThread;
    
        private void GetVals()
        {            
            while (true)
            {
                try
                {
                    DateTime nextRead = DateTime.Now.AddMinutes(1.0); //just in case the http call fails/junk is returned (if nightscout is down or something we'll only try every minute instead of every 5 seconds
                    try
                    {
                        string resp = nightscout.GetLatest(1);
                        if (!string.IsNullOrWhiteSpace(resp))
                        {
                            CgmEntry[] entries = JsonConvert.DeserializeObject<CgmEntry[]>(resp);
                            addVal(entries[0]);
                            DateTime dataTime = DateTime.Parse(entries[0].dateString);
                            nextRead = dataTime.AddMinutes(5.3); //seems to be more accurate to when info is available than the website by a few seconds, without needing to check each 5 secs
                        }
                    }
                    catch
                    {
                        //guess it failed! probably home assistant is down/bad connection.
                    }
                    if (nextRead < DateTime.Now)
                        nextRead = DateTime.Now.AddSeconds(5.0); //if we checked 5 mins after last reading and it hasnt updated yet, check again in 5 seconds
                    Thread.Sleep(nextRead - DateTime.Now);
                }
                catch { /* really hasn't been an issue if this fails occasionally, just dont break */ }
            }
        }
        private void addVal(CgmEntry data)
        {
            SetText(labelGlucose, string.Format("{0} {1}", data.sgv, trendArrows[data.direction])); //↓↘↑⇈⇊
            SetText(labelDelta, string.Format("{0}{1}", (data.delta > 0 ? "+" : ""), data.delta.ToString("0.0")));
            SetText(labelTime, DateTimeOffset.Parse(data.dateString).LocalDateTime.ToShortTimeString());
            BgValues.Add(data.sgv);
            BgTimes.Add(DateTimeOffset.Parse(data.dateString).LocalDateTime);
            int removed = BgValues[0]; 
            BgValues.RemoveAt(0);
            BgTimes.RemoveAt(0);
            AddGraphVal(bgGraph, BgTimes.Last(), BgValues.Last(), removed >= (int)category.high ? category.high : (removed <= (int)category.low ? category.low : category.mid));
        }

        #region UI
        delegate void dSetText(Control ctrl, string text);
        private void SetText(Control ctrl, string text)
        {
            if (ctrl.InvokeRequired)
            {
                dSetText dst = new dSetText(SetText);
                ctrl.Invoke(dst, new object[] { ctrl, text });
            }
            else
            {
                ctrl.Text = text;
            }
        }
        enum category
        {
            mid = 0,
            high = 170,
            low = 70
        }
        delegate void dSetGraph(Chart ctrl, List<DateTime> times, List<int> values);
        private void SetGraph(Chart chart,  List<DateTime> times, List<int> values)
        {
            if (chart.InvokeRequired)
            {
                dSetGraph dsg = new dSetGraph(SetGraph);
                chart.Invoke(dsg, new object[] { chart, times, values });
            }
            else
            {
                chart.Series.Clear();
                chart.Series.Add(category.high.ToString()).Color = Color.FromArgb(253, 151, 31);
                chart.Series.Add(category.mid.ToString()).Color = Color.FromArgb(102, 217, 239);
                chart.Series.Add(category.low.ToString()).Color = Color.FromArgb(249, 38, 114);
                chart.Series[category.high.ToString()].ChartType = SeriesChartType.FastPoint;
                chart.Series[category.mid.ToString()].ChartType = SeriesChartType.FastPoint;
                chart.Series[category.low.ToString()].ChartType = SeriesChartType.FastPoint;
                chart.Series[category.high.ToString()].MarkerStyle = MarkerStyle.Circle;
                chart.Series[category.mid.ToString()].MarkerStyle = MarkerStyle.Circle;
                chart.Series[category.low.ToString()].MarkerStyle = MarkerStyle.Circle;
                chart.Series[category.high.ToString()].XValueType = ChartValueType.Time;
                chart.Series[category.mid.ToString()].XValueType = ChartValueType.Time;
                chart.Series[category.low.ToString()].XValueType = ChartValueType.Time;
                chart.Series[category.high.ToString()].YValueType = ChartValueType.Int32;
                chart.Series[category.mid.ToString()].YValueType = ChartValueType.Int32;
                chart.Series[category.low.ToString()].YValueType = ChartValueType.Int32;

                for (int i = 0; i < values.Count; i++)
                {
                    if (values[i] >= (int)category.high)
                    {
                        chart.Series[category.high.ToString()].Points.AddXY(times[i], values[i]);
                    } 
                    else if (values[i] <= (int)category.low)
                    {
                        chart.Series[category.low.ToString()].Points.AddXY(times[i], values[i]);
                    } 
                    else
                    {
                        chart.Series[category.mid.ToString()].Points.AddXY(times[i], values[i]);
                    }
                }
                chart.ChartAreas[0].AxisY.Maximum = Math.Max((values.Max() + 10), 200);
                chart.ChartAreas[0].AxisY.Minimum = Math.Min((values.Min() - 10), 80);
                chart.ChartAreas[0].RecalculateAxesScale();

            }
        }
        delegate void dAddGraphVal(Chart ctrl, DateTime time, int value, category removeFrom);
        private void AddGraphVal(Chart chart, DateTime time, int value, category removeFrom)
        {
            if (chart.InvokeRequired)
            {
                dAddGraphVal dsg = new dAddGraphVal(AddGraphVal);
                chart.Invoke(dsg, new object[] { chart, time, value, removeFrom });
            }
            else
            {
                chart.Series[removeFrom.ToString()].Points.RemoveAt(0);
                if (value >= (int)category.high)
                {
                    chart.Series[category.high.ToString()].Points.AddXY(time, value);
                }
                else if (value <= (int)category.low)
                {
                    chart.Series[category.low.ToString()].Points.AddXY(time, value);
                }
                else
                {
                    chart.Series[category.mid.ToString()].Points.AddXY(time, value);
                }
                chart.ChartAreas[0].AxisY.Maximum = (BgValues.Max() + 5);
                chart.ChartAreas[0].AxisY.Minimum = (BgValues.Min() - 5);
                chart.ChartAreas[0].RecalculateAxesScale();
            }
        }
        //this lets us hide the windows title bar (mostly just to make things smaller/mesh better in VR using XSOverlay)
        //also toggles always on top mode
        private static FormBorderStyle fbs = FormBorderStyle.None;
        private void labelTime_Click(object sender, EventArgs e)
        {
            TopMost = !TopMost;
            if (this.FormBorderStyle != FormBorderStyle.None)
            {
                fbs = this.FormBorderStyle;
                this.FormBorderStyle = FormBorderStyle.None;
            }
            else
            {
                this.FormBorderStyle = fbs;
            }
        }
        #endregion UI
        #region MoveWithBackground
        //this lets us still move around the window while the titlebar is hidden by clicking and dragging the background
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void PanelBackGround_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        #endregion MoveWithBackground
    }
}
