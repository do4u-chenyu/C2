
using C2.Business.Model;
using C2.Core;
using C2.Globalization;
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
            this.leftPictureBox.Image = global::C2.Properties.Resources.Apk;
            switch (ffp)
            {
                case "APK":
                    this.txtButton.Text = Lang._("APK");
                    this.leftPictureBox.Image = global::C2.Properties.Resources.Apk;
                    break;
                case "BaseStation":
                    this.txtButton.Text = Lang._("BaseStation");
                    this.leftPictureBox.Image = global::C2.Properties.Resources.BaseStation;
                    break;
                case "Wifi":
                    this.txtButton.Text = Lang._("Wifi");
                    this.leftPictureBox.Image = global::C2.Properties.Resources.Wifi;
                    break;
                case "Card":
                    this.txtButton.Text = Lang._("Card");
                    this.leftPictureBox.Image = global::C2.Properties.Resources.Card;
                    break;
                case "Tude":
                    this.txtButton.Text = Lang._("Tude");
                    this.leftPictureBox.Image = global::C2.Properties.Resources.Tude;
                    break;
                case "Ip":
                    this.txtButton.Text = Lang._("Ip");
                    this.leftPictureBox.Image = global::C2.Properties.Resources.Ip;
                    break;
            }
        }

    }
}
