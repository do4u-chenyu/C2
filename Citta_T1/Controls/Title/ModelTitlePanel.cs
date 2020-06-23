using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Citta_T1.Controls.Title
{
    public delegate void NewDocumentEventHandler(string modelTitle);
    public delegate void DocumentSwitchHandler(string modelTitle);
    public partial class ModelTitlePanel : UserControl
    {
        private static LogUtil log = LogUtil.GetInstance("ModelTitlePanel");

        private static Point OriginalLocation = new System.Drawing.Point(1, 6);
        private List<ModelTitleControl> modelTitleControls;
        private int rawModelTitleNum = 9;
        public event NewDocumentEventHandler NewModelDocument;
        public event DocumentSwitchHandler ModelDocumentSwitch;

        public ModelTitlePanel()
        {
            modelTitleControls = new List<ModelTitleControl>();
            InitializeComponent();
        }


        public void UpModelTitle()
        {
            rawModelTitleNum = this.Width / 142;
            foreach (ModelTitleControl mt in modelTitleControls)
            {
                if (modelTitleControls.Count <= rawModelTitleNum)
                    mt.SetOriginalModelTitle(mt.ModelTitle);
                else if (modelTitleControls.Count > rawModelTitleNum && modelTitleControls.Count < 17)
                    mt.SetNewModelTitle(mt.ModelTitle, 3);
                else if (modelTitleControls.Count >= 17 && modelTitleControls.Count < 20)
                    mt.SetNewModelTitle(mt.ModelTitle, 2);
                else if (modelTitleControls.Count >= 20 && modelTitleControls.Count < 24)
                    mt.SetNewModelTitle(mt.ModelTitle, 1);
                else if (modelTitleControls.Count >= 24)
                    mt.SetNewModelTitle(mt.ModelTitle, 0);
            }
            //log.Info("modetitle :标题长度" +)
        }
        public void LoadModelDocument(string[] modelTitles)
        {
            int end = modelTitles.Length - 1;
            for (int i = 0; i < modelTitles.Length; i++)
            {
                ModelTitleControl mtControl = new ModelTitleControl();
                mtControl.ModelDocumentSwitch += DocumentSwitch;
                this.modelTitleControls.Add(mtControl);
                this.Controls.Add(mtControl);

                // 根据元素个数调整位置和大小
                mtControl.SetOriginalModelTitle(modelTitles[i]);
                if (i == 0)
                    mtControl.Location = OriginalLocation;
                else
                {
                    ModelTitleControl preMTC = modelTitleControls[modelTitleControls.Count - 2];
                    mtControl.Location = new Point(preMTC.Location.X + preMTC.Width + 2, 6);
                    ResizeModel();
                    UpModelTitle();
                }
                if (i == end)
                {
                    mtControl.BorderStyle = BorderStyle.FixedSingle;
                    mtControl.Selected = true;
                }


            }

        }

        public void AddModel(string modelTitle)
        {
            ModelTitleControl mtControl = new ModelTitleControl();
            mtControl.ModelDocumentSwitch += DocumentSwitch;
            modelTitleControls.Add(mtControl);
            mtControl.SetOriginalModelTitle(modelTitle);
            NewModelDocument?.Invoke(modelTitle);



            // 根据容器中最后一个ModelTitleControl的Location
            // 设置新控件在ModelTitlePanel中的Location
            if (modelTitleControls.Count <= 1)
            {
                mtControl.Location = OriginalLocation;
                this.Controls.Add(mtControl);
                mtControl.ShowSelectedBorder();
            }
            else // models.Count > 1
            {
                ModelTitleControl preMTC = modelTitleControls[modelTitleControls.Count - 2];
                mtControl.Location = new Point(preMTC.Location.X + preMTC.Width + 2, 6);
                this.Controls.Add(mtControl);
                ResizeModel();
                UpModelTitle();
                mtControl.ShowSelectedBorder();
            }
        }

        /*
         * removeTag  删除动作引起的ResizeModel
         */
        public void ResizeModel(bool removeTag = false)
        {

            rawModelTitleNum = this.Width / 142;
            try
            {
                if (0 < modelTitleControls.Count && modelTitleControls.Count <= rawModelTitleNum && removeTag)
                {
                    for (int i = 0; i < modelTitleControls.Count; i++)
                    {
                        modelTitleControls[i].Size = new Size(140, 26);
                        if (i == 0)
                            modelTitleControls[i].Location = OriginalLocation;
                        else
                        {
                            ModelTitleControl preMTC = modelTitleControls[i - 1];
                            modelTitleControls[i].Location = new Point(preMTC.Location.X + preMTC.Width + 2, 6);
                        }
                    }
                }
                if (modelTitleControls.Count > rawModelTitleNum)
                {
                    for (int i = 0; i < modelTitleControls.Count; i++)
                    {
                        ModelTitleControl mtc = modelTitleControls[i];
                        mtc.Width = (this.Size.Width - 1) / modelTitleControls.Count - 2;
                        int origWidth = mtc.Width;
                        if (i == 0)
                            mtc.Location = OriginalLocation;
                        else if (i >= modelTitleControls.Count - 3)
                        {
                            mtc.Width = (this.Size.Width - (origWidth + 2) * modelTitleControls.Count) / 3 + origWidth;
                            mtc.Location = new Point((origWidth + 2) * (modelTitleControls.Count - 3) + (mtc.Width + 2) * (i - modelTitleControls.Count + 3), 6);
                        }
                        else
                            mtc.Location = new Point((mtc.Width + 2) * i, 6);
                    }
                }
            }
            catch (Exception ex)
            { log.Error("ModelTitlePanel 未将对象引用设置到对象的实例: " + ex.ToString()); }

        }
        public void RemoveModel(ModelTitleControl mtControl)
        {
            // 关闭正是当前文档，需要重新选定左右两边的文档中的一个
            if (mtControl.Selected)
            {
                int index = modelTitleControls.IndexOf(mtControl);
                // 优先选择右边的
                if (index != -1 && index + 1 < modelTitleControls.Count)
                    modelTitleControls[index + 1].ShowSelectedBorder();
                // 其次选择左边的
                else if (index != -1 && index - 1 >= 0)
                    modelTitleControls[index - 1].ShowSelectedBorder();
                log.Info("删除的index为" + index.ToString());
            }
            modelTitleControls.Remove(mtControl);
            this.Controls.Remove(mtControl);
            mtControl.Dispose();
            // 当文档全部关闭时，自动创建一个新的默认文档
            if (modelTitleControls.Count == 0)
                AddModel("新建模型");
            UpModelTitle();
            ResizeModel(true);//重新设置model大小


        }
        public void ClearSelectedBorder()
        {
            foreach (ModelTitleControl mtc in this.modelTitleControls)
                mtc.BorderStyle = BorderStyle.None;
        }

        private void ModelTitlePanel_SizeChanged(object sender, EventArgs e)
        {
            ResizeModel(true);
            UpModelTitle();
        }
        public void DocumentSwitch(string modelTitle)
        {
            ModelDocumentSwitch?.Invoke(modelTitle);
        }

        public bool ContainModel(string modelTitle)
        {
            return SearchModelTitleControl(modelTitle).Count > 0;
        }

        public void ResetDirtyPictureBox(string modelTitle, bool dirty)
        {
            foreach (ModelTitleControl mtc in SearchModelTitleControl(modelTitle))
            {
                if (dirty)
                    mtc.SetDirtyPictureBox();
                else
                    mtc.ClearDirtyPictureBox();
            }

        }
        private List<ModelTitleControl> SearchModelTitleControl(string modelTitle)
        {
            return modelTitleControls.Where(x => x.ModelTitle == modelTitle).ToList();
        }
    }
}
