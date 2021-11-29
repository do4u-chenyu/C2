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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Base64");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("URL");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Unicode");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("HEX");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("编码/解码", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4});
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("MD5128");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("MD564");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("AES128");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("SHA1");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("SHA256");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("SHA512");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("NTLM");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("加密/解密", new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode7,
            treeNode8,
            treeNode9,
            treeNode10,
            treeNode11,
            treeNode12});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.commonPlugin = new MD5Plugin.CommonPlugin();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeView1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "Base64";
            treeNode1.Text = "Base64";
            treeNode2.Name = "URL";
            treeNode2.Text = "URL";
            treeNode3.Name = "Unicode";
            treeNode3.Text = "Unicode";
            treeNode4.Name = "HEX";
            treeNode4.Text = "HEX";
            treeNode5.Name = "节点0";
            treeNode5.Text = "编码/解码";
            treeNode6.Name = "MD5128";
            treeNode6.Text = "MD5128";
            treeNode7.Name = "MD564";
            treeNode7.Text = "MD564";
            treeNode8.Name = "AES128";
            treeNode8.Text = "AES128";
            treeNode9.Name = "SHA1";
            treeNode9.Text = "SHA1";
            treeNode10.Name = "SHA256";
            treeNode10.Text = "SHA256";
            treeNode11.Name = "SHA512";
            treeNode11.Text = "SHA512";
            treeNode12.Name = "NTLM";
            treeNode12.Text = "NTLM";
            treeNode13.Name = "节点1";
            treeNode13.Text = "加密/解密";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode13});
            this.treeView1.Size = new System.Drawing.Size(180, 497);
            this.treeView1.TabIndex = 12;
            this.treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView1_NodeMouseDoubleClick);
            // 
            // commonPlugin1
            // 
            this.commonPlugin.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.commonPlugin.Location = new System.Drawing.Point(186, 2);
            this.commonPlugin.Name = "commonPlugin1";
            this.commonPlugin.Size = new System.Drawing.Size(910, 491);
            this.commonPlugin.TabIndex = 13;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(1094, 497);
            this.Controls.Add(this.commonPlugin);
            this.Controls.Add(this.treeView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form2";
            this.Text = "加密解密";
            this.ResumeLayout(false);

        }
        
        #endregion
        private System.Windows.Forms.TreeView treeView1;
        private CommonPlugin commonPlugin;
    }
}