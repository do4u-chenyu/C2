using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Citta_T1.Business;

namespace Citta_T1.Controls.Left
{
    public partial class DataSourceControl : UserControl
    {
        // 从`FormInputData.cs`导入模块收到的数据，以索引的形式存储
        //private Dictionary<string, Button> dataSourceDictI2B = new Dictionary<string, Button>();
        public Dictionary<string, DataButton> dataSourceDictI2B = new Dictionary<string, DataButton>();
        private System.Windows.Forms.Button tempButton = new System.Windows.Forms.Button();
        public DataSourceControl()
        {
            InitializeComponent();
        }

        public void LeftPaneOp_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // 使用`DataObject`对象来传参数，更加自由
                DataObject dragDropData = new DataObject();
                dragDropData.SetData("Type", ElementType.DataSource);
                dragDropData.SetData("Path", (sender as Button).Name);
                dragDropData.SetData("Text", (sender as Button).Text);
                // 需要记录他的编码格式
                dragDropData.SetData("isUTF8", ((sender as Button).Parent as DataButton).isUTF8);
                (sender as Button).DoDragDrop(dragDropData, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }
        public void GenDataButton(string dataName, string filePath, bool isutf8)
        {
            // 根据导入数据动态生成一个button
            DataButton b = new DataButton(filePath, dataName, isutf8);
            b.Location = new System.Drawing.Point(30, 50 * (this.dataSourceDictI2B.Count() + 1)); // 递增
            b.txtButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LeftPaneOp_MouseDown);
            this.dataSourceDictI2B.Add(filePath, b);
            this.LocalFrame.Controls.Add(b);
        }

        public void RenameDataButton(string index, string dstName)
        {
            // 根据index重命名button
            this.dataSourceDictI2B[index].txtButton.Text = dstName;
        }

        private void LocalFrame_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ExternalData_Click(object sender, EventArgs e)
        {
            this.ExternalData.Font= new Font("微软雅黑", 12,FontStyle.Bold );
            this.LocalData.Font = new Font("微软雅黑", 12, FontStyle.Regular);
            this.ExternalFrame.Visible = true;
            this.LocalFrame.Visible = false;
        }

         private void LocalData_Click(object sender, EventArgs e)
         {
             this.LocalData.Font = new Font("微软雅黑", 12, FontStyle.Bold);
             this.ExternalData.Font = new Font("微软雅黑", 12, FontStyle.Regular);
             this.LocalFrame.Visible = true;
             this.ExternalFrame.Visible = false;
         }
        public void AddDataSource(string modelName)
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
