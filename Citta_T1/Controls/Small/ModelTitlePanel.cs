using System;
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
        private bool removeit=false;
        public ModelTitlePanel()
        {
            InitializeComponent();
            models = new List<ModelTitleControl>();
            InitializeDefaultModelTitleControl();


        }

        private void InitializeDefaultModelTitleControl()
        {
            ModelTitleControl defaultModelTitleControl = new ModelTitleControl();
            defaultModelTitleControl.Location = OriginalLocation;
            defaultModelTitleControl.BorderStyle = BorderStyle.FixedSingle;
           
            this.Controls.Add(defaultModelTitleControl);
            models.Add(defaultModelTitleControl);
        }
        public void updatetext()
        {
            for (int h = 0; h < models.Count; h++)
            {
                if (models.Count < 12 && removeit)
                {
                    models[h].SetModelTitle(models[h].storeModelName());
                }
                else if (models.Count >= 12 && models.Count < 17)
                {

                    models[h].SetModelTitle3(models[h].storeModelName());

                }
                else if (models.Count >= 17 && models.Count < 20)
                {
                    models[h].SetModelTitle2(models[h].storeModelName());
                }
                else if (models.Count >= 20 && models.Count < 24)
                {
                    models[h].SetModelTitle1(models[h].storeModelName());
                }
                else if (models.Count >= 24)
                {
                    models[h].SetModelTitle0();
                }
            }
        }

        public void AddModel(string modelTitle)
        {
           
            ModelTitleControl mtControl = new ModelTitleControl();
            models.Add(mtControl);
            mtControl.SetModelTitle(modelTitle);
            updatetext();

            // 根据容器中最后一个ModelTitleControl的Location
            // 设置新控件在ModelTitlePanel中的Location
            if (models.Count == 1)
                mtControl.Location = new System.Drawing.Point(1, 6);

            else if (models.Count > 1)
            {



                ModelTitleControl preMTC = models[models.Count - 2];
                Point newLocation = new Point();
                newLocation.X = preMTC.Location.X + preMTC.Width + 2;
                newLocation.Y = 6;
                mtControl.Location = newLocation;
                this.Controls.Add(mtControl);
                ResizeModel(mtControl);
            }
            mtControl.ShowSelectedBorder();
            removeit = false;
        }
       public void ResizeModel(ModelTitleControl mtControl)
        {
            int distance1 = 0;
            int num = 0;
            int num2 = 0;
            
            if (models.Count > 1)
            {
                if (models.Count < 12 && removeit)
                {
                    if (models.Count > 0)
                        models[0].Location = OriginalLocation;
                        models[0].Size = new Size(140, 26);
                    for (int j = 1; j < models.Count; j++)
                    {
                        models[j].Size = new Size(140, 26);
                        ModelTitleControl preMTC = models[j - 1];
                        Point newLocation = new Point();
                        newLocation.X = preMTC.Location.X + preMTC.Width + 2;
                        newLocation.Y = 6;
                        models[j].Location = newLocation;
                    }
                }
                if (models.Count >= 12)
                {
                    foreach (ModelTitleControl obj in models)
                    {
                        if (distance1 == 0)
                        {
                            distance1 = obj.Size.Width;
                        }
                        obj.Width = this.Size.Width / models.Count - 2;
                        int orig_width = obj.Width;
                        if (num >= models.Count - 3)
                        {
                            obj.Width = (this.Size.Width - (orig_width + 2) * (models.Count)) / 3 + orig_width;
                            obj.Location = new Point((orig_width + 2) * (models.Count - 3) + (obj.Width + 2) * num2,6);
                            num2 += 1;
                        }
                        else
                        {
                            if (num == 0)
                            {
                                obj.Width = this.Size.Width / models.Count - 3;
                                obj.Location = new Point(1, 6);
                            }
                            else
                                obj.Location = new Point((obj.Width + 2) * num, 6);
                        }
                        num += 1;

                    }

                }
            }
            removeit = false;
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
            //else
            // ResetModelLocation();// 重新排版
            removeit = true;
            updatetext();
            ResizeModel(mtControl);//重新设置model大小
           
        }

        private void ResetModelLocation()
        {
            if (models.Count > 0)
                models[0].Location = OriginalLocation;
            for (int i = 1; i < models.Count; i++)
            {
                ModelTitleControl preMTC = models[i - 1];
                Point newLocation = new Point();
                newLocation.X = preMTC.Location.X + preMTC.Width + 2;
                newLocation.Y = 6;
                models[i].Location  = newLocation;
            }
        }

        public void ClearSelectedBorder()
        {
            foreach (ModelTitleControl mtc in this.models)
                mtc.BorderStyle = BorderStyle.None;

        }


    }
}
