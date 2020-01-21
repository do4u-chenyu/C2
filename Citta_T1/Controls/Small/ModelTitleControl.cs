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
    public partial class ModelTitleControl : UserControl
    {
        private string modelTitle;
        public ModelTitleControl()
        {
            InitializeComponent();
            SetModelTitle("新建模型");
        }

        public void SetModelTitle(string modelTitle)
        {
            this.modelTitle = modelTitle;
            if (modelTitle.Length > 10)
                this.label1.Text = modelTitle.Substring(0, 10) + "...";
            else
                this.label1.Text = modelTitle;
        }
    }


}
