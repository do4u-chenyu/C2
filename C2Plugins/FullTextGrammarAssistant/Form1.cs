using C2.IAOLab.Plugins;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace FullTextGrammarAssistant
{
    public partial class Form1 : Form, IPlugin
    {
        private readonly List<string> queryclientTextList = new List<string>(new string[] { "","", "", "", "", "", "", "", "" });
        private readonly List<string> jarTextList = new List<string>(new string[] { "", "", "", "", "" });
        public Form1()
        {
            InitializeComponent();
        }

        public string GetPluginDescription()
        {
            return "全文语法助手";
        }

        public Image GetPluginImage()
        {
            return this.Icon.ToBitmap();
        }

        public string GetPluginName()
        {
            return "全文语法助手";
        }

        public string GetPluginVersion()
        {
            return "0.0.1";
        }

        public DialogResult ShowFormDialog()
        {
            return this.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.KeyWordsBox.Text = "hello AND world OR 你好 AND _TEXT:login OR _HOST:www.baidu.com";
            this.StartTimeBox.Text = DateTime.Now.AddYears(-1).ToString("yyyyMMddHHmmss");
            this.EndTimeBox.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
            this.queryclientTextList[1] = "--start " + this.StartTimeBox.Text;
            this.jarTextList[1] = "--startTime " + this.StartTimeBox.Text;
            this.queryclientTextList[2] = "--end " + this.EndTimeBox.Text;
            this.jarTextList[2] = "--endTime " + this.EndTimeBox.Text;
            this.queryclientTextList[3] = "--querystring \"" + this.KeyWordsBox.Text + "\"";
            this.jarTextList[3] = "--queryStr \"" + this.KeyWordsBox.Text + "\"";
            this.SecondFilterOne.SelectedIndex = 6;
            this.FilterConditionOne.SelectedIndex = 6;
            this.ConditionOne.Text = "\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.161";
            UpdatePreviewText();
        }

        private void Form1_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            string helpfile = Application.StartupPath;
            helpfile += @"\Resources\Help\检索系统帮助.html";
            Process.Start(helpfile);
        }

        private void UpdatePreviewText()
        {
            this.jarTextList[0] = "java -jar batchQueryAndExport_1.7.jar --ip 127.0.0.1 --port 9870 --queryCount 10000 --resultPath /home/result";
            this.queryclientTextList[0] = ".   /home/search/search_profile\r\n/home/search/sbin/queryclient --server 127.0.0.1 --port 9870";
            string attrText1 = Attribute_filter(this.SecondFilterOne.Text);
            string attrText2 = Attribute_filter(this.SecondFilterTwo.Text);
            string attrText3 = Attribute_filter(this.SecondFilterThree.Text);
            string conText1 = Condition_filter(this.FilterConditionOne.Text);
            string conText2 = Condition_filter(this.FilterConditionTwo.Text);
            string conText3 = Condition_filter(this.FilterConditionThree.Text);

            string newTextBox2 = string.Empty;
            string newTextBox5 = string.Empty;
            string newTextBox6 = string.Empty;

            if (this.FilterConditionOne.Text == "正则表达式")
                newTextBox2 = "\""+ this.ConditionOne.Text+"\"";
            else
                newTextBox2 = this.ConditionOne.Text;

            if (this.FilterConditionTwo.Text == "正则表达式")
                newTextBox5 = "\"" + this.ConditionTwo.Text + "\"";
            else
                newTextBox5 = this.ConditionTwo.Text;

            if (this.FilterConditionThree.Text == "正则表达式")
                newTextBox6 = "\"" + this.ConditionThree.Text + "\"";
            else
                newTextBox6 = this.ConditionThree.Text;


            if(AndOrNotOne.SelectedIndex == -1 & AndOrNotTwo.SelectedIndex == -1)
                this.queryclientTextList[5] = "--dbfilter \'" + attrText1 + conText1 + newTextBox2 + "\'";
            else if (AndOrNotTwo.SelectedIndex == -1)
                this.queryclientTextList[5] = "--dbfilter \'" + attrText1 + conText1 + newTextBox2 + " " + this.AndOrNotOne.Text + " " + attrText2 + conText2 + newTextBox5 + "\'";
            else
                this.queryclientTextList[5] = "--dbfilter \'" + attrText1 + conText1 + newTextBox2 + " " + this.AndOrNotOne.Text + " " + attrText2 + conText2 + newTextBox5 + " " + this.AndOrNotTwo.Text + " " + attrText3 + conText3 + " " + newTextBox6 + "\'";
            this.previewqueryText.Text = String.Join(" ", this.queryclientTextList) + "\r\n\r\n#登陆全文主节点执行#全文主节点当前针对每个查询条件一次限制返回10W行";
            this.previewjarText.Text = String.Join(" ", this.jarTextList) + "\r\n\r\n#batchQueryAndExport_1.7.jar不支持全文的dbfilter语法\r\n##全文主节点当前针对每个查询条件一次限制返回10W行\r\n##batchQueryAndExport_1.7.jar不支持选择查询方式及数据类型,且数据类型只能查normal类型,不能查garbage类型#";
        }

        private string Condition_filter(string condition)
        {
            Dictionary<string, string> conditionType = new Dictionary<string, string>();
            conditionType.Add("大于", ">");
            conditionType.Add("大于等于", ">=");
            conditionType.Add("等于", "=");
            conditionType.Add("不等于", "!=");
            conditionType.Add("小于", "<");
            conditionType.Add("小于等于", "<=");
            conditionType.Add("正则表达式", "=~");

            string conditionText = string.Empty;

            foreach (KeyValuePair<string, string> con in conditionType)
            {
                if (con.Key == condition)
                {
                    conditionText = con.Value;
                }
            }

            return conditionText;
        }

        private string Attribute_filter(string attribute)
        {
            Dictionary<string, string> attributeType = new Dictionary<string, string>();
            attributeType.Add("文件大小（单位：字节）", "FILESIZE");
            attributeType.Add("上网账号", "AUTH_ACCOUNT");
            attributeType.Add("宿端口", "DST_PORT");
            attributeType.Add("设备号", "EQUIPMENT_ID");
            attributeType.Add("IMSI号", "IMSI");
            attributeType.Add("源端口", "SRC_PORT");
            attributeType.Add("源IP", "STRSRC_IP");
            attributeType.Add("宿IP", "STRDST_IP");
            attributeType.Add("附件数", "_ATTACHNUM");
            attributeType.Add("域名", "DOMAIN");

            string attrText = string.Empty;

            foreach (KeyValuePair<string, string> attr in attributeType)
            {
                if (attr.Key == attribute)
                {
                    attrText = attr.Value;
                }
            }

            return attrText;

        }

        private void TraverPanelCheck()
        {
            int count = 0;
            foreach (CheckBox ch in panel2.Controls)
            {
                if (ch.Checked == true)
                    count++;
            }
            if (count != 0)
                AllProType.Checked = false;
            else
                AllProType.Checked = true;
        }

        private void TraverPanel()
        {
            Dictionary<string, string> protoType = new Dictionary<string, string>();
            protoType.Add("网页访问", "1000001");
            protoType.Add("电子邮箱", "1011007");
            protoType.Add("认证信息", "1020007");
            protoType.Add("即时聊天", "1030001");
            protoType.Add("FTP", "1050001");
            protoType.Add("网络聊天", "1060001");
            protoType.Add("网络论坛", "1071002");
            protoType.Add("TELNET", "1080000");
            protoType.Add("VOIP", "1090001");
            protoType.Add("网络赌博", "1140005");
            protoType.Add("博客网站", "11580001");
            protoType.Add("社交网站", "1197007");

            string protoTypeText = String.Empty;

            foreach (CheckBox ch in panel2.Controls)
            {
                if (ch.Checked == true)
                {
                    foreach (KeyValuePair<string, string> proto in protoType)
                    {
                        if (proto.Key == ch.Text)
                        {
                            protoTypeText += proto.Value + " ";
                        }
                    }
                }
            }
            string proType = "\'" + protoTypeText + "\'";

            if (string.IsNullOrEmpty(protoTypeText))
            {
                this.queryclientTextList[4] = string.Empty;
                this.jarTextList[4] = string.Empty;
            }
            else 
            {
                this.queryclientTextList[4] = "--protofilter " + proType;
                this.jarTextList[4] = "--protypeFilter " + proType;
            }
        }
        private void SearchPanel() 
        {
            Dictionary<string, string> searchRange = new Dictionary<string, string>();
            searchRange.Add("正文", "_TEXT:");
            searchRange.Add("附件", "_ATTACHTEXT:");
            searchRange.Add("邮件主题", "_SUBJECT:");

            string searchRangeText = string.Empty;
            List<string> searchRangeList = new List<string>(new string[] { "", "", "" });
            int count = 0;

            foreach (CheckBox ch in panel5.Controls)
            {
                if (ch.Checked == true)
                {
                    foreach (KeyValuePair<string, string> search in searchRange)
                    {
                        if (search.Key == ch.Text)
                        {
                            searchRangeList[count] = search.Value + this.KeyWordsBox.Text;
                        }
                    }
                    count++;
                }
            }

            if (count == 1)
                searchRangeText = searchRangeList[0];
            else if (count == 2)
                searchRangeText = "(" + searchRangeList[0] + ") OR (" + searchRangeList[1] + ")";
            else if (count == 3)
                searchRangeText = "(" + searchRangeList[0] + ") OR (" + searchRangeList[1] + ") OR (" + searchRangeList[2] + ")";

            if (string.IsNullOrEmpty(searchRangeText))
            {
                this.queryclientTextList[3] = "--querystring \"" + KeyWordsBox.Text + "\"";
                this.jarTextList[3] = "--queryStr \"" + KeyWordsBox.Text + "\"";
            }
            else 
            {
                this.queryclientTextList[3] = "--querystring \"" + searchRangeText + "\"";
                this.jarTextList[3] = "--queryStr \"" + searchRangeText + "\"";
            }

        }
        private void DataTypePanel()
        {
            Dictionary<string, string> DataType = new Dictionary<string, string>();
            DataType.Add("正常", "normal");
            DataType.Add("垃圾", "garbage");

            string dataTypeText = string.Empty;
            int count = 0;

            foreach (CheckBox ch in panel4.Controls)
            {
                if (ch.Checked == true) 
                {
                    foreach (KeyValuePair<string, string> data in DataType)
                    {
                        if (data.Key == ch.Text)
                            dataTypeText = data.Value;
                    }
                    count++;
                }
            }


            if (string.IsNullOrEmpty(dataTypeText)|count==2)
                this.queryclientTextList[7] = string.Empty;
            else
                this.queryclientTextList[7] = "--datatype " + dataTypeText;
        }

        private void OptionPanel()
        {
            Dictionary<string, string> OptionType = new Dictionary<string, string>();
            OptionType.Add("含有附件", "attachment");
            OptionType.Add("同义词", "synonymy");
            OptionType.Add("加密文件", "encrypt");
            OptionType.Add("过滤内容相似文件", "similar");

            string OptionTypeText = string.Empty;

            foreach (CheckBox ch in panel3.Controls)
            {
                if (ch.Checked == true)
                {
                    foreach (KeyValuePair<string, string> option in OptionType)
                    {
                        if (option.Key == ch.Text)
                        {
                            OptionTypeText += option.Value + " ";
                        }
                    }
                }
            }
            string newOptionType = "\'" + OptionTypeText + "\'";

            if (string.IsNullOrEmpty(OptionTypeText))
                this.queryclientTextList[8] = string.Empty;
            else
                this.queryclientTextList[8] = "--option " + newOptionType;
        }

        private void SearchPanelCheck()
        {
            int count = 0;
            foreach (CheckBox ch in panel5.Controls)
            {
                if (ch.Checked == true)
                    count++;
            }
            if (count != 0)
                AllSearch.Checked = false;
            else
                AllSearch.Checked = true;
        }

        private void DataTypePanelCheck()
        {
            int count = 0;
            foreach (CheckBox ch in panel4.Controls)
            {
                if (ch.Checked == true)
                    count++;
            }
            if (count != 0)
                AllData.Checked = false;
            else
                AllData.Checked = true;
        }

        private void OptionPanelCheck()
        {
            int count = 0;
            foreach (CheckBox ch in panel3.Controls)
            {
                if (ch.Checked == true)
                    count++;
            }
            if (count != 0)
                AllSeed.Checked = false;
            else
                AllSeed.Checked = true;
        }

        private void StartTimeBox_TextChanged(object sender, EventArgs e)
        {
            this.queryclientTextList[1] = "--start " + this.StartTimeBox.Text;
            this.jarTextList[1] = "--startTime " + this.StartTimeBox.Text;
            UpdatePreviewText();
        }

        private void EndTimeBox_TextChanged(object sender, EventArgs e)
        {
            this.queryclientTextList[2] = "--end " + this.EndTimeBox.Text;
            this.jarTextList[2] = "--endTime " + this.EndTimeBox.Text;
            UpdatePreviewText();
        }

        private void KeyWordsBox_TextChanged(object sender, EventArgs e)
        {
            this.queryclientTextList[3] = "--querystring \"" + this.KeyWordsBox.Text + "\"";
            this.jarTextList[3] = "--queryStr \"" + this.KeyWordsBox.Text + "\"";
            UpdatePreviewText();
        }

        private void SecondFilterOne_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePreviewText();
        }

        private void SecondFilterTwo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePreviewText();
        }

        private void SecondFilterThree_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePreviewText();
        }

        private void FilterConditionOne_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePreviewText();
        }

        private void FilterConditionTwo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePreviewText();
        }

        private void FilterConditionThree_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePreviewText();
        }

        private void ConditionOne_TextChanged(object sender, EventArgs e)
        {
            UpdatePreviewText();
        }

        private void ConditionTwo_TextChanged(object sender, EventArgs e)
        {
            UpdatePreviewText();
        }

        private void ConditionThree_TextChanged(object sender, EventArgs e)
        {
            UpdatePreviewText();
        }

        private void AndOrNotOne_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePreviewText();
        }

        private void AndOrNotTwo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePreviewText();
        }

        private void AllProType_CheckedChanged(object sender, EventArgs e)
        {
            if (AllProType.Checked == true)
            {
                foreach (CheckBox ch in panel2.Controls)
                {
                    ch.Checked = false;
                }
            }
            TraverPanel();
            UpdatePreviewText();
        }

        private void WebAccess_CheckedChanged(object sender, EventArgs e)
        {
            TraverPanelCheck();
            TraverPanel();
            UpdatePreviewText();
        }

        private void Email_CheckedChanged(object sender, EventArgs e)
        {
            TraverPanelCheck();
            TraverPanel();
            UpdatePreviewText();
        }

        private void Account_CheckedChanged(object sender, EventArgs e)
        {
            TraverPanelCheck();
            TraverPanel();
            UpdatePreviewText();
        }

        private void IM_CheckedChanged(object sender, EventArgs e)
        {
            TraverPanelCheck();
            TraverPanel();
            UpdatePreviewText();
        }

        private void FTP_CheckedChanged(object sender, EventArgs e)
        {
            TraverPanelCheck();
            TraverPanel();
            UpdatePreviewText();
        }

        private void InternetChat_CheckedChanged(object sender, EventArgs e)
        {
            TraverPanelCheck();
            TraverPanel();
            UpdatePreviewText();
        }

        private void BBS_CheckedChanged(object sender, EventArgs e)
        {
            TraverPanelCheck();
            TraverPanel();
            UpdatePreviewText();
        }

        private void Telnet_CheckedChanged(object sender, EventArgs e)
        {
            TraverPanelCheck();
            TraverPanel();
            UpdatePreviewText();
        }

        private void Voip_CheckedChanged(object sender, EventArgs e)
        {
            TraverPanelCheck();
            TraverPanel();
            UpdatePreviewText();
        }

        private void Gamble_CheckedChanged(object sender, EventArgs e)
        {
            TraverPanelCheck();
            TraverPanel();
            UpdatePreviewText();
        }

        private void GambleNet_CheckedChanged(object sender, EventArgs e)
        {
            TraverPanelCheck();
            TraverPanel();
            UpdatePreviewText();
        }

        private void AllSearch_CheckedChanged(object sender, EventArgs e)
        {
            if (AllSearch.Checked == true)
            {
                foreach (CheckBox ch in panel5.Controls)
                {
                    ch.Checked = false;
                }
            }
            SearchPanel();
            UpdatePreviewText();
        }

        private void Attachment_CheckedChanged(object sender, EventArgs e)
        {
            SearchPanelCheck();
            SearchPanel();
            UpdatePreviewText();
        }

        private void Subject_CheckedChanged(object sender, EventArgs e)
        {
            SearchPanelCheck();
            SearchPanel();
            UpdatePreviewText();
        }

        private void AllData_CheckedChanged(object sender, EventArgs e)
        {
            if (AllData.Checked == true)
            {
                foreach (CheckBox ch in panel4.Controls)
                {
                    ch.Checked = false;
                }
            }
            DataTypePanel();
            UpdatePreviewText();
        }

        private void Normal_CheckedChanged(object sender, EventArgs e)
        {
            DataTypePanelCheck();
            DataTypePanel();
            UpdatePreviewText();
        }

        private void Garbage_CheckedChanged(object sender, EventArgs e)
        {
            DataTypePanelCheck();
            DataTypePanel();
            UpdatePreviewText();
        }

        private void AllSeed_CheckedChanged(object sender, EventArgs e)
        {
            if (AllSeed.Checked == true)
            {
                foreach (CheckBox ch in panel3.Controls)
                {
                    ch.Checked = false;
                }
            }
            OptionPanel();
            UpdatePreviewText();
        }

        private void Attach_CheckedChanged(object sender, EventArgs e)
        {
            OptionPanelCheck();
            OptionPanel();
            UpdatePreviewText();
        }

        private void Synonymy_CheckedChanged(object sender, EventArgs e)
        {
            OptionPanelCheck();
            OptionPanel();
            UpdatePreviewText();
        }

        private void Encrypt_CheckedChanged(object sender, EventArgs e)
        {
            OptionPanelCheck();
            OptionPanel();
            UpdatePreviewText();
        }

        private void Similar_CheckedChanged(object sender, EventArgs e)
        {
            OptionPanelCheck();
            OptionPanel();
            UpdatePreviewText();
        }

        private void Social_CheckedChanged(object sender, EventArgs e)
        {
            TraverPanelCheck();
            TraverPanel();
            UpdatePreviewText();
        }

        private void Clipboard_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Text == "queryclient")
                System.Windows.Forms.Clipboard.SetDataObject(previewqueryText.Text);
            else
                System.Windows.Forms.Clipboard.SetDataObject(previewjarText.Text);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ContentText_CheckedChanged(object sender, EventArgs e)
        {
            SearchPanelCheck();
            SearchPanel();
            UpdatePreviewText();
        }
    }
}
