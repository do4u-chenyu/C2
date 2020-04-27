using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Citta_T1.Business.DataSource;
using Citta_T1.Business.Model;
using Citta_T1.Utils;

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

        private static int ButtonGapHeight = 50;
        private static int ButtonLeftX = 30;
        private static int ButtonBottomOffsetY = 40;

        public void LeftPaneOp_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // 使用`DataObject`对象来传参数，更加自由
                DataObject dragDropData = new DataObject();
                dragDropData.SetData("Type", ElementType.DataSource);
                dragDropData.SetData("Path", (sender as Button).Name);
                dragDropData.SetData("Text", (sender as Button).Text);
                dragDropData.SetData("Separator", ((sender as Button).Parent as DataButton).Separator);
                dragDropData.SetData("ExtType", ((sender as Button).Parent as DataButton).ExtType);
                // 需要记录他的编码格式
                dragDropData.SetData("Encoding", ((sender as Button).Parent as DataButton).Encoding);
                (sender as Button).DoDragDrop(dragDropData, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }

        // 手工导入时调用
        public void GenDataButton(string dataName, string fullFilePath, char separator, DSUtil.ExtType extType, DSUtil.Encoding encoding)
        {
            // 根据导入数据动态生成一个button
            DataButton dataButton = new DataButton(fullFilePath, dataName, separator, extType, encoding);
            dataButton.Location = new Point(ButtonLeftX, ButtonGapHeight * (this.dataSourceDictI2B.Count() + 1) - ButtonBottomOffsetY); // 递增
            dataButton.txtButton.MouseDown += new MouseEventHandler(this.LeftPaneOp_MouseDown);
            // 判断是否有路径文件
            if (this.dataSourceDictI2B.ContainsKey(fullFilePath))
            {
                String name = this.dataSourceDictI2B[fullFilePath].txtButton.Text;
                MessageBox.Show("该文件已存在，数据名为：" + name);
                return;
            }
            this.dataSourceDictI2B.Add(fullFilePath, dataButton);
            this.localFrame.Controls.Add(dataButton);
            //数据源持久化存储
            DataSourceInfo dataSource = new DataSourceInfo(Global.GetMainForm().UserName);
            dataSource.WriteDataSourceInfo(dataButton);
        }

        // 程序启动加载时调用
        public void GenDataButton(DataButton dataButton)
        {
            // 供load时调用

            dataButton.Location = new System.Drawing.Point(ButtonLeftX, ButtonGapHeight * (this.dataSourceDictI2B.Count() + 1) - ButtonBottomOffsetY); // 递增
            dataButton.txtButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LeftPaneOp_MouseDown);
            this.dataSourceDictI2B.Add(dataButton.FullFilePath, dataButton);
            this.localFrame.Controls.Add(dataButton);
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
            this.externalDataLabel.Font = new Font("微软雅黑", 12,FontStyle.Bold );
            this.localDataLabel.Font = new Font("微软雅黑", 12, FontStyle.Regular);
            this.externalFrame.Visible = true;
            this.localFrame.Visible = false;
        }

         private void LocalData_Click(object sender, EventArgs e)
         {
             this.localDataLabel.Font = new Font("微软雅黑", 12, FontStyle.Bold);
             this.externalDataLabel.Font = new Font("微软雅黑", 12, FontStyle.Regular);
             this.localFrame.Visible = true;
             this.externalFrame.Visible = false; ;
         }

        private void ReLayoutLocalFrame()
        {
            // 先暂停布局,然后调整button位置,最后恢复布局,可以避免闪烁
            this.SuspendLayout();
            for (int i = 0; i < this.localFrame.Controls.Count; i++)
            {
                Control ct = this.localFrame.Controls[i];
                ct.Location = new Point(ButtonLeftX, ButtonGapHeight * (i + 1) - ButtonBottomOffsetY);
            }
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        public void RemoveDataButton(DataButton dataButton)
        {
            this.dataSourceDictI2B.Remove(dataButton.FullFilePath);
            this.localFrame.Controls.Remove(dataButton);
            // 重新布局
            ReLayoutLocalFrame();
            // 保存
            DataSourceInfo dataSource = new DataSourceInfo(Global.GetMainForm().UserName);
            dataSource.SaveDataSourceInfo(dataSourceDictI2B.Values.ToArray());
        }
    }
}
