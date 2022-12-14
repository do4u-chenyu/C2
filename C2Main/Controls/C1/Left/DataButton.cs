using C2.Business.Model;
using C2.Core;
using C2.Model;
using C2.Utils;
using System;
using System.Windows.Forms;

namespace C2.Controls.Left
{
    public partial class DataButton : UserControl
    {
        private OpUtil.Encoding encoding;
        private OpUtil.ExtType extType;
        private char separator;
        private int count = 0;

        public OpUtil.Encoding Encoding { get => this.encoding; set => this.encoding = value; }
        public OpUtil.ExtType ExtType { get => extType; set => extType = value; }
        public char Separator { get => separator; set => separator = value; }
        public string FullFilePath { get => this.txtButton.Name; set => this.txtButton.Name = value; }
        public string DataSourceName { get; set; }
        public int Count { get => this.count; set => this.count = value; }
        private static readonly string DataButtonFlowTemplate = "编码:{0} 文件类型:{1} 引用次数:{2} 分割符:{3}";


        public DataButton(string ffp, string dataSourceName, char separator, OpUtil.ExtType extType, OpUtil.Encoding encoding)
        {
            InitializeComponent();
            txtButton.Name = ffp;
            txtButton.Text = dataSourceName;
            this.separator = separator;
            this.extType = extType;
            this.encoding = encoding;
            DataSourceName = dataSourceName;
        }

        private void DataButton_Load(object sender, EventArgs e)
        {
            // 数据源全路径浮动提示信息
            String helpInfo = FullFilePath;
            this.helpToolTip.SetToolTip(this.rightPictureBox, helpInfo);
            // 数据源名称浮动提示信息
            helpInfo = String.Format(DataButtonFlowTemplate,
                                    encoding.ToString(),
                                    this.ExtType,
                                    0,
                                    this.Separator == OpUtil.TabSeparator ? "TAB" : this.Separator.ToString());
            this.helpToolTip.SetToolTip(this.leftPictureBox, helpInfo);
            this.helpToolTip.SetToolTip(this.txtButton, this.txtButton.Text);
        }

        #region 右键菜单
        private void ReviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Global.GetMainForm().PreViewDataByFullFilePath(this, FullFilePath, this.separator, this.extType, this.encoding);
            Global.GetMainForm().ShowBottomPanel();
        }


        private void RemoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO 这一块先不做，按设计来说模型文档是不可以导入数据的，检测引用要看业务视图
            //int count = Global.GetModelDocumentDao().CountDataSourceUsage(this.FullFilePath);
            DialogResult rs = DialogResult.OK;

            // 数据源引用大于0时,弹出警告窗,告诉用户该模型还在使用
            if (count > 0)
                rs = MessageBox.Show("此数据正被使用, 继续卸载请点击 \"确定\"",
                    "卸载 " + this.DataSourceName,
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Information);
            else // count == 0, 不需要特别的警告信息
                rs = MessageBox.Show("卸载数据源,请点击 \"确定\"",
                    "卸载 " + this.DataSourceName,
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Information);

            if (rs != DialogResult.OK)
                return;

            // 卸载数据源
            Global.GetDataSourceControl().RemoveDataButton(this);
            // 引用不为0时,有可能还会预览该数据源的数据,此时不用移除buffer
            //if (count == 0)//不管是否有引用，均清空缓存
            BCPBuffer.GetInstance().Remove(this.FullFilePath);
        }
        #endregion

        private void OpenFilePathMenuItem_Click(object sender, EventArgs e)
        {
            FileUtil.ExploreDirectory(FullFilePath);
        }



        private void CopyFullFilePathToClipboard(object sender, EventArgs e)
        {
            FileUtil.TryClipboardSetText(FullFilePath);
        }


        private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Global.GetMainForm().PreViewDataByFullFilePath(this, FullFilePath, this.separator, this.extType, this.encoding, true);
            Global.GetMainForm().ShowBottomPanel();
        }

        private void TxtButton_MouseDown(object sender, MouseEventArgs e)
        {
            // 左键单击拖拽
            if (e.Button != MouseButtons.Left || e.Clicks != 1)
                return;

            // 使用`DataObject`对象来传参数，更加自由
            DataObject dragDropData = new DataObject();
            dragDropData.SetData("Type", ElementType.DataSource);
            dragDropData.SetData("DataType", DatabaseType.Null);  //本地数据还是外部数据
            dragDropData.SetData("Path", FullFilePath);           // 数据源文件全路径
            dragDropData.SetData("Text", DataSourceName);         // 数据源名称
            dragDropData.SetData("Separator", Separator);         // 分隔符
            dragDropData.SetData("ExtType", ExtType);             // 扩展名,文件类型
            // 需要记录他的编码格式
            dragDropData.SetData("Encoding", Encoding);
            this.txtButton.DoDragDrop(dragDropData, DragDropEffects.Copy | DragDropEffects.Move);
        }

        private void ContextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.ReviewToolStripMenuItem.Enabled = Global.GetBottomViewPanel().Visible;
            this.ReviewToolStripMenuItem.ToolTipText = this.ReviewToolStripMenuItem.Enabled ? "预览数据源前一千条数据" : HelpUtil.ReviewToolStripMenuItemInfo;
        }
    }
}
