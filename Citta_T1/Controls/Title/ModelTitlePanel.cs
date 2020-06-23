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

        private static Point OriginalPoint = new System.Drawing.Point(1, 6);            //第一个模型标题的位置
        private List<ModelTitleControl> modelTitleControls;
        private int threshold = 9;                                                      //模型标题长度变化阈值
        public event NewDocumentEventHandler NewModelDocument;
        public event DocumentSwitchHandler ModelDocumentSwitch;

        public ModelTitlePanel()
        {
            modelTitleControls = new List<ModelTitleControl>();
            InitializeComponent();
        }


        public void UpModelTitle()
        {
            threshold = this.Width / 142;
            foreach (ModelTitleControl mt in modelTitleControls)
            {
                if (modelTitleControls.Count <= threshold)
                    mt.SetOriginalModelTitle(mt.ModelTitle);
                else if (modelTitleControls.Count > threshold && modelTitleControls.Count < 17)
                    mt.SetNewModelTitle(mt.ModelTitle, 3);
                else if (modelTitleControls.Count >= 17 && modelTitleControls.Count < 20)
                    mt.SetNewModelTitle(mt.ModelTitle, 2);
                else if (modelTitleControls.Count >= 20 && modelTitleControls.Count < 24)
                    mt.SetNewModelTitle(mt.ModelTitle, 1);
                else if (modelTitleControls.Count >= 24)
                    mt.SetNewModelTitle(mt.ModelTitle, 0);
              
            }
           if(modelTitleControls.Count > 0)
                log.Info("modetitle :标题长度" + modelTitleControls[0].Width);
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
                    mtControl.Location = OriginalPoint;
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
                mtControl.Location = OriginalPoint;
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
            if (modelTitleControls == null)
                return;
            threshold = this.Width / 142;
            try
            {       
                int count = modelTitleControls.Count;
                // 删除ModelTitle（数目 <= rawModelTitleNum）,其大小改变
                if (count <= threshold && removeTag)
                {

                    for (int i = 0; i < count; i++)
                    {
                        modelTitleControls[i].Size = new Size(140, 26);
                        modelTitleControls[i].Location = i == 0 ? OriginalPoint : new Point(modelTitleControls[i - 1].Location.X + modelTitleControls[i - 1].Width + 2, 6);                    
                    }
                }

                // 增加、删除ModelTitle(数目 > rawModelTitleNum),其大小改变
                if (modelTitleControls.Count <= threshold)
                    return;
                // 标题缩小后的宽度
                int shrinkWidth= (this.Size.Width - 1) / count - 2;
                // 第一个标题的位置、宽度
                modelTitleControls[0].Location = OriginalPoint;
                modelTitleControls[0].Width = shrinkWidth;
                for (int i = 0; i < count; i++)
                {
                    ModelTitleControl mtc = modelTitleControls[i];
                    // 最后三个补一下余数造成的ModelTitlePanel最后的空余
                    mtc.Width = i >= count - 3 ? (this.Size.Width - (shrinkWidth + 2) * count) / 3 + shrinkWidth : shrinkWidth;
                    mtc.Location= i >= count - 3 ? new Point((shrinkWidth + 2) * (count - 3) + (mtc.Width + 2) * (i - count + 3), 6): new Point((mtc.Width + 2) * i, 6);
                }
            }
            catch (Exception ex)
            { log.Error("ModelTitlePanel : " + ex.ToString()); }

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
