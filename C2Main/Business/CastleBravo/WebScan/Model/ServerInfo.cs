using System;
using System.Collections.Generic;

namespace C2.Business.CastleBravo.WebScan.Model
{
    public class ServerInfo
    {
        public String target = string.Empty;//扫描目标
        public String host = string.Empty;//host主机头
        public String url = string.Empty;//pathAndQuery
        public String path = string.Empty;//host主机头
        public int port = 80;
        public String request = string.Empty;
        public String server = string.Empty;
        public String header = string.Empty;
        public String body = string.Empty;
        public String reuqestHeader = string.Empty;
        public Dictionary<String, String> headers = new Dictionary<String, String>();
        public String response = string.Empty;
        public String gzip = string.Empty;
        public String encoding = string.Empty;
        public String contentType = string.Empty;
        public long id = 0;
        public long length = 0;
        public String ip = string.Empty;
        public String type = string.Empty;
        public int code = 0;
        public int mode = 0;
        public String location = string.Empty;
        public long runTime = 0;//获取网页消耗时间，毫秒
        public int sleepTime = 0;//休息时间
        public String powerBy = string.Empty;
        public Boolean timeout = false;
    }
}
