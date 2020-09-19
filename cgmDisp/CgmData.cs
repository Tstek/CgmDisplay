using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cgmDisp
{
    public class CgmData
    {
        public int value = 0;
        public string time = "";
        public int x = 0;
        public int trend = 4;
        public string trend_symbol = "";
        public string trend_words = "";
        public int delta = 0;
        public string units = "";
        public double mmol = 0.0;
        public string reading = "";
        public string timestamp = "";
        public string full = "";
        public UserSettings user_settings = null;
    }

    public class UserSettings
    {
        public int normal_bottom_mgdl = 0;
        public int normal_top_mgdl = 0;
        public int urgent_low_mgdl = 0;
    }
}
