using C2.Controls.Left;
using C2.Core;
using System.IO;

namespace C2.Controls.C1.Left
{
    public class ManualButton : C2Button
    {
        public ManualButton(string c2Title) : base(c2Title)
        {
            FullFilePath = Path.Combine(Global.ManualViewPath, ModelTitle, ModelTitle + ".bmd");
            this.desc = "战术手册";
            this.contextMenuStrip.Items.Remove(DeleteToolStripMenuItem);
        }
    }
}
