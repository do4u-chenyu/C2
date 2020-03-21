using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Citta_T1.Controls.Left
{
    public partial class DataButton : UserControl
    {
        public bool isUTF8;
        private int count = 0;
        public bool Encoding { get => this.isUTF8; set => this.isUTF8 = value; }
        public string FilePath { get => this.txtButton.Name; set => this.txtButton.Name = value; }
        public string DataName { get => this.txtButton.Text; set => this.txtButton.Text = value; }
        public int Count { get => this.count; set => this.count = value; }

        public DataButton()
        {
            InitializeComponent();
        }
        public DataButton(string ffp, string dataName, bool isutf8)
        {
            InitializeComponent();
            txtButton.Name = ffp;
            txtButton.Text = dataName;
            this.isUTF8 = isutf8;
        }
        private void moveOpControl1_Load(object sender, EventArgs e)
        {

        }

        private void rightPictureBox_MouseEnter(object sender, EventArgs e)
        {
            //String helpInfo = Program.inputDataDict[txtButton.Name].filePath;
            String helpInfo = txtButton.Name;
            this.helpToolTip.SetToolTip(this.rightPictureBox, helpInfo);
        }
        #region 右键菜单
        private void ReviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // TODO 1. 怎么呈现这个预览？

        }

        private void RenameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 1. DataSource中重命名
            // 2. Program中重命名
            // TODO 3. 画布中已存在的该如何处理？ 
            ((DataButton)(this.Parent.Controls.Find(this.Name, false)[0])).txtButton.Text = "重命名";
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 1. DataSource中删除控件
            // 2. Program中删除数据
            // TODO 3. 画布中已存在的该如何处理？ 
            this.Parent.Controls.Remove(this);
            Program.DataPreviewDict.Remove(this.txtButton.Name);

        }
        #endregion
    }
}
