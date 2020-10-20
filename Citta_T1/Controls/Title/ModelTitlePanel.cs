using C2.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace C2.Controls.Title
{
    public delegate void NewDocumentEventHandler(string modelTitle);
    public delegate void DocumentSwitchHandler(string modelTitle);
    public partial class ModelTitlePanel : UserControl
    {
        private static LogUtil log = LogUtil.GetInstance("ModelTitlePanel");

        private static Point OriginalPoint = new System.Drawing.Point(2, 6);            //第一个模型标题的位置
        private List<ModelTitleControl> modelTitleControls;
        private int threshold = 9;                                                      //模型标题长度变化阈值
        public event NewDocumentEventHandler NewModelDocument;
        public event DocumentSwitchHandler ModelDocumentSwitch;

        public ModelTitlePanel()
        {
            modelTitleControls = new List<ModelTitleControl>();
            InitializeComponent();
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

                // RightModelTitleButton
                RightModelTitleControl rmtControl = new RightModelTitleControl();
                this.Controls.Add(rmtControl);

                // 根据元素个数调整位置和大小
                mtControl.SetOriginalModelTitle(modelTitles[i]);
                if (i == 0)
                    mtControl.Location = OriginalPoint;
                else
                {
                    ModelTitleControl preMTC = modelTitleControls[modelTitleControls.Count - 2];
                    mtControl.Location = new Point(preMTC.Location.X + preMTC.Width + 2, 6);
                    ResizeModel();
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
                mtControl.ShowSelectedBorder();
            }
        }

        /*
         * removeTag  删除动作引起的ResizeModel
         */
        public void ResizeModel()
        {
            // 增加、删除ModelTitle(数目 > rawModelTitleNum),其大小改变
            if (modelTitleControls == null)
                return;
            threshold = this.Width / 142;
            try
            {       
                int count = modelTitleControls.Count;
                if (count <= threshold)
                {

                    for (int i = 0; i < count; i++)
                    {
                        modelTitleControls[i].Size = new Size(140, 26);
                        modelTitleControls[i].Location = i == 0 ? OriginalPoint : new Point(modelTitleControls[i - 1].Location.X + modelTitleControls[i - 1].Width + 2, 6);
                        ChangeTitleLength(modelTitleControls[i]);
                    }
                    return;
                }

                // 标题控件宽度缩小后的设定值,this.Size.Width - 2让第一个标题和左侧有个空隙，-2是每个title留的间距
                int rawWidth = (this.Size.Width - 2) / count;
                int shrinkWidth = rawWidth - 2;
               
                for (int i = 0; i < count; i++)
                {
                    ModelTitleControl mtc = modelTitleControls[i];
                    mtc.Width = shrinkWidth;
                    mtc.Location = new Point(rawWidth * i, 6);
                    // 第一个标题和左侧有长度为1的间隙，所以后续标题位置整体右移2
                    mtc.Location = new Point(mtc.Location.X + 2, mtc.Location.Y);
                    ChangeTitleLength(mtc);
                }
            }
            catch (Exception ex)
            { log.Error("ModelTitlePanel : " + ex.ToString()); }

        }


        private void ChangeTitleLength(ModelTitleControl mtc)
        {
            // 改变Title显示的标题字数
            int width = mtc.Width;
            string title = mtc.ModelTitle;
            if (width == 140)
                mtc.SetOriginalModelTitle(title);
            else if (91 <= width && width < 140)
                mtc.SetNewModelTitle(title, 3);
            else if (77 <= width && width < 91)
                mtc.SetNewModelTitle(title, 2);
            else if (64 <= width && width < 77)
                mtc.SetNewModelTitle(title, 1);
            else if (44 <= width && width < 64)
                mtc.SetNewModelTitle(title, 0);
            else
                mtc.SetNewModelTitle(title, -1);
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
                //log.Info("删除的文档index为" + index.ToString());
            }
            modelTitleControls.Remove(mtControl);
            this.Controls.Remove(mtControl);
            mtControl.Dispose();
            // 当文档全部关闭时，自动创建一个新的默认文档
            if (modelTitleControls.Count == 0)
                AddModel("新建模型");
            ResizeModel();//重新设置model大小


        }
        public void ClearSelectedBorder()
        {
            foreach (ModelTitleControl mtc in this.modelTitleControls)
                mtc.BorderStyle = BorderStyle.None;
        }

        private void ModelTitlePanel_SizeChanged(object sender, EventArgs e)
        {
            ResizeModel();
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
