using C2.Controls.C1.Left;
using C2.Core;
using C2.Utils;
using System.Collections.Generic;

namespace C2.Forms.Splash
{
    public class  ManualSplashForm : BaseSplashForm
    {
        public ManualSplashForm()
        {
            this.Height -= 20;
        }
        public new void ShowDialog()
        {
            List<ManualButton> buttons = Global.GetManualControl().ManualButtons;
            foreach (ManualButton button in buttons)
                AddItem(button.Type, button.Desc, button);
            base.ShowDialog();
        }

        protected override void OpenItem(object button)
        {
            if (button is ManualButton)
            {
                using (GuarderUtil.WaitCursor)
                    (button as ManualButton).Open();
                this.Close();
            }
                
        }
    }
}
