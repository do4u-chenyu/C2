namespace QQSpiderPlugin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.image = new System.Windows.Forms.DataGridViewImageColumn();
            this.uin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nick = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.country = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.province = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.city = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gender = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.age = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.outputButton1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.InputActButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ActStartButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel6 = new System.Windows.Forms.Panel();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.image2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.max = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.area = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.category = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.memo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel5 = new System.Windows.Forms.Panel();
            this.outputButton2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.InputGroupButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.GroupStartButton = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label11 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(658, 485);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel3);
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(650, 459);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "QQ爬虫";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.richTextBox1);
            this.panel3.Controls.Add(this.dataGridView1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 69);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(644, 332);
            this.panel3.TabIndex = 5;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(121, 332);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged_1);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.image,
            this.uin,
            this.nick,
            this.country,
            this.province,
            this.city,
            this.gender,
            this.age});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Right;
            this.dataGridView1.Location = new System.Drawing.Point(121, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(523, 332);
            this.dataGridView1.TabIndex = 2;
            // 
            // image
            // 
            this.image.HeaderText = "头像";
            this.image.Name = "image";
            this.image.ReadOnly = true;
            // 
            // uin
            // 
            this.uin.HeaderText = "账号";
            this.uin.Name = "uin";
            this.uin.ReadOnly = true;
            // 
            // nick
            // 
            this.nick.HeaderText = "昵称";
            this.nick.Name = "nick";
            this.nick.ReadOnly = true;
            this.nick.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.nick.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // country
            // 
            this.country.HeaderText = "国家";
            this.country.Name = "country";
            this.country.ReadOnly = true;
            // 
            // province
            // 
            this.province.HeaderText = "省市";
            this.province.Name = "province";
            this.province.ReadOnly = true;
            // 
            // city
            // 
            this.city.HeaderText = "城市";
            this.city.Name = "city";
            this.city.ReadOnly = true;
            // 
            // gender
            // 
            this.gender.HeaderText = "性别";
            this.gender.Name = "gender";
            this.gender.ReadOnly = true;
            // 
            // age
            // 
            this.age.HeaderText = "年龄";
            this.age.Name = "age";
            this.age.ReadOnly = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.outputButton1);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.progressBar1);
            this.panel2.Controls.Add(this.InputActButton);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(644, 66);
            this.panel2.TabIndex = 4;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // outputButton1
            // 
            this.outputButton1.Location = new System.Drawing.Point(545, 3);
            this.outputButton1.Name = "outputButton1";
            this.outputButton1.Size = new System.Drawing.Size(75, 23);
            this.outputButton1.TabIndex = 6;
            this.outputButton1.Text = "导出";
            this.outputButton1.UseVisualStyleBackColor = true;
            this.outputButton1.Click += new System.EventHandler(this.OutputButton1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(15, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "查询进度";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(104, 34);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(516, 23);
            this.progressBar1.TabIndex = 4;
            // 
            // InputActButton
            // 
            this.InputActButton.Location = new System.Drawing.Point(441, 3);
            this.InputActButton.Name = "InputActButton";
            this.InputActButton.Size = new System.Drawing.Size(75, 23);
            this.InputActButton.TabIndex = 3;
            this.InputActButton.Text = "导入";
            this.InputActButton.UseVisualStyleBackColor = true;
            this.InputActButton.Click += new System.EventHandler(this.InputIDDataButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(397, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "选中导入QQ号列表文件，或者粘贴到左侧任务列表中，一行一个QQ号";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ActStartButton);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 401);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(644, 55);
            this.panel1.TabIndex = 3;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // ActStartButton
            // 
            this.ActStartButton.Location = new System.Drawing.Point(441, 17);
            this.ActStartButton.Name = "ActStartButton";
            this.ActStartButton.Size = new System.Drawing.Size(75, 23);
            this.ActStartButton.TabIndex = 1;
            this.ActStartButton.Text = "开始";
            this.ActStartButton.UseVisualStyleBackColor = true;
            this.ActStartButton.Click += new System.EventHandler(this.ActStartButton_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(543, 17);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Cancle_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel6);
            this.tabPage2.Controls.Add(this.panel5);
            this.tabPage2.Controls.Add(this.panel4);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(650, 459);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "QQ群爬虫";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.richTextBox2);
            this.panel6.Controls.Add(this.dataGridView2);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(3, 69);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(644, 332);
            this.panel6.TabIndex = 2;
            // 
            // richTextBox2
            // 
            this.richTextBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox2.Location = new System.Drawing.Point(0, 0);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(121, 332);
            this.richTextBox2.TabIndex = 5;
            this.richTextBox2.Text = "";
            this.richTextBox2.TextChanged += new System.EventHandler(this.richTextBox2_TextChanged);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.image2,
            this.code,
            this.name,
            this.num,
            this.max,
            this.area,
            this.category,
            this.label,
            this.memo});
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Right;
            this.dataGridView2.Location = new System.Drawing.Point(121, 0);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(523, 332);
            this.dataGridView2.TabIndex = 4;
            // 
            // image2
            // 
            this.image2.HeaderText = "群头像";
            this.image2.Name = "image2";
            this.image2.ReadOnly = true;
            // 
            // code
            // 
            this.code.HeaderText = "群ID";
            this.code.Name = "code";
            this.code.ReadOnly = true;
            // 
            // name
            // 
            this.name.HeaderText = "群名称";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // num
            // 
            this.num.HeaderText = "群人数";
            this.num.Name = "num";
            this.num.ReadOnly = true;
            // 
            // max
            // 
            this.max.HeaderText = "群上限";
            this.max.Name = "max";
            this.max.ReadOnly = true;
            // 
            // area
            // 
            this.area.HeaderText = "地域";
            this.area.Name = "area";
            this.area.ReadOnly = true;
            // 
            // category
            // 
            this.category.HeaderText = "分类";
            this.category.Name = "category";
            this.category.ReadOnly = true;
            // 
            // label
            // 
            this.label.HeaderText = "标签";
            this.label.Name = "label";
            this.label.ReadOnly = true;
            // 
            // memo
            // 
            this.memo.HeaderText = "群简介";
            this.memo.Name = "memo";
            this.memo.ReadOnly = true;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.outputButton2);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Controls.Add(this.progressBar2);
            this.panel5.Controls.Add(this.InputGroupButton);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(3, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(644, 66);
            this.panel5.TabIndex = 1;
            // 
            // outputButton2
            // 
            this.outputButton2.Location = new System.Drawing.Point(545, 3);
            this.outputButton2.Name = "outputButton2";
            this.outputButton2.Size = new System.Drawing.Size(75, 23);
            this.outputButton2.TabIndex = 9;
            this.outputButton2.Text = "导出";
            this.outputButton2.UseVisualStyleBackColor = true;
            this.outputButton2.Click += new System.EventHandler(this.OutputButton2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(15, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "查询进度";
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(104, 34);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(516, 23);
            this.progressBar2.TabIndex = 7;
            // 
            // InputGroupButton
            // 
            this.InputGroupButton.Location = new System.Drawing.Point(441, 3);
            this.InputGroupButton.Name = "InputGroupButton";
            this.InputGroupButton.Size = new System.Drawing.Size(75, 23);
            this.InputGroupButton.TabIndex = 5;
            this.InputGroupButton.Text = "导入";
            this.InputGroupButton.UseVisualStyleBackColor = true;
            this.InputGroupButton.Click += new System.EventHandler(this.InputGroupButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(410, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "选中导入QQ群号列表文件，或者粘贴到左侧任务列表中，一行一个QQ群";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.GroupStartButton);
            this.panel4.Controls.Add(this.button5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(3, 401);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(644, 55);
            this.panel4.TabIndex = 0;
            // 
            // GroupStartButton
            // 
            this.GroupStartButton.Location = new System.Drawing.Point(441, 17);
            this.GroupStartButton.Name = "GroupStartButton";
            this.GroupStartButton.Size = new System.Drawing.Size(75, 23);
            this.GroupStartButton.TabIndex = 3;
            this.GroupStartButton.Text = "开始";
            this.GroupStartButton.UseVisualStyleBackColor = true;
            this.GroupStartButton.Click += new System.EventHandler(this.GroupStartButton_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(543, 17);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 4;
            this.button5.Text = "取消";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.GroupCancle_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label11);
            this.tabPage3.Controls.Add(this.button3);
            this.tabPage3.Controls.Add(this.label10);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.button1);
            this.tabPage3.Controls.Add(this.textBox1);
            this.tabPage3.Controls.Add(this.pictureBox1);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(650, 459);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "WXID查头像昵称";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(186, 301);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(253, 28);
            this.label11.TabIndex = 10;
            this.label11.Text = "opps，二维码生成有问题";
            this.label11.Visible = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(545, 428);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 9;
            this.button3.Text = "取消";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.WXCancle_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(90, 158);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(272, 19);
            this.label10.TabIndex = 8;
            this.label10.Text = "步骤5: 再次扫二维码，就可以看到头像和昵称";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(90, 133);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(389, 19);
            this.label9.TabIndex = 7;
            this.label9.Text = "步骤4: 此时可以看到头像，然后添加好友，不用理会对方是否通过";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(90, 108);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(220, 19);
            this.label8.TabIndex = 6;
            this.label8.Text = "步骤3: 打开自己的微信扫一扫二维码";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(90, 83);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(220, 19);
            this.label7.TabIndex = 5;
            this.label7.Text = "步骤2: 点击按钮生成微信好友二维码";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(90, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(220, 19);
            this.label6.TabIndex = 4;
            this.label6.Text = "步骤1: 输入wxid_开头的微信内部ID";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(472, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "生成二维码";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(189, 24);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(259, 21);
            this.textBox1.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pictureBox1.Location = new System.Drawing.Point(189, 195);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(250, 250);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9.5F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(84, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 19);
            this.label5.TabIndex = 0;
            this.label5.Text = "wxid_开头的ID";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(658, 485);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QQ爬虫";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button ActStartButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button InputActButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button GroupStartButton;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button InputGroupButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button outputButton1;
        private System.Windows.Forms.Button outputButton2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewImageColumn image;
        private System.Windows.Forms.DataGridViewTextBoxColumn uin;
        private System.Windows.Forms.DataGridViewTextBoxColumn nick;
        private System.Windows.Forms.DataGridViewTextBoxColumn country;
        private System.Windows.Forms.DataGridViewTextBoxColumn province;
        private System.Windows.Forms.DataGridViewTextBoxColumn city;
        private System.Windows.Forms.DataGridViewTextBoxColumn gender;
        private System.Windows.Forms.DataGridViewTextBoxColumn age;
        private System.Windows.Forms.DataGridViewImageColumn image2;
        private System.Windows.Forms.DataGridViewTextBoxColumn code;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn num;
        private System.Windows.Forms.DataGridViewTextBoxColumn max;
        private System.Windows.Forms.DataGridViewTextBoxColumn area;
        private System.Windows.Forms.DataGridViewTextBoxColumn category;
        private System.Windows.Forms.DataGridViewTextBoxColumn label;
        private System.Windows.Forms.DataGridViewTextBoxColumn memo;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label11;
    }
}

