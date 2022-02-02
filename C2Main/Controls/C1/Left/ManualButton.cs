using C2.Controls.Left;
using C2.Core;
using System;
using System.IO;
using System.Windows.Forms;

namespace C2.Controls.C1.Left
{
    public class ManualButton : C2Button
    {
        public ManualButton(string c2Title) : base(c2Title)
        {
            FullFilePath = Path.Combine(Global.ManualViewPath, ModelTitle, ModelTitle + ".bmd");
            this.desc = "战术手册";
            this.contextMenuStrip.Items.Remove(DeleteToolStripMenuItem);
            this.toolTip1.SetToolTip(this.lelfPictureBox, "经典分析战术");
            this.lelfPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.lelfPictureBox.Image = global::C2.Properties.Resources.战术手册;
        }


        protected override void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Global.GetMainForm().OpenManualDocument(FullFilePath);
        }

        protected override void TextButton_MouseDown(object sender, MouseEventArgs e)
        {
            // 鼠标左键双击触发
            if (e.Button != MouseButtons.Left || e.Clicks != 2)
                return;
            // 双击打开对应模型
            Global.GetMainForm().OpenManualDocument(FullFilePath);
        }
    }

    
}
