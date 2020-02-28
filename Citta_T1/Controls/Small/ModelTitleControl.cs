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
            SetModelTitle("新建模型");


        }

        public void SetModelTitle(string modelTitle)
        {

            this.modelTitle = modelTitle;
            int maxLength = 6;
            int sumcount = 0;

            sumcount = Regex.Matches(modelTitle.Substring(0, Math.Min(maxLength, modelTitle.Length)), "[a-zA-Z0-9]").Count;

            if (modelTitle.Length > maxLength && sumcount >= 4)
            {
                if (modelTitle.Length <= 8 || sumcount == 6 && modelTitle.Length <= 9)
                    this.label1.Text = modelTitle.Substring(0, modelTitle.Length);
                else if (sumcount == 6 && modelTitle.Length > 9)
                    this.label1.Text = modelTitle.Substring(0, Math.Min(9, modelTitle.Length)) + "...";
                else
                    this.label1.Text = modelTitle.Substring(0, Math.Min(7, modelTitle.Length)) + "...";
            }
            else if (modelTitle.Length > maxLength && sumcount < 4)
            {
                this.label1.Text = modelTitle.Substring(0, maxLength) + "...";
            }
            else
            {
                this.label1.Text = modelTitle.Substring(0, modelTitle.Length);
            }
            this.toolTip.SetToolTip(this.label1, modelTitle);
        }
        public void SetModelTitle(string modelTitle, int nr)
        {
            if (modelTitle.Length > nr)
                this.label1.Text = modelTitle.Substring(0, nr) + "...";
            else
                this.label1.Text = modelTitle.Substring(0, Math.Min(modelTitle.Length, nr));
        }
        public void SetEmptyModelTitle()
        {

            this.label1.Text = "";

        }
        public string storeModelName()
        { return modelTitle; }

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
