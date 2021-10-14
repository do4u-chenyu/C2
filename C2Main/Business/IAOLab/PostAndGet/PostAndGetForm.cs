using C2.Controls;
using C2.Utils;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace C2.Business.IAOLab.PostAndGet
{
    public partial class PostAndGetForm : BaseDialog
    {
        string splitType;
        string encodeOutput;
        string IpProtocol;
        HttpWebResponse cnblogsRespone;
        public PostAndGetForm()
        {
            InitializeComponent();
            InitializeSelectedIndex();
        }

        private void InitializeSelectedIndex()
        {
            comboBoxHttpMethod.SelectedIndex = 0; // 默认选 POST 和 UTF-8
            comboBoxEncodeMethod.SelectedIndex = 0;
            comboBoxIpProtocol.SelectedIndex = 0;// 默认选择HTTP
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            textBoxUrl.Text = string.Empty;
            textBoxPost.Text = string.Empty;
            textBoxCookie.Text = string.Empty;
            textBoxHeader.Text = string.Empty;
            textBoxIp.Text = string.Empty;
            richTextBoxResponse.Text = string.Empty;
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            splitType = comboBoxHttpMethod.SelectedItem as string;
        }
        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            encodeOutput = "UTF-8";
            switch (comboBoxEncodeMethod.SelectedIndex)
            {
                case 0:
                    encodeOutput = "UTF-8";
                    break;
                case 1:
                    encodeOutput = "GBK";
                    break;
            }       
        }
        
        private string ConvertJsonString(string json)
        {
            JsonSerializer serializer = new JsonSerializer();
            TextReader tr = new StringReader(json);
            JsonTextReader jtr = new JsonTextReader(tr);
            object obj = serializer.Deserialize(jtr);
            if (obj != null)
            {
                StringWriter textWriter = new StringWriter();
                JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                {
                    Formatting = Formatting.Indented,
                    Indentation = 4,
                    IndentChar = OpUtil.Blank
                };
                serializer.Serialize(jsonWriter, obj);
                return textWriter.ToString();
            }
            else
            {
                return json;
            }
        }

        private void PostText(HttpWebRequest req, byte[] bytesToPost, string responseResult)
        {
            req.ContentLength = bytesToPost.Length;
            try 
            {
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(bytesToPost, 0, bytesToPost.Length);
                }
                cnblogsRespone = (HttpWebResponse)req.GetResponse();
                if (cnblogsRespone != null && cnblogsRespone.StatusCode == HttpStatusCode.OK)
                {
                    StreamReader sr;
                    System.Text.Encoding readerEncode = encodeOutput == "UTF-8" ? Encoding.UTF8 : Encoding.Default;
                    using (sr = new StreamReader(cnblogsRespone.GetResponseStream(), readerEncode))
                    {
                        responseResult = sr.ReadToEnd();
                    }
                    sr.Close();
                }
                cnblogsRespone.Close();
            }
            catch(System.Net.WebException ex) 
            {
                responseResult = ex.Message;
            }
            /*
            HttpWebResponse hwr = (HttpWebResponse)req.GetResponse();
            //StringBuilder sb = new StringBuilder();
            //foreach (DictionaryEntry head in hwr.Headers)
            //    sb.AppendLine(String.Format("{0}:{1}", head.Key, head.Value));

            WebHeaderCollection head = hwr.Headers;
            IEnumerator iem = head.GetEnumerator();
            ArrayList value = new ArrayList();
            for (int i = 0; iem.MoveNext(); i++)
            {
                string key = head.GetKey(i);
                value.Add(head.GetKey(i) + ":" + head.Get(key) + "\r\n");
            }
            StringBuilder ss = new StringBuilder();
            foreach (var s in value)
            {
                ss.Append(s.ToString());
            }
            */
            try
            {
                string result = encodeOutput == "UTF-8" ? Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(responseResult)) : Encoding.Default.GetString(Encoding.Default.GetBytes(responseResult));
                richTextBoxResponse.Text = ConvertJsonString(result.ToString());
            }
            catch 
            {
                string result = encodeOutput == "UTF-8" ? Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(responseResult)) : Encoding.Default.GetString(Encoding.Default.GetBytes(responseResult));
                richTextBoxResponse.Text = result.ToString();
            }
        }
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }
        public static byte[] ReadFully(Stream stream)
        {
            byte[] buffer = new byte[512];
            using (MemoryStream ms = new MemoryStream())
            {
                while (true)
                {
                    int read = stream.Read(buffer, 0, buffer.Length);
                    if (read <= 0)
                        return ms.ToArray();
                    ms.Write(buffer, 0, read);
                }
            }
        }

        public async Task HeadTextAsync()
        {
            try 
            {
                var client = new HttpClient(new HttpClientHandler { UseProxy = false });
                var request = new HttpRequestMessage(HttpMethod.Head, textBoxUrl.Text);
                HttpResponseMessage response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                string result = await response.Content.ReadAsStringAsync();
                richTextBoxResponse.Text = result;
            }
            catch (Exception ex) 
            {
                richTextBoxResponse.Text = ex.Message;
            }
        }

        public async Task OptionsTextAsync()
        {
            var client = new HttpClient(new HttpClientHandler { UseProxy = false });
            var request = new HttpRequestMessage(HttpMethod.Options, textBoxUrl.Text);
            HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string result = await response.Content.ReadAsStringAsync();
            richTextBoxResponse.Text = result;
        }

        public StringBuilder GetHeaders(HttpWebResponse resp)
        {
            WebHeaderCollection head = resp.Headers;
            IEnumerator iem = head.GetEnumerator();
            ArrayList value = new ArrayList();
            for (int i = 0; iem.MoveNext(); i++)
            {
                string key = head.GetKey(i);
                value.Add(head.GetKey(i) + ":" + head.Get(key) + "\r\n");
            }
            StringBuilder ss = new StringBuilder();
            foreach (var s in value)
            {
                ss.Append(s.ToString());
            }
            return ss;
        }
        public string GetResultNullParam(HttpWebResponse resp)
        {
            string getResult = string.Empty;
            Stream stream = resp.GetResponseStream();
            try
            {
                System.Text.Encoding readerEncode = encodeOutput == "UTF-8" ? Encoding.UTF8 : Encoding.Default;
                using (StreamReader reader = new StreamReader(stream, readerEncode))
                {
                    getResult = reader.ReadToEnd();
                }
                stream.Close();
            }
            catch(Exception ex)
            {
                getResult = ex.Message;
            }
            return getResult;
        }

        HttpWebRequest req;
        private async void Submit_ClickAsync(object sender, EventArgs e)
        {
            using (GuarderUtil.WaitCursor)
            {
                if (splitType == "POST")
                {
                    string data = textBoxPost.Text;
                    //byte[] bytesToPost = encodeOutput == "UTF-8" ? Encoding.UTF8.GetBytes(data) : Encoding.UTF8.GetBytes(data);
                    byte[] bytesToPost = Encoding.UTF8.GetBytes(data);
                    string responseResult = String.Empty;
                    try
                    {
                        req = (HttpWebRequest)HttpWebRequest.Create(textBoxUrl.Text);
                        req.Method = splitType;
                        req.Timeout = Convert.ToInt32(textBoxTime.Text) * 1000;
                        req.ContentType = "application/x-www-form-urlencoded";//header
                        req.Headers.Set("cookie", textBoxCookie.Text);
                        if (textBoxIp.Text != string.Empty)
                        {
                            try
                            {
                                WebProxy proxy = new WebProxy();
                                string IpPrefix = IpProtocol == "HTTP" ? "http://" : "socks://";
                                proxy.Address = new Uri(String.Format("{0}{1}", IpPrefix, textBoxIp.Text));
                                req.Proxy = proxy;
                                PostText(req, bytesToPost, responseResult);
                            }
                            catch(Exception ex)
                            {
                                richTextBoxResponse.Text = ex.Message;
                            }
                        }
                        else
                        {
                            PostText(req, bytesToPost, responseResult);
                        }
                    }
                    catch (Exception ex)
                    {
                        richTextBoxResponse.Text = ex.Message;
                    }
                }
                else if (splitType == "GET")
                {
                    //GET没有参数
                    if (textBoxPost.Text == string.Empty)
                    {
                        //try
                        //{
                            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(textBoxUrl.Text);
                            req.Method = splitType;
                            req.Timeout = Convert.ToInt32(textBoxTime.Text) * 1000;
                            req.ContentType = "application/x-www-form-urlencoded";
                            req.Headers["Accept-Language"] = "zh-CN,zh;q=0.8";
                            if (textBoxIp.Text != string.Empty)
                            {
                                try
                                {
                                    WebProxy proxy = new WebProxy();
                                    string IpPrefix = IpProtocol == "HTTP" ? "http://" : "socks://";
                                    proxy.Address = new Uri(String.Format("{0}{1}", IpPrefix, textBoxIp.Text));
                                    req.Proxy = proxy;
                                    HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                                    StringBuilder headerResult = GetHeaders(resp);
                                    string result = GetResultNullParam(resp);
                                    richTextBoxResponse.Text = result;
                                }
                                catch(Exception ex)
                                {
                                    richTextBoxResponse.Text = ex.Message;
                                }
                            }
                            else
                            {
                                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                                string result = GetResultNullParam(resp);
                                richTextBoxResponse.Text = result;
                            }
                        //}
                        //catch (Exception ex)
                        //{
                            //richTextBoxResponse.Text = ex.Message;
                        //}
                    }
                    //Get 含有参数
                    else
                    {
                        string result = string.Empty;
                        StringBuilder builder = new StringBuilder();
                        builder.Append(textBoxUrl.Text + "?");
                        builder.AppendFormat(textBoxPost.Text);
                        try
                        {
                            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(builder.ToString());
                            req.Timeout = Convert.ToInt32(textBoxTime.Text) * 1000;
                            req.Method = splitType;
                            req.ContentType = "application/x-www-form-urlencoded";
                            req.Headers["Accept-Language"] = "zh-CN,zh;q=0.8";
                            if (textBoxIp.Text != string.Empty)
                            {
                                WebProxy proxy = new WebProxy();
                                string IpPrefix = IpProtocol == "HTTP" ? "http://" : "socks://";
                                proxy.Address = new Uri(String.Format("{0}{1}", IpPrefix, textBoxIp.Text));
                                req.Proxy = proxy;
                                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                                StringBuilder headerResult = GetHeaders(resp);
                                string resultHasParam = GetResultNullParam(resp);
                                richTextBoxResponse.Text = resultHasParam;
                            }
                            else
                            {
                                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                                string resultHasParam = GetResultNullParam(resp);
                                richTextBoxResponse.Text = resultHasParam;
                            }
                        }
                        catch (Exception ex)
                        {
                            richTextBoxResponse.Text = ex.Message;
                        }
                    }
                }
                else if (splitType == "PUT")
                {
                    byte[] paramsData = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(textBoxPost.Text);
                    HttpWebRequest re = (HttpWebRequest)HttpWebRequest.Create(textBoxUrl.Text);
                    req.Method = splitType;
                    req.Timeout = Convert.ToInt32(textBoxTime.Text) * 1000;
                    req.AllowAutoRedirect = false;
                    req.ContentType = "application/json";

                    if (textBoxPost.Text.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                    {
                        ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                    }
                    Stream requestStream = null;
                    string responseStr = string.Empty;
                    try
                    {
                        if (paramsData != null)
                        {
                            req.ContentLength = paramsData.Length;
                            requestStream = req.GetRequestStream();
                            requestStream.Write(paramsData, 0, paramsData.Length);
                            requestStream.Close();
                        }
                        else
                        {
                            req.ContentLength = 0;
                        }
                        using (HttpWebResponse webResponse = (HttpWebResponse)req.GetResponse())
                        {
                            Stream getStream = webResponse.GetResponseStream();
                            byte[] outBytes = ReadFully(getStream);
                            getStream.Close();
                            responseStr = Encoding.UTF8.GetString(outBytes);
                        }
                        richTextBoxResponse.Text = responseStr;
                    }
                    catch (Exception ex)
                    {
                        richTextBoxResponse.Text = ex.Message;
                    }
                }
                else if (splitType == "HEAD")
                {
                    await HeadTextAsync();
                }
                else if (splitType == "OPTIONS")
                {
                    await OptionsTextAsync();
                }
            }     
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            IpProtocol = comboBoxIpProtocol.SelectedItem as string;
        }
    }
}
