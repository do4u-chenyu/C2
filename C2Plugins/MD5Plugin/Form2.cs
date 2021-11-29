using C2.IAOLab.Plugins;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MD5Plugin
{
    public partial class Form2 : Form, IPlugin
    {
        //CommonPlugin commonPlugin;
        public Form2()
        {
            InitializeComponent();
            treeView1.ExpandAll();
        }
        
        private void removeControls()
        {
            this.Controls.Remove(this.commonPlugin);
        }
        
        private void addControls()
        {
            this.commonPlugin.BackColor = SystemColors.InactiveCaption;
            this.commonPlugin.Location = new Point(186, 2);
            this.commonPlugin.Name = "commonPlugin1";
            this.commonPlugin.Size = new System.Drawing.Size(910, 491);
            this.commonPlugin.TabIndex = 13;

            this.Controls.Add(this.commonPlugin);
        }
        
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        private void TreeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            removeControls();
            if (e.Node.Name == "MD5128") 
                this.commonPlugin = new MD5128Plugin();
            else if (e.Node.Name == "MD564")
                this.commonPlugin = new MD564Plugin();
            else if (e.Node.Name == "Base64")
                this.commonPlugin = new Base64Plugin();
            else if (e.Node.Name == "URL")
                this.commonPlugin = new URLlPlugin();
            else if (e.Node.Name == "Unicode")
                this.commonPlugin = new UnicodePlugin();
            else if (e.Node.Name == "HEX")
                this.commonPlugin = new HexPlugin();
            else if (e.Node.Name == "AES128")
                this.commonPlugin = new AES128Plugin();
            else if (e.Node.Name == "SHA1")
                this.commonPlugin = new SHA1Plugin();
            else if (e.Node.Name == "SHA256")
                this.commonPlugin = new SHA256Plugin();
            else if (e.Node.Name == "SHA512")
                this.commonPlugin = new SHA512Plugin();
            else if (e.Node.Name == "NTLM")
                this.commonPlugin = new NTLMPlugin();
            addControls();
        }
       
        public Image GetPluginImage()
        {
            return this.Icon.ToBitmap();
        }
        public string GetPluginDescription()
        {
            return "将字符串进行常用的加密、解密、编码和解码操作；如MD5加密，Base64，Url编码和解码，UTF8和GBK转码等。";
        }
        public string GetPluginVersion()
        {
            return "0.0.3";
        }
        public string GetPluginName()
        {
            return "加密解密";
        }
        public DialogResult ShowFormDialog()
        {
            return this.ShowDialog();
        }
    }
}
