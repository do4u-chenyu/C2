using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Citta_T1.Utils;

namespace Citta_T1.Controls.Move
{

    public partial class MoveDtControl : MoveOpControl
    {
        private System.Windows.Forms.ToolStripMenuItem overViewMenuItem;
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MoveOpControl));
        public string MDCName { get => this.textBox1.Text; }


        public string GetBcpPath()
        {
            return this.Name;
        }

        public MoveDtControl(string bcpPath, int sizeL, string name, Point loc)
        {
            InitializeComponent();
            AddOverViewToMenu();
            this.textBox1.Text = name;
            this.Location = loc;
            this.Name = bcpPath;
            this.doublelPinFlag = doublePin.Contains(this.textBox1.Text.ToString());
            InitializeOpPinPicture();
            ResetSize(sizeL);
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
            this.overViewMenuItem.Click += new System.EventHandler(this.PreViewMenuItem_Click);
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
            Global.GetMainForm().SetDocumentDirty();

        }
        public void txtButton_Click(object sender, EventArgs e)
        {

            // TODO 一层一层找爸爸方法有点蠢
            MainForm prt = (MainForm)Parent.Parent;
            prt.PreViewDataByBcpPath(this.GetBcpPath());
            System.Console.WriteLine("[MoveDtControl] isClicked:" + isClicked);
            if (isClicked)
            {
                TimeSpan span = DateTime.Now - clickTime;
                clickTime = DateTime.Now;
                if (span.TotalMilliseconds < SystemInformation.DoubleClickTime)

                //  把milliseconds改成totalMilliseconds 因为前者不是真正的时间间隔，totalMilliseconds才是真正的时间间隔
                {
                    RenameMenuItem_Click(this, e);
                    isClicked = false;
                }
            }
            else
            {
                isClicked = true;
                clickTime = DateTime.Now;
            }

        }

        public void PreViewMenuItem_Click(object sender, EventArgs e)
        {
            MainForm prt = (MainForm)Parent.Parent;
            prt.PreViewDataByBcpPath(this.Name);
        }

        public override void rightPictureBox_MouseEnter(object sender, EventArgs e)
        {
            this.nameToolTip.SetToolTip(this.rightPictureBox, this.Name);
        }
        public override void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            base.DeleteMenuItem_Click(sender, e);
        }
    }
}
