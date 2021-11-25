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
        }
        
        private void removeControls()
        {
            this.Controls.Remove(this.commonPlugin);
        }
        
        private void addControls()
        {
            this.commonPlugin.BackColor = SystemColors.InactiveCaption;
            this.commonPlugin.Location = new Point(12, 55);
            this.commonPlugin.Name = "commonPlugin";
            this.commonPlugin.Size = new Size(1046, 549);
            this.commonPlugin.TabIndex = 12;
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

        private void RadioButton_Click(object sender, EventArgs e)
        {
            //this.SuspendLayout();
            removeControls();

            RadioButton radioButtonClick = sender as RadioButton;
            if (radioButtonClick.Checked)
            {
                switch (radioButtonClick.Name)
                {
                    case "md5128RadioButton":
                        this.commonPlugin = new MD5128Plugin();
                        break;
                    
                    case "md564RadioButton":
                        this.commonPlugin = new MD564Plugin();
                        break;

                    case "sha1RadioButton":
                        this.commonPlugin = new SHA1Plugin();
                        break;
                    case "sha256RadioButton":
                        this.commonPlugin = new SHA256Plugin();
                        break;

                    case "sha512RadioButton":
                        this.commonPlugin = new SHA512Plugin();
                        break;

                    case "NTLMRadioButton":
                        this.commonPlugin = new NTLMPlugin();
                        break;

                    case "urlRadioButton":
                        this.commonPlugin = new URLlPlugin();
                        break;

                    case "unicodeRadioButton":
                        this.commonPlugin = new UnicodePlugin();
                        break;

                    case "base64RadioButton":
                        this.commonPlugin = new Base64Plugin();
                        break;

                    case "ASE128RadioButton":
                        this.commonPlugin = new AES128Plugin();
                        break;

                    case "hexRadioButton":
                        this.commonPlugin = new HexPlugin();
                        break;
                }
            }
            addControls();
            //this.ResumeLayout(false);
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
