using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Controls.C1.Left
{
    public partial class LeftFoldButton : UserControl
    {
        private static readonly Color DefaultColor = Color.FromArgb(195, 195, 195); 
        public LeftFoldButton()
        {
            InitializeComponent();
            CustomButtonShape();
        }

        // 圆角代码
        public void CustomButtonShape()
        {
            System.Drawing.Drawing2D.GraphicsPath oPath = new System.Drawing.Drawing2D.GraphicsPath();
            oPath.AddLine(0, 0, 7, 18);         // 上顶点
            oPath.AddLine(7, 18, 7, 82);        // 下顶点
            oPath.AddLine(7, 82, 0, 100);       
            oPath.CloseAllFigures();
            Region = new Region(oPath);
        }
    }
}
