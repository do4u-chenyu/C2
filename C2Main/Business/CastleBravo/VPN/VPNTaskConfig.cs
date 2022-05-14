using C2.Core;
using C2.Utils;
using System;
using System.Text.RegularExpressions;
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
            // 只有vless才有, 先返回一个默认值吧
            return "xtls-rprx-origin";
        }

        internal string security()
        {
            return this.Method;
        }

        internal int alterId()
        {
            if (configType() != (int)EConfigType.Vmess)
                return 0;

            Match mat = Regex.Match(this.OtherInfo, @"\baid=(\d+)");
            if (mat.Success && mat.Groups[1].Success)
                return ConvertUtil.TryParseInt(mat.Groups[1].Value);

            return 0;
        }

        internal string network()
        {
            Match mat = Regex.Match(this.OtherInfo, @"\bnet=([^;]+)");
            if (mat.Success && mat.Groups[1].Success)
            {
                string ret = mat.Groups[1].Value;
                return ret.IsNullOrEmpty() ? v2rayN.Global.DefaultNetwork : ret;
            }

            return v2rayN.Global.DefaultNetwork;
        }

        // 伪装访问地址
        internal string requestHost()
        {
            // 不同协议字段不同, vless样本太少
            Match mat = null;
           
            if (configType() == (int)EConfigType.Vmess)
                mat = Regex.Match(this.OtherInfo, @"\bhost=([^;]+)");

            if (configType() == (int)EConfigType.Trojan)
                mat = Regex.Match(this.OtherInfo, @"\bsni=([^;]+)");

            if (mat != null && mat.Success && mat.Groups[1].Success)
                return mat.Groups[1].Value;

            return string.Empty;
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
            if (configType() == (int)EConfigType.Vmess)
            {
                Match mat = Regex.Match(this.OtherInfo, @"\btls=([^;]+)");
                if (mat.Success && mat.Groups[1].Success)
                    return mat.Groups[1].Value;
            }

            // 不能100%肯定,目前观察看,似乎Trojan都是tls
            if (configType() == (int)EConfigType.Trojan)
                return v2rayN.Global.StreamSecurity;

            // 剩下的不知道了
            return string.Empty;
        }

        internal bool allowInsecure()
        {
            // 不知道,目前观察到的好像都是false
            return false;
        }

        internal string path()
        {
            // vmess 和 vless 有
            // 不同协议字段不同, vless样本太少
            Match mat = null;

            if (configType() == (int)EConfigType.Vmess)
                mat = Regex.Match(this.OtherInfo, @"\bpath=([^;]+)");

            if (configType() == (int)EConfigType.VLESS)
                mat = Regex.Match(this.OtherInfo, @"\bpath=([^;]+)");

            if (mat != null && mat.Success && mat.Groups[1].Success)
                return mat.Groups[1].Value;

            return string.Empty;
        }

        internal string headerType()
        {
            string ret = string.Empty;
            Match mat = null;

            if (configType() == (int)EConfigType.Vmess)
                mat = Regex.Match(this.OtherInfo, @"\bheaderType==([^;]+)");

            if (configType() == (int)EConfigType.VLESS)
                mat = Regex.Match(this.OtherInfo, @"\bheaderType==([^;]+)");

            if (mat != null && mat.Success && mat.Groups[1].Success)
                return mat.Groups[1].Value;

            if (configType() == (int)EConfigType.Vmess)
                return v2rayN.Global.None;
            if (configType() == (int)EConfigType.VLESS)
                return v2rayN.Global.None;

            return string.Empty;
        }
        #endregion
    }
}
