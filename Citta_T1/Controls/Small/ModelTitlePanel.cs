﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Citta_T1.Controls.Small
{
    public partial class ModelTitlePanel : UserControl
    {
        private static Point OriginalLocation = new System.Drawing.Point(1, 6);
        private List<ModelTitleControl> models;
        private int rawModelTitleNum = 9;
        public ModelTitlePanel()
        {
            InitializeComponent();
            models = new List<ModelTitleControl>();
            InitializeDefaultModelTitleControl(); //new 用户,老用户没有模型;老用户有模型,       
        }

        private void InitializeDefaultModelTitleControl()
        {
            AddModel("新建模型");
        }
        public void UpModelTitle()
        {

            foreach (ModelTitleControl mt in models)
            {
                if (models.Count < rawModelTitleNum)
                    mt.SetModelTitle(mt.ModelTitle);
                else if (models.Count >= rawModelTitleNum && models.Count < 17)
                    mt.SetModelTitle(mt.ModelTitle, 3);
                else if (models.Count >= 17 && models.Count < 20)
                    mt.SetModelTitle(mt.ModelTitle, 2);
                else if (models.Count >= 20 && models.Count < 24)
                    mt.SetModelTitle(mt.ModelTitle, 1);
                else if (models.Count >= 24)
                    mt.SetEmptyModelTitle();
            }
        }

        public void AddModel(string modelTitle)
        {
            //TODO
            ModelTitleControl mtControl = new ModelTitleControl();
            models.Add(mtControl);
            mtControl.SetModelTitle(modelTitle);
            UpModelTitle();
            // 根据容器中最后一个ModelTitleControl的Location
            // 设置新控件在ModelTitlePanel中的Location
            if (models.Count <= 1)
            {
                mtControl.Location = OriginalLocation;
                mtControl.BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(mtControl);
            }
            else // models.Count > 1
            {
                ModelTitleControl preMTC = models[models.Count - 2];
                mtControl.Location = new Point(preMTC.Location.X + preMTC.Width + 2, 6);
                this.Controls.Add(mtControl);
                ResizeModel();
                mtControl.ShowSelectedBorder();
            }
        }

        /*
         * removeTag  删除动作引起的ResizeModel
         */
        public void ResizeModel(bool removeTag = false)
        {

            if (models.Count < rawModelTitleNum && removeTag)
            {
                if (models.Count > 0)
                {
                    models[0].Location = OriginalLocation;
                    models[0].Size = new Size(140, 26);
                }

                for (int i = 1; i < models.Count; i++)
                {
                    models[i].Size = new Size(140, 26);
                    ModelTitleControl preMTC = models[i - 1];
                    models[i].Location = new Point(preMTC.Location.X + preMTC.Width + 2, 6);
                }
            }
            if (models.Count >= rawModelTitleNum)
            {
                int num2 = 0;
                for (int i = 0; i < models.Count; i++)
                {
                    ModelTitleControl mtc = models[i];
                    mtc.Width = this.Size.Width / models.Count - 2;
                    int origWidth = mtc.Width;
                    if (i == 0)
                    {
                        mtc.Width = this.Size.Width / models.Count - 3;
                        mtc.Location = OriginalLocation;
                    }
                    else if (i >= models.Count - 3)
                    {
                        mtc.Width = (this.Size.Width - (origWidth + 2) * (models.Count)) / 3 + origWidth;
                        mtc.Location = new Point((origWidth + 2) * (models.Count - 3) + (mtc.Width + 2) * num2, 6);
                        num2 += 1; // num2 == index - models.Count -3 - ?
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
            }
            models.Remove(mtControl);
            this.Controls.Remove(mtControl);
            mtControl.Dispose();
            // 当文档全部关闭时，自动创建一个新的默认文档
            if (models.Count == 0)
                InitializeDefaultModelTitleControl();
            UpModelTitle();
            ResizeModel(true);//重新设置model大小

        }
        public void ClearSelectedBorder()
        {
            foreach (ModelTitleControl mtc in this.models)
                mtc.BorderStyle = BorderStyle.None;
        }
    }
}
