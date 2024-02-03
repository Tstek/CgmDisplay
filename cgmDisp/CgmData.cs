using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace cgmDisp
{
    public class NightscoutConfig
    {
        public string baseUrl;
        public string token;
    }

    public class NightscoutAPI
    {
        string _baseUrl;
        string _token;

        public NightscoutAPI(string url, string token)
        {
            _baseUrl = url;
            _token = token;
        }

        public string GetLatest(int count = 1)
        {
            if (count < 1)
            {
                count = 1;
            }
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("api-secret", _token);
            var url = _baseUrl + $"/api/v1/entries?count={count}";
            HttpResponseMessage response = client.GetAsync(url).Result;
            return response.Content.ReadAsStringAsync().Result;
        }
    }

    public class CgmEntry
    {
        public string _id;
        public string device;
        public long date;
        public string dateString;
        public int sgv;
        public double delta;
        public string direction;
        public string type;
        public int filtered;
        public int unfiltered;
        public int rssi;
        public int noise;
        public string sysTime;
        public int utcOffset;
        public long mills;
    }
}
