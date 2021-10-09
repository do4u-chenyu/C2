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
        string encodeoutput;
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
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
        }


        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            splitType = comboBox1.SelectedItem as string;
        }

        
        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            encodeoutput = "UTF-8";
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    encodeoutput = "UTF-8";
                    break;
                case 1:
                    encodeoutput = "GBK";
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
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(bytesToPost, 0, bytesToPost.Length);
            }
            HttpWebResponse cnblogsRespone = (HttpWebResponse)req.GetResponse();
            if (cnblogsRespone != null && cnblogsRespone.StatusCode == HttpStatusCode.OK)
            {
                StreamReader sr;
                using (sr = new StreamReader(cnblogsRespone.GetResponseStream()))
                {
                    responseResult = sr.ReadToEnd();
                }
                sr.Close();
            }
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
            cnblogsRespone.Close();
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
            var client = new HttpClient(new HttpClientHandler { UseProxy = false });
            var request = new HttpRequestMessage(HttpMethod.Head, textBox.Text);
            HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string result = await response.Content.ReadAsStringAsync();
            richTextBox1.Text = result;
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
            }
            finally
            {
                stream.Close();
            }
            return getResult;
        }


        HttpWebRequest req;
        private async void Submit_ClickAsync(object sender, EventArgs e)
        {
            if (splitType == "POST")
            {
                string data = textBox1.Text;
                byte[] bytesToPost = encodeoutput == "UTF-8" ? Encoding.UTF8.GetBytes(data) : Encoding.Default.GetBytes(data) ;
                string responseResult = String.Empty;
                try
                {
                    req = (HttpWebRequest)HttpWebRequest.Create(textBox.Text);
                    req.Method = splitType;
                    req.ContentType = textBox3.Text;//header
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
                catch
                {
                    richTextBox1.Text = "请输入正确的接口或参数";
                }
            }


            else if (splitType == "GET")
            {
                //GET没有参数
                if (textBox1.Text == string.Empty)
                {
                    try
                    {
                        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(textBox.Text);
                        req.Method = splitType;
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
                    catch 
                    {
                        richTextBox1.Text = "请输入正确的接口或参数";
                    }
                }
                //Get 含有参数
                else
                {
                    string result = string.Empty;
                    StringBuilder builder = new StringBuilder();
                    builder.Append(textBox.Text);
                    /*
                    if (dict.Count > 0)
                    {
                        builder.Append("?");
                        int i = 0;
                        foreach (var item in dic)
                        {
                            if (i > 0)
                                builder.Append("&");
                            builder.AppendFormat("{0}={1}", item.Key, item.Value);
                            i++;
                        }
                    }
                    */
                    builder.AppendFormat(textBox1.Text);

                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(builder.ToString());
                    //添加参数
                    HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
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
                    Stream stream = resp.GetResponseStream();
                    try
                    {
                        //获取内容
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            result = reader.ReadToEnd();
                        }
                    }
                    finally
                    {
                        stream.Close();
                    }
                    richTextBox1.Text = result;
                }
            }
            else if (splitType == "PUT")
            {
                byte[] datas = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(textBox1.Text);//data可以直接传字节类型 byte[] data,然后这一段就可以去掉
                //if (encoding == null)
                    //encoding = Encoding.UTF8;
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(textBox.Text);
                request.Method = splitType;
                request.Timeout = 150000;
                request.AllowAutoRedirect = false;
                if (!string.IsNullOrEmpty(textBox3.Text))
                {
                    request.ContentType = "application/json";
                }
                if (textBox1.Text.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                }
                Stream requestStream = null;
                string responseStr = null;
                try
                {
                    if (datas != null)
                    {
                        request.ContentLength = datas.Length;
                        requestStream = request.GetRequestStream();
                        requestStream.Write(datas, 0, datas.Length);
                        requestStream.Close();
                    }
                    else
                    {
                        request.ContentLength = 0;
                    }
                    using (HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse())
                    {
                        Stream getStream = webResponse.GetResponseStream();
                        byte[] outBytes = ReadFully(getStream);
                        getStream.Close();
                        responseStr = Encoding.UTF8.GetString(outBytes);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    request = null;
                    requestStream = null;
                }
                richTextBox1.Text = responseStr;
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
