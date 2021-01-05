
using C2.Business.Model;
using C2.Core;
using C2.Database;
using C2.Model;
using C2.Utils;
using System;
using System.Windows.Forms;

namespace C2.Controls.Left
{
    public partial class TableButton : UserControl
    {
        public string ConnectionInfo { get => TableItem.PrettyDatabaseInfo; }
        public DatabaseItem TableItem { get; set; }
        public string LinkSourceName { get; set; }

        public TableButton(DatabaseItem tableItem)
        {
            InitializeComponent();
            TableItem = tableItem;
            txtButton.Name = tableItem.DataTable.Name;
            txtButton.Text = FileUtil.ReName(tableItem.DataTable.Name);
            LinkSourceName = tableItem.DataTable.Name;
        }

        private void TableButton_Load(object sender, EventArgs e)
        {
            // 数据源全路径浮动提示信息
            String helpInfo = "连接信息：" + ConnectionInfo;
            this.helpToolTip.SetToolTip(this.rightPictureBox, helpInfo);

            // 数据源名称浮动提示信息
            helpInfo = LinkSourceName;
            this.helpToolTip.SetToolTip(this.txtButton, helpInfo);
        }


        #region 右键菜单
        private void ReviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreviewDbDataForm previewDbDataForm = new PreviewDbDataForm();
            previewDbDataForm.MaxNumChanged += new MaxNumChangedEventHandler(OnDataGridViewMaxNumChanged);
            if (!DbUtil.TestConn(new OraConnection(TableItem)))
            {
                HelpUtil.ShowMessageBox(HelpUtil.DbCannotBeConnectedInfo);
                return;
            }
            DbUtil.FillDGVWithTbContent(previewDbDataForm.DataGridView, new OraConnection(TableItem), this.TableItem.DataTable, previewDbDataForm.MaxNum);
            previewDbDataForm.Show();
        }
        private void OnDataGridViewMaxNumChanged(object sender, int maxNum)
        {
            PreviewDbDataForm pddf = (sender as PreviewDbDataForm);
            DbUtil.FillDGVWithTbContent(pddf.DataGridView, new OraConnection(TableItem), this.TableItem.DataTable, pddf.MaxNum);
        }

        #endregion

        private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Global.GetMainForm().ShowBottomPanel();
        }

        private void TxtButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks != 1 || e.Button != MouseButtons.Left)
                return;

            // 使用`DataObject`对象来传参数，更加自由
            DataObject dragDropData = new DataObject();
            dragDropData.SetData("Type", ElementType.DataSource);
            dragDropData.SetData("DataType", DatabaseType.Oracle);   //本地数据还是外部数据
            dragDropData.SetData("TableInfo", TableItem);            // 数据表信息
            dragDropData.SetData("Text", TableItem.DataTable.Name);  // 数据表名

            this.txtButton.DoDragDrop(dragDropData, DragDropEffects.Copy | DragDropEffects.Move);
        }

        private void ContextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.ReviewToolStripMenuItem.ToolTipText = this.ReviewToolStripMenuItem.Enabled ? "预览数据源部分信息" : HelpUtil.ReviewToolStripMenuItemInfo;
        }

        private void ReviewStruToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreviewTableSchema previewTableSchema = new PreviewTableSchema();
            DbUtil.FillDGVWithTbSchema(previewTableSchema.DataGridView, new OraConnection(TableItem), this.TableItem.DataTable.Name);
            previewTableSchema.Show();
        }

        private void CopyTableNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileUtil.TryClipboardSetText(TableItem.DataTable.Name);
        }
    }
}
