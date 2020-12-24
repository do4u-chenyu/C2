
using C2.Business.Model;
using C2.Core;
using C2.Utils;
using System;
using System.Windows.Forms;

namespace C2.Controls.Left
{
    public partial class TableButton : UserControl
    {
        private OpUtil.Encoding encoding;
        private OpUtil.ExtType extType;
        private char separator;
        private int count = 0;
        private string oldTextString;
        public OpUtil.Encoding Encoding { get => this.encoding; set => this.encoding = value; }
        public OpUtil.ExtType ExtType { get => extType; set => extType = value; }
        public char Separator { get => separator; set => separator = value; }
        public string FullFilePath { get => this.txtButton.Name; set => this.txtButton.Name = value; }
        public string DataSourceName { get; set; }
        public int Count { get => this.count; set => this.count = value; }
        private static string TableButtonFlowTemplate = "编码:{0} 文件类型:{1} 引用次数:{2} 分割符:{3}";


        public TableButton(string ffp, string dataSourceName, char separator, OpUtil.ExtType extType, OpUtil.Encoding encoding)
        {
            InitializeComponent();
            txtButton.Name = ffp;
            txtButton.Text = Utils.FileUtil.ReName(dataSourceName);
            this.separator = separator;
            this.extType = extType;
            this.encoding = encoding;
            this.oldTextString = dataSourceName;
            DataSourceName = dataSourceName;
        }

        private void TableButton_Load(object sender, EventArgs e)
        {
            // 数据源全路径浮动提示信息
            String helpInfo = FullFilePath;
            this.helpToolTip.SetToolTip(this.rightPictureBox, helpInfo);

            // 数据源名称浮动提示信息
            helpInfo = DataSourceName;
            this.helpToolTip.SetToolTip(this.txtButton, helpInfo);

            helpInfo = String.Format(TableButtonFlowTemplate,
                                    encoding.ToString(),
                                    this.ExtType,
                                    0,
                                    this.Separator == OpUtil.DefaultSeparator ? "TAB" : this.Separator.ToString());
            this.helpToolTip.SetToolTip(this.leftPictureBox, helpInfo);
        }


        #region 右键菜单
        private void ReviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Global.GetMainForm().PreViewDataByFullFilePath(this, FullFilePath, this.separator, this.extType, this.encoding);
            Global.GetMainForm().ShowBottomPanel();
        }

        #endregion


        private void CopyFullFilePathToClipboard(object sender, EventArgs e)
        {
            FileUtil.TryClipboardSetText(FullFilePath);
        }

        private void LeftPictureBox_MouseEnter(object sender, EventArgs e)
        {
            string helpInfo = String.Format(TableButtonFlowTemplate,
                                        encoding.ToString(),
                                        this.ExtType,
                                        0,
                                        this.Separator == OpUtil.DefaultSeparator ? "TAB" : this.Separator.ToString());
            this.helpToolTip.SetToolTip(this.leftPictureBox, helpInfo);
        }

        private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Global.GetMainForm().PreViewDataByFullFilePath(this, FullFilePath, this.separator, this.extType, this.encoding, true);
            Global.GetMainForm().ShowBottomPanel();
        }

        private void TxtButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            if (e.Clicks == 1) // 单击拖拽
            {
                // 使用`DataObject`对象来传参数，更加自由
                DataObject dragDropData = new DataObject();
                dragDropData.SetData("Type", ElementType.DataSource);
                dragDropData.SetData("Path", FullFilePath);    // 数据源文件全路径
                dragDropData.SetData("Text", DataSourceName);  // 数据源名称
                dragDropData.SetData("Separator", Separator);  // 分隔符
                dragDropData.SetData("ExtType", ExtType);      // 扩展名,文件类型
                // 需要记录他的编码格式
                dragDropData.SetData("Encoding", Encoding);
                this.txtButton.DoDragDrop(dragDropData, DragDropEffects.Copy | DragDropEffects.Move);
            }
            //else if (e.Clicks == 2)
            //{   // 双击改名 
            //    RenameToolStripMenuItem_Click(sender, e);
            //}
        }
        
        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.ReviewToolStripMenuItem.Enabled = Global.GetBottomViewPanel().Visible;
            this.ReviewToolStripMenuItem.ToolTipText = this.ReviewToolStripMenuItem.Enabled ? "预览数据源部分信息" : HelpUtil.ReviewToolStripMenuItemInfo;
        }
    }
}
