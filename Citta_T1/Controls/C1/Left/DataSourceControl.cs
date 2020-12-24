using C2.Business.DataSource;
using C2.Core;
using C2.Database;
using C2.Dialogs;
using C2.Model;
using C2.Utils;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace C2.Controls.Left
{
    public partial class DataSourceControl : UserControl
    {
        // 从`FormInputData.cs`导入模块收到的数据，以索引的形式存储
        public int SelectedTableSource { get; set; }//选中数据
        public List<DataItem> ComboTableSource { get; set; }//下拉数据
        public DataSourceControl()
        {
            dataSourceDictI2B = new Dictionary<string, DataButton>();
            linkSourceDictI2B = new Dictionary<string, LinkButton>();
            InitializeComponent();
            startPoint = new Point(ButtonLeftX, -ButtonBottomOffsetY);
            startPoint.Y += 65;
        }
     
        private static readonly int ButtonGapHeight = 50;//上下间隔
        private static readonly int ButtonLeftX = 18;
        private static readonly int ButtonBottomOffsetY = 100;
        //private Point startPoint = new Point(ButtonLeftX, -ButtonBottomOffsetY);
        private Point startPoint;
        private Point linkPoint = new Point(0,0);
        private Dictionary<string, DataButton> dataSourceDictI2B;
        private Dictionary<string, LinkButton> linkSourceDictI2B;

        // 这个控件属性不需要在属性面板显示和序列化,不加这个标签,在引用这个控件的Designer中,会序列化它
        // 然后就是各种奇葩问题
        // DesignerSerializationVisibility.Hidden  设计器不为这个属性生成代码
        // Browsable(false) 这个属性不出现在设计器的属性窗口中
        // MergableProperty(false) 不知道是干嘛的, 看网上帖子就加进去了
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), MergableProperty(false)]
        public Dictionary<string, DataButton> DataSourceDictI2B { get => dataSourceDictI2B; }
        public Dictionary<string, LinkButton> LinkSourceDictI2B { get => linkSourceDictI2B; }

        // 手工导入时调用
        public void GenDataButton(string dataName, string fullFilePath, char separator, OpUtil.ExtType extType, OpUtil.Encoding encoding)
        {
            // 根据导入数据动态生成一个button
            DataButton dataButton = new DataButton(fullFilePath, dataName, separator, extType, encoding);
            LayoutModelButtonLocation(dataButton);

            // 判断是否有路径文件
            if (this.DataSourceDictI2B.ContainsKey(fullFilePath))
            {
                String name = this.DataSourceDictI2B[fullFilePath].DataSourceName;
                HelpUtil.ShowMessageBox("该文件已存在，数据名为：" + name);
                return;
            }
            this.DataSourceDictI2B.Add(fullFilePath, dataButton);
            this.localFrame.Controls.Add(dataButton);
            //数据源持久化存储
            DataSourceInfo dataSource = new DataSourceInfo(Global.GetMainForm().UserName);
            dataSource.WriteDataSourceInfo(dataButton);
        }
        private void LayoutModelButtonLocation(DataButton ct)
        {

            if (this.localFrame.Controls.Count > 0)
                startPoint = this.localFrame.Controls[this.localFrame.Controls.Count - 1].Location;

            startPoint.Y += ButtonGapHeight;
            ct.Location = startPoint;
        }
        private void LayoutModelButtonLocation(LinkButton lb)
        {
            if (this.linkPanel.Controls.Count > 0)
                linkPoint = this.linkPanel.Controls[this.linkPanel.Controls.Count - 1].Location;
            linkPoint.Y += ButtonGapHeight;
            lb.Location = linkPoint;
        }
        // 程序启动加载时调用
        public void GenDataButton(DataButton dataButton)
        {
            // 供load时调用

            LayoutModelButtonLocation(dataButton); // 递增
            this.DataSourceDictI2B.Add(dataButton.FullFilePath, dataButton);
            this.localFrame.Controls.Add(dataButton);
        }
        public void GenLinkButton(LinkButton linkButton)
        {
            // 供load时调用

            LayoutModelButtonLocation(linkButton); // 递增
            //this.LinkSourceDictI2B.Add(linkButton.FullFilePath, linkButton);
            this.linkPanel.Controls.Add(linkButton);
   
        }
        private void GenConnectButton(DatabaseItem databaseInfo)
        {
            // TODO 生成一个button
            //throw new NotImplementedException();
            // 根据导入数据动态生成一个button
            LinkButton linkButton = new LinkButton(databaseInfo);

            GenLinkButton(linkButton);


        }
        private void ExternalData_Click(object sender, EventArgs e)
        {
            this.externalDataLabel.Font = new Font("微软雅黑", 12, FontStyle.Bold);
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
            this.localFrame.SuspendLayout();
            List<Control> tmp = new List<Control>();
            foreach (DataButton ct in this.localFrame.Controls)
                tmp.Add(ct);

            this.localFrame.Controls.Clear();
            // 重新排序
            foreach (Control ct in tmp)
            {
                LayoutModelButtonLocation(ct as DataButton);
                this.localFrame.Controls.Add(ct);
            }

            this.localFrame.ResumeLayout(false);
            this.localFrame.PerformLayout();
        }
        private void ReLayoutExternalFrame()
        {
            // 先暂停布局,然后调整button位置,最后恢复布局,可以避免闪烁
            this.linkPanel.SuspendLayout();
            List<Control> tmp = new List<Control>();
            foreach (LinkButton lb in this.linkPanel.Controls)
                tmp.Add(lb);

            this.linkPanel.Controls.Clear();
            // 重新排序
            foreach (Control lb in tmp)
            {
                LayoutModelButtonLocation(lb as LinkButton);
                this.linkPanel.Controls.Add(lb);
            }

            this.linkPanel.ResumeLayout(false);
            this.linkPanel.PerformLayout();
        }
        public void RemoveDataButton(DataButton dataButton)
        {
            // panel左上角坐标随着滑动条改变而改变，以下就是将panel左上角坐标校验
            if (this.localFrame.Controls.Count > 0)
                this.startPoint.Y = this.localFrame.Controls[0].Location.Y - ButtonGapHeight;

            this.DataSourceDictI2B.Remove(dataButton.FullFilePath);
            this.localFrame.Controls.Remove(dataButton);
            // 重新布局
            ReLayoutLocalFrame();
            // 保存
            SaveDataSourceInfo();
        }
        public void RemoveLinkButton(LinkButton linkButton)
        {
            // panel左上角坐标随着滑动条改变而改变，以下就是将panel左上角坐标校验
            if (this.linkPanel.Controls.Count > 0)
                this.startPoint.Y = this.linkPanel.Controls[0].Location.Y - ButtonGapHeight;

            this.DataSourceDictI2B.Remove(linkButton.FullFilePath);
            this.linkPanel.Controls.Remove(linkButton);
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

        private void DataSourceControl_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.FromArgb(195, 195, 195), 1);  //画笔 1宽度.
            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            p.DashPattern = new float[] { 4, 4 };
            g.DrawLine(p, 0, 30, 200, 30);//x1,y1,x2,y2
        }

        private void AddConnectLabel_Click(object sender, EventArgs e)
        {
            var dialog = new AddDatabaseDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                GenConnectButton(dialog.DatabaseInfo);
                ConnectDatabase(dialog.DatabaseInfo);
            }
        }

      

        private void ConnectDatabase(DatabaseItem databaseInfo)
        {
            MessageBox.Show(databaseInfo.Type + databaseInfo.Server + databaseInfo.Service + databaseInfo.Port + databaseInfo.User + databaseInfo.Password);
            // Name, User, Pass, Host, Sid, Service, Port;
            Connection conn = new Connection(databaseInfo);

            List<Schema> schemas = conn.Schemas;
            if (schemas == null)
                return;
            DataTable dt = new DataTable();
            for (int i = 0; i <= schemas[0].Tables.Count; i++)
            {
                schemas[0].Tables[i].Name.ToString();
                
            }


            List<string> users = DbUtil.GetUsers(conn);

            //List<string> tables = DbUtil.GetTablesByUser(conn, "SYS");
            this.UpdateFrameCombo(users);
            //this.UpdateTables(schemas);
        }

        private void UpdateTables(List<Schema> schemas)
        {
            throw new NotImplementedException();
        }

        private void UpdateFrameCombo(List<string> users)
        {
            //throw new NotImplementedException();
            this.frameCombo.Text =users[4].ToString();//这边是不是4太粗暴了?
            users.ForEach(x => frameCombo.Items.Add(x.ToString()));
        }


        private void GenLinkButton(Connection conn)
        {
            throw new NotImplementedException();
        }

        public void GenLinkButton(string dataName, string fullFilePath, char separator, OpUtil.ExtType extType, OpUtil.Encoding encoding)
        {
            // 根据导入数据动态生成一个button
            DataButton dataButton = new DataButton(fullFilePath, dataName, separator, extType, encoding);
            LayoutModelButtonLocation(dataButton);

            // 判断是否有路径文件
            if (this.DataSourceDictI2B.ContainsKey(fullFilePath))
            {
                String name = this.DataSourceDictI2B[fullFilePath].DataSourceName;
                HelpUtil.ShowMessageBox("该文件已存在，数据名为：" + name);
                return;
            }
            this.DataSourceDictI2B.Add(fullFilePath, dataButton);
            this.localFrame.Controls.Add(dataButton);
            //数据源持久化存储
            DataSourceInfo dataSource = new DataSourceInfo(Global.GetMainForm().UserName);
            dataSource.WriteDataSourceInfo(dataButton);

        }

         private void frameCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetComboTableSource();//每次清空一下
            SelectedTableSource = this.frameCombo.SelectedIndex;
            if (ComboTableSource == null || this.frameCombo.SelectedIndex < 0)
                return;



        }
        private void SetComboTableSource()
        {
            //this.frameCombo.Items.Clear();
        }
    }
}
