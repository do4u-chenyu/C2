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
    public partial class DataSourceControl : UserControl
    {
        // 从输入导入模块收到的数据
        private List<Citta_T1.Data> contents = new List<Citta_T1.Data>();
        private System.Windows.Forms.Button tempButton = new System.Windows.Forms.Button();
        public DataSourceControl()
        {
            InitializeComponent();
            
        }

        private void LeftPaneOp_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                tempButton.DoDragDrop((sender as Button).Text, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }
        public void AddData(Citta_T1.Data data)
        {
            // 根据导入数据动态生成一个panel
            this.contents.Add(data);
            System.Windows.Forms.Button b = new System.Windows.Forms.Button();
            b.Location = new System.Drawing.Point(46, 50 * this.contents.Count()); // 递增
            b.Name = "button1";
            b.Size = new System.Drawing.Size(100, 40); // 固定的
            b.TabIndex = 0;
            b.Text = data.name;
            b.UseVisualStyleBackColor = true;
            b.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LeftPaneOp_MouseDown);
            this.LocalFrame.Controls.Add(b);
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
    }
}
