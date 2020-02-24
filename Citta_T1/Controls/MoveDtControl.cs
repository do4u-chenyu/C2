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
    public partial class MoveDtControl : MoveOpControl
    {
        public string index;
        public MoveDtControl()
        {
            InitializeComponent();
        }

        public new void InitializeOpPinPicture()
        {
            SetOpControlName(this.textBox1.Text);
            this.Controls.Remove(this.leftPinPictureBox);
        }
        public override void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 按下回车键
            if (e.KeyChar == 13)
            {
                if (this.textBox1.Text.Length == 0)
                    return;
                this.textBox1.ReadOnly = true;
                SetOpControlName(this.textBox1.Text);
                this.textBox1.Visible = false;
                this.txtButton.Visible = true;
                // 数据button
                ReNameDataButton(index, this.textBox1.Text);
            }
        }
        public override void textBox1_Leave(object sender, EventArgs e)
        {
            if (this.textBox1.Text.Length == 0)
                return;
            this.textBox1.ReadOnly = true;
            SetOpControlName(this.textBox1.Text);
            this.textBox1.Visible = false;
            this.txtButton.Visible = true;
            // 数据button
            ReNameDataButton(index, this.textBox1.Text);
        }
        public override void txtButton_Click(object sender, EventArgs e)
        {
            // TODO 一层一层找爸爸方法有点蠢
            // TODO 需要所有数据控件都更新名称
            MainForm prt = (MainForm)Parent.Parent;
            prt.OverViewDataByIndex(this.index);
            System.Console.WriteLine("[MoveDtControl] isClicked:" + isClicked);
            if (isClicked)
            {
                TimeSpan span = DateTime.Now - clickTime;
                clickTime = DateTime.Now;
                if (span.TotalMilliseconds < SystemInformation.DoubleClickTime)

                //  把milliseconds改成totalMilliseconds 因为前者不是真正的时间间隔，totalMilliseconds才是真正的时间间隔
                {
                    重命名ToolStripMenuItem_Click_1(this, e);
                    isClicked = false;
                }
            }
            else
            {
                isClicked = true;
                clickTime = DateTime.Now;
            }

        }
        public void ReNameDataButton(string index, string dstName)
        {
            Console.WriteLine("MoveDtControl重命名");
            // 修改数据字典里的数据
            Citta_T1.Data data = Program.inputDataDict[index];
            data.dataName = dstName;
            string srcName = data.dataName;
            Program.inputDataDictN2I.Remove(srcName);
            Program.inputDataDictN2I.Add(dstName, index);
            // 修改DataSourceControl.cs中的展示名称
            MainForm prt = (MainForm)Parent.Parent;
            prt.RenameDataButton(this.index, dstName);
        }
    }
}
