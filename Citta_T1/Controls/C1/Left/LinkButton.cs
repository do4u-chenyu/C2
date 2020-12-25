
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
        public string FullFilePath { get => this.txtButton.Name; set => this.txtButton.Name = value; }
        public string LinkSourceName { get; set; }
        public int Count { get => this.count; set => this.count = value; }
        public DatabaseItem DatabaseItem { get; set; }

        public LinkButton(DatabaseItem item)
        {
            InitializeComponent();
            DatabaseItem =item;
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
            // TODO 这一块先不做，按设计来说模型文档是不可以导入数据的，检测引用要看业务视图
            //int count = Global.GetModelDocumentDao().CountDataSourceUsage(this.FullFilePath);
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

            //// 卸载数据源
            //Global.GetDataSourceControl().RemoveDataButton(this);
            // 引用不为0时,有可能还会预览该数据源的数据,此时不用移除buffer
            //if (count == 0)//不管是否有引用，均清空缓存
            BCPBuffer.GetInstance().Remove(this.FullFilePath);
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

            if (e.Clicks == 1) // 单击拖拽
            {
                // 使用`DataObject`对象来传参数，更加自由
                DataObject dragDropDataTable = new DataObject();
                dragDropDataTable.SetData("Type", ElementType.DataSource);
                dragDropDataTable.SetData("Path", FullFilePath);    // 数据源文件全路径
                dragDropDataTable.SetData("Text", LinkSourceName);  // 数据源名称
                this.txtButton.DoDragDrop(dragDropDataTable, DragDropEffects.Copy | DragDropEffects.Move);
            }
            //else if (e.Clicks == 2)
            //{   // 双击改名 
            //    RenameToolStripMenuItem_Click(sender, e);
            //}
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
       
    }
}
