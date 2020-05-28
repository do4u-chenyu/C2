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
using Citta_T1.Controls.Left;
using Citta_T1.Core;

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
            SetOriginalModelTitle("我的新模型");
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
            //文档没有被选中返回
            if (this.BorderStyle != BorderStyle.FixedSingle)
                return;
            ModelTitlePanel parentPanel = Global.GetModelTitlePanel();
            MainForm mainForm = Global.GetMainForm();
            if (!Global.GetCurrentDocument().Dirty)
            {    
                if (parentPanel.Controls.Count != 2)
                {
                    Global.GetMyModelControl().EnableClosedDocumentMenu(this.modelTitle);
                    mainForm.DeleteCurrentDocument();
                    parentPanel.RemoveModel(this);
                }
                Global.GetCurrentDocument().TaskManager.CloseThread(); // 此处可能是个bug,需要讨论
                return;
            }                       
            DialogResult result= MessageBox.Show(String.Format("保存文件\"{0}\"?", modelTitle),
                                                 "保存",
                                                  MessageBoxButtons.YesNoCancel, 
                                                  MessageBoxIcon.Question);
            // 取消操作
            if (result == DialogResult.Cancel)
                return;

            // 保存文件
            if (result == DialogResult.Yes)
            {                
                mainForm.SaveCurrentDocument();
                ClearDirtyPictureBox();
            }

            // 关闭文件
            if (parentPanel.Controls.Count != 2) // 要问下这个地方为啥是!=2
            {
                Global.GetMyModelControl().EnableClosedDocumentMenu(this.modelTitle);
                mainForm.DeleteCurrentDocument();
                parentPanel.RemoveModel(this);                  
            }
            Global.GetCurrentDocument().TaskManager.CloseThread();
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
