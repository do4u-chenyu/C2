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
        HttpWebResponse cnblogsRespone;
        //string decompression;
        public PostAndGetForm()
        {
            InitializeComponent();
            InitializeSelectedIndex();
        }

        private void InitializeSelectedIndex()
        {
            comboBox1.SelectedIndex = 0; // 默认选 POST 和 UTF-8
            comboBox2.SelectedIndex = 0;
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            textBox.Text = string.Empty;
            textBoxUrl.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            richTextBox1.Text = string.Empty;
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            splitType = comboBox1.SelectedItem as string;
        }

        
        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            encodeOutput = "UTF-8";
            switch (comboBox2.SelectedIndex)
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
                    using (sr = new StreamReader(cnblogsRespone.GetResponseStream()))
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
                richTextBox1.Text = ConvertJsonString(responseResult);
            }
            catch 
            {
                richTextBox1.Text = responseResult;
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
                var request = new HttpRequestMessage(HttpMethod.Head, textBox.Text);
                HttpResponseMessage response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                string result = await response.Content.ReadAsStringAsync();
                richTextBox1.Text = result;
            }
            catch (Exception ex) 
            {
                richTextBox1.Text = ex.Message;
            }
        }

        public async Task OptionsTextAsync()
        {
            var client = new HttpClient(new HttpClientHandler { UseProxy = false });
            var request = new HttpRequestMessage(HttpMethod.Options, textBox.Text);
            HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string result = await response.Content.ReadAsStringAsync();
            richTextBox1.Text = result;
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
                using (StreamReader reader = new StreamReader(stream, Encoding.GetEncoding("UTF-8")))
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
            if (splitType == "POST")
            {
                string data = textBoxUrl.Text;
                byte[] bytesToPost = encodeOutput == "UTF-8" ? Encoding.UTF8.GetBytes(data) : Encoding.Default.GetBytes(data) ;
                string responseResult = String.Empty;
                try
                {
                    req = (HttpWebRequest)HttpWebRequest.Create(textBox.Text);
                    req.Method = splitType;
                    req.Timeout = 150000;
                    req.ContentType = "application/x-www-form-urlencoded";//header
                    req.Headers.Set("cookie", textBox2.Text);
                    if (textBox4.Text != string.Empty)
                    {
                        try
                        {
                            req.Proxy = new WebProxy(textBox4.Text);
                            PostText(req, bytesToPost, responseResult);
                        }
                        catch
                        {
                            richTextBox1.Text = "代理不可用，请更换代理";
                        }
                    }
                    else
                    {
                        PostText(req, bytesToPost, responseResult);
                    }
                }
                catch(System.UriFormatException ex)
                {
                    richTextBox1.Text = ex.Message;
                }
            }
            else if (splitType == "GET")
            {
                //GET没有参数
                if (textBoxUrl.Text == string.Empty)
                {
                    try
                    {
                        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(textBox.Text);
                        req.Method = splitType;
                        req.Timeout = 150000;
                        req.ContentType = "application/x-www-form-urlencoded";
                        req.Headers["Accept-Language"] = "zh-CN,zh;q=0.8";
                        HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                        StringBuilder headerResult = GetHeaders(resp);
                        if (textBox4.Text != string.Empty)
                        {
                            try
                            {
                                WebProxy wp = new WebProxy(textBox4.Text);
                                req.Proxy = wp;
                                string result = GetResultNullParam(resp);
                                richTextBox1.Text = result;
                            }
                            catch
                            {
                                richTextBox1.Text = "代理不可用，请更换代理";
                            }
                        }
                        else
                        {
                            string result = GetResultNullParam(resp);
                            richTextBox1.Text = result;
                        }
                    }
                    catch (Exception ex)
                    {
                        //System.UriFormatException || System.Net.WebException
                        richTextBox1.Text = ex.Message;
                    }
                }
                //Get 含有参数
                else
                {
                    string result = string.Empty;
                    StringBuilder builder = new StringBuilder();
                    builder.Append(textBox.Text + "?");
                    builder.AppendFormat(textBoxUrl.Text);
                    try
                    {
                        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(builder.ToString());
                        req.Timeout = 150000;
                        req.Method = splitType;
                        req.ContentType = "application/x-www-form-urlencoded";
                        req.Headers["Accept-Language"] = "zh-CN,zh;q=0.8";
                        HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                        StringBuilder headerResult = GetHeaders(resp);

                        if (textBox4.Text != string.Empty)
                        {
                            WebProxy wp = new WebProxy(textBox4.Text);
                            req.Proxy = wp;
                            string resultHasParam = GetResultNullParam(resp);
                            richTextBox1.Text = resultHasParam;
                        }
                        else
                        {
                            string resultHasParam = GetResultNullParam(resp);
                            richTextBox1.Text = resultHasParam;
                        }     
                    }
                    catch (Exception ex)
                    {
                        richTextBox1.Text = ex.Message;
                    }
                }
            }
            else if (splitType == "PUT")
            {
                byte[] paramsData = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(textBoxUrl.Text);
                HttpWebRequest re = (HttpWebRequest)HttpWebRequest.Create(textBox.Text);
                req.Method = splitType;
                req.Timeout = 150000;
                req.AllowAutoRedirect = false;
                req.ContentType = "application/json";
                
                if (textBoxUrl.Text.StartsWith("https", StringComparison.OrdinalIgnoreCase))
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
                    richTextBox1.Text = responseStr;
                }
                catch (Exception ex)
                {
                    richTextBox1.Text = ex.Message;
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
}
