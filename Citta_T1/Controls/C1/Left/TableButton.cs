
using C2.Business.Model;
using C2.Core;
using C2.Database;
using C2.Model;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace C2.Controls.Left
{
    public partial class TableButton : UserControl
    {
        public string ConnectionInfo { get => TableItem.PrettyDatabaseInfo; }
        public DatabaseItem TableItem { get; set; }
        public string LinkSourceName { get; set; }
        public List<string> ColumnName { get; set; }
        public TableButton(DatabaseItem tableItem)
        {
            InitializeComponent();
            TableItem = tableItem;
            txtButton.Name = tableItem.DataTable.Name;
            txtButton.Text = FileUtil.ReName(tableItem.DataTable.Name);
            LinkSourceName = tableItem.DataTable.Name;
            ColumnName = tableItem.DataTable.Columns;
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
            PreviewDbDataForm previewDbDataForm = GenericSingleton<PreviewDbDataForm>.CreateInstance();
            IDAO dao = DAOFactory.CreateDAO(TableItem);
            if (!dao.TestConn())
            {
                HelpUtil.ShowMessageBox(HelpUtil.DbCannotBeConnectedInfo);
                return;
            }
            if (TableItem != null && previewDbDataForm.Flush(this.TableItem))
            {
                previewDbDataForm.Focus();
                previewDbDataForm.Show();
            }
        }
        private void ReviewStruToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreviewTableSchema previewTableSchema = GenericSingleton<PreviewTableSchema>.CreateInstance();
            IDAO dao = DAOFactory.CreateDAO(TableItem);
            if (!dao.TestConn())
            {
                HelpUtil.ShowMessageBox(HelpUtil.DbCannotBeConnectedInfo);
                return;
            }
            dao.FillDGVWithTbSchema(previewTableSchema.DataGridView, this.TableItem.DataTable);
            if (TableItem != null)
            {
                previewTableSchema.Focus();
                previewTableSchema.Show();
            }
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
            dragDropData.SetData("DataType", TableItem.Type);   //本地数据还是外部数据
            dragDropData.SetData("TableInfo", TableItem);            // 数据表信息
            dragDropData.SetData("Text", TableItem.DataTable.Name);  // 数据表名

            this.txtButton.DoDragDrop(dragDropData, DragDropEffects.Copy | DragDropEffects.Move);
        }

        private void ContextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.ReviewToolStripMenuItem.ToolTipText = this.ReviewToolStripMenuItem.Enabled ? "预览数据源前一千条数据" : HelpUtil.ReviewToolStripMenuItemInfo;
        }

        private void CopyTableNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileUtil.TryClipboardSetText(TableItem.DataTable.Name);
        }
    }
}
