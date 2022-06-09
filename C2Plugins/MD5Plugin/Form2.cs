using C2.IAOLab.Plugins;
using C2.Log;
using System.Drawing;
using System.Windows.Forms;

namespace MD5Plugin
{
    public partial class Form2 : Form, IPlugin
    {
        public Form2()
        {
            InitializeComponent();
            treeView1.ExpandAll();
        }
        
        private void RemoveControls()
        {
            this.Controls.Remove(this.commonPlugin);
        }
        
        private void AddControls()
        {
            this.commonPlugin.BackColor = SystemColors.InactiveCaption;
            this.commonPlugin.Location = new Point(186, 0);
            this.commonPlugin.Name = "commonPlugin";
            this.commonPlugin.Size = new Size(910, 499);
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
            return "1.0.1";
        }
        public string GetPluginName()
        {
            return "加密解密";
        }
        public DialogResult ShowFormDialog()
        {
            return this.ShowDialog();
        }

        private void TreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            new Log().LogManualButton(e.Node.Name, "打开");
            if (e.Button == MouseButtons.Right)
                return;

            RemoveControls();
            this.Text = e.Node.Name;
            if (e.Node.Name == "MD5(128位)")
                this.commonPlugin = new MD5128Plugin();
            else if (e.Node.Name == "MD5(64位)")
                this.commonPlugin = new MD564Plugin();
            else if (e.Node.Name == "超级Base64")
                this.commonPlugin = new Base64Plugin();
            else if (e.Node.Name == "URL编解码")
                this.commonPlugin = new URLPlugin();
            else if (e.Node.Name == "Unicode编解码")
                this.commonPlugin = new UnicodePlugin();
            else if (e.Node.Name == "Hex编解码")
                this.commonPlugin = new HexPlugin();
            else if (e.Node.Name == "AES(128位)")
                this.commonPlugin = new AES128Plugin();
            else if (e.Node.Name == "SHA-1")
                this.commonPlugin = new SHA1Plugin();
            else if (e.Node.Name == "SHA-256")
                this.commonPlugin = new SHA256Plugin();
            else if (e.Node.Name == "SHA-512")
                this.commonPlugin = new SHA512Plugin();
            else if (e.Node.Name == "NTLM")
                this.commonPlugin = new NTLMPlugin();
            else if (e.Node.Name == "十进制转十六")
                this.commonPlugin = new HexDecimal();
            else if (e.Node.Name == "八进制转十六")
                this.commonPlugin = new OctDecimal();
            else if (e.Node.Name == "Mysql5")
                this.commonPlugin = new Mysql5Plugin();
            else if (e.Node.Name == "RC4")
                this.commonPlugin = new RC4Plugin();
            else
                this.Text = "加密解密";
            AddControls();
        }
    }
}
