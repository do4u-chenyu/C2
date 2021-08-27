using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Business.CastleBravo.WebScan.Tools
{
    class Config
    {
        public string Method { set; get; }
        public string ShowCodes { set; get; }
        public int ThreadSize { set; get; }
        public int TimeOut { set; get; }
        public int SleepTime { set; get; }//毫秒

        public int scanMode = 2;
        public string UserAgent = "Mozilla/5.0 (compatible; Baiduspider-render/2.0; +http://www.baidu.com/search/spider.html)";
        public string url = "";
        public bool scanWAF = false;
        public bool getBanner = false;
        public string ext = "";
        public string request = "";
        public string key = "";
        public int show = 0;
        public int isExists = 0;
        public bool getHeaderFirstLine = true;
        public int dicType = 0;
        public int contentSelect = 0;
        public int contentLength = -2;
        public string contentType = "";
        public bool keeAlive = true;
    }
}
