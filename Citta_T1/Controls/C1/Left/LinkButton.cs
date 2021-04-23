﻿using C2.Core;
using C2.Dialogs;
using C2.Model;
using C2.Utils;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace C2.Controls.Left
{
    public partial class LinkButton : UserControl
    {
        private int count = 0;
        public string FullFilePath { get => DatabaseItem.AllDatabaseInfo; }

        private string _LinkSourceName;
        public string LinkSourceName 
        {
            set 
            {
                _LinkSourceName = value;
                txtButton.Name = _LinkSourceName;
                txtButton.Text = _LinkSourceName;

            }
            get
            {
                return _LinkSourceName;
            }
        }

        public Image LeftControlImage { set { this.leftPictureBox.Image = value; }  } 
        public event EventHandler<ChangeDatabaseItemEventArgs> DatabaseItemChanged;
        public event EventHandler<SelectLinkButtonEventArgs> LinkButtonSelected;
        private DatabaseItem _DatabaseItem;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DatabaseItem DatabaseItem
        {
            get
            {
                return _DatabaseItem;
            }
            set
            {
                if (_DatabaseItem != value)
                {
                    DatabaseItem old = _DatabaseItem;
                    _DatabaseItem = value;
                    OnDatabaseItemChange(old);
                }
            }
        }

        public LinkButton(DatabaseItem item)
        {
            InitializeComponent();
            DatabaseItem = item;
            if (item.Type == DatabaseType.Hive)
                this.leftPictureBox.Image = global::C2.Properties.Resources.Hive;
            if (item.Type == DatabaseType.Postgre)
                this.leftPictureBox.Image = global::C2.Properties.Resources.PostgreSQL;

            toolTip1.SetToolTip(this.txtButton, item.PrettyDatabaseInfo);
        }

        #region 右键菜单
        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 编辑连接
            DoEditDatabaseInfo();
        }

        private void DoEditDatabaseInfo()
        {
            /*
             * 1、如果编辑后的dialog.data与link本身的data一致，不做操作
             * 2、如果不一致，先要判断dialog.data是否在dict里，在里面先移除原来的key再添加
             */
            var dialog = new AddDatabaseDialog(DatabaseItem, DatabaseDialogMode.Edit, this);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                if (DatabaseItem.AllDatabaseInfo.Equals(dialog.DatabaseInfo.AllDatabaseInfo))
                    return;
                else if (Global.GetDataSourceControl().LinkSourceDictI2B.ContainsKey(DatabaseItem.AllDatabaseInfo))
                    Global.GetDataSourceControl().LinkSourceDictI2B.Remove(DatabaseItem.AllDatabaseInfo);
                Global.GetDataSourceControl().LinkSourceDictI2B.Add(dialog.DatabaseInfo.AllDatabaseInfo, this);
                //修改图标
                if (dialog.DatabaseInfo.Type == DatabaseType.Hive)
                    this.leftPictureBox.Image = global::C2.Properties.Resources.Hive;
                else if (dialog.DatabaseInfo.Type == DatabaseType.Oracle)
                    this.leftPictureBox.Image = global::C2.Properties.Resources.oracle;
                else if (dialog.DatabaseInfo.Type == DatabaseType.Postgre)
                    this.leftPictureBox.Image = global::C2.Properties.Resources.PostgreSQL;
                else
                { };
                DatabaseItem = dialog.DatabaseInfo;
                Global.GetDataSourceControl().SaveExternalData();
            }
        }

        private void RemoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult rs = DialogResult.OK;

            // 数据源引用大于0时,弹出警告窗,告诉用户该模型还在使用
            if (count > 0)
                rs = MessageBox.Show("有模型在使用此数据, 继续卸载请点击 \"确定\"",
                    "卸载 " + this.LinkSourceName,
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Information);
            else // count == 0, 不需要特别的警告信息
                rs = MessageBox.Show("卸载数据源,请点击 \"确定\"",
                    "卸载 " + this.LinkSourceName,
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Information);

            if (rs != DialogResult.OK)
                return;

            // 卸载数据源
            Global.GetDataSourceControl().RemoveLinkButton(this);
        }
        #endregion

        private void TxtButton_MouseDown(object sender, MouseEventArgs e)
        {
            // 双击编辑
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
                DoEditDatabaseInfo();
        }

        private void OnDatabaseItemChange(DatabaseItem databaseItem)
        {
            LinkSourceName = string.Format("{0}@{1}", DatabaseItem.User, DatabaseItem.Server);
            DatabaseItemChanged?.Invoke(this, new ChangeDatabaseItemEventArgs() { databaseItem = databaseItem });
        }

        private void ConnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (new GuarderUtil.CursorGuarder(Cursors.WaitCursor))
            {
                LinkButtonSelected(this, new SelectLinkButtonEventArgs() { linkButton = this });
            }
               
        }
    }
    public class SelectLinkButtonEventArgs : EventArgs
    {
        public LinkButton linkButton;
    }
    public class ChangeDatabaseItemEventArgs : EventArgs
    {
        public DatabaseItem databaseItem;
    }
}
