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
 

        private void rightPictureBox_MouseEnter(object sender, EventArgs e)
        {
            String helpInfo = "模型文档的名称";
            this.toolTip1.SetToolTip(this.rightPictureBox, helpInfo);
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Global.GetMainForm().LoadDocument(this.textButton.Text);
            this.OpenToolStripMenuItem.Enabled = false;
        }
     
    }


}
