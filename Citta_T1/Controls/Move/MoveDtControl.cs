using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Citta_T1.Controls.Move
{
    public delegate void DtDocumentDirtyEventHandler();
    public partial class MoveDtControl : MoveOpControl
    {
        public string index;
        private System.Windows.Forms.ToolStripMenuItem overViewMenuItem;
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MoveOpControl));
        public string GetIndex { get =>index; }
        public string mdControlName { get => this.textBox1.Text; }
        public event DtDocumentDirtyEventHandler DtDocumentDirtyEvent; 
        public MoveDtControl()
        {
            InitializeComponent();
            AddOverViewToMenu();
        }
        public MoveDtControl(string idx, int sizeL, string text, Point p)
        {
            InitializeComponent();
            AddOverViewToMenu();
            textBox1.Text = text;
            Location = p;
            index = idx;
            doublelPinFlag = doublePin.Contains(this.textBox1.Text.ToString());
            InitializeOpPinPicture();
            resetSize(sizeL);
            Console.WriteLine("Create a MoveDtControl, sizeLevel = " + sizeLevel);
        }
        public new void InitializeOpPinPicture()
        {
            SetOpControlName(this.textBox1.Text);
            this.Controls.Remove(this.leftPinPictureBox);
        }

        private void AddOverViewToMenu()
        {
            this.overViewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.overViewMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.overViewMenuItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("菜单1ToolStripMenuItem.BackgroundImage")));
            this.overViewMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.overViewMenuItem.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.overViewMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.overViewMenuItem.Name = "菜单1ToolStripMenuItem";
            this.overViewMenuItem.Size = new System.Drawing.Size(133, 24);
            this.overViewMenuItem.Text = "预览";
            this.overViewMenuItem.Click += new System.EventHandler(this.overViewMenuItem_Click);
            this.contextMenuStrip.Items.Insert(0, this.overViewMenuItem);
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

        public void overViewMenuItem_Click(object sender, EventArgs e)
        {
            MainForm prt = (MainForm)Parent.Parent;
            prt.OverViewDataByIndex(this.index);
        }

        public override void rightPictureBox_MouseEnter(object sender, EventArgs e)
        {
            String helpInfo = Program.inputDataDict[index].filePath;
            this.nameToolTip.SetToolTip(this.rightPictureBox, helpInfo);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DtDocumentDirtyEvent?.Invoke();
        }

        private void MoveDtControl_LocationChanged(object sender, EventArgs e)
        {
            DtDocumentDirtyEvent?.Invoke();
        }
        public override void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            base.删除ToolStripMenuItem_Click(sender, e);
            DtDocumentDirtyEvent?.Invoke();

        }
        
    }
}
