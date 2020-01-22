using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Citta_T1.Controls.Small
{
    public partial class ModelTitleControl : UserControl
    {
        private string modelTitle;
        private bool selected;

        public bool Selected { get => selected; set => selected = value; }

        public ModelTitleControl()
        {
            InitializeComponent();
            SetModelTitle("新建模型");
        }

        public void SetModelTitle(string modelTitle)
        {
            this.modelTitle = modelTitle;
            int maxLength = 6;
            if (modelTitle.Length > maxLength)
                this.label1.Text = modelTitle.Substring(0, maxLength) + "...";
            else
                this.label1.Text = modelTitle;
            this.toolTip.SetToolTip(this.label1, modelTitle);
        }

        private void ClosePictureBox_Click(object sender, EventArgs e)
        {
            ModelTitlePanel parentPanel = (ModelTitlePanel)this.Parent;
            parentPanel.RemoveModel(this);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            ShowSelectedBorder();
        }

        public void ShowSelectedBorder()
        {
            ModelTitlePanel parentPanel = (ModelTitlePanel)this.Parent;
            parentPanel.ClearSelectedBorder();
            this.BorderStyle = BorderStyle.FixedSingle;
            this.selected = true;
        }
    }


}
