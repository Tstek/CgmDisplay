using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Windows.Forms.DataVisualization.Charting;

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
                sugarmateURL = File.ReadAllText(".//sugarmateURL.txt");
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format("{0}\n{1}", "Error: Could not read sugarmateURL.txt", e.Message));
            }
            
            panelBackGround.MouseDown += PanelBackGround_MouseDown;
            panelBackGround.Select();

            _updateThread = new Thread(GetVals);
            _updateThread.IsBackground = true;
            _updateThread.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(sugarmateURL))
            {
                this.Close();
            }
        }

        private readonly string sugarmateURL;
        private Thread _updateThread;
        delegate void dSetText(Control ctrl, string text);
        private void SetText(Control ctrl, string text)
        {
            if(ctrl.InvokeRequired)
            {
                dSetText dst = new dSetText(SetText);
                ctrl.Invoke(dst, new object[] { ctrl, text });
            }
            else
            {
                ctrl.Text = text;
            }
        }

        private void GetVals()
        {
           while(true)
           {
                try
                {
                    DateTime nextRead = DateTime.Now.AddMinutes(1.0); //just in case the http call fails/junk is returned (if sugar mate is down or something we'll only try every minute instead of every 5 seconds
                    try
                    {
                        string resp = HttpsGet(sugarmateURL);
                        if (!string.IsNullOrWhiteSpace(resp))
                        {
                            CgmData data = JsonConvert.DeserializeObject<CgmData>(resp);
                            SetText(labelGlucose, string.Format("{0} {1}", data.value, data.trend_symbol));
                            SetText(labelDelta, string.Format("{0}{1}", (data.delta >0 ? "+" : ""), data.delta.ToString()));
                            SetText(labelTime, data.time);
                            DateTime dataTime = DateTime.Parse(data.timestamp);
                            nextRead = dataTime.AddMinutes(5.4); //seems to be more accurate to when info is available than the sugarmate website by a few seconds, without needing to check each 5 secs
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

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string HttpsGet(string url)
        {
            string retVal = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream resStream = response.GetResponseStream();
                byte[] buffer = new byte[1024];

                int len = resStream.ReadAsync(buffer, 0, 1024).Result;
                retVal = Encoding.UTF8.GetString(buffer, 0, len);
                resStream.Dispose();
                response.Dispose();
            }
            catch// (Exception e) //naahhhhh
            {
            //    MessageBox.Show(e.Message);
            }
            return retVal;
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
