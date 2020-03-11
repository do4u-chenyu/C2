using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Citta_T1.Controls.Title
{
    public delegate void NewDocumentEventHandler(string modelTitle);
    public delegate void DocumentSwitchHandler(string modelTitle);
    public partial class ModelTitlePanel : UserControl
    {
        private static Point OriginalLocation = new System.Drawing.Point(1, 6);
        private List<ModelTitleControl> models;
        private int rawModelTitleNum = 9;
        public event NewDocumentEventHandler NewModelDocument;
        public event DocumentSwitchHandler ModelDocumentSwitch;
 
        public ModelTitlePanel()
        {
            InitializeComponent();
            models = new List<ModelTitleControl>();
        }


        public void UpModelTitle()
        {
            rawModelTitleNum = this.Width / 142;
            foreach (ModelTitleControl mt in models)
            {
                if (models.Count <= rawModelTitleNum)
                    mt.SetOriginalModelTitle(mt.ModelTitle);
                else if (models.Count > rawModelTitleNum && models.Count < 17)
                    mt.SetNewModelTitle(mt.ModelTitle, 3);
                else if (models.Count >= 17 && models.Count < 20)
                    mt.SetNewModelTitle(mt.ModelTitle, 2);
                else if (models.Count >= 20 && models.Count < 24)
                    mt.SetNewModelTitle(mt.ModelTitle, 1);
                else if (models.Count >= 24)
                    mt.SetNewModelTitle(mt.ModelTitle, 0);
            }
        }
        public void LoadModelDocument(DirectoryInfo[] directoryInfos) 
        {
            int docCounts = directoryInfos.Count()-1;
            for (int i = 0; i <= docCounts; i++)
            {
                string modelTitle = directoryInfos[i].ToString();
                ModelTitleControl mtControl = new ModelTitleControl();
                mtControl.ModelDocumentSwitch += DocumentSwitch;
                models.Add(mtControl);
                mtControl.SetOriginalModelTitle(modelTitle);
                if (i == 0)
                    mtControl.Location = OriginalLocation;
                else
                {
                    ModelTitleControl preMTC = models[models.Count - 2];
                    mtControl.Location = new Point(preMTC.Location.X + preMTC.Width + 2, 6);
                    ResizeModel();
                    UpModelTitle();
                }
                if (i == docCounts)
                { 
                    mtControl.BorderStyle = BorderStyle.FixedSingle;
                    mtControl.Selected = true;
                }
                    
                this.Controls.Add(mtControl);
            }
           
        }

        public void AddModel(string modelTitle)
        {
            //TODO
            ModelTitleControl mtControl = new ModelTitleControl();
            mtControl.ModelDocumentSwitch += DocumentSwitch;
            models.Add(mtControl);
            mtControl.SetOriginalModelTitle(modelTitle);

            //TODO 创建事件

            NewModelDocument?.Invoke(modelTitle);



            // 根据容器中最后一个ModelTitleControl的Location
            // 设置新控件在ModelTitlePanel中的Location
            if (models.Count <= 1)
            {
                mtControl.Location = OriginalLocation;
                this.Controls.Add(mtControl);
                mtControl.ShowSelectedBorder();
            }
            else // models.Count > 1
            {
                ModelTitleControl preMTC = models[models.Count - 2];
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
            if (0 < models.Count && models.Count <= rawModelTitleNum && removeTag)
            {
                for (int i = 0; i < models.Count; i++)
                {
                    models[i].Size = new Size(140, 26);
                    if (i == 0)
                        models[i].Location = OriginalLocation;
                    else
                    {
                        ModelTitleControl preMTC = models[i - 1];
                        models[i].Location = new Point(preMTC.Location.X + preMTC.Width + 2, 6);
                    }
                }
            }
            if (models.Count > rawModelTitleNum)
            {
                for (int i = 0; i < models.Count; i++)
                {
                    ModelTitleControl mtc = models[i];
                    mtc.Width = (this.Size.Width - 1) / models.Count - 2;
                    int origWidth = mtc.Width;
                    if (i == 0)
                        mtc.Location = OriginalLocation;
                    else if (i >= models.Count - 3)
                    {
                        mtc.Width = (this.Size.Width - (origWidth + 2) * models.Count) / 3 + origWidth;
                        mtc.Location = new Point((origWidth + 2) * (models.Count - 3) + (mtc.Width + 2) * (i - models.Count + 3), 6);
                    }
                    else
                        mtc.Location = new Point((mtc.Width + 2) * i, 6);
                }
            }
        }
        public void RemoveModel(ModelTitleControl mtControl)
        {
            // 关闭正是当前文档，需要重新选定左右两边的文档中的一个
            if (mtControl.Selected)
            {
                int index = models.IndexOf(mtControl);
                // 优先选择右边的
                if (index != -1 && index + 1 < models.Count)
                    models[index + 1].ShowSelectedBorder();
                // 其次选择左边的
                else if (index != -1 && index - 1 >= 0)
                    models[index - 1].ShowSelectedBorder();
                Console.WriteLine("删除的index为" + index.ToString());
            }
            models.Remove(mtControl);
            this.Controls.Remove(mtControl);
            mtControl.Dispose();
            // 当文档全部关闭时，自动创建一个新的默认文档
            if (models.Count == 0)
                AddModel("新建模型");
            UpModelTitle();
            ResizeModel(true);//重新设置model大小
           

        }
        public void ClearSelectedBorder()
        {
            foreach (ModelTitleControl mtc in this.models)
                mtc.BorderStyle = BorderStyle.None;
        }
        public void SelectedModel(string modelTitle)
        {
            foreach (ModelTitleControl mtc in this.models)
            {
                if(mtc.ModelTitle == modelTitle)
                 mtc.ShowSelectedBorder();
            }
               
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
    }
}
