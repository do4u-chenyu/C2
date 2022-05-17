using C2.Controls.Left;
using C2.Core;
using System;
using System.IO;
using System.Windows.Forms;
using C2.Log;

namespace C2.Controls.C1.Left
{
    public class ManualButton : C2Button
    {
        private static string modelName;
        public ManualButton(string c2Title) : base(c2Title)
        {
            FullFilePath = Path.Combine(Global.ManualViewPath, ModelTitle, ModelTitle + ".bmd");
            this.desc = Global.ManualDesc;
            this.contextMenuStrip.Items.Remove(DeleteToolStripMenuItem);
            this.toolTip1.SetToolTip(this.lelfPictureBox, "经典分析战术");
            this.lelfPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            this.lelfPictureBox.Image = Properties.Resources.战术手册;
            modelName = ModelTitle;
        }


        protected override void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Open();
            new Log.Log().LogManualButton("战术手册" + "-" + modelName, "01");
        }

        protected override void TextButton_MouseDown(object sender, MouseEventArgs e)
        {
            // 鼠标左键双击触发
            if (e.Button != MouseButtons.Left || e.Clicks != 2)
                return;
            // 双击打开对应模型
            Open();
            new Log.Log().LogManualButton("战术手册" + "-" + modelName, "01");
        }

        public void Open()
        {
            Global.GetMainForm().OpenManualDocument(FullFilePath);
        }
    }

    
}
