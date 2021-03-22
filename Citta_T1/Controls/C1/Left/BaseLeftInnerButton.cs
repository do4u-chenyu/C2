using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using C2.Core;
using C2.Utils;

namespace C2.Controls.C1.Left
{
    public partial class BaseLeftInnerButton : UserControl
    {
        public string ButtonText { get => this.noFocusButton.Text; set => this.noFocusButton.Text = value; }

        public BaseLeftInnerButton()
        {
            InitializeComponent();
        }

        public BaseLeftInnerButton(string buttonText) : this()
        {
            ButtonText = buttonText;
        }

        private void ContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            SetMenuStripEnable();
        }

        virtual public void SetMenuStripEnable()
        {

            return;
        }

    }
}
