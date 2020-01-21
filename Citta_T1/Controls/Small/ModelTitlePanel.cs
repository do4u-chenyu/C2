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
           // defaultModelTitleControl.t
            this.Controls.Add(defaultModelTitleControl);
            models.Add(defaultModelTitleControl);
        }

        public void AddModel(string modelTitle)
        {
            ModelTitleControl mtControl = new ModelTitleControl();
            mtControl.SetModelTitle(modelTitle);
            models.Add(mtControl);
            // 根据容器中最后一个ModelTitleControl的Location
            // 设置新控件在ModelTitlePanel中的Location
            if (models.Count == 1)
                mtControl.Location = new System.Drawing.Point(1, 9);
            else if(models.Count > 1)
            {
                ModelTitleControl preMTC = models[models.Count - 2];
                Point newLocation = new Point();
                newLocation.X = preMTC.Location.X + preMTC.Width + 2;
                newLocation.Y = 6;
                mtControl.Location = newLocation;
                this.Controls.Add(mtControl);
            }
        }

        public void RemoveModel(ModelTitleControl mtControl)
        {

            models.Remove(mtControl);
            this.Controls.Remove(mtControl);
            mtControl.Dispose();
            // 当文档全部关闭时，自动创建一个新的默认文档
            if (models.Count == 0)
                InitializeDefaultModelTitleControl();
            // 重新排版
            ResetModelLocation();
        }

        private void ResetModelLocation()
        {
            if (models.Count == 1)
            {
                models[0].Location = OriginalLocation;
            }
            for (int i = 1; i < models.Count; i++)
            {
                ModelTitleControl preMTC = models[i - 1];
                Point newLocation = new Point();
                newLocation.X = preMTC.Location.X + preMTC.Width + 2;
                newLocation.Y = 6;
                models[i].Location  = newLocation;
            }
        }

    }
}
