using C2.Controls;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Drawing;
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
        }

        private void textbox_MouseDown(object sender, EventArgs e)
        {
            if (textBox.Text == "输入你测试的url")
            {
                textBox.Text = string.Empty;
                textBox.ForeColor = Color.Black;
            }
        }
       
        private void textbox_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = "输入你测试的url";
                textBox.ForeColor = Color.Gray;
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            string defaultHeadText = "application/x-www-form-urlencoded";
            if(textBox.Text != "输入你测试的url")
                textBox.Text = string.Empty;
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            if (textBox3.Text != defaultHeadText)
                textBox3.Text = string.Empty;
            if (String.IsNullOrEmpty(textBox3.Text))
                textBox3.Text = defaultHeadText;
                textBox4.Text = string.Empty;
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == comboBox1.Items.IndexOf("POST"))
                splitType = "POST";
            else if(comboBox1.SelectedIndex == comboBox1.Items.IndexOf("GET"))
                splitType = "GET";
            else if (comboBox1.SelectedIndex == comboBox1.Items.IndexOf("HEAD"))
                splitType = "HEAD";
            else if (comboBox1.SelectedIndex == comboBox1.Items.IndexOf("OPTIONS"))
                splitType = "OPTIONS";
            else if (comboBox1.SelectedIndex == comboBox1.Items.IndexOf("PUT"))
                splitType = "PUT";

        }

        
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == comboBox2.Items.IndexOf("UTF-8 --接口输出的编码"))
                encodeoutput = "UTF-8";
            else if (comboBox2.SelectedIndex == comboBox2.Items.IndexOf("GBK   --接口输出的编码"))
                encodeoutput = "GBK";
        }
        
        

        /*
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex == comboBox3.Items.IndexOf("自动解压(gzip,deflate,flate)"))
                decompression = "UTF-8 --接口输出的编码";
            else if (comboBox3.SelectedIndex == comboBox3.Items.IndexOf("不解压"))
                decompression = "GBK   --接口输出的编码";
        }
        */

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
                    IndentChar = ' '
                };
                serializer.Serialize(jsonWriter, obj);
                return textWriter.ToString();
            }
            else
            {
                return json;
            }
        }

        private void postText(HttpWebRequest req, byte[] bytesToPost, string responseResult)
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
            richTextBox2.Text = responseResult;
            richTextBox1.Text = ConvertJsonString(responseResult);
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

        public async Task headTextAsync()
        {
            var client = new HttpClient(new HttpClientHandler { UseProxy = false });
            var request = new HttpRequestMessage(HttpMethod.Head, textBox.Text);
            HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string result = await response.Content.ReadAsStringAsync();
            richTextBox2.Text = richTextBox1.Text = result;
        }

        public async Task optionsTextAsync()
        {
            var client = new HttpClient(new HttpClientHandler { UseProxy = false });
            var request = new HttpRequestMessage(HttpMethod.Options, textBox.Text);
            HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string result = await response.Content.ReadAsStringAsync();
            richTextBox2.Text = richTextBox1.Text = result;
        }

        public StringBuilder getHeaders(HttpWebResponse resp)
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
        public string getResultNullParam(HttpWebResponse resp)
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
        private async void submit_ClickAsync(object sender, EventArgs e)
        {
            if (splitType == "POST")
            {
                string data = textBox1.Text;
                byte[] bytesToPost = encodeoutput == "GBK" ? System.Text.Encoding.Default.GetBytes(data) : encodeoutput == "UTF-8" ? System.Text.Encoding.UTF8.GetBytes(data) : System.Text.Encoding.UTF8.GetBytes(data);
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
                            WebProxy wp = new WebProxy(textBox4.Text);
                            req.Proxy = wp;
                            postText(req, bytesToPost, responseResult);
                        }
                        catch
                        {
                            richTextBox2.Text = richTextBox1.Text = "代理不可用，请更换代理";
                        }
                    }
                    else
                    {
                        postText(req, bytesToPost, responseResult);
                    }
                }
                catch
                {
                    richTextBox2.Text = richTextBox1.Text = "请输入正确的接口或参数";
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
                        StringBuilder headerResult = getHeaders(resp);
                        if (textBox4.Text != string.Empty)
                        {
                            try
                            {
                                WebProxy wp = new WebProxy(textBox4.Text);
                                req.Proxy = wp;
                                string result = getResultNullParam(resp);
                                richTextBox2.Text = richTextBox1.Text = result;
                            }
                            catch
                            {
                                richTextBox2.Text = richTextBox1.Text = "代理不可用，请更换代理";
                            }
                        }
                        else
                        {
                            string result = getResultNullParam(resp);
                            richTextBox2.Text = richTextBox1.Text = result;
                        }
                    }
                    catch 
                    {
                        richTextBox2.Text = richTextBox1.Text = "请输入正确的接口或参数";
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
                    richTextBox2.Text = richTextBox1.Text = result;
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
                richTextBox2.Text = richTextBox1.Text = responseStr;
            }
            else if (splitType == "HEAD")
            {
                await headTextAsync();
            }
            else if (splitType == "OPTIONS")
            {
                await optionsTextAsync();
            }
        }
    }
}
