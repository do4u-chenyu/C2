using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace C2.IAOLab.WifiMac
{
    public class WifiMac
    {
        private static WifiMac instance;
        public static WifiMac GetInstance()
        {
            if (instance == null)
                instance = new WifiMac();
            return instance;
        }
        public String MacLocate(String input)
        {
                string macList = input;
                string[] macArr = macList.Split('\n');
                int j = macArr.Length+1;
                for (int i = 0; i < j; i++)
                {
                    string mac = macArr[i];
                    string location = getInfo(mac);
                    StringBuilder macLocation = new StringBuilder();
                    string m_macLocation = mac + "\t" + location + "\n";
                    macLocation.Append(m_macLocation);
                    string s_macLocation = macLocation.ToString();
                    return s_macLocation;
                }
                return null;
           
        }
        public string getInfo(string mac)
        {

            string url = "http://218.94.117.234%3A8484%2FTest01%2Fsearch.do&hts=http%3A%2F%2F&type=Post&charset=UTF-8&cookies=&params_box=true&header_box=true&cookie_box=false&parms_tab=tab_kv&kvParms=%5B%7B%22key%22%3A%22mac%22%2C%22value%22%3A%22" + mac + "%22%7D%5D&kvHeads=%5B%7B%22key%22%3A%22Content-Type%22%2C%22value%22%3A%22application%2Fx-www-form-urlencoded%3Bcharset%3DUTF-8%22%7D%5D";
            Encoding encoding = Encoding.UTF8;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.ContentType = "application/json";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }

        }

    }
}
