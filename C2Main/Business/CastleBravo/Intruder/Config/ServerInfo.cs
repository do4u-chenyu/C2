using System;
using System.Collections.Generic;

namespace C2.Business.CastleBravo.Intruder.Config
{
    class ServerInfo
    {
        public String target = "";//扫描目标
        public String host = "";//host主机头
        public String url = "";//pathAndQuery
        public String path = "";//host主机头
        public int port = 80;
        public String request = "";
        public String server = "";
        public String header = "";
        public String body = "";
        public String reuqestHeader = "";
        public Dictionary<String, String> headers = new Dictionary<String, String>();
        public String response = "";
        public String gzip = "";
        public String encoding = "";
        public String contentType = "";
        public long id = 0;
        public long length = 0;
        public String ip = "";
        public String type = "";
        public int code = 0;
        public String password = "";
        public String mistake = "";
        public int mode = 0;
        public String location = "";
        public long runTime = 0;//获取网页消耗时间，毫秒
        public int sleepTime = 0;//休息时间
        public String powerBy = "";
        public String timeout = "";
        public String content = "";
        //public List<string> responseHeaders = new List<string>();
        public String responseHeaders = "";
    }
}
