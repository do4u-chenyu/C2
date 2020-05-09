using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Citta_T1.Business.DataSource;
using Citta_T1.Utils;

namespace Citta_T1.Controls.Left
{
    public partial class DataSourceControl : UserControl
    {
        // 从`FormInputData.cs`导入模块收到的数据，以索引的形式存储
       
        public DataSourceControl()
        {
            dataSourceDictI2B = new Dictionary<string, DataButton>();
            InitializeComponent(); 
        }

        private static int ButtonGapHeight = 50;
        private static int ButtonLeftX = 17;
        private static int ButtonBottomOffsetY = 40;

        private Dictionary<string, DataButton> dataSourceDictI2B;

        // 这个控件属性不需要在属性面板显示和序列化,不加这个标签,在引用这个控件的Designer中,会序列化它
        // 然后就是各种奇葩问题
        // DesignerSerializationVisibility.Hidden  设计器不为这个属性生成代码
        // Browsable(false) 这个属性不出现在设计器的属性窗口中
        // MergableProperty(false) 不知道是干嘛的, 看网上帖子就加进去了
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), MergableProperty(false)]
        public Dictionary<string, DataButton> DataSourceDictI2B { get => dataSourceDictI2B; }

        // 手工导入时调用
        public void GenDataButton(string dataName, string fullFilePath, char separator, DSUtil.ExtType extType, DSUtil.Encoding encoding)
        {
            // 根据导入数据动态生成一个button
            DataButton dataButton = new DataButton(fullFilePath, dataName, separator, extType, encoding);
            dataButton.Location = new Point(ButtonLeftX, ButtonGapHeight * (this.DataSourceDictI2B.Count() + 1) - ButtonBottomOffsetY); // 递增

            // 判断是否有路径文件
            if (this.DataSourceDictI2B.ContainsKey(fullFilePath))
            {
                String name = this.DataSourceDictI2B[fullFilePath].DataSourceName;
                MessageBox.Show("该文件已存在，数据名为：" + name);
                return;
            }
            this.DataSourceDictI2B.Add(fullFilePath, dataButton);
            this.localFrame.Controls.Add(dataButton);
            //数据源持久化存储
            DataSourceInfo dataSource = new DataSourceInfo(Global.GetMainForm().UserName);
            dataSource.WriteDataSourceInfo(dataButton);
        }

        // 程序启动加载时调用
        public void GenDataButton(DataButton dataButton)
        {
            // 供load时调用

            dataButton.Location = new System.Drawing.Point(ButtonLeftX, ButtonGapHeight * (this.DataSourceDictI2B.Count() + 1) - ButtonBottomOffsetY); // 递增
            this.DataSourceDictI2B.Add(dataButton.FullFilePath, dataButton);
            this.localFrame.Controls.Add(dataButton);
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
            this.DataSourceDictI2B.Remove(dataButton.FullFilePath);
            this.localFrame.Controls.Remove(dataButton);
            // 重新布局
            ReLayoutLocalFrame();
            // 保存
            SaveDataSourceInfo();
        }
        public void SaveDataSourceInfo()
        {
            DataSourceInfo dataSource = new DataSourceInfo(Global.GetMainForm().UserName);
            dataSource.SaveDataSourceInfo(DataSourceDictI2B.Values.ToArray());
        }

        private void DataSourceControl_MouseDown(object sender, MouseEventArgs e)
        {
            // 强制编辑控件失去焦点,触发重命名控件的Leave事件 
            Global.GetMainForm().BlankButtonFocus();
        }
    }
}
