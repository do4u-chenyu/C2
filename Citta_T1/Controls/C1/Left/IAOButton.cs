
using C2.Business.Model;
using C2.Core;
using C2.Model;
using C2.Utils;
using System;
using System.Windows.Forms;

namespace C2.Controls.Left
{
    public partial class IAOButton : UserControl
    {
 
        public IAOButton(string ffp)
        {
            InitializeComponent();
            txtButton.Name = ffp;
            txtButton.Text = ffp;
            switch (ffp)
            {
                case "APK":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.Apk;
                    break;
                case "BaseStation":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.BaseStation;
                    break;
                case "Wifi":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.Wifi;
                    break;
                case "Card":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.Card;
                    break;
                case "Tude":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.Tude;
                    break;
                case "Ip":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.Ip;
                    break;
            }
        }

    }
}
