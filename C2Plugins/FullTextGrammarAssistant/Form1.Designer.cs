using System;

namespace FullTextGrammarAssistant
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.KeyWordsBox = new System.Windows.Forms.TextBox();
            this.AllProType = new System.Windows.Forms.CheckBox();
            this.WebAccess = new System.Windows.Forms.CheckBox();
            this.Email = new System.Windows.Forms.CheckBox();
            this.Account = new System.Windows.Forms.CheckBox();
            this.IM = new System.Windows.Forms.CheckBox();
            this.FTP = new System.Windows.Forms.CheckBox();
            this.InternetChat = new System.Windows.Forms.CheckBox();
            this.Social = new System.Windows.Forms.CheckBox();
            this.BBS = new System.Windows.Forms.CheckBox();
            this.Telnet = new System.Windows.Forms.CheckBox();
            this.Voip = new System.Windows.Forms.CheckBox();
            this.Gamble = new System.Windows.Forms.CheckBox();
            this.GambleNet = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Similar = new System.Windows.Forms.CheckBox();
            this.Encrypt = new System.Windows.Forms.CheckBox();
            this.Synonymy = new System.Windows.Forms.CheckBox();
            this.Attach = new System.Windows.Forms.CheckBox();
            this.AllSeed = new System.Windows.Forms.CheckBox();
            this.Garbage = new System.Windows.Forms.CheckBox();
            this.Normal = new System.Windows.Forms.CheckBox();
            this.AllData = new System.Windows.Forms.CheckBox();
            this.Attachment = new System.Windows.Forms.CheckBox();
            this.ContentText = new System.Windows.Forms.CheckBox();
            this.AllSearch = new System.Windows.Forms.CheckBox();
            this.Subject = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.Clipboard = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.AndOrNotOne = new System.Windows.Forms.ComboBox();
            this.SecondFilterTwo = new System.Windows.Forms.ComboBox();
            this.FilterConditionTwo = new System.Windows.Forms.ComboBox();
            this.ConditionTwo = new System.Windows.Forms.TextBox();
            this.StartTimeBox = new System.Windows.Forms.TextBox();
            this.EndTimeBox = new System.Windows.Forms.TextBox();
            this.FilterConditionOne = new System.Windows.Forms.ComboBox();
            this.ConditionOne = new System.Windows.Forms.TextBox();
            this.SecondFilterOne = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.previewqueryText = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.previewjarText = new System.Windows.Forms.TextBox();
            this.AndOrNotTwo = new System.Windows.Forms.ComboBox();
            this.FilterConditionThree = new System.Windows.Forms.ComboBox();
            this.ConditionThree = new System.Windows.Forms.TextBox();
            this.SecondFilterThree = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(94, 225);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "时间：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(77, 264);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 24);
            this.label2.TabIndex = 4;
            this.label2.Text = "关键词：";
            this.toolTip1.SetToolTip(this.label2, "1)支持 AND, OR的组合查询;2)支持指定字段查询,如 HOST:www.baidu.com 就是只在Host字段中查询");
            // 
            // KeyWordsBox
            // 
            this.KeyWordsBox.Location = new System.Drawing.Point(149, 264);
            this.KeyWordsBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.KeyWordsBox.Name = "KeyWordsBox";
            this.KeyWordsBox.Size = new System.Drawing.Size(748, 25);
            this.KeyWordsBox.TabIndex = 5;
            this.KeyWordsBox.TextChanged += new System.EventHandler(this.KeyWordsBox_TextChanged);
            // 
            // AllProType
            // 
            this.AllProType.AutoSize = true;
            this.AllProType.Checked = true;
            this.AllProType.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AllProType.Location = new System.Drawing.Point(149, 421);
            this.AllProType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AllProType.Name = "AllProType";
            this.AllProType.Size = new System.Drawing.Size(89, 19);
            this.AllProType.TabIndex = 7;
            this.AllProType.Text = "全部类型";
            this.AllProType.UseVisualStyleBackColor = true;
            this.AllProType.CheckedChanged += new System.EventHandler(this.AllProType_CheckedChanged);
            // 
            // WebAccess
            // 
            this.WebAccess.AutoSize = true;
            this.WebAccess.Location = new System.Drawing.Point(13, 4);
            this.WebAccess.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.WebAccess.Name = "WebAccess";
            this.WebAccess.Size = new System.Drawing.Size(89, 19);
            this.WebAccess.TabIndex = 8;
            this.WebAccess.Text = "网页访问";
            this.toolTip1.SetToolTip(this.WebAccess, "HTTP_POST(1000001)");
            this.WebAccess.UseVisualStyleBackColor = true;
            this.WebAccess.CheckedChanged += new System.EventHandler(this.WebAccess_CheckedChanged);
            // 
            // Email
            // 
            this.Email.AutoSize = true;
            this.Email.Location = new System.Drawing.Point(140, 4);
            this.Email.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Email.Name = "Email";
            this.Email.Size = new System.Drawing.Size(89, 19);
            this.Email.TabIndex = 9;
            this.Email.Text = "电子邮箱";
            this.toolTip1.SetToolTip(this.Email, "QQ邮箱(1011007)");
            this.Email.UseVisualStyleBackColor = true;
            this.Email.CheckedChanged += new System.EventHandler(this.Email_CheckedChanged);
            // 
            // Account
            // 
            this.Account.AutoSize = true;
            this.Account.Location = new System.Drawing.Point(261, 4);
            this.Account.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Account.Name = "Account";
            this.Account.Size = new System.Drawing.Size(89, 19);
            this.Account.TabIndex = 11;
            this.Account.Text = "认证信息";
            this.toolTip1.SetToolTip(this.Account, "身份证(1020007)");
            this.Account.UseVisualStyleBackColor = true;
            this.Account.CheckedChanged += new System.EventHandler(this.Account_CheckedChanged);
            // 
            // IM
            // 
            this.IM.AutoSize = true;
            this.IM.Location = new System.Drawing.Point(387, 4);
            this.IM.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.IM.Name = "IM";
            this.IM.Size = new System.Drawing.Size(89, 19);
            this.IM.TabIndex = 12;
            this.IM.Text = "即时聊天";
            this.toolTip1.SetToolTip(this.IM, "QQ(1030001)");
            this.IM.UseVisualStyleBackColor = true;
            this.IM.CheckedChanged += new System.EventHandler(this.IM_CheckedChanged);
            // 
            // FTP
            // 
            this.FTP.AutoSize = true;
            this.FTP.Location = new System.Drawing.Point(519, 4);
            this.FTP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.FTP.Name = "FTP";
            this.FTP.Size = new System.Drawing.Size(53, 19);
            this.FTP.TabIndex = 13;
            this.FTP.Text = "FTP";
            this.toolTip1.SetToolTip(this.FTP, "标准FTP(1050001)");
            this.FTP.UseVisualStyleBackColor = true;
            this.FTP.CheckedChanged += new System.EventHandler(this.FTP_CheckedChanged);
            // 
            // InternetChat
            // 
            this.InternetChat.AutoSize = true;
            this.InternetChat.Location = new System.Drawing.Point(623, 4);
            this.InternetChat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.InternetChat.Name = "InternetChat";
            this.InternetChat.Size = new System.Drawing.Size(89, 19);
            this.InternetChat.TabIndex = 14;
            this.InternetChat.Text = "网络聊天";
            this.toolTip1.SetToolTip(this.InternetChat, "QQ(1060001)");
            this.InternetChat.UseVisualStyleBackColor = true;
            this.InternetChat.CheckedChanged += new System.EventHandler(this.InternetChat_CheckedChanged);
            // 
            // Social
            // 
            this.Social.AutoSize = true;
            this.Social.Location = new System.Drawing.Point(623, 41);
            this.Social.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Social.Name = "Social";
            this.Social.Size = new System.Drawing.Size(89, 19);
            this.Social.TabIndex = 16;
            this.Social.Text = "社交网站";
            this.toolTip1.SetToolTip(this.Social, "腾讯(1197007)");
            this.Social.UseVisualStyleBackColor = true;
            this.Social.CheckedChanged += new System.EventHandler(this.Social_CheckedChanged);
            // 
            // BBS
            // 
            this.BBS.AutoSize = true;
            this.BBS.Location = new System.Drawing.Point(13, 41);
            this.BBS.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BBS.Name = "BBS";
            this.BBS.Size = new System.Drawing.Size(89, 19);
            this.BBS.TabIndex = 13;
            this.BBS.Text = "网络论坛";
            this.toolTip1.SetToolTip(this.BBS, "百度贴吧(1071002)");
            this.BBS.UseVisualStyleBackColor = true;
            this.BBS.CheckedChanged += new System.EventHandler(this.BBS_CheckedChanged);
            // 
            // Telnet
            // 
            this.Telnet.AutoSize = true;
            this.Telnet.Location = new System.Drawing.Point(140, 41);
            this.Telnet.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Telnet.Name = "Telnet";
            this.Telnet.Size = new System.Drawing.Size(77, 19);
            this.Telnet.TabIndex = 12;
            this.Telnet.Text = "TELNET";
            this.toolTip1.SetToolTip(this.Telnet, "TELNET(1080000)");
            this.Telnet.UseVisualStyleBackColor = true;
            this.Telnet.CheckedChanged += new System.EventHandler(this.Telnet_CheckedChanged);
            // 
            // Voip
            // 
            this.Voip.AutoSize = true;
            this.Voip.Location = new System.Drawing.Point(261, 41);
            this.Voip.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Voip.Name = "Voip";
            this.Voip.Size = new System.Drawing.Size(61, 19);
            this.Voip.TabIndex = 11;
            this.Voip.Text = "VOIP";
            this.toolTip1.SetToolTip(this.Voip, "QQ(1090001)");
            this.Voip.UseVisualStyleBackColor = true;
            this.Voip.CheckedChanged += new System.EventHandler(this.Voip_CheckedChanged);
            // 
            // Gamble
            // 
            this.Gamble.AutoSize = true;
            this.Gamble.Location = new System.Drawing.Point(387, 41);
            this.Gamble.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Gamble.Name = "Gamble";
            this.Gamble.Size = new System.Drawing.Size(89, 19);
            this.Gamble.TabIndex = 10;
            this.Gamble.Text = "网络赌博";
            this.toolTip1.SetToolTip(this.Gamble, "皇冠(1140005)");
            this.Gamble.UseVisualStyleBackColor = true;
            this.Gamble.CheckedChanged += new System.EventHandler(this.Gamble_CheckedChanged);
            // 
            // GambleNet
            // 
            this.GambleNet.AutoSize = true;
            this.GambleNet.Location = new System.Drawing.Point(519, 41);
            this.GambleNet.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.GambleNet.Name = "GambleNet";
            this.GambleNet.Size = new System.Drawing.Size(89, 19);
            this.GambleNet.TabIndex = 9;
            this.GambleNet.Text = "博客网站";
            this.toolTip1.SetToolTip(this.GambleNet, "新浪博客(11580001)");
            this.GambleNet.UseVisualStyleBackColor = true;
            this.GambleNet.CheckedChanged += new System.EventHandler(this.GambleNet_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(26, 301);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 24);
            this.label4.TabIndex = 16;
            this.label4.Text = "条件二次过滤：";
            this.toolTip1.SetToolTip(this.label4, "在关键词查出的结果集上进行二次过滤，对应全文的dbfilter功能；queryclient支持，Jar包不支持");
            // 
            // Similar
            // 
            this.Similar.AutoSize = true;
            this.Similar.Location = new System.Drawing.Point(552, 9);
            this.Similar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Similar.Name = "Similar";
            this.Similar.Size = new System.Drawing.Size(149, 19);
            this.Similar.TabIndex = 25;
            this.Similar.Text = "过滤内容相似文件";
            this.toolTip1.SetToolTip(this.Similar, "查询结果中前后内容相似的结果只取1条");
            this.Similar.UseVisualStyleBackColor = true;
            this.Similar.CheckedChanged += new System.EventHandler(this.Similar_CheckedChanged);
            // 
            // Encrypt
            // 
            this.Encrypt.AutoSize = true;
            this.Encrypt.Location = new System.Drawing.Point(372, 9);
            this.Encrypt.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Encrypt.Name = "Encrypt";
            this.Encrypt.Size = new System.Drawing.Size(89, 19);
            this.Encrypt.TabIndex = 24;
            this.Encrypt.Text = "加密文件";
            this.toolTip1.SetToolTip(this.Encrypt, "主要指类似邮件协议中上传的附件中含有带密码的rar等类型压缩包");
            this.Encrypt.UseVisualStyleBackColor = true;
            this.Encrypt.CheckedChanged += new System.EventHandler(this.Encrypt_CheckedChanged);
            // 
            // Synonymy
            // 
            this.Synonymy.AutoSize = true;
            this.Synonymy.Location = new System.Drawing.Point(193, 9);
            this.Synonymy.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Synonymy.Name = "Synonymy";
            this.Synonymy.Size = new System.Drawing.Size(74, 19);
            this.Synonymy.TabIndex = 23;
            this.Synonymy.Text = "同义词";
            this.Synonymy.UseVisualStyleBackColor = true;
            this.Synonymy.CheckedChanged += new System.EventHandler(this.Synonymy_CheckedChanged);
            // 
            // Attach
            // 
            this.Attach.AutoSize = true;
            this.Attach.Location = new System.Drawing.Point(13, 9);
            this.Attach.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Attach.Name = "Attach";
            this.Attach.Size = new System.Drawing.Size(89, 19);
            this.Attach.TabIndex = 22;
            this.Attach.Text = "含有附件";
            this.toolTip1.SetToolTip(this.Attach, "主要指邮件中带有附件");
            this.Attach.UseVisualStyleBackColor = true;
            this.Attach.CheckedChanged += new System.EventHandler(this.Attach_CheckedChanged);
            // 
            // AllSeed
            // 
            this.AllSeed.AutoSize = true;
            this.AllSeed.Checked = true;
            this.AllSeed.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AllSeed.Location = new System.Drawing.Point(149, 624);
            this.AllSeed.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AllSeed.Name = "AllSeed";
            this.AllSeed.Size = new System.Drawing.Size(59, 19);
            this.AllSeed.TabIndex = 21;
            this.AllSeed.Text = "全选";
            this.AllSeed.UseVisualStyleBackColor = true;
            this.AllSeed.CheckedChanged += new System.EventHandler(this.AllSeed_CheckedChanged);
            // 
            // Garbage
            // 
            this.Garbage.AutoSize = true;
            this.Garbage.Location = new System.Drawing.Point(109, 9);
            this.Garbage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Garbage.Name = "Garbage";
            this.Garbage.Size = new System.Drawing.Size(59, 19);
            this.Garbage.TabIndex = 30;
            this.Garbage.Text = "垃圾";
            this.Garbage.UseVisualStyleBackColor = true;
            this.Garbage.CheckedChanged += new System.EventHandler(this.Garbage_CheckedChanged);
            // 
            // Normal
            // 
            this.Normal.AutoSize = true;
            this.Normal.Location = new System.Drawing.Point(12, 9);
            this.Normal.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Normal.Name = "Normal";
            this.Normal.Size = new System.Drawing.Size(59, 19);
            this.Normal.TabIndex = 29;
            this.Normal.Text = "正常";
            this.Normal.UseVisualStyleBackColor = true;
            this.Normal.CheckedChanged += new System.EventHandler(this.Normal_CheckedChanged);
            // 
            // AllData
            // 
            this.AllData.AutoSize = true;
            this.AllData.Checked = true;
            this.AllData.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AllData.Location = new System.Drawing.Point(684, 542);
            this.AllData.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AllData.Name = "AllData";
            this.AllData.Size = new System.Drawing.Size(59, 19);
            this.AllData.TabIndex = 28;
            this.AllData.Text = "全选";
            this.AllData.UseVisualStyleBackColor = true;
            this.AllData.CheckedChanged += new System.EventHandler(this.AllData_CheckedChanged);
            // 
            // Attachment
            // 
            this.Attachment.AutoSize = true;
            this.Attachment.Location = new System.Drawing.Point(109, 9);
            this.Attachment.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Attachment.Name = "Attachment";
            this.Attachment.Size = new System.Drawing.Size(59, 19);
            this.Attachment.TabIndex = 34;
            this.Attachment.Text = "附件";
            this.toolTip1.SetToolTip(this.Attachment, "文件附件文本域 _ATTACHTEXT:关键词");
            this.Attachment.UseVisualStyleBackColor = true;
            this.Attachment.CheckedChanged += new System.EventHandler(this.Attachment_CheckedChanged);
            // 
            // ContentText
            // 
            this.ContentText.AutoSize = true;
            this.ContentText.Location = new System.Drawing.Point(13, 9);
            this.ContentText.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ContentText.Name = "ContentText";
            this.ContentText.Size = new System.Drawing.Size(59, 19);
            this.ContentText.TabIndex = 33;
            this.ContentText.Text = "正文";
            this.toolTip1.SetToolTip(this.ContentText, "查询正文域 _TEXT:关键词");
            this.ContentText.UseVisualStyleBackColor = true;
            this.ContentText.CheckedChanged += new System.EventHandler(this.ContentText_CheckedChanged);
            // 
            // AllSearch
            // 
            this.AllSearch.AutoSize = true;
            this.AllSearch.Checked = true;
            this.AllSearch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AllSearch.Location = new System.Drawing.Point(149, 544);
            this.AllSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AllSearch.Name = "AllSearch";
            this.AllSearch.Size = new System.Drawing.Size(89, 19);
            this.AllSearch.TabIndex = 32;
            this.AllSearch.Text = "全部范围";
            this.AllSearch.UseVisualStyleBackColor = true;
            this.AllSearch.CheckedChanged += new System.EventHandler(this.AllSearch_CheckedChanged);
            // 
            // Subject
            // 
            this.Subject.AutoSize = true;
            this.Subject.Location = new System.Drawing.Point(209, 9);
            this.Subject.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Subject.Name = "Subject";
            this.Subject.Size = new System.Drawing.Size(89, 19);
            this.Subject.TabIndex = 35;
            this.Subject.Text = "邮件主题";
            this.toolTip1.SetToolTip(this.Subject, "查询邮件主题域 _SUBJECT:关键词");
            this.Subject.UseVisualStyleBackColor = true;
            this.Subject.CheckedChanged += new System.EventHandler(this.Subject_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(26, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(129, 24);
            this.label8.TabIndex = 36;
            this.label8.Text = "对应查询条件：";
            // 
            // Clipboard
            // 
            this.Clipboard.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Clipboard.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.Clipboard.Location = new System.Drawing.Point(704, 21);
            this.Clipboard.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Clipboard.Name = "Clipboard";
            this.Clipboard.Size = new System.Drawing.Size(79, 34);
            this.Clipboard.TabIndex = 37;
            this.Clipboard.Text = "复制";
            this.toolTip1.SetToolTip(this.Clipboard, "复制当前生成语法命令到剪贴板");
            this.Clipboard.UseVisualStyleBackColor = false;
            this.Clipboard.Click += new System.EventHandler(this.Clipboard_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.CloseButton.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.CloseButton.Location = new System.Drawing.Point(863, 21);
            this.CloseButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(79, 34);
            this.CloseButton.TabIndex = 38;
            this.CloseButton.Text = "关闭";
            this.CloseButton.UseVisualStyleBackColor = false;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.WebAccess);
            this.panel2.Controls.Add(this.Email);
            this.panel2.Controls.Add(this.Social);
            this.panel2.Controls.Add(this.InternetChat);
            this.panel2.Controls.Add(this.Gamble);
            this.panel2.Controls.Add(this.Voip);
            this.panel2.Controls.Add(this.FTP);
            this.panel2.Controls.Add(this.Account);
            this.panel2.Controls.Add(this.IM);
            this.panel2.Controls.Add(this.GambleNet);
            this.panel2.Controls.Add(this.BBS);
            this.panel2.Controls.Add(this.Telnet);
            this.panel2.Location = new System.Drawing.Point(136, 452);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(752, 70);
            this.panel2.TabIndex = 41;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.Attach);
            this.panel3.Controls.Add(this.Synonymy);
            this.panel3.Controls.Add(this.Encrypt);
            this.panel3.Controls.Add(this.Similar);
            this.panel3.Location = new System.Drawing.Point(136, 652);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(752, 35);
            this.panel3.TabIndex = 45;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.Normal);
            this.panel4.Controls.Add(this.Garbage);
            this.panel4.Location = new System.Drawing.Point(672, 568);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(215, 35);
            this.panel4.TabIndex = 46;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.ContentText);
            this.panel5.Controls.Add(this.Attachment);
            this.panel5.Controls.Add(this.Subject);
            this.panel5.Location = new System.Drawing.Point(136, 569);
            this.panel5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(324, 35);
            this.panel5.TabIndex = 47;
            // 
            // AndOrNotOne
            // 
            this.AndOrNotOne.FormattingEnabled = true;
            this.AndOrNotOne.Items.AddRange(new object[] {
            "AND",
            "OR",
            "NOT"});
            this.AndOrNotOne.Location = new System.Drawing.Point(149, 339);
            this.AndOrNotOne.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AndOrNotOne.Name = "AndOrNotOne";
            this.AndOrNotOne.Size = new System.Drawing.Size(92, 23);
            this.AndOrNotOne.TabIndex = 42;
            this.AndOrNotOne.SelectedIndexChanged += new System.EventHandler(this.AndOrNotOne_SelectedIndexChanged);
            // 
            // SecondFilterTwo
            // 
            this.SecondFilterTwo.FormattingEnabled = true;
            this.SecondFilterTwo.Items.AddRange(new object[] {
            "文件大小（单位：字节）",
            "上网账号",
            "宿端口",
            "设备号",
            "IMSI号",
            "源端口",
            "源IP",
            "宿IP",
            "附件数",
            "域名"});
            this.SecondFilterTwo.Location = new System.Drawing.Point(247, 339);
            this.SecondFilterTwo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SecondFilterTwo.Name = "SecondFilterTwo";
            this.SecondFilterTwo.Size = new System.Drawing.Size(207, 23);
            this.SecondFilterTwo.TabIndex = 18;
            this.SecondFilterTwo.SelectedIndexChanged += new System.EventHandler(this.SecondFilterTwo_SelectedIndexChanged);
            // 
            // FilterConditionTwo
            // 
            this.FilterConditionTwo.FormattingEnabled = true;
            this.FilterConditionTwo.Items.AddRange(new object[] {
            "大于",
            "大于等于",
            "等于",
            "不等于",
            "小于",
            "小于等于",
            "正则表达式"});
            this.FilterConditionTwo.Location = new System.Drawing.Point(459, 339);
            this.FilterConditionTwo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.FilterConditionTwo.Name = "FilterConditionTwo";
            this.FilterConditionTwo.Size = new System.Drawing.Size(161, 23);
            this.FilterConditionTwo.TabIndex = 19;
            this.FilterConditionTwo.SelectedIndexChanged += new System.EventHandler(this.FilterConditionTwo_SelectedIndexChanged);
            // 
            // ConditionTwo
            // 
            this.ConditionTwo.Location = new System.Drawing.Point(625, 339);
            this.ConditionTwo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ConditionTwo.Name = "ConditionTwo";
            this.ConditionTwo.Size = new System.Drawing.Size(271, 25);
            this.ConditionTwo.TabIndex = 20;
            this.ConditionTwo.TextChanged += new System.EventHandler(this.ConditionTwo_TextChanged);
            // 
            // StartTimeBox
            // 
            this.StartTimeBox.Location = new System.Drawing.Point(149, 225);
            this.StartTimeBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.StartTimeBox.Name = "StartTimeBox";
            this.StartTimeBox.Size = new System.Drawing.Size(368, 25);
            this.StartTimeBox.TabIndex = 50;
            this.StartTimeBox.TextChanged += new System.EventHandler(this.StartTimeBox_TextChanged);
            // 
            // EndTimeBox
            // 
            this.EndTimeBox.Location = new System.Drawing.Point(523, 225);
            this.EndTimeBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.EndTimeBox.Name = "EndTimeBox";
            this.EndTimeBox.Size = new System.Drawing.Size(375, 25);
            this.EndTimeBox.TabIndex = 51;
            this.EndTimeBox.TextChanged += new System.EventHandler(this.EndTimeBox_TextChanged);
            // 
            // FilterConditionOne
            // 
            this.FilterConditionOne.FormattingEnabled = true;
            this.FilterConditionOne.Items.AddRange(new object[] {
            "大于",
            "大于等于",
            "等于",
            "不等于",
            "小于",
            "小于等于",
            "正则表达式"});
            this.FilterConditionOne.Location = new System.Drawing.Point(459, 302);
            this.FilterConditionOne.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.FilterConditionOne.Name = "FilterConditionOne";
            this.FilterConditionOne.Size = new System.Drawing.Size(161, 23);
            this.FilterConditionOne.TabIndex = 18;
            this.FilterConditionOne.SelectedIndexChanged += new System.EventHandler(this.FilterConditionOne_SelectedIndexChanged);
            // 
            // ConditionOne
            // 
            this.ConditionOne.Location = new System.Drawing.Point(625, 302);
            this.ConditionOne.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ConditionOne.Name = "ConditionOne";
            this.ConditionOne.Size = new System.Drawing.Size(271, 25);
            this.ConditionOne.TabIndex = 19;
            this.ConditionOne.TextChanged += new System.EventHandler(this.ConditionOne_TextChanged);
            // 
            // SecondFilterOne
            // 
            this.SecondFilterOne.FormattingEnabled = true;
            this.SecondFilterOne.Items.AddRange(new object[] {
            "文件大小（单位：字节）",
            "上网账号",
            "宿端口",
            "设备号",
            "IMSI号",
            "源端口",
            "源IP",
            "宿IP",
            "附件数",
            "域名"});
            this.SecondFilterOne.Location = new System.Drawing.Point(149, 302);
            this.SecondFilterOne.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SecondFilterOne.Name = "SecondFilterOne";
            this.SecondFilterOne.Size = new System.Drawing.Size(304, 23);
            this.SecondFilterOne.TabIndex = 17;
            this.SecondFilterOne.SelectedIndexChanged += new System.EventHandler(this.SecondFilterOne_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.previewqueryText);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Size = new System.Drawing.Size(737, 161);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "queryclient";
            this.toolTip1.SetToolTip(this.tabPage1, "全文主节点自带的查询工具");
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // previewqueryText
            // 
            this.previewqueryText.BackColor = System.Drawing.SystemColors.HighlightText;
            this.previewqueryText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.previewqueryText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewqueryText.Location = new System.Drawing.Point(3, 2);
            this.previewqueryText.Margin = new System.Windows.Forms.Padding(1);
            this.previewqueryText.Multiline = true;
            this.previewqueryText.Name = "previewqueryText";
            this.previewqueryText.ReadOnly = true;
            this.previewqueryText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.previewqueryText.Size = new System.Drawing.Size(731, 157);
            this.previewqueryText.TabIndex = 3;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.previewjarText);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Size = new System.Drawing.Size(737, 161);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "batchQueryAndExport_1.7.jar";
            this.toolTip1.SetToolTip(this.tabPage2, "搞不清楚历史沿革的jar包查询全文工具");
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // previewjarText
            // 
            this.previewjarText.BackColor = System.Drawing.SystemColors.HighlightText;
            this.previewjarText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.previewjarText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewjarText.Location = new System.Drawing.Point(3, 2);
            this.previewjarText.Margin = new System.Windows.Forms.Padding(1);
            this.previewjarText.Multiline = true;
            this.previewjarText.Name = "previewjarText";
            this.previewjarText.ReadOnly = true;
            this.previewjarText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.previewjarText.Size = new System.Drawing.Size(731, 157);
            this.previewjarText.TabIndex = 3;
            // 
            // AndOrNotTwo
            // 
            this.AndOrNotTwo.FormattingEnabled = true;
            this.AndOrNotTwo.Items.AddRange(new object[] {
            "AND",
            "OR",
            "NOT"});
            this.AndOrNotTwo.Location = new System.Drawing.Point(149, 376);
            this.AndOrNotTwo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AndOrNotTwo.Name = "AndOrNotTwo";
            this.AndOrNotTwo.Size = new System.Drawing.Size(93, 23);
            this.AndOrNotTwo.TabIndex = 43;
            this.AndOrNotTwo.SelectedIndexChanged += new System.EventHandler(this.AndOrNotTwo_SelectedIndexChanged);
            // 
            // FilterConditionThree
            // 
            this.FilterConditionThree.FormattingEnabled = true;
            this.FilterConditionThree.Items.AddRange(new object[] {
            "大于",
            "大于等于",
            "等于",
            "不等于",
            "小于",
            "小于等于",
            "正则表达式"});
            this.FilterConditionThree.Location = new System.Drawing.Point(459, 376);
            this.FilterConditionThree.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.FilterConditionThree.Name = "FilterConditionThree";
            this.FilterConditionThree.Size = new System.Drawing.Size(161, 23);
            this.FilterConditionThree.TabIndex = 45;
            this.FilterConditionThree.SelectedIndexChanged += new System.EventHandler(this.FilterConditionThree_SelectedIndexChanged);
            // 
            // ConditionThree
            // 
            this.ConditionThree.Location = new System.Drawing.Point(625, 376);
            this.ConditionThree.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ConditionThree.Name = "ConditionThree";
            this.ConditionThree.Size = new System.Drawing.Size(271, 25);
            this.ConditionThree.TabIndex = 46;
            this.ConditionThree.TextChanged += new System.EventHandler(this.ConditionThree_TextChanged);
            // 
            // SecondFilterThree
            // 
            this.SecondFilterThree.FormattingEnabled = true;
            this.SecondFilterThree.Items.AddRange(new object[] {
            "文件大小（单位：字节）",
            "上网账号",
            "宿端口",
            "设备号",
            "IMSI号",
            "源端口",
            "源IP",
            "宿IP",
            "附件数",
            "域名"});
            this.SecondFilterThree.Location = new System.Drawing.Point(247, 376);
            this.SecondFilterThree.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SecondFilterThree.Name = "SecondFilterThree";
            this.SecondFilterThree.Size = new System.Drawing.Size(207, 23);
            this.SecondFilterThree.TabIndex = 44;
            this.SecondFilterThree.SelectedIndexChanged += new System.EventHandler(this.SecondFilterThree_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(153, 19);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(745, 194);
            this.tabControl1.TabIndex = 57;
            // 
            // groupBox1
            // 
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(53, 540);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(463, 76);
            this.groupBox1.TabIndex = 58;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "搜索范围：";
            // 
            // groupBox2
            // 
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(587, 539);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(312, 78);
            this.groupBox2.TabIndex = 59;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据类型：";
            // 
            // groupBox3
            // 
            this.groupBox3.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(53, 620);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(845, 74);
            this.groupBox3.TabIndex = 60;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "查询方式：";
            // 
            // groupBox4
            // 
            this.groupBox4.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox4.Location = new System.Drawing.Point(53, 418);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(845, 114);
            this.groupBox4.TabIndex = 61;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "协议类型：";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel6.Controls.Add(this.Clipboard);
            this.panel6.Controls.Add(this.CloseButton);
            this.panel6.Location = new System.Drawing.Point(0, 712);
            this.panel6.Margin = new System.Windows.Forms.Padding(4);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(956, 72);
            this.panel6.TabIndex = 62;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(956, 786);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.ConditionThree);
            this.Controls.Add(this.SecondFilterThree);
            this.Controls.Add(this.FilterConditionThree);
            this.Controls.Add(this.AndOrNotTwo);
            this.Controls.Add(this.EndTimeBox);
            this.Controls.Add(this.ConditionTwo);
            this.Controls.Add(this.FilterConditionTwo);
            this.Controls.Add(this.SecondFilterTwo);
            this.Controls.Add(this.AndOrNotOne);
            this.Controls.Add(this.ConditionOne);
            this.Controls.Add(this.SecondFilterOne);
            this.Controls.Add(this.FilterConditionOne);
            this.Controls.Add(this.StartTimeBox);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.AllSearch);
            this.Controls.Add(this.AllData);
            this.Controls.Add(this.AllSeed);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.AllProType);
            this.Controls.Add(this.KeyWordsBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.panel6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "全文语法助手";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.Form1_HelpButtonClicked);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox KeyWordsBox;
        private System.Windows.Forms.CheckBox AllProType;
        private System.Windows.Forms.CheckBox WebAccess;
        private System.Windows.Forms.CheckBox Email;
        private System.Windows.Forms.CheckBox Account;
        private System.Windows.Forms.CheckBox IM;
        private System.Windows.Forms.CheckBox FTP;
        private System.Windows.Forms.CheckBox Social;
        private System.Windows.Forms.CheckBox InternetChat;
        private System.Windows.Forms.CheckBox BBS;
        private System.Windows.Forms.CheckBox Telnet;
        private System.Windows.Forms.CheckBox Voip;
        private System.Windows.Forms.CheckBox Gamble;
        private System.Windows.Forms.CheckBox GambleNet;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox Similar;
        private System.Windows.Forms.CheckBox Encrypt;
        private System.Windows.Forms.CheckBox Synonymy;
        private System.Windows.Forms.CheckBox Attach;
        private System.Windows.Forms.CheckBox AllSeed;
        private System.Windows.Forms.CheckBox Garbage;
        private System.Windows.Forms.CheckBox Normal;
        private System.Windows.Forms.CheckBox AllData;
        private System.Windows.Forms.CheckBox Attachment;
        private System.Windows.Forms.CheckBox ContentText;
        private System.Windows.Forms.CheckBox AllSearch;
        private System.Windows.Forms.CheckBox Subject;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button Clipboard;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox StartTimeBox;
        private System.Windows.Forms.TextBox EndTimeBox;
        private System.Windows.Forms.ComboBox SecondFilterTwo;
        private System.Windows.Forms.ComboBox FilterConditionTwo;
        private System.Windows.Forms.TextBox ConditionTwo;
        private System.Windows.Forms.ComboBox FilterConditionOne;
        private System.Windows.Forms.TextBox ConditionOne;
        private System.Windows.Forms.ComboBox SecondFilterOne;
        private System.Windows.Forms.ComboBox AndOrNotOne;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox AndOrNotTwo;
        private System.Windows.Forms.ComboBox FilterConditionThree;
        private System.Windows.Forms.TextBox ConditionThree;
        private System.Windows.Forms.ComboBox SecondFilterThree;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox previewqueryText;
        private System.Windows.Forms.TextBox previewjarText;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Panel panel6;
    }
}

