using C2.Controls;
using C2.Utils;
using MihaZupan;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Business.IAOLab.PostAndGet
{
    public partial class PostAndGetForm : BaseDialog
    {
        string splitType;
        string encodeOutput;
        string IpProtocol;
        HttpWebResponse cnblogsRespone;
        HttpWebRequest req;
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
        private void PostText(HttpWebRequest req, byte[] bytesToPost)
        {
            string responseResult = string.Empty;
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
                    Encoding readerEncode = encodeOutput == "UTF-8" ? Encoding.UTF8 : Encoding.Default;
                    using (sr = new StreamReader(cnblogsRespone.GetResponseStream(), readerEncode))
                    {
                        responseResult = sr.ReadToEnd();
                        sr.Close();
                    }
                }
                cnblogsRespone.Close();
            }
            catch(WebException ex) 
            {
                responseResult = ex.Message;
            }
            richTextBoxHeaders.Text = GetHeaders(cnblogsRespone).ToString();
            string result = encodeOutput == "UTF-8" ? Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(responseResult)) : Encoding.Default.GetString(Encoding.Default.GetBytes(responseResult));
            richTextBoxResponse.Text = result.ToString();
            //richTextBoxResponse.Text = ConvertJsonString(result.ToString());
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
            var reponsestatusCode = Convert.ToInt32(resp.StatusCode);
            StringBuilder HeadersValue = new StringBuilder();
            HeadersValue.AppendLine("StatusCode:" + reponsestatusCode);
            for (int i = 0; i < resp.Headers.Count; i++)
            {
                string key = resp.Headers.GetKey(i);
                string value = resp.Headers.Get(key);
                HeadersValue.AppendLine(key + ":" + value);
            }
            return HeadersValue;
        }
        public string GetResultNullParam(HttpWebResponse resp)
        {
            string getResult = string.Empty;
            Stream stream = resp.GetResponseStream();
            try
            {
                Encoding readerEncode = encodeOutput == "UTF-8" ? Encoding.UTF8 : Encoding.Default;
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
            string result = encodeOutput == "UTF-8" ? Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(getResult)) : Encoding.Default.GetString(Encoding.Default.GetBytes(getResult));
            return result;
        }
        public HttpWebRequest WeatherIpProHttp(HttpWebRequest req)
        {
            WebProxy proxy = new WebProxy();
            string IpPrefix = "http://";
            proxy.Address = new Uri(String.Format("{0}{1}", IpPrefix, textBoxIp.Text));
            req.Proxy = proxy;
            return req;
        }
        public HttpWebRequest WeatherIpProSocks(HttpWebRequest req)
        {
            string[] strArray = textBoxIp.Text.Split(new char[] { ':' }, 2);
            var proxyScocks = new HttpToSocks5Proxy(new[] { new ProxyInfo(strArray[0], Convert.ToInt32(strArray[1])) });
            req.Proxy = proxyScocks;
            return req;
        }

        public void HistoryPost(string postData)
        {
            if (postData == string.Empty)
                return;

            if (!comboBoxHistory.Items.Contains(postData))
                comboBoxHistory.Items.Insert(0, postData);

            if (comboBoxHistory.Items.Count > comboBoxHistory.MaxDropDownItems)
                comboBoxHistory.Items.RemoveAt(comboBoxHistory.Items.Count - 1);
            // 动态调整下拉框的长度， 固定5:1宽高比
            int width = TextRenderer.MeasureText(postData, comboBoxHistory.Font).Width;
            if (width > comboBoxHistory.DropDownWidth)
                comboBoxHistory.DropDownWidth = Math.Min(width, comboBoxHistory.DropDownHeight * 5);
        }

        private async void Submit_ClickAsync(object sender, EventArgs e)
        {
            using (GuarderUtil.WaitCursor)
            {
                if (splitType == "POST")
                {
                    PostSubmit();
                }
                else if (splitType == "GET")
                {
                    GetSubmit();
                }
                else if (splitType == "PUT")
                {
                    PutSubmit();
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
        private void weatherNullPost(byte[] paramsData,HttpWebRequest req)
        {
            if (paramsData != null)
            {
                req.ContentLength = paramsData.Length;
                Stream requestStream = req.GetRequestStream();
                requestStream.Write(paramsData, 0, paramsData.Length);
                requestStream.Close();
            }
            else
            {
                req.ContentLength = 0;
            }
        }

        private void PutSubmit()
        {
            string responseStr = string.Empty;
            byte[] paramsData = Encoding.GetEncoding("UTF-8").GetBytes(textBoxPost.Text);
            try
            {
                req = WebRequest.Create(textBoxUrl.Text) as HttpWebRequest;
                req.Method = splitType;
                req.Timeout = ConvertUtil.TryParseInt(textBoxTime.Text) * 1000;
                req.AllowAutoRedirect = false;
                req.ContentType = "application/json";
                weatherNullPost(paramsData,req);
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

        private void GetSubmit()
        {
            req = textBoxPost.Text == string.Empty ? WebRequest.Create(textBoxUrl.Text) as HttpWebRequest : WebRequest.Create(textBoxUrl.Text + "?" + textBoxPost.Text) as HttpWebRequest;
            try
            {
                req.Method = splitType;
                req.Timeout = ConvertUtil.TryParseInt(textBoxTime.Text) * 1000;
                req.ContentType = "application/x-www-form-urlencoded";
                req.Headers.Set("cookie", textBoxCookie.Text);
                if (textBoxIp.Text != string.Empty)
                    req = IpProtocol == "HTTP" ? WeatherIpProHttp(req) : WeatherIpProSocks(req);
                HistoryPost(textBoxPost.Text);
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                richTextBoxResponse.Text = GetResultNullParam(resp);
                richTextBoxHeaders.Text = GetHeaders(resp).ToString();
            }
            catch (Exception ex)
            {
                richTextBoxResponse.Text = ex.Message;
            }
        }

        private void PostSubmit()
        {
            HistoryPost(textBoxPost.Text);
            byte[] bytesToPost = Encoding.UTF8.GetBytes(textBoxPost.Text);
            try
            {
                req = WebRequest.Create(textBoxUrl.Text) as HttpWebRequest;
                req.Method = splitType;
                req.Timeout = ConvertUtil.TryParseInt(textBoxTime.Text) * 1000;
                req.ContentType = "application/x-www-form-urlencoded";
                req.Headers.Set("cookie", textBoxCookie.Text);
                req.ContentLength = bytesToPost.Length;
                if (textBoxIp.Text != string.Empty)
                    req = IpProtocol == "HTTP" ? WeatherIpProHttp(req) : WeatherIpProSocks(req);
                PostText(req, bytesToPost);
            }
            catch (Exception ex)
            {
                richTextBoxResponse.Text = ex.Message;
            }
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            IpProtocol = comboBoxIpProtocol.SelectedItem as string;
        }
        private void comboBoxHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxHistory.SelectedItem != null)
            {
                textBoxPost.Text = comboBoxHistory.SelectedItem.ToString();
            }
        }
    }
}
