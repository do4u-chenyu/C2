using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Citta_T1.Controls
{
    public partial class DataButton : UserControl
    {
        private string index;
        public DataButton()
        {
            InitializeComponent();
        }
        //public DataButton(string n, string i, Delegate d)
        //{
        //    InitializeComponent();
        //    this.txtButton.Text = n;
        //    this.index = i;
        //    this.txtButton.MouseDown += new System.Windows.Forms.MouseEventHandler(d);
        //}
        private void moveOpControl1_Load(object sender, EventArgs e)
        {

        }

        private void rightPictureBox_MouseEnter(object sender, EventArgs e)
        {
            String helpInfo = Program.inputDataDict[txtButton.Name].filePath;
            this.helpToolTip.SetToolTip(this.rightPictureBox, helpInfo);
        }
        #region 右键菜单
        private void ReviewToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void RenameToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Parent.Controls.Remove(this);
        }
        #endregion
    }
}
