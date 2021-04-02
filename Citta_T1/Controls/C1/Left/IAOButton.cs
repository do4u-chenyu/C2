using C2.Dialogs.IAOLab;
using C2.Globalization;
using C2.IAOLab.Plugins;
using C2.Utils;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
namespace C2.Controls.Left
{
    public delegate void OpenToolFormDelegate();
    public partial class IAOButton : UserControl
    {

        private OpenToolFormDelegate openToolForm;
        private string buttonType;

        public OpenToolFormDelegate ShowDialogDelegate { get => openToolForm; set => openToolForm = value; }

        public void SetToolTip(string desc)
        {
            toolTip1.SetToolTip(this.rightPictureBox, desc);
        }

        public void SetIcon(Image icon)
        {
            this.leftPictureBox.Image = icon;
        }

        public IAOButton(string ffp)
        {
            InitializeComponent();
            buttonType = ffp;
            this.ContextMenuStrip = contextMenuStrip1;
            this.txtButton.Text = Lang._(ffp);
            switch (ffp)
            {
                case "APK":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.Apk;
                    toolTip1.SetToolTip(this.rightPictureBox, HelpUtil.ApkToolFormHelpInfo);
                    break;
                case "BaseStation":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.BaseStation;
                    toolTip1.SetToolTip(this.rightPictureBox, HelpUtil.BaseStationFormHelpInfo);
                    break;
                case "Wifi":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.Wifi;
                    toolTip1.SetToolTip(this.rightPictureBox, HelpUtil.WifiLocationFormHelpInfo);
                    break;
                case "Card":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.Card;
                    toolTip1.SetToolTip(this.rightPictureBox, HelpUtil.BankToolFormHelpInfo);
                    break;
                case "Tude":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.Tude;
                    toolTip1.SetToolTip(this.rightPictureBox, HelpUtil.GPSTransformFormHelpInfo);
                    break;
                case "Ip":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.Ip;
                    toolTip1.SetToolTip(this.rightPictureBox, HelpUtil.TimeAndIPTransformFormHelpInfo);
                    break;
                case "BigAPK":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.BigAPK;
                    toolTip1.SetToolTip(this.rightPictureBox, HelpUtil.BigAPKFormHelpInfo);
                    break;
            }

        }
        #region 定义弹窗
        private Form BaseStationForm(string formType)
        {
           return new WifiLocation()
            {
                Text = "基站查询",
                InputLable = "请在下方输入基站号码",
                Tip = HelpUtil.BaseStationHelpInfo,
                FormType = formType
            };

        }
        private Form BankToolForm(string formType)
        {
            return new WifiLocation()
            {
                Text = "银行卡信息查询",
                InputLable = "请在下方输入银行卡",
                Tip = HelpUtil.BankToolHelpInfo,
                FormType = formType
            };

        }
        private Form GPSTransformForm(string formType)
        {
            return new CoordinateConversion()
            {
                Tab0Tip = HelpUtil.GPSTransformHelpInfo,
                Tib1Tip = HelpUtil.GPSDistanceHelpInfo,
                FormType = formType
            };

        }
        private Form TimeAndIPTransformForm(string formType)
        {
            CoordinateConversion form = new CoordinateConversion()
            {
                Tab0Tip = HelpUtil.IPTransformHelpInfo,
                Tib1Tip = HelpUtil.TimeTransformHelpInfo,
                FormType = formType
            };
            form.ReLayoutForm();
            return form;

        }

        private Form BigAPKForm()
        {
            return new BigAPKForm() { WindowState = FormWindowState.Maximized };
        }
        #endregion
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenToolForm();
        }

        private void TxtButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
                OpenToolForm();
        }
        private void OpenToolForm()
        {
            switch (buttonType)
            {
                case "APK":
                    new ApkTool().ShowDialog();
                    break;
                case "BaseStation":
                    BaseStationForm(buttonType).ShowDialog();
                    break;
                case "Wifi":
                    new WifiLocation() { FormType = buttonType }.ShowDialog();
                    break;
                case "Card":
                    BankToolForm(buttonType).ShowDialog();
                    break;
                case "Tude":
                    GPSTransformForm(buttonType).ShowDialog();
                    break;
                case "Ip":
                    TimeAndIPTransformForm(buttonType).ShowDialog();
                    break;
                case "BigAPK":
                    BigAPKForm().ShowDialog();
                    break;
                default:
                    openToolForm?.Invoke();
                    break;
            }
        }


    }
}
