using C2.Controls.Left;
using C2.Core;
using System.ComponentModel;
using System.IO;

namespace C2.Controls.C1.Left
{
    class ManualButton : C2Button
    {
        public ManualButton(string c2Title) : base(c2Title)
        {
            FullFilePath = Path.Combine(Global.ManualViewPath, ModelTitle, ModelTitle + ".bmd");
            this.desc = "战术手册";
        }

        protected override void ContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            base.ContextMenuStrip_Opening(sender, e);
            this.DeleteToolStripMenuItem.Enabled = false;
        }
    }
}
