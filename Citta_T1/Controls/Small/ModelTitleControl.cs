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
            int sumcount = 0;
           
            sumcount=Regex.Matches(modelTitle.Substring(0, Math.Min(maxLength, modelTitle.Length)), "[a-zA-Z0-9]").Count;

            if (modelTitle.Length > maxLength && sumcount >= 4)
            {
                if(modelTitle.Length <= 8|| sumcount == 6 && modelTitle.Length <= 9)
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
        public void SetModelTitle3(string modelTitle)
        {
            if (modelTitle.Length > 3)
                this.label1.Text = modelTitle.Substring(0, 3) + "...";
            else
                this.label1.Text = modelTitle.Substring(0,Math.Min(modelTitle.Length, 3));

        }
        public void SetModelTitle2(string modelTitle)
        {
            if(modelTitle.Length>2)
                this.label1.Text = modelTitle.Substring(0,  2) + "...";
            else
                this.label1.Text = modelTitle.Substring(0, Math.Min(modelTitle.Length, 2));

        }
        public void SetModelTitle1(string modelTitle)
        {
            if (modelTitle.Length > 1)
                this.label1.Text = modelTitle.Substring(0, 1) + "...";
            else
                this.label1.Text = modelTitle.Substring(0,Math.Min(modelTitle.Length, 1));

        }
        public void SetModelTitle0()
        {
       
            this.label1.Text ="";

        }
        public string storeModelName()
        { return modelTitle; }

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
