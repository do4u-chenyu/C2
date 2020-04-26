using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Citta_T1.Utils;
using System.IO;

namespace Citta_T1.Controls.Left
{
    public partial class ModelButton : UserControl
    {
        public ModelButton()
        {
            InitializeComponent();
            
        }

        public void SetModelName(string modelName)
        {
            this.textButton.Text = modelName;
        }

        public string GetModelName()
        {
            return this.textButton.Text;
            
        }
        public bool EnableOpenDocument { get => this.OpenToolStripMenuItem.Enabled; set => this.OpenToolStripMenuItem.Enabled=value; }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)

        {
            Global.GetMainForm().LoadDocument(this.textButton.Text);
            this.OpenToolStripMenuItem.Enabled = false;
        }

        private void ModelButton_Load(object sender, EventArgs e)
        {
            // 模型全路径浮动提示信息
            String helpInfo = Path.Combine(Global.GetCurrentDocument().UserPath, this.textButton.Text, this.textButton.Text + ".xml");
            this.toolTip1.SetToolTip(this.rightPictureBox, helpInfo);

            // 模型名称浮动提示信息
            helpInfo = GetModelName();
            this.toolTip1.SetToolTip(this.textButton, helpInfo);
        }
    }


}
