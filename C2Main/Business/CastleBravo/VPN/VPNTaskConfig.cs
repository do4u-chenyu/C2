using C2.Utils;
using System;
using v2rayN.Mode;

namespace C2.Business.CastleBravo.VPN
{
    [Serializable]
    class VPNTaskConfig
    {
        public static readonly VPNTaskConfig Empty = new VPNTaskConfig();

        public string CreateTime;      // 类字段顺序与持久化要求必须保持一致
        public string Remark;
        public string Host;
        public string Port;
        public string Password;
        public string Method;          // 加密方法
        public string Status;          // 验活结果
        public string SSVersion;       // 服务器版本, 如 ss/ssr/vmess
        public string ProbeInfo;       // 探针信息
        public string OtherInfo;       // 其他信息
        public string IP;              // IP地址
        public string Country;         // 归属地
        public string Content;         // 分享地址原内容
        

        public VPNTaskConfig() : this(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty) { }

        public VPNTaskConfig(string cTime, string remark, string host, string port, string pwd, string method, string status, string SSVersion, string probeinfo, string otherinfo, string ip, string c1, string ss)
        : this(new string[] { cTime, remark, host, port, pwd, method, status, SSVersion, probeinfo, otherinfo, ip, c1, ss})
        { }
        public VPNTaskConfig(string[] array)
        {
            if(array.Length > 9)
            {
                CreateTime = array[0];
                Remark = array[1];
                Host = array[2];
                Port = array[3];
                Password = array[4];
                Method = array[5];
                Status = array[6];
                SSVersion = array[7];
                ProbeInfo = array[8];
                OtherInfo = array[9];
            }

            IP = array.Length > 10 ? array[10] : "0.0.0.0";
            Country = array.Length > 11 ? array[11] : string.Empty;
            Content = array.Length > 12 ? array[12] : string.Empty;
        }

        #region 跟v2ray代码兼容的字段
        internal bool muxEnabled = false;
        internal KcpItem kcpItem
        {
            get; set;
        }
        internal string address()
        {
            return this.Host;
        }

        internal int port()
        {
            return ConvertUtil.TryParseInt(this.Port, 0);
        }

        internal string id()
        {
            return this.Password;
        }

        internal string flow()
        {
            throw new NotImplementedException();
        }

        internal string security()
        {
            return this.Method;
        }

        internal int alterId()
        {
            throw new NotImplementedException();
        }

        internal string network()
        {
            throw new NotImplementedException();
        }

        internal string requestHost()
        {
            throw new NotImplementedException();
        }

        internal int configType()
        {
            int ret;
            switch (this.SSVersion.ToLower())
            {
                case "ss":
                    ret = (int)EConfigType.Shadowsocks;
                    break;
                case "vmess":
                    ret = (int)EConfigType.Vmess;
                    break;
                case "vless":
                    ret = (int)EConfigType.VLESS;
                    break;
                case "trojan":
                    ret = (int)EConfigType.Trojan;
                    break;
                case "Socks":
                    ret = (int)EConfigType.Socks;
                    break;
                default:
                    ret = (int)EConfigType.Custom;
                    break;
            }
            return ret;
        }

        internal string streamSecurity()
        {
            throw new NotImplementedException();
        }

        internal bool allowInsecure()
        {
            return false;
        }

        internal string path()
        {
            throw new NotImplementedException();
        }

        internal string headerType()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
