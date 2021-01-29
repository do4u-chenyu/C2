using C2.Business.Model;
using C2.Controls.C1.Left;
using C2.Core;
using C2.Database;
using C2.Model;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace C2.Controls
{
    class TableListControl : ListBoxControl<TableListItem>
    {
        private List<DatabaseItem> _DatabaseItems;
        private DatabaseItem _SelectedTableItem;
        private ContextMenuStrip tableContextMenuStrip;
        public event System.EventHandler SelectedTableItemChanged;
        public TableListControl()
        {
            InitializationTableContextMenuStrip();
        }
        public List<DatabaseItem> DatabaseItems
        {
            get { return _DatabaseItems; }
            set
            {
                if (value != null)
                {
                    _DatabaseItems = value;
                    OnDatabaseItemsChanged();
                }
            }
        }
        public DatabaseItem SelectedTableItem
        {
            get { return _SelectedTableItem; }
            set
            {
                if (_SelectedTableItem != value)
                {
                    _SelectedTableItem = value;
                    OnSelectedTableItemChanged();
                }
            }
        }

        private void OnSelectedTableItemChanged()
        {
            SelectedTableItemChanged?.Invoke(this, EventArgs.Empty);
        }
        private void OnDatabaseItemsChanged()
        {
            this.Items.Clear();

            var dbis = (from g in DatabaseItems.ToArray()
                          orderby g.DataTable.Name
                          select g).ToArray();
            if (!dbis.IsNullOrEmpty())
            {
                this.SuspendLayout();
                foreach (var dbi in dbis)
                {
                    var tli = new TableListItem(dbi);
                    tli.Text = FileUtil.RenameAndCenterPadding(dbi.DataTable.Name, 23, 15);
                    tli.ToolTipText = dbi.DataTable.Name;
                    tli.Image = Properties.Resources.Table;
                    tli.Tag = dbi;
                    this.Items.Add(tli);
                }

                // select default
                if (this.Items.Count > 0)
                    this.SelectedItem = this.Items.First();

                this.VerticalScroll.Value = 0;
                this.ResumeLayout();
                this.PerformLayout();
            }
        }

        #region mouse event
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            var listItem = GetItemAt(e.X, e.Y);
            if (listItem != null)
            {
                SelectedIndices = new int[] { listItem.Index };
                SelectedTableItem = listItem.DatabaseItem;
            }
            if (e.Button == MouseButtons.Left && e.Clicks == 1 && SelectedTableItem != null)
            {
                // 使用`DataObject`对象来传参数，更加自由
                DataObject dragDropData = new DataObject();
                dragDropData.SetData("Type", ElementType.DataSource);
                dragDropData.SetData("DataType", SelectedTableItem.Type);   //本地数据还是外部数据
                dragDropData.SetData("TableInfo", SelectedTableItem);            // 数据表信息
                dragDropData.SetData("Text", SelectedTableItem.DataTable.Name);  // 数据表名
                this.DoDragDrop(dragDropData, DragDropEffects.Copy | DragDropEffects.Move);
            }

            if (e.Button == MouseButtons.Right && tableContextMenuStrip != null)
                tableContextMenuStrip.Show(this, new Point(e.X, e.Y));
        }
        #endregion
        #region MenuStrip
        ToolStripMenuItem PreviewTableToolStripMenuItem;
        ToolStripMenuItem PreviewTableSchemaToolStripMenuItem;
        ToolStripMenuItem CopyTableNameToolStripMenuItem;
        private void InitializationTableContextMenuStrip()
        {
            tableContextMenuStrip = new ContextMenuStrip();
            tableContextMenuStrip.Opening += TableContextMenuStrip_Opening;

            PreviewTableToolStripMenuItem = new ToolStripMenuItem();
            PreviewTableSchemaToolStripMenuItem = new ToolStripMenuItem();
            CopyTableNameToolStripMenuItem = new ToolStripMenuItem();
            //
            tableContextMenuStrip.Items.AddRange(new ToolStripItem[] {
                PreviewTableToolStripMenuItem,
                PreviewTableSchemaToolStripMenuItem,
                CopyTableNameToolStripMenuItem
            });
            tableContextMenuStrip.SuspendLayout();
            // PreviewTableToolStripMenuItem
            PreviewTableToolStripMenuItem.Image = Properties.Resources.image;
            PreviewTableToolStripMenuItem.Name = "PreviewTable";
            PreviewTableToolStripMenuItem.Text = "预览表";
            PreviewTableToolStripMenuItem.Click += new System.EventHandler(PreviewTableToolStripMenuItem_Click);

            // PreviewTableSchemaToolStripMenuItem
            PreviewTableSchemaToolStripMenuItem.Image = Properties.Resources.备注;
            PreviewTableSchemaToolStripMenuItem.Name = "PreviewTableSchema";
            PreviewTableSchemaToolStripMenuItem.Text = "预览表结构";
            PreviewTableSchemaToolStripMenuItem.Click += new System.EventHandler(PreviewTableSchemaToolStripMenuItem_Click);

            // CopyTableNameToolStripMenuItem
            CopyTableNameToolStripMenuItem.Image = Properties.Resources.progress_bar;
            CopyTableNameToolStripMenuItem.Name = "CopyTableName";
            CopyTableNameToolStripMenuItem.Text = "复制表名";
            CopyTableNameToolStripMenuItem.Click += new System.EventHandler(CopyTableNameToolStripMenuItem_Click);

            tableContextMenuStrip.ResumeLayout();
        }

        private void CopyTableNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileUtil.TryClipboardSetText(SelectedTableItem.DataTable.Name);
        }

        private void PreviewTableSchemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreviewTableSchema previewTableSchema = GenericSingleton<PreviewTableSchema>.CreateInstance();
            IDAO dao = DAOFactory.CreateDAO(SelectedTableItem);
            if (!dao.TestConn())
            {
                HelpUtil.ShowMessageBox(HelpUtil.DbCannotBeConnectedInfo);
                return;
            }
            dao.FillDGVWithTbSchema(previewTableSchema.DataGridView, this.SelectedTableItem.DataTable);
            if (SelectedTableItem != null)
            {
                previewTableSchema.Focus();
                previewTableSchema.Show();
            }
        }

        private void PreviewTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreviewDbDataForm previewDbDataForm = GenericSingleton<PreviewDbDataForm>.CreateInstance();
            IDAO dao = DAOFactory.CreateDAO(SelectedTableItem);
            if (!dao.TestConn())
            {
                HelpUtil.ShowMessageBox(HelpUtil.DbCannotBeConnectedInfo);
                return;
            }
            if (SelectedTableItem != null && previewDbDataForm.Flush(this.SelectedTableItem))
            {
                previewDbDataForm.Focus();
                previewDbDataForm.Show();
            }
        }

        private void TableContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.PreviewTableSchemaToolStripMenuItem.ToolTipText =
                this.PreviewTableSchemaToolStripMenuItem.Enabled ?
                "预览数据源前一千条数据"
                :
                HelpUtil.ReviewToolStripMenuItemInfo;
        }
        #endregion
        #region
        public void Clear()
        {
            this.Items.Clear();
            this.DatabaseItems.Clear();
            this.SelectedItem = null;
            this.PerformLayout();
        }
        #endregion
    }
}
