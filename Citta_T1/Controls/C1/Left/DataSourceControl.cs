using C2.Business.DataSource;
using C2.Core;
using C2.Database;
using C2.Dialogs;
using C2.Model;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace C2.Controls.Left
{
    public partial class DataSourceControl : UserControl
    {
        private InputDataForm inputDataForm;
        private static readonly int ButtonGapHeight = 50;//上下间隔
        private static readonly int ButtonLeftX = 18;

        private Point startPoint;
        private Point linkPoint;
        private Point tablePoint;
        private Dictionary<string, List<string>> relateTableCol;

        // 这个控件属性不需要在属性面板显示和序列化,不加这个标签,在引用这个控件的Designer中,会序列化它
        // 然后就是各种奇葩问题
        // DesignerSerializationVisibility.Hidden  设计器不为这个属性生成代码
        // Browsable(false) 这个属性不出现在设计器的属性窗口中
        // MergableProperty(false) 不知道是干嘛的, 看网上帖子就加进去了
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), MergableProperty(false)]
        public Dictionary<string, DataButton> DataSourceDictI2B { get; }
        public Dictionary<string, LinkButton> LinkSourceDictI2B { get; }

        private LinkButton _SelectLinkButton;

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

        private List<TableButton> _RelateTableButtons;
        public List<TableButton> RelateTableButtons
        {
            set
            {
                if (_RelateTableButtons != value)
                {
                    this.tableFilterTextBox.Text = string.Empty;
                }
                _RelateTableButtons = value;
            }
            get
            {
                return _RelateTableButtons;
            }
        }

        public DataSourceControl()
        {
            InitializeInputDataForm();
            InitializeComponent();

            DataSourceDictI2B = new Dictionary<string, DataButton>();
            LinkSourceDictI2B = new Dictionary<string, LinkButton>();
            startPoint = new Point(ButtonLeftX, -ButtonGapHeight);
            linkPoint = new Point(ButtonLeftX - 11, -ButtonGapHeight);
            tablePoint = new Point(ButtonLeftX, -ButtonGapHeight);
            _RelateTableButtons = new List<TableButton>();
        }

        #region 内外部数据面板切换
        private void ExternalData_Click(object sender, EventArgs e)
        {
            this.externalDataLabel.Font = new Font("微软雅黑", 12, FontStyle.Bold);
            this.localDataLabel.Font = new Font("微软雅黑", 12, FontStyle.Regular);
            this.externalFrame.Visible = true;
            this.localFrame.Visible = false;
            this.addLocalConnectLabel.Visible = false;

        }

        private void LocalData_Click(object sender, EventArgs e)
        {
            this.localDataLabel.Font = new Font("微软雅黑", 12, FontStyle.Bold);
            this.externalDataLabel.Font = new Font("微软雅黑", 12, FontStyle.Regular);
            this.localFrame.Visible = true;
            this.externalFrame.Visible = false;
            this.addLocalConnectLabel.Visible = true;
        }
        #endregion

        #region 本地数据导入
        void InitializeInputDataForm()
        {
            this.inputDataForm = new Dialogs.InputDataForm();
            this.inputDataForm.InputDataEvent += InputDataFormEvent;
        }
        private void InputDataFormEvent(string name, string fullFilePath, char separator, OpUtil.ExtType extType, OpUtil.Encoding encoding)
        {
            Global.GetDataSourceControl().GenDataButton(name, fullFilePath, separator, extType, encoding);
            Global.GetDataSourceControl().Visible = true;
        }
        #endregion

        #region 本地数据删除
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

        private void ReLayoutLocalFrame()
        {
            // 先暂停布局,然后调整button位置,最后恢复布局,可以避免闪烁
            List<Control> tmp = new List<Control>();
            foreach (Control ct in this.localFrame.Controls)
            {
                if (ct is DataButton)
                    tmp.Add(ct);
            }
            if (tmp.Count <= 0)
                return;

            this.localFrame.Controls.Clear();
            // 重新排序
            this.AutoScroll = false;
            LayoutModelButtonLocation(tmp[0] as DataButton);
            this.localFrame.Controls.Add(tmp[0]);

            tmp.Remove(tmp[0]);
            this.localFrame.SuspendLayout();
            foreach (Control ct in tmp)
            {
                LayoutModelButtonLocation(ct as DataButton);
                this.localFrame.Controls.Add(ct);
            }

            this.localFrame.ResumeLayout(false);
            this.localFrame.PerformLayout();
            this.AutoScroll = true;
        }

        public void SaveDataSourceInfo()
        {
            DataSourceInfo dataSource = new DataSourceInfo(Global.GetMainForm().UserName);
            dataSource.SaveDataSourceInfo(DataSourceDictI2B.Values.ToArray());
        }
        #endregion

        #region 本地数据布局
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

        // 程序启动加载时调用
        public void GenDataButton(DataButton dataButton)
        {
            // 供load时调用

            LayoutModelButtonLocation(dataButton); // 递增
            this.DataSourceDictI2B.Add(dataButton.FullFilePath, dataButton);
            this.localFrame.Controls.Add(dataButton);
        }

        private void LayoutModelButtonLocation(DataButton ct)
        {
            if (this.localFrame.Controls.Count > 0)
                startPoint = this.localFrame.Controls[this.localFrame.Controls.Count - 1].Location;
            else
                startPoint = new Point(ButtonLeftX, -20);
            startPoint.Y += ButtonGapHeight;
            ct.Location = startPoint;
        }

        #endregion

        #region 外部数据添加连接
        private void AddConnectLabel_Click(object sender, EventArgs e)
        {
            var dialog = new AddDatabaseDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                GenLinkButton(dialog.DatabaseInfo, true);
            }
        }
        #endregion

        #region 外部数据库删除
        public void RemoveLinkButton(LinkButton linkButton)
        {
            // panel左上角坐标随着滑动条改变而改变，以下就是将panel左上角坐标校验
            if (this.linkPanel.Controls.Count > 0)
                this.linkPoint.Y = this.linkPanel.Controls[0].Location.Y - ButtonGapHeight;

            this.LinkSourceDictI2B.Remove(linkButton.FullFilePath);
            this.linkPanel.Controls.Remove(linkButton);
            // 重新布局
            ReLayoutExternalFrame();
            // 保存
            SaveExternalData();
            //清空下方内容
            ClearTablesContent();
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
        public void SaveExternalData()
        {
            DataSourceInfo dataSource = new DataSourceInfo(Global.GetMainForm().UserName, "ExternalDataInformation.xml");
            dataSource.SaveExternalDataInfo(LinkSourceDictI2B.Values.ToArray());
        }
        private void ClearTablesContent()
        {
            this.schemaComboBox.Items.Clear();
            this.schemaComboBox.Text = string.Empty;
            RelateTableButtons.Clear();
            //this.dataTableTextBox.Text = string.Empty;
            this.tabelPanel.Controls.Clear();
        }

        #endregion

        #region 外部数据库布局
        public void GenLinkButton(DatabaseItem dbinfo, bool updateFrameAndTables = false)
        {               
            SelectLinkButton = new LinkButton(dbinfo);
            GenLinkButton(SelectLinkButton);
            if (updateFrameAndTables)
                ConnectDatabase(dbinfo);//连接一次数据库，刷新架构及数据表
            SaveExternalData();
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
        private void LayoutModelButtonLocation(LinkButton lb)
        {
            if (this.linkPanel.Controls.Count > 0)
                linkPoint = this.linkPanel.Controls[this.linkPanel.Controls.Count - 1].Location;
            linkPoint.Y += ButtonGapHeight;
            lb.Location = linkPoint;
        }
        #endregion

        #region 外部表添加
        private void GenTableButton(TableButton tableButton)
        {
            LayoutModelButtonLocation(tableButton); // 递增
            this.tabelPanel.Controls.Add(tableButton);
        }
        #endregion

        #region 外部表布局
        private void LayoutModelButtonLocation(TableButton tb)
        {
            if (this.tabelPanel.Controls.Count > 0)
                tablePoint = this.tabelPanel.Controls[this.tabelPanel.Controls.Count - 1].Location;
            tablePoint.Y += ButtonGapHeight;
            tb.Location = tablePoint;
        }

        private void ReLayoutTableFrame(List<TableButton> tableButtons)
        {
            // panel左上角坐标随着滑动条改变而改变，以下就是将panel左上角坐标校验
            if (this.tabelPanel.Controls.Count > 0)
                this.tablePoint.Y = this.tabelPanel.Controls[0].Location.Y - ButtonGapHeight;

            // 先暂停布局,然后调整button位置,最后恢复布局,可以避免闪烁
            this.tabelPanel.SuspendLayout();

            this.tabelPanel.Controls.Clear();
            // 重新排序
            foreach (TableButton tb in tableButtons)
            {
                LayoutModelButtonLocation(tb);
                this.tabelPanel.Controls.Add(tb);
            }

            this.tabelPanel.ResumeLayout(false);
            this.tabelPanel.PerformLayout();
        }
        #endregion

        #region 事件
        void Link_LinkButtonSelected(object sender, SelectLinkButtonEventArgs e)
        {
            SelectLinkButton = e.linkButton;
        }

        void Link_DatabaseItemChanged(object sender, ChangeDatabaseItemEventArgs e)
        {
            if (SelectLinkButton == null)
                return;
            //改变持久化dict的key值
            if (this.LinkSourceDictI2B.ContainsKey(e.databaseItem.AllDatabaseInfo))
            {
                this.LinkSourceDictI2B.Remove(e.databaseItem.AllDatabaseInfo);
            }
            ConnectDatabase(SelectLinkButton.DatabaseItem);//连接一次数据库，刷新架构及数据表
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

        private void SchemaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //根据架构改变数据表
            List<Table> tables;
            if (SelectLinkButton.DatabaseItem.Type == DatabaseType.Hive)
            {
                HiveConnection hiveConn = new HiveConnection(SelectLinkButton.DatabaseItem);
                //刷新数据表
                tables = hiveConn.GetTablesByDB(this.schemaComboBox.Text);
            }
            else
            {
                OraConnection conn = new OraConnection(SelectLinkButton.DatabaseItem);
                tables = DbUtil.GetTablesByUser(conn, this.schemaComboBox.Text);
            }
        
            UpdateTables(tables, SelectLinkButton.DatabaseItem);
            this.optComboBox.Text = "表名";
            this.tableFilterTextBox.Text = "";
        }

        private void addLocalConnectLabel_MouseClick(object sender, MouseEventArgs e)
        {
            this.inputDataForm.StartPosition = FormStartPosition.CenterScreen;
            this.inputDataForm.ShowDialog();
            this.inputDataForm.ReSetParams();
        }
        private void TableFilterTextBox_TextChanged(object sender, EventArgs e)
        {
            this.tableFilterTextBox.ForeColor = SystemColors.WindowText;
            if (this.tableFilterTextBox.Text.ToString() == String.Empty)
            {
                ReLayoutTableFrame(RelateTableButtons);
                return;
            }
            switch (this.optComboBox.Text.ToString())
            {
                case "表名": ReLayoutTableFrame(RelateTableButtons.FindAll(t => t.LinkSourceName.Contains(tableFilterTextBox.Text.ToUpper())));
                    break;
                case "字段名": ReLayoutTableFrame(RelateTableButtons.FindAll(t => t.ColumnName.Contains(tableFilterTextBox.Text.ToUpper())));
                    break;
            }

        }
        #endregion

        public void OnSelectLinkButton(LinkButton linkButton)
        {
            //改变选中的button,刷新架构，默认显示用户名登陆的表结构
            ConnectDatabase(linkButton.DatabaseItem);//连接一次数据库，刷新架构及数据表
        }

        private void ConnectDatabase(DatabaseItem databaseInfo)
        {
            /* 
             * TODO DK 优化代码
             * 1. 优化函数名称，首先这个名字取得不怎么好
             * [x]. 优化代码逻辑，一旦出现连接不上的问题依然会查两次数据库，等待时间很长，每次连接的时候最好测试一下连接
             */

            //Hive 连接数据库
            if (databaseInfo.Type == DatabaseType.Hive)
            {
                HiveConnection hiveConn = new HiveConnection(databaseInfo);
                //刷新架构
                List<string> baseNames = hiveConn.GetHiveDatabases();
                if (baseNames.Count == 0)
                    return;
                UpdateFrameCombo(baseNames, baseNames[0]);
                //刷新数据表
                List<Table> hiveTables = hiveConn.GetTablesByDB(baseNames[0]);
                UpdateTables(hiveTables, databaseInfo); // TODO 没有Hive实现
                return;
            }
            //连接数据库
            OraConnection conn = new OraConnection(databaseInfo);
            if (!DbUtil.TestConn(conn))
            {
                HelpUtil.ShowMessageBox(HelpUtil.DbCannotBeConnectedInfo);
                return;
            }

            //刷新架构
            List<string> users = DbUtil.GetUsers(conn);
            UpdateFrameCombo(users, databaseInfo.User, databaseInfo.Type);

            //刷新数据表
            List<Table> tables = DbUtil.GetTablesByUser(conn, databaseInfo.User);
            UpdateTables(tables, databaseInfo);
        }

        private void UpdateFrameCombo(List<string> users,string loginUser)
        {
            this.schemaComboBox.Items.Clear();
            //this.dataTableTextBox.Text = string.Empty;//刷新架构，数据表搜索框清空
            RelateTableButtons.Clear();
            if (users == null)
                return;

            this.schemaComboBox.Text = this.schemaComboBox.Text = users.Contains(loginUser.ToLower()) ? "选择架构" : loginUser.ToUpper(); ;

            users.ForEach(x => schemaComboBox.Items.Add(x.ToString()));
        }

        private void UpdateTables(List<Table> tables, DatabaseItem databaseInfo)
        {
            //先清空上一次的数据表内容
            RelateTableButtons.Clear();
            this.tabelPanel.Controls.Clear();
            OraConnection conn = new OraConnection(databaseInfo);
            relateTableCol = DbUtil.GetTableCol( conn ,tables);
            tablePoint = new Point(ButtonLeftX, -ButtonGapHeight);
            List<string> tmp = new List<string>();
            foreach (Table table in tables.Take(Math.Min(300,tables.Count)))
            {
                foreach ( List<string> kvp in relateTableCol.Values)
                {
                    tmp.AddRange(kvp);
                }
                table.Columns = tmp;
                DatabaseItem tmpDatabaseItem = databaseInfo.Clone();
                tmpDatabaseItem.DataTable = table;
                tmpDatabaseItem.Group = this.schemaComboBox.Text;
                TableButton tableButton = new TableButton(tmpDatabaseItem);
                GenTableButton(tableButton);//生成数据表按钮
            }

            foreach (TableButton tb in this.tabelPanel.Controls)
            {
                RelateTableButtons.Add(tb);
            }

                
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


        private void optComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.tableFilterTextBox.Text = "";
            ReLayoutTableFrame(RelateTableButtons);
        }
    }
}
