using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Citta_T1.Controls
{
    public partial class MyModelControl : UserControl
    {
        public MyModelControl()
        {
            InitializeComponent();
        }

        public void AddModel(string modelName)
        {
            ModelButton mb = new ModelButton();
            mb.SetModelName(modelName);
            // 获得当前要添加的model button的初始位置
            Point startPoint = new Point(15, -12);
            if (this.Controls.Count > 0)
                startPoint = this.Controls[this.Controls.Count - 1].Location;

            startPoint.Y += mb.Height + 12;
            mb.Location = startPoint;

            this.Controls.Add(mb);

        }
    }
}
