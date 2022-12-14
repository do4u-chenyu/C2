namespace MD5Plugin
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("超级Base64");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("URL编解码");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Unicode编解码");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Hex编解码");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("编码/解码", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4});
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("AES(128位)");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("RC4");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("RC4-MD5(施工中)");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("chacha20(施工中)");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("salsa20(施工中)");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("ss-table(施工中)");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("加密/解密", new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode7,
            treeNode8,
            treeNode9,
            treeNode10,
            treeNode11});
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("MD5(128位)");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("MD5(64位)");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("SHA-1");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("SHA-256");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("SHA-512");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Mysql5");
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("NTLM");
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("Hash", new System.Windows.Forms.TreeNode[] {
            treeNode13,
            treeNode14,
            treeNode15,
            treeNode16,
            treeNode17,
            treeNode18,
            treeNode19});
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("十进制转十六");
            System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("八进制转十六");
            System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("进制转换", new System.Windows.Forms.TreeNode[] {
            treeNode21,
            treeNode22});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.commonPlugin = new MD5Plugin.CommonPlugin();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.BackColor = System.Drawing.Color.GhostWhite;
            this.treeView1.Font = new System.Drawing.Font("Times New Roman", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "超级Base64";
            treeNode1.Text = "超级Base64";
            treeNode2.Name = "URL编解码";
            treeNode2.Text = "URL编解码";
            treeNode3.Name = "Unicode编解码";
            treeNode3.Text = "Unicode编解码";
            treeNode4.Name = "Hex编解码";
            treeNode4.Text = "Hex编解码";
            treeNode5.ForeColor = System.Drawing.SystemColors.ControlDark;
            treeNode5.Name = "节点0";
            treeNode5.Text = "编码/解码";
            treeNode6.Name = "AES(128位)";
            treeNode6.Text = "AES(128位)";
            treeNode7.Name = "RC4";
            treeNode7.Text = "RC4";
            treeNode8.Name = "RC4-MD5";
            treeNode8.Text = "RC4-MD5(施工中)";
            treeNode9.Name = "chacha20";
            treeNode9.Text = "chacha20(施工中)";
            treeNode10.Name = "salsa20";
            treeNode10.Text = "salsa20(施工中)";
            treeNode11.Name = "shadowsocks-table";
            treeNode11.Text = "ss-table(施工中)";
            treeNode12.ForeColor = System.Drawing.SystemColors.ControlDark;
            treeNode12.Name = "节点1";
            treeNode12.Text = "加密/解密";
            treeNode13.Name = "MD5(128位)";
            treeNode13.Text = "MD5(128位)";
            treeNode14.Name = "MD5(64位)";
            treeNode14.Text = "MD5(64位)";
            treeNode15.Name = "SHA-1";
            treeNode15.Text = "SHA-1";
            treeNode16.Name = "SHA-256";
            treeNode16.Text = "SHA-256";
            treeNode17.Name = "SHA-512";
            treeNode17.Text = "SHA-512";
            treeNode18.Name = "Mysql5";
            treeNode18.Text = "Mysql5";
            treeNode19.Name = "NTLM";
            treeNode19.Text = "NTLM";
            treeNode20.Name = "节点0";
            treeNode20.Text = "Hash";
            treeNode21.Name = "十进制转十六";
            treeNode21.Text = "十进制转十六";
            treeNode22.Name = "八进制转十六";
            treeNode22.Text = "八进制转十六";
            treeNode23.ForeColor = System.Drawing.SystemColors.ControlDark;
            treeNode23.Name = "节点0";
            treeNode23.Text = "进制转换";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode12,
            treeNode20,
            treeNode23});
            this.treeView1.Size = new System.Drawing.Size(180, 493);
            this.treeView1.TabIndex = 12;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView1_NodeMouseClick);
            // 
            // commonPlugin
            // 
            this.commonPlugin.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.commonPlugin.Location = new System.Drawing.Point(186, 0);
            this.commonPlugin.Name = "commonPlugin";
            this.commonPlugin.Size = new System.Drawing.Size(910, 499);
            this.commonPlugin.TabIndex = 13;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(1102, 502);
            this.Controls.Add(this.commonPlugin);
            this.Controls.Add(this.treeView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "加密解密";
            this.ResumeLayout(false);

        }
        
        #endregion
        private System.Windows.Forms.TreeView treeView1;
        private CommonPlugin commonPlugin;
    }
}