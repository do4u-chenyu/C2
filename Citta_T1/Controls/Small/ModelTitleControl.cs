using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Citta_T1.Business;
using Citta_T1.Utils;
using Citta_T1.Controls;

namespace Citta_T1.Controls.Small
{
    public partial class ModelTitleControl : UserControl
    {
        private string modelTitle;
        private bool selected;

        public bool Selected { get => selected; set => selected = value; }
        public string ModelTitle { get => modelTitle; }

        public ModelTitleControl()
        {
            InitializeComponent();
            SetOriginalModelTitle("新建模型");


        }

        public void SetOriginalModelTitle(string modelTitle)
        {

            this.modelTitle = modelTitle;
            int maxLength = 6;
            int digitLetterCount = 0;
            this.toolTip.SetToolTip(this.label1, modelTitle);

            if (modelTitle.Length <= maxLength)
                this.label1.Text = modelTitle;
            else
            {
                digitLetterCount = Regex.Matches(modelTitle.Substring(0, maxLength), "[a-zA-Z0-9]").Count;
                if (digitLetterCount < 4)
                    this.label1.Text = modelTitle.Substring(0, maxLength) + "...";
                else //>=4
                {
                    this.label1.Text = modelTitle.Substring(0, Math.Min(8, modelTitle.Length));
                    if (modelTitle.Length > 8)
                        this.label1.Text += "...";
                }
            }
            
        }
        public void SetNewModelTitle(string modelTitle, int nr)
        {
            if (nr == 0)
                this.label1.Text = "";
            else
            {
                this.label1.Text = modelTitle.Substring(0, Math.Min(modelTitle.Length, nr));
                if (modelTitle.Length > nr)
                    this.label1.Text += "...";
            }
        }
 

        private void ClosePictureBox_Click(object sender, EventArgs e)
        {
            ModelTitlePanel parentPanel = (ModelTitlePanel)this.Parent;
            parentPanel.RemoveModel(this);

            //MessageBox.Show("文件尚未保存","保存",MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

        }

        private void label1_Click(object sender, EventArgs e)
        {
            ShowSelectedBorder();



            // TODO
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
