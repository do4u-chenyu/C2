using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Citta_T1.Controls.Left
{
    public partial class ModelButton : UserControl
    {
        public ModelButton()
        {
            InitializeComponent();
        }

        public void SetModelName(string modelName)
        {
            this.textButton.Text = modelName;
        }

        public string GetModelName()
        {
            return this.textButton.Text;
        }
    }


}
