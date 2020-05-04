using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Citta_T1.Utils;

namespace Citta_T1.Controls.Left
{
    public partial class MyModelControl : UserControl
    {
        public MyModelControl()
        {
            InitializeComponent(); 
        }

        private static int ButtonLeftX = 15;
        private static int ButtonBottomOffsetY = 12;
        public void AddModel(string modelName)
        {
            ModelButton mb = new ModelButton(modelName);
            // 获得当前要添加的model button的初始位置
            LayoutModelButtonLocation(mb);

            this.Controls.Add(mb);
        }

        private void LayoutModelButtonLocation(Control ct)
        {
            Point startPoint = new Point(ButtonLeftX, -ButtonBottomOffsetY);
            if (this.Controls.Count > 0)
                startPoint = this.Controls[this.Controls.Count - 1].Location;

            startPoint.Y += ct.Height + ButtonBottomOffsetY;
            ct.Location = startPoint;
        }

        public bool ContainModel(string modelTitle)
        {
            foreach (Control ct in this.Controls)
            {
                if (ct is ModelButton)
                    if ((ct as ModelButton).ModelTitle== modelTitle)
                        return true;
            }
            return false;
        }
        // 文档关闭后, 菜单栏可以打开,删除,重命名
        public void EnableClosedDocumentMenu(string modelTitle)
        {
            foreach (ModelButton mb in this.Controls)
                if (mb.ModelTitle == modelTitle)
                {
                    mb.EnableOpenDocumentMenu();
                    mb.EnableDeleteDocumentMenu();
                    mb.EnableRenameDocumentMenu();
                }

        }


        public void RemoveModelButton(ModelButton modelButton)
        {
            this.Controls.Remove(modelButton);
            // 重新布局
            ReLayoutLocalFrame();
        }

        private void ReLayoutLocalFrame()
        {
            // 先暂停布局,然后调整button位置,最后恢复布局,可以避免闪烁
            this.SuspendLayout();
            // 清空位置
            List<Control> tmp = new List<Control>();
            foreach (Control ct in Controls)
                tmp.Add(ct);

            Controls.Clear();
            // 重新排序
            foreach (Control ct in tmp)
            {
                LayoutModelButtonLocation(ct);
                Controls.Add(ct);
            }
               

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
