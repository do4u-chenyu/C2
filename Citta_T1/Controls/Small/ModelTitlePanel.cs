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

            defaultModelTitleControl.Location = new System.Drawing.Point(1, 6);
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
                Point newLocation = models[models.Count - 2].Location;
                newLocation.X = newLocation.X + mtControl.Width + 2;
                newLocation.Y = 6;
                mtControl.Location = newLocation;
                this.Controls.Add(mtControl);
            }
        }

        public void RemoveModel(ModelTitleControl mtControl)
        {
            //TODO 需要在某个地方确定标题控件析构
            models.Remove(mtControl);
        }

    }
}
