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

namespace Citta_T1.Controls.Title
{
    public delegate void DocumentSwitchEventHandler(string modelTitle);
    public partial class ModelTitleControl : UserControl
    {
        private string modelTitle;
        private bool selected;
        private bool dirty;
        public bool Selected { get => selected; set => selected = value; }
        public string ModelTitle { get => modelTitle; }
        public bool Dirty { get => dirty; set => dirty = value; }

        public event DocumentSwitchEventHandler ModelDocumentSwitch;

        public ModelTitleControl()
        {
            InitializeComponent();
            SetOriginalModelTitle("新建模型");
            dirty = false;
            ClearDirtyPictureBox();

        }

        public void SetDirtyPictureBox()
        {
            dirty = true;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModelTitleControl));
            this.dirtyPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("dirtyPictureBox.Image")));
        }

        public void ClearDirtyPictureBox()
        {
            dirty = false;
            this.dirtyPictureBox.Image = null;
        }

        public void SetOriginalModelTitle(string modelTitle)
        {

            this.modelTitle = modelTitle;
            int maxLength = 6;
            int digitLetterCount = 0;
            this.toolTip.SetToolTip(this.modelTitlelabel, modelTitle);

            if (modelTitle.Length <= maxLength)
                this.modelTitlelabel.Text = modelTitle;
            else
            {
                digitLetterCount = Regex.Matches(modelTitle.Substring(0, maxLength), "[a-zA-Z0-9]").Count;
                if (digitLetterCount < 4)
                    this.modelTitlelabel.Text = modelTitle.Substring(0, maxLength) + "...";
                else //>=4
                {
                    this.modelTitlelabel.Text = modelTitle.Substring(0, Math.Min(8, modelTitle.Length));
                    if (modelTitle.Length > 8)
                        this.modelTitlelabel.Text += "...";
                }
            }
            
        }
        public void SetNewModelTitle(string modelTitle, int nr)
        {
            if (nr == 0)
                this.modelTitlelabel.Text = "";
            else
            {
                this.modelTitlelabel.Text = modelTitle.Substring(0, Math.Min(modelTitle.Length, nr));
                if (modelTitle.Length > nr)
                    this.modelTitlelabel.Text += "...";
            }
        }
 

        private void ClosePictureBox_Click(object sender, EventArgs e)
        {
            if (this.BorderStyle != BorderStyle.FixedSingle)
                return;
            ModelTitlePanel parentPanel = (ModelTitlePanel)this.Parent;
            MainForm mainForm = (MainForm)this.ParentForm;
            if (!Global.GetCurrentDocument().Dirty)
            {   // 
                if (parentPanel.Controls.Count != 2)
                {
                    mainForm.DeleteCurrentDocument();
                    parentPanel.RemoveModel(this);
                }
                return;
            }                       
            DialogResult result= MessageBox.Show("保存文件"+"\""+modelTitle + "\"" + "?","保存",MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {                
                mainForm.SaveDocument();
                ClearDirtyPictureBox();
                if (parentPanel.Controls.Count != 2)
                {
                    mainForm.DeleteCurrentDocument();
                    parentPanel.RemoveModel(this);                    
                }
            }
            else if (result == DialogResult.No)
            {
                if (parentPanel.Controls.Count != 2)
                {
                    mainForm.DeleteCurrentDocument();
                    parentPanel.RemoveModel(this);                  
                }
            }
            else
                return;

        }

        public void ShowSelectedBorder()
        {
            ModelTitlePanel parentPanel = (ModelTitlePanel)this.Parent;
            if (parentPanel != null)
                parentPanel.ClearSelectedBorder();
            this.BorderStyle = BorderStyle.FixedSingle;
            this.selected = true;
            ModelDocumentSwitch?.Invoke(modelTitle);
        }
        
        private void MdelTitlelabel_Click(object sender, EventArgs e)
        {
            ShowSelectedBorder();
        }
    }


}
