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
        public DataSourceControl()
        {
            dataSourceDictI2B = new Dictionary<string, DataButton>();
            linkSourceDictI2B = new Dictionary<string, LinkButton>();
            InitializeComponent();
            startPoint = new Point(ButtonLeftX, -ButtonGapHeight);
            linkPoint = new Point(ButtonLeftX, -ButtonGapHeight);
            tablePoint = new Point(ButtonLeftX, -ButtonGapHeight);
        }
     
        private static readonly int ButtonGapHeight = 50;//上下间隔
        private static readonly int ButtonLeftX = 18;
        //private static readonly int ButtonBottomOffsetY = 100;
        //private Point startPoint = new Point(ButtonLeftX, -ButtonBottomOffsetY);
        private Point startPoint;
        private Point linkPoint;
        private Point tablePoint;
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

        private LinkButton _SelectLinkButton;
        //数据库相关属性
        public LinkButton SelectLinkButton 
        {
            set
            {
                _SelectLinkButton = value;
                OnSelectLinkButton(value);
            }
            get 
            {
                return _SelectLinkButton;
            }
        }

        public void OnSelectLinkButton(LinkButton linkButton)
        {
            //改变选中的button,刷新架构，默认显示用户名登陆的表结构
            ConnectDatabase(linkButton.DatabaseItem);//连接一次数据库，刷新架构及数据表
        }
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
            linkPoint = new Point(ButtonLeftX, -ButtonGapHeight);
            if (this.linkPanel.Controls.Count > 0)
                linkPoint = this.linkPanel.Controls[this.linkPanel.Controls.Count - 1].Location;
            linkPoint.Y += ButtonGapHeight;
            lb.Location = linkPoint;
        }
        private void LayoutModelButtonLocation(TableButton tb)
        {
            tablePoint = new Point(ButtonLeftX, -ButtonGapHeight);
            if (this.dataTabelPanel.Controls.Count > 0)
                tablePoint = this.dataTabelPanel.Controls[this.dataTabelPanel.Controls.Count - 1].Location;
            tablePoint.Y += ButtonGapHeight;
            tb.Location = tablePoint;
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
            LayoutModelButtonLocation(linkButton); // 递增
            //判断是否为同一链接
            if (this.LinkSourceDictI2B.ContainsKey(linkButton.FullFilePath))
            {
                String name = this.LinkSourceDictI2B[linkButton.FullFilePath].Name;
                HelpUtil.ShowMessageBox("该数据库连接已存在，数据库名为：" + name);
                return;
            }
            this.LinkSourceDictI2B.Add(linkButton.FullFilePath, linkButton);
            linkButton.DatabaseItemChanged += Link_DatabaseItemChanged;
            linkButton.LinkButtonSelected += Link_LinkButtonSelected;
            this.linkPanel.Controls.Add(linkButton);
        }
        void Link_LinkButtonSelected(object sender, SelectLinkButtonEventArgs e)
        {
            SelectLinkButton = e.linkButton;
        }

        void Link_DatabaseItemChanged(object sender, ChangeDatabaseItemEventArgs e)
        {
            if (SelectLinkButton == null)
                return;
            //改变持久化dict的key值
            if (this.LinkSourceDictI2B.ContainsKey(e.databaseItem.AllDatabaeInfo))
            {
                this.LinkSourceDictI2B.Remove(e.databaseItem.AllDatabaeInfo);
            }
            ConnectDatabase(SelectLinkButton.DatabaseItem);//连接一次数据库，刷新架构及数据表
        }

        private void GenTableButton(TableButton tableButton)
        {
            LayoutModelButtonLocation(tableButton); // 递增
            this.dataTabelPanel.Controls.Add(tableButton);
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

            this.LinkSourceDictI2B.Remove(linkButton.FullFilePath);
            this.linkPanel.Controls.Remove(linkButton);
            // 重新布局
            ReLayoutExternalFrame();
            // 保存
            SaveExternalData();
        }
        public void SaveDataSourceInfo()
        {
            DataSourceInfo dataSource = new DataSourceInfo(Global.GetMainForm().UserName);
            dataSource.SaveDataSourceInfo(DataSourceDictI2B.Values.ToArray());
        }

        public void SaveExternalData()
        {
            DataSourceInfo dataSource = new DataSourceInfo(Global.GetMainForm().UserName, "ExternalDataInformation.xml");
            dataSource.SaveExternalDataInfo(linkSourceDictI2B.Values.ToArray());
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
                GenLinkButton(dialog.DatabaseInfo, true);
            }
        }
        public void GenLinkButton(DatabaseItem dbinfo, bool updateFrameAndTables=false)
        {
            LinkButton linkButton = new LinkButton(dbinfo);
            GenLinkButton(linkButton);
            if (updateFrameAndTables)
                ConnectDatabase(dbinfo);//连接一次数据库，刷新架构及数据表
            SaveExternalData();
        }

        private void ConnectDatabase(DatabaseItem databaseInfo)
        {
            /* 
             * TODO Dk 优化代码
             * 1. 优化函数名称，首先这个名字取得不怎么好
             * 2. 优化代码逻辑，一旦出现连接不上的问题依然会查两次数据库，等待时间很长，每次连接的时候最好测试一下连接
             */
            //连接数据库
            OraConnection conn = new OraConnection(databaseInfo);
            if (!DbUtil.TestConn(conn, true))
                return;
            //刷新架构
            List<string> users = DbUtil.GetUsers(conn);
            UpdateFrameCombo(users, databaseInfo.User);

            //刷新数据表
            List<Table> tables = DbUtil.GetTablesByUser(conn, databaseInfo.User);
            UpdateTables(tables, databaseInfo);

        }
        private void UpdateTables(List<Table> tables, DatabaseItem databaseInfo)
        {
            //先清空上一次的数据表内容
            this.dataTabelPanel.Controls.Clear();

            if (tables == null)
                return;
            foreach (Table tmpTable in tables)
            {
                DatabaseItem tmpDatabaseItem = databaseInfo;
                tmpDatabaseItem.DataTable = tmpTable;
                TableButton tableButton = new TableButton(tmpDatabaseItem);
                GenTableButton(tableButton);//生成数据表按钮
            }
        }

        private void UpdateFrameCombo(List<string> users,string loginUser)
        {
            this.frameCombo.Items.Clear();
            if (users == null)
                return;

            this.frameCombo.Text = users.Find( x => x.Equals(loginUser.ToUpper())) == null ? "选择架构" : loginUser.ToUpper();
            users.ForEach(x => frameCombo.Items.Add(x.ToString()));
        }

        private void FrameCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //根据架构改变数据表
            OraConnection conn = new OraConnection(SelectLinkButton.DatabaseItem);
            List<Table> tables = DbUtil.GetTablesByUser(conn, this.frameCombo.Text);
            UpdateTables(tables, SelectLinkButton.DatabaseItem);
        }

        public List<DatabaseItem> GetAllExternalData()
        {
            List<DatabaseItem> allExternalData = new List<DatabaseItem>();
            if (this.linkPanel.Controls.Count > 0)
            {
                foreach (LinkButton linkButton in this.linkPanel.Controls)
                    allExternalData.Add(linkButton.DatabaseItem);
            }

            return allExternalData;
        }
    }
}
