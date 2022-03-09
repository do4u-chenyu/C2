using C2.Controls.Left;
using C2.Core;
using System.Collections.Generic;

namespace C2.Forms.Splash
{
    public class HIBUSplashForm : BaseSplashForm 
    {
        public new void ShowDialog()
        {
            List<IAOButton> buttons = Global.GetHIBUControl().IAOButtons;
            foreach (IAOButton button in buttons)
                AddItem(button.Type, button.Desc, button);
            base.ShowDialog();
        }
    }
}
