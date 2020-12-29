
using C2.Business.Model;
using C2.Core;
using C2.Utils;
using System;
using System.Windows.Forms;
using C2.Model;
using C2.Controls;
using C2.Dialogs;
namespace C2.Controls.Left
{
    public partial class LinkButton : UserControl
    {
        private int count = 0;
        private string oldTextString;
        public string FullFilePath { get => DatabaseItem.AllDatabaeInfo; }
        public string LinkSourceName { get; set; }


        public event EventHandler<ChangeDatabaseItemEventArgs> DatabaseItemChanged;
        public event EventHandler<SelectLinkButtonEventArgs> LinkButtonSelected;
        private DatabaseItem _DatabaseItem;
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
            txtButton.Name = DatabaseItem.Server;
            txtButton.Text = Utils.FileUtil.ReName(DatabaseItem.Server);
            this.oldTextString = DatabaseItem.Server;
            LinkSourceName = DatabaseItem.Server;
        }

        private void LinkButton_Load(object sender, EventArgs e)
        {
            //// 数据源全路径浮动提示信息
            //String helpInfo = FullFilePath;
            //// 数据源名称浮动提示信息
            //helpInfo = LinkSourceName;
            //this.helpToolTip.SetToolTip(this.txtButton, helpInfo);

            //helpInfo = String.Format(DataButtonFlowTemplate,
            //                        0
            //                       );
            //this.helpToolTip.SetToolTip(this.leftPictureBox, helpInfo);
        }

        #region 右键菜单
        private void EiditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
             * 编辑连接
             * 1、如果编辑后的dialog.data与link本身的data一致，不做操作
             * 2、如果不一致，先要判断dialog.data是否在dict里，在里面先移除原来的key再添加
             */
            var dialog = new AddDatabaseDialog(DatabaseItem);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                if (DatabaseItem.AllDatabaeInfo.Equals(dialog.DatabaseInfo.AllDatabaeInfo))
                    return;
                else if (Global.GetDataSourceControl().LinkSourceDictI2B.ContainsKey(DatabaseItem.AllDatabaeInfo))
                    Global.GetDataSourceControl().LinkSourceDictI2B.Remove(DatabaseItem.AllDatabaeInfo);
                Global.GetDataSourceControl().LinkSourceDictI2B.Add(dialog.DatabaseInfo.AllDatabaeInfo, this);

                DatabaseItem = dialog.DatabaseInfo;
            }
        }

        private void RenameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.textBox.ReadOnly = false;
            this.oldTextString = LinkSourceName;
            this.textBox.Text = LinkSourceName;
            this.txtButton.Visible = false;
            this.textBox.Visible = true;
            this.textBox.Focus();//获取焦点
            this.textBox.Select(this.textBox.TextLength, 0);
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


        private void LeftPictureBox_MouseEnter(object sender, EventArgs e)
        {
            //string helpInfo = String.Format(DataButtonFlowTemplate,
            //                            0
            //                           );
            //this.helpToolTip.SetToolTip(this.leftPictureBox, helpInfo);
        }

        private void TxtButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            if (e.Clicks == 2) // 双击连接
            {
                if (LinkButtonSelected != null)
                {
                    LinkButtonSelected(this, new SelectLinkButtonEventArgs() { linkButton = this });
                }
            }
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 按下回车键
            if (e.KeyChar == 13)
            {
                FinishTextChange();
            }
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            FinishTextChange();
        }

        private void FinishTextChange()
        {
            if (this.textBox.Text.Trim().Length == 0)
                this.textBox.Text = this.oldTextString;

            if (this.textBox.Text.Length > 125)
            {
                this.textBox.Text = this.oldTextString;
                MessageBox.Show("重命名内容过长,超过125个字节.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.textBox.ReadOnly = true;
            this.textBox.Visible = false;
            this.txtButton.Visible = true;
            if (this.oldTextString == this.textBox.Text)
                return;
            LinkSourceName = this.textBox.Text;
            this.txtButton.Text = Utils.FileUtil.ReName(this.textBox.Text);
            if (this.oldTextString != this.textBox.Text)
            {
                this.oldTextString = this.textBox.Text;
            }
            // 保存
            Global.GetDataSourceControl().SaveExternalData();
            this.helpToolTip.SetToolTip(this.txtButton, LinkSourceName);
        }

        private void OnDatabaseItemChange(DatabaseItem databaseItem)
        {
            if (DatabaseItemChanged != null)
            {
                DatabaseItemChanged(this, new ChangeDatabaseItemEventArgs() { databaseItem = databaseItem });
            }
        }

        private void ConnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinkButtonSelected(this, new SelectLinkButtonEventArgs() { linkButton = this });
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
