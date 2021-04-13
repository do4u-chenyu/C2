using C2.IAOLab.Plugins;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FullTextGrammarAssistant
{
    public partial class Form1 : Form, IPlugin
    {
        private readonly List<string> previewTextList = new List<string>(new string[] { "","", "", "", "", "", "", "", "", "" });
        private readonly List<string> jarTextList = new List<string>(new string[] { "", "", "", "", "", "", "", "", "", "" });
        public Form1()
        {
            InitializeComponent();
            foreach (CheckBox ck in panel2.Controls)
            {
                ck.CheckedChanged += checkBox1_CheckedChanged;
            }
            foreach (CheckBox ck in panel3.Controls)
            {
                ck.CheckedChanged += checkBox48_CheckedChanged;
            }
            foreach (CheckBox ck in panel4.Controls)
            {
                ck.CheckedChanged += checkBox53_CheckedChanged;
            }
            foreach (CheckBox ck in panel5.Controls)
            {
                ck.CheckedChanged += checkBox50_CheckedChanged;
            }
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
            this.textBox1.Text = "hello AND world OR 你好";
            this.textBox3.Text = DateTime.Now.AddYears(-1).ToString("yyyyMMddHHmmss");
            this.textBox4.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
            this.comboBox1.SelectedIndex = 0;
            this.comboBox2.SelectedIndex = 2;
            this.textBox2.Text = "100";
            this.comboBox5.SelectedIndex = 0;
            this.comboBox3.SelectedIndex = 4;
            this.comboBox4.SelectedIndex = 0;
            this.textBox5.Text = "460013440113856";
            this.comboBox6.SelectedIndex = 2;
            this.comboBox7.SelectedIndex = 8;
            this.comboBox8.SelectedIndex = 5;
            this.textBox6.Text = "10";
            this.previewTextList[1] = "--start " + this.textBox3.Text;
            this.jarTextList[1] = "--startTime " + this.textBox3.Text;
            this.previewTextList[2] = "--end " + this.textBox4.Text;
            this.jarTextList[2] = "--endTime " + this.textBox4.Text;
            this.previewTextList[3] = "--querystring \"" + this.textBox1.Text + "\"";
            this.jarTextList[3] = "--queryStr \"" + this.textBox1.Text + "\"";
            UpdatePreviewText();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_Click_1(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Checked)
            {
                foreach (CheckBox ck in panel2.Controls)
                    ck.Checked = true;
            }
            else
            {
                foreach (CheckBox ck in panel2.Controls)
                    ck.Checked = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox c = sender as CheckBox;
            if (c.Checked == true)
            {
                foreach (CheckBox ch in panel2.Controls)
                {
                    if (ch.Checked == false)
                        return;
                }
                checkBox1.Checked = true;
            }
            else
            {
                checkBox1.Checked = false;
            }
        }

        private void checkBox37_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox22_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox48_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox c = sender as CheckBox;
            if (c.Checked == true)
            {
                foreach (CheckBox ch in panel3.Controls)
                {
                    if (ch.Checked == false)
                        return;
                }
                checkBox48.Checked = true;
            }
            else
            {
                checkBox48.Checked = false;
            }
        }

        private void checkBox48_Click(object sender, EventArgs e)
        {
            if (checkBox48.CheckState == CheckState.Checked)
            {
                foreach (CheckBox ck in panel3.Controls)
                    ck.Checked = true;
            }
            else
            {
                foreach (CheckBox ck in panel3.Controls)
                    ck.Checked = false;
            }
        }

        private void checkBox53_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox c = sender as CheckBox;
            if (c.Checked == true)
            {
                foreach (CheckBox ch in panel4.Controls)
                {
                    if (ch.Checked == false)
                        return;
                }
                checkBox53.Checked = true;
            }
            else
            {
                checkBox53.Checked = false;
            }
        }

        private void checkBox53_Click(object sender, EventArgs e)
        {
            if (checkBox53.CheckState == CheckState.Checked)
            {
                foreach (CheckBox ck in panel4.Controls)
                    ck.Checked = true;
            }
            else
            {
                foreach (CheckBox ck in panel4.Controls)
                    ck.Checked = false;
                MessageBox.Show("请选择数据类型。", "提示");
            }
        }

        private void checkBox50_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox c = sender as CheckBox;
            if (c.Checked == true)
            {
                foreach (CheckBox ch in panel5.Controls)
                {
                    if (ch.Checked == false)
                        return;
                }
                checkBox50.Checked = true;
            }
            else
            {
                checkBox50.Checked = false;
            }
        }

        private void checkBox50_Click(object sender, EventArgs e)
        {
            if (checkBox50.CheckState == CheckState.Checked)
            {
                foreach (CheckBox ck in panel5.Controls)
                    ck.Checked = true;
            }
            else
            {
                foreach (CheckBox ck in panel5.Controls)
                    ck.Checked = false;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void Form1_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            string helpfile = Application.StartupPath;
            helpfile += @"\Resources\Help\检索系统帮助.html";
            Process.Start(helpfile);
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            this.previewTextList[1] = "--start " + this.textBox3.Text;
            this.jarTextList[1] = "--startTime " + this.textBox3.Text;
            UpdatePreviewText();
        }
        private void UpdatePreviewText()
        {
            this.jarTextList[0] = "java -jar batchQueryAndExport_1.7.jar --ip 15.42.118.1 --port 9870";
            this.previewTextList[0] = "/home/search/sbin/queryclient --server 127.0.0.1 --port 9871";
            string attrText1 = Attribute_filter(this.comboBox1.Text);
            string attrText2 = Attribute_filter(this.comboBox3.Text);
            string attrText3 = Attribute_filter(this.comboBox7.Text);
            string conText1 = Condition_filter(this.comboBox2.Text);
            string conText2 = Condition_filter(this.comboBox4.Text);
            string conText3 = Condition_filter(this.comboBox8.Text);
            this.previewTextList[5] = "--dbfilter " + attrText1 + " " + conText1 + " " + this.textBox2.Text + " " + this.comboBox5.Text + " " + attrText2 + " " + conText2 + " " + this.textBox5.Text + " " + this.comboBox6.Text + " " + attrText3 + " " + conText3 + " " + this.textBox6.Text;
            this.previewCmdText.Text = String.Join(" ", this.previewTextList);
            this.textBox9.Text = String.Join(" ", this.jarTextList);
        }

        private string Condition_filter(string condition)
        {
            Dictionary<string, string> conditionType = new Dictionary<string, string>();
            conditionType.Add("精确匹配", "FILESIZE");
            conditionType.Add("模糊匹配", "AUTH_ACCOUNT");
            conditionType.Add("大于", ">");
            conditionType.Add("大于等于", ">=");
            conditionType.Add("等于", "=");
            conditionType.Add("小于", "<");
            conditionType.Add("小于等于", "<=");

            string conditionText = "";

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
            attributeType.Add("源IP", "SRC_IP");
            attributeType.Add("宿IP", "DST_IP");
            attributeType.Add("附件数", "_ATTACHNUM");
            attributeType.Add("域名", "DOMAIN");

            string attrText = "";

            foreach (KeyValuePair<string, string> attr in attributeType)
            {
                if (attr.Key == attribute)
                {
                    attrText = attr.Value;
                }
            }

            return attrText;

        }

        private void previewCmdText_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            this.previewTextList[2] = "--end " + this.textBox4.Text;
            UpdatePreviewText();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.previewTextList[3] = "--querystring \"" + this.textBox1.Text + "\"";
            UpdatePreviewText();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            TraverPanel();
            UpdatePreviewText();
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

            string protoTypeText = "";

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
                else
                    MessageBox.Show("请选择协议类型。", "提示");
            }
            previewTextList[4] = "--protofilter " + protoTypeText;
        }
        private void SearchPanel() 
        {
            Dictionary<string, string> searchRange = new Dictionary<string, string>();
            searchRange.Add("正文", "_TEXT");
            searchRange.Add("附件", "_ATTACHEMENTTEXT");
            searchRange.Add("邮件主题", "_SUBJECT");

            string searchRangeText = "";

            foreach (CheckBox ch in panel2.Controls)
            {
                if (ch.Checked == true)
                {
                    foreach (KeyValuePair<string, string> search in searchRange)
                    {
                        if (search.Key == ch.Text)
                        {
                            searchRangeText += search.Value + " ";
                        }
                    }
                }
                //else
                //return;
            }
            //previewTextList[4] = "--protofilter " + searchRangeText;

        }
        private void dataTypePanel()
        {
            Dictionary<string, string> DataType = new Dictionary<string, string>();
            DataType.Add("正常", "normal");
            DataType.Add("垃圾", "garbage");

            string dataTypeText = "";

            foreach (CheckBox ch in panel4.Controls)
            {
                if (ch.Checked == true)
                {
                    foreach (KeyValuePair<string, string> data in DataType)
                    {
                        if (data.Key == ch.Text)
                        {
                            dataTypeText += data.Value + " ";
                        }
                    }
                }
                //else
                //return;
            }
            previewTextList[7] = "--datatype " + dataTypeText;
        }
        private void optionPanel()
        {
            Dictionary<string, string> OptionType = new Dictionary<string, string>();
            OptionType.Add("含有附件", "attachment");
            OptionType.Add("同义词", "synonymy");
            OptionType.Add("加密文件", "encrypt");
            OptionType.Add("过滤内容相似文件", "similar");
            OptionType.Add("关键词精确匹配", "garbage");

            string OptionTypeText = "";

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
                //else
                //return;
            }
            previewTextList[8] = "--option " + OptionTypeText;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            TraverPanel();
            UpdatePreviewText();
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            TraverPanel();
            UpdatePreviewText();
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            TraverPanel();
            UpdatePreviewText();
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            TraverPanel();
            UpdatePreviewText();
        }

        private void checkBox13_CheckedChanged(object sender, EventArgs e)
        {
            TraverPanel();
            UpdatePreviewText();
        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            TraverPanel();
            UpdatePreviewText();
        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            TraverPanel();
            UpdatePreviewText();
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            TraverPanel();
            UpdatePreviewText();
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            TraverPanel();
            UpdatePreviewText();
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            TraverPanel();
            UpdatePreviewText();
        }

        private void checkBox15_CheckedChanged(object sender, EventArgs e)
        {
            TraverPanel();
            UpdatePreviewText();
        }

        private void checkBox49_CheckedChanged(object sender, EventArgs e)
        {
            SearchPanel();
            UpdatePreviewText();
        }

        private void checkBox52_CheckedChanged(object sender, EventArgs e)
        {
            dataTypePanel();
            UpdatePreviewText();
        }

        private void checkBox47_CheckedChanged(object sender, EventArgs e)
        {
            optionPanel();
            UpdatePreviewText();
        }

        private void checkBox42_CheckedChanged(object sender, EventArgs e)
        {
            SearchPanel();
            UpdatePreviewText();
        }

        private void checkBox54_CheckedChanged(object sender, EventArgs e)
        {
            SearchPanel();
            UpdatePreviewText();
        }

        private void checkBox51_CheckedChanged(object sender, EventArgs e)
        {
            dataTypePanel();
            UpdatePreviewText();
        }

        private void checkBox46_CheckedChanged(object sender, EventArgs e)
        {
            optionPanel();
            UpdatePreviewText();
        }

        private void checkBox45_CheckedChanged(object sender, EventArgs e)
        {
            optionPanel();
            UpdatePreviewText();
        }

        private void checkBox44_CheckedChanged(object sender, EventArgs e)
        {
            optionPanel();
            UpdatePreviewText();
        }

        private void checkBox43_CheckedChanged(object sender, EventArgs e)
        {
            optionPanel();
            UpdatePreviewText();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePreviewText();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePreviewText();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            UpdatePreviewText();
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePreviewText();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePreviewText();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePreviewText();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            UpdatePreviewText();
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePreviewText();
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePreviewText();
        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePreviewText();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            UpdatePreviewText();
        }
    }
}
