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
    class TableListControl : Control
    {
        private List<DatabaseItem> _DatabaseItems;
        private DatabaseItem _SelectedTableItem;
        private ListBoxControl<TableListItem> listBoxControl;
        private ContextMenuStrip tableContextMenuStrip;
        public TableListControl()
        {
            listBoxControl = new ListBoxControl<TableListItem>();
            InitializationTableContextMenuStrip();
        }
        public List<DatabaseItem> DatabaseItems
        {
            get { return _DatabaseItems; }
            set
            {
                if (_DatabaseItems != value)
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
                    OnSelectedTableItem();
                }
            }
        }

        private void OnSelectedTableItem()
        {
            throw new NotImplementedException();
        }
        private void OnDatabaseItemsChanged()
        {
            listBoxControl.Items.Clear();

            var dbis = (from g in DatabaseItems.ToArray()
                          orderby g.DataTable.Name
                          select g).ToArray();
            if (!dbis.IsNullOrEmpty())
            {
                this.SuspendLayout();
                foreach (var dbi in dbis)
                {
                    var miExport = new TableListItem(dbi);
                    miExport.Text = FileUtil.RenameAndCenterPadding(dbi.DataTable.Name, 23, 15);
                    miExport.ToolTipText = dbi.DataTable.Name;
                    miExport.Image = Properties.Resources.Table;
                    miExport.Tag = dbi;
                    listBoxControl.Items.Add(miExport);
                }

                // select default
                if (listBoxControl.Items.Count > 0)
                    listBoxControl.SelectedItem = listBoxControl.Items.First();

                listBoxControl.VerticalScroll.Value = 0;
                listBoxControl.ResumeLayout();
                listBoxControl.PerformLayout();
            }
        }


        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Right && tableContextMenuStrip != null)
            {
                tableContextMenuStrip.Show(this, new Point(e.X, e.Y));
            }
        }
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
            // MenuAddIcon
            PreviewTableToolStripMenuItem.Image = Properties.Resources.image;
            PreviewTableToolStripMenuItem.Name = "PreviewTable";
            PreviewTableToolStripMenuItem.Text = "预览表";
            PreviewTableToolStripMenuItem.Click += new System.EventHandler(PreviewTableToolStripMenuItem_Click);

            // MenuAddRemark
            PreviewTableSchemaToolStripMenuItem.Image = Properties.Resources.备注;
            PreviewTableSchemaToolStripMenuItem.Name = "PreviewTableSchema";
            PreviewTableSchemaToolStripMenuItem.Text = "预览表结构";
            PreviewTableSchemaToolStripMenuItem.Click += new System.EventHandler(PreviewTableSchemaToolStripMenuItem_Click);

            // 
            CopyTableNameToolStripMenuItem.Image = Properties.Resources.progress_bar;
            CopyTableNameToolStripMenuItem.Name = "CopyTableName";
            CopyTableNameToolStripMenuItem.Text = "复制表明";
            CopyTableNameToolStripMenuItem.Click += new System.EventHandler(CopyTableNameToolStripMenuItem_Click);
            //
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
    }
}
