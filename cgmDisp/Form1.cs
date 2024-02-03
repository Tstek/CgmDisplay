using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

//Icons made by'Pixel perfect' from www.flaticon.com

namespace cgmDisp
{
    public partial class Form1 : Form
    {
        public Form1()
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

            panelBackGround.MouseDown += PanelBackGround_MouseDown;
            panelBackGround.Select();

            trendArrows.Add("Flat", "→");
            trendArrows.Add("SingleUp", "↑");
            trendArrows.Add("SingleDown", "↓");
            trendArrows.Add("FortyFiveUp", "↗");
            trendArrows.Add("FortyDiveDown", "↘");
            trendArrows.Add("DoubleUp", "↑↑");
            trendArrows.Add("DoubleDown", "↓↓");

            _updateThread = new Thread(GetVals);
            _updateThread.IsBackground = true;
            _updateThread.Start();
        }

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
                    DateTime nextRead = DateTime.Now.AddMinutes(1.0); //just in case the http call fails/junk is returned (if sugar mate is down or something we'll only try every minute instead of every 5 seconds
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
                        //guess it failed!
                    }
                    if (nextRead < DateTime.Now)
                        nextRead = DateTime.Now.AddSeconds(5.0); //if we checked 5 mins after last reading and it hasnt updated yet, check again in 5 seconds
                    Thread.Sleep(nextRead - DateTime.Now);
                }
                catch { /* really dont care if this fails occasionally, just dont break */ }
            }
        }
        private void addVal(CgmEntry data)
        {
            SetText(labelGlucose, string.Format("{0} {1}", data.sgv, trendArrows[data.direction])); //↓↘↑⇈⇊
            SetText(labelDelta, string.Format("{0}{1}", (data.delta > 0 ? "+" : ""), data.delta.ToString("0.0")));
            SetText(labelTime, DateTimeOffset.Parse(data.dateString).LocalDateTime.ToShortTimeString());
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
        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
