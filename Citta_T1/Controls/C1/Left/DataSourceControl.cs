using C2.Business.DataSource;
using C2.Configuration;
using C2.Core;
using C2.Database;
using C2.Dialogs;
using C2.Globalization;
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
        private static readonly int ButtonGapY = 50;//上下间隔
        private static readonly int ButtonLeftX = 18;

        private Point startPoint;
        private Point linkPoint;
        private Point tablePoint;

        // 这个控件属性不需要在属性面板显示和序列化,不加这个标签,在引用这个控件的Designer中,会序列化它
        // 然后就是各种奇葩问题
        // DesignerSerializationVisibility.Hidden  设计器不为这个属性生成代码
        // Browsable(false) 这个属性不出现在设计器的属性窗口中
        // MergableProperty(false) 不知道是干嘛的, 看网上帖子就加进去了
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), MergableProperty(false)]
        public Dictionary<string, DataButton> DataSourceDictI2B { get; }
        public Dictionary<string, LinkButton> LinkSourceDictI2B { get; }

        private LinkButton _SelectLinkButton;
        private List<DatabaseItem> _RelateDBIs;

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
        public List<DatabaseItem> RelateDBIs
        {
            get
            {
                return _RelateDBIs;
            }
            set
            {
                if (_RelateDBIs != value)
                {
                    _RelateDBIs = value;
                    this.tableFilterTextBox.Text = string.Empty;
                }
            }
        }

        public DataSourceControl()
        {
            InitializeInputDataForm();
            InitializeComponent();

            this.tableListControl1.ItemIconGetter = (item => item.Image);
            this.tableListControl1.ItemToolTipTextGetter = (item => item.ToolTipText);
            RelateDBIs = new List<DatabaseItem>();

            DataSourceDictI2B = new Dictionary<string, DataButton>();
            LinkSourceDictI2B = new Dictionary<string, LinkButton>();
            startPoint = new Point(ButtonLeftX, -ButtonGapY);
            linkPoint = new Point(ButtonLeftX - 15, -ButtonGapY);
            tablePoint = new Point(ButtonLeftX, -ButtonGapY);

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
                this.startPoint.Y = this.localFrame.Controls[0].Location.Y - ButtonGapY;


            int idx = this.localFrame.Controls.IndexOf(dataButton);
            using (new GuarderUtil.LayoutGuarder(this.localFrame))
            {     
                ReLayoutLocalFrame(idx); // 重新布局
                this.localFrame.Controls.Remove(dataButton); // 布局完成后删除控件
            }
            this.DataSourceDictI2B.Remove(dataButton.FullFilePath);
            SaveDataSourceInfo();        // 保存
        }

        private void ReLayoutLocalFrame(int index)
        {
            for (int i = index + 1; i < this.localFrame.Controls.Count; i++)
            {
                Control ct = this.localFrame.Controls[i];
                ct.Location = new Point(ct.Location.X, ct.Location.Y - ButtonGapY);
            }
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
            {
                this.localFrame.VerticalScroll.Value = 0;
                startPoint = new Point(ButtonLeftX, -20);
            }    
            startPoint.Y += ButtonGapY;
            ct.Location = startPoint;
        }

        #endregion

        #region 外部数据添加连接
        private void AddConnectLabel_Click(object sender, EventArgs e)
        {
            var dialog = new AddDatabaseDialog();
            if (dialog.ShowDialog(this) != DialogResult.OK)
                return;
            using (new GuarderUtil.CursorGuarder(Cursors.WaitCursor))
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
                this.linkPoint.Y = this.linkPanel.Controls[0].Location.Y - ButtonGapY;

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
            this.RelateDBIs.Clear();
            this.tableListControl1.Clear();
        }
        #endregion

        #region 外部数据库布局
        public void GenLinkButton(DatabaseItem dbinfo, bool updateFrameAndTables = false)
        {
            SelectLinkButton = new LinkButton(dbinfo);
            GenLinkButton(SelectLinkButton);
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
            else
            {
                this.linkPanel.VerticalScroll.Value = 0;
                linkPoint = new Point(ButtonLeftX - 15, -ButtonGapY); 
            }  
            linkPoint.Y += ButtonGapY;
            lb.Location = linkPoint;
        }
        #endregion


        #region 外部表布局
        private void ReLayoutTableFrame(List<DatabaseItem> dbis)
        {
            this.tableListControl1.DatabaseItems = dbis;
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
            // 根据架构改变数据表
            List<Table> tables;
            Dictionary<string, List<string>> tableColDict;
            using (new GuarderUtil.CursorGuarder(Cursors.WaitCursor))
            {
                IDAO dao = DAOFactory.CreateDAO(SelectLinkButton.DatabaseItem);
                if (!dao.TestConn())
                {
                    HelpUtil.ShowMessageBox(HelpUtil.DbCannotBeConnectedInfo);
                    return;
                }
                try
                {
                    tables = dao.GetTables(this.schemaComboBox.Text);
                    tableColDict = dao.GetColNameBySchema(this.schemaComboBox.Text);
                    UpdateTables(tables, SelectLinkButton.DatabaseItem, tableColDict);
                }
                catch (Exception ex)
                {
                    HelpUtil.ShowMessageBox(HelpUtil.DbQueryFailInfo, ex.Message);
                }
                this.tableFilterTextBox.Text = String.Empty;
            }
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
            if (String.IsNullOrEmpty(this.tableFilterTextBox.Text.ToString()))
            {
                ReLayoutTableFrame(RelateDBIs);
                return;
            }
            var filteredItem = RelateDBIs.FindAll(
                t =>
                    t.DataTable.Columns.Exists(c => c.IndexOf(tableFilterTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    t.DataTable.Name.IndexOf(tableFilterTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            ReLayoutTableFrame(filteredItem);
        }
        #endregion

        public void OnSelectLinkButton(LinkButton linkButton)
        {
            //改变选中的button,刷新架构，默认显示用户名登陆的表结构
            ConnectDatabase(linkButton.DatabaseItem);//连接一次数据库，刷新架构及数据表
        }

        private void ConnectDatabase(DatabaseItem dbi)
        {
            /* 
             * 1. 优化函数名称，首先这个名字取得不怎么好
             * [x]. 优化代码逻辑，一旦出现连接不上的问题依然会查两次数据库，等待时间很长，每次连接的时候最好测试一下连接
             */
            List<Table> tables = new List<Table>();
            Dictionary<string, List<string>> tableColDict = new Dictionary<string, List<string>>();
            IDAO dao = DAOFactory.CreateDAO(dbi);
            if (!dao.TestConn())
            {
                HelpUtil.ShowMessageBox(HelpUtil.DbCannotBeConnectedInfo);
                return;
            }
            try
            {
                List<string> users = dao.GetUsers();
                UpdateFrameCombo(users, dbi, dao.DefaultSchema());

                //刷新数据表
                tables = dao.GetTables(this.schemaComboBox.Text);
                tableColDict = dao.GetColNameByTables(tables);
            }
            catch (Exception ex)
            {
                HelpUtil.ShowMessageBox(ex.Message, "提示信息", System.Windows.Forms.MessageBoxIcon.Warning);
            }
            //刷新架构 dao会会变化
            this.schemaComboBox.SelectedIndexChanged -= SchemaComboBox_SelectedIndexChanged;
            UpdateTables(tables, dbi, tableColDict);
            this.schemaComboBox.SelectedIndexChanged += SchemaComboBox_SelectedIndexChanged;
        }

        private void UpdateFrameCombo(List<string> users, DatabaseItem dbi, string defaultSchema)
        {
            string loginUser = dbi.User;
            this.schemaComboBox.Items.Clear();
            _RelateDBIs.Clear();
            if (users == null)
                return;


            if (dbi.Type == DatabaseType.Hive)
            {
                this.schemaComboBox.Text = defaultSchema;
            }
            if (dbi.Type == DatabaseType.Postgre)
            {
                this.schemaComboBox.Text = defaultSchema;
            }
            else
            {
                this.schemaComboBox.Text = users.Contains(loginUser.ToUpper()) ? defaultSchema : "选择架构";
            }


            users.ForEach(x => schemaComboBox.Items.Add(x.ToString()));
        }
        private void UpdateTables(List<Table> tables, DatabaseItem databaseInfo, Dictionary<string, List<string>> tableColDict)
        {
            List<DatabaseItem> dbis = new List<DatabaseItem>();
            _RelateDBIs.Clear();
            tablePoint = new Point(ButtonLeftX, -ButtonGapY);
            List<string> tmp = new List<string>();
            foreach (Table table in tables)
            {
                if (tableColDict.TryGetValue(table.Name, out tmp))
                    table.Columns = tmp;
                DatabaseItem tmpDatabaseItem = databaseInfo.Clone();
                tmpDatabaseItem.DataTable = table;
                tmpDatabaseItem.Schema = this.schemaComboBox.Text;
                dbis.Add(tmpDatabaseItem.Clone());
            }
            this.tableListControl1.DatabaseItems = dbis;
            RelateDBIs = dbis;
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
