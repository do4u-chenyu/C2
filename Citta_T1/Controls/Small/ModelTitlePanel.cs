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
        }

        public void AddModel(string modelTitle)
        {
            ModelTitleControl mtControl = new ModelTitleControl();
            mtControl.SetModelTitle(modelTitle);
            models.Add(mtControl);
        }

        public void RemoveModel(ModelTitleControl mtControl)
        {
            models.Remove(mtControl);
        }

    }
}
