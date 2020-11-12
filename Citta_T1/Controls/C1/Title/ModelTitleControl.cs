using C2.Business.Schedule;
using C2.Core;
using C2.Forms;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace C2.Controls.Title
{
    public delegate void DocumentSwitchEventHandler(string modelTitle);

    public partial class ModelTitleControl : UserControl
    {
        private string modelTitle;
        private bool selected;
        public bool Selected { get => selected; set => selected = value; }
        public string ModelTitle { get => modelTitle; }
        public event DocumentSwitchEventHandler ModelDocumentSwitch;


        public ModelTitleControl()
        {
            InitializeComponent();
            SetOriginalModelTitle("我的新模型");
            ClearDirtyPictureBox();

        }

        public void SetDirtyPictureBox()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModelTitleControl));
            this.dirtyPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("dirtyPictureBox.Image")));
        }

        public void ClearDirtyPictureBox()
        {
            this.dirtyPictureBox.Image = null;
        }

        public void SetOriginalModelTitle(string modelTitle)
        {

            this.modelTitle = modelTitle;
            int maxLength = 6;
            this.toolTip.SetToolTip(this.modelTitlelabel, modelTitle);

            if (modelTitle.Length <= maxLength)
            {
                this.modelTitlelabel.Text = modelTitle;
                return;
            }

            int digitLetterCount = Regex.Matches(modelTitle.Substring(0, maxLength), "[a-zA-Z0-9]").Count;
            if (digitLetterCount < 4)
                this.modelTitlelabel.Text = modelTitle.Substring(0, maxLength) + "...";
            else 
            {
                this.modelTitlelabel.Text = modelTitle.Substring(0, Math.Min(8, modelTitle.Length));
                if (modelTitle.Length > 8)
                    this.modelTitlelabel.Text += "...";
            }

        }
        public void SetNewModelTitle(string modelTitle, int nr)
        {
            if (String.IsNullOrEmpty(modelTitle))
                return;
            if (nr == -1)
                this.modelTitlelabel.Text = string.Empty;
            else if (nr == 0)
                this.modelTitlelabel.Text = modelTitle.Substring(0, 1);
            else
            {
                this.modelTitlelabel.Text = modelTitle.Substring(0, Math.Min(modelTitle.Length, nr));
                if (modelTitle.Length > nr)
                    this.modelTitlelabel.Text += "...";
            }
            
        }


        private void ClosePictureBox_Click(object sender, EventArgs e)
        {
            // 不再使用
            ////文档没有被选中返回
            //if (this.BorderStyle != BorderStyle.FixedSingle)
            //    return;
            //ModelTitlePanel parentPanel = Global.GetTaskBar();
            //CanvasForm mainForm = Global.GetCanvsaForm();
            ////判断当前模型是否正在运行，运行中的不能关闭
            //if(Global.GetCurrentDocument().TaskManager.ModelStatus == ModelStatus.Running || Global.GetCurrentDocument().TaskManager.ModelStatus == ModelStatus.Pause)
            //{
            //    DialogResult isDocClose = MessageBox.Show(string.Format("\"{0}\"正在运行中，是否强制关闭？", modelTitle),"关闭", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //    // 取消关闭，直接return
            //    // 强制关闭
            //    if (isDocClose == DialogResult.Yes)
            //    {
            //        Global.GetCurrentDocument().TaskManager.Stop();
            //        mainForm.UpdateRunbuttonImageInfo();
            //        Global.GetMyModelControl().EnableClosedDocumentMenu(this.modelTitle);
            //        mainForm.DeleteCurrentDocument();
            //        parentPanel.RemoveModel(this);
            //    }
            //    return;
            //}

            //if (!Global.GetCurrentDocument().Dirty)
            //{
            //    if (parentPanel.Controls.Count != 2)
            //    {
            //        Global.GetMyModelControl().EnableClosedDocumentMenu(this.modelTitle);
            //        mainForm.DeleteCurrentDocument();
            //        parentPanel.RemoveModel(this);
            //    }
            //    //Global.GetCurrentDocument().TaskManager.CloseThread(); // 此处可能是个bug,需要讨论
            //    return;
            //}
            //DialogResult result = MessageBox.Show(String.Format("保存文件\"{0}\"?", modelTitle),
            //                                     "保存",
            //                                      MessageBoxButtons.YesNoCancel,
            //                                      MessageBoxIcon.Question);
            //// 取消操作
            //if (result == DialogResult.Cancel)
            //    return;

            //// 保存文件
            //if (result == DialogResult.Yes)
            //{
            //    mainForm.SaveCurrentDocument();
            //    ClearDirtyPictureBox();
            //}

            //// 关闭文件
            //if (parentPanel.Controls.Count != 2) // 要问下这个地方为啥是!=2
            //{
            //    Global.GetMyModelControl().EnableClosedDocumentMenu(this.modelTitle);
            //    mainForm.DeleteCurrentDocument();
            //    parentPanel.RemoveModel(this);
            //}
            ////Global.GetCurrentDocument().TaskManager.CloseThread();
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
