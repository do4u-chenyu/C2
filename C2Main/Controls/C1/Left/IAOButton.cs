using C2.Business.IAOLab.Visualization.Dialogs;
using C2.Business.HIBU.OCR;
using C2.Business.HIBU.NER;
using C2.Business.HIBU.ASR;
using C2.Business.HIBU.QRCode;
using C2.Business.HIBU.FaceAgeGender;
using C2.Business.HIBU.LanguageDetect;
using C2.Business.HIBU.DrugTextRecognition;
using C2.Business.HIBU.PoliticsTextRecognition;
using C2.Business.HIBU.PornRecognition;
using C2.Business.HIBU.TrackRecognition;
using C2.Business.HIBU.RedPocketRecognition;
using C2.Business.HIBU.QRCodeRecognition;
using C2.Business.HIBU.BankCardRecognition;
using C2.Business.HIBU.CardRecognition;
using C2.Business.HIBU.RedHeaderRecognition;
using C2.Dialogs.IAOLab;
using C2.Globalization;
using C2.Utils;
using System;
using System.Drawing;
using System.Windows.Forms;
using C2.Business.HIBU.InfoExtraction;

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
            toolTip1.SetToolTip(this.txtButton, this.txtButton.Text);
            switch (ffp)
            {
                case "APK":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.Apk;
                    toolTip1.SetToolTip(this.rightPictureBox, HelpUtil.ApkToolFormHelpInfo);
                    break;
                case "Visualization":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.Visualization;
                    toolTip1.SetToolTip(this.rightPictureBox, HelpUtil.VisualizationFormHelpInfo);
                    break;
                case "Wifi":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.Wifi;
                    toolTip1.SetToolTip(this.rightPictureBox, HelpUtil.WifiLocationFormHelpInfo);
                    break;
                case "Address":
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
                //case "Ip":
                //    this.leftPictureBox.Image = global::C2.Properties.Resources.Ip;
                //    toolTip1.SetToolTip(this.rightPictureBox, HelpUtil.TimeAndIPTransformFormHelpInfo);
                //    break;
                case "BigAPK":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.BigAPK;
                    toolTip1.SetToolTip(this.rightPictureBox, HelpUtil.BigAPKFormHelpInfo);
                    break;
                case "OCR":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.OCR;
                    toolTip1.SetToolTip(this.rightPictureBox, HelpUtil.OCRFormHelpInfo);
                    break;
                case "命名实体识别":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.NER;
                    toolTip1.SetToolTip(this.rightPictureBox, HelpUtil.NERFormHelpInfo);
                    break;
                case "语音转文本":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.ASR;
                    toolTip1.SetToolTip(this.rightPictureBox, HelpUtil.ASRFormHelpInfo);
                    break;
                case "二维码识别":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.QRCode;
                    toolTip1.SetToolTip(this.rightPictureBox, HelpUtil.QRCodeFormHelpInfo);
                    break;
                    
                case "人脸年龄性别识别":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.FaceAgeGender;
                    toolTip1.SetToolTip(this.rightPictureBox, HelpUtil.FaceAgeGenderFormHelpInfo);
                    break;
                case "InfoExtraction":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.InfoExtraction;
                    toolTip1.SetToolTip(this.rightPictureBox, HelpUtil.InfoExtractionFormHelpInfo);
                    break;
                case "语种识别":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.LanguageDetect;
                    toolTip1.SetToolTip(this.rightPictureBox, HelpUtil.LanguageDetectFormHelpInfo);
                    break;
                case "涉赌文本识别":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.DrugTextRecognition;
                    toolTip1.SetToolTip(this.rightPictureBox, HelpUtil.DrugTextRecognitionFormHelpInfo);
                    break;
                case "涉政文本识别":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.PoliticsTextRecognition;
                    toolTip1.SetToolTip(this.rightPictureBox, HelpUtil.PoliticsTextRecognitionFormHelpInfo);
                    break;
                case "涉黄图像识别":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.PornRecognition;
                    toolTip1.SetToolTip(this.rightPictureBox, HelpUtil.PornRecognitionFormHelpInfo);
                    break;
                case "轨迹联通类图像识别":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.TrackRecognition;
                    toolTip1.SetToolTip(this.rightPictureBox, HelpUtil.TrackRecognitionFormHelpInfo);
                    break;
                case "红包转账图像识别":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.RedPocketRecognition;
                    toolTip1.SetToolTip(this.rightPictureBox, HelpUtil.RedPocketRecognitionFormHelpInfo);
                    break;
                case "二维码图像识别":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.QRCodeRecognition;
                    toolTip1.SetToolTip(this.rightPictureBox, HelpUtil.QRCodeRecognitionFormHelpInfo);
                    break;
                case "银行卡图像识别":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.BankCardRecognition;
                    toolTip1.SetToolTip(this.rightPictureBox, HelpUtil.BankCardRecognitionFormHelpInfo);
                    break;
                case "卡证识别":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.CardRecognition;
                    toolTip1.SetToolTip(this.rightPictureBox, HelpUtil.CardRecognitionFormHelpInfo);
                    break;
                case "红头文件识别":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.RedHeaderRecognition;
                    toolTip1.SetToolTip(this.rightPictureBox, HelpUtil.RedHeaderRecognitionFormHelpInfo);
                    break;
            }

        }
        
        #region 定义弹窗
        private Form BaseStationForm(string formType)
        {
            return new WifiLocation()
            {
                TabControlVisible = true,
                TipBS = HelpUtil.BaseStationHelpInfo,
                FormType = formType
            };

        }

        private Form BankToolForm(string formType)
        {
            return new WifiLocation()
            {
                TabControlVisible = false,
                Text = "银行卡信息查询",
                TipBank = HelpUtil.BankToolHelpInfo,
                FormType = formType
            };

        }
        private Form GPSTransformForm(string formType)
        {
            return new CoordinateConversion()
            {
                Tab0Tip = HelpUtil.GPSTransformHelpInfo,
                Tib1Tip = HelpUtil.GPSDistanceHelpInfo,
                IPTip = HelpUtil.IPTransformHelpInfo,
                TimeTip = HelpUtil.TimeTransformHelpInfo,
                FormType = formType
            };

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
                case "Visualization":
                    new ShowChartDialog().ShowDialog();
                    break;
                case "Wifi":
                    new WifiLocation() { FormType = buttonType }.ShowDialog();
                    //new WifiLocation().ShowDialog();
                    break;
                case "Card":
                    BankToolForm(buttonType).ShowDialog();
                    break;
                case "Tude":
                    GPSTransformForm(buttonType).ShowDialog();
                    break;
                case "BigAPK":
                    BigAPKForm().ShowDialog();
                    break;
                case "OCR":
                    new OCRForm().ShowDialog();
                    break;
                case "命名实体识别":
                    new NERForm().ShowDialog();
                    break;
                case "语音转文本":
                    new ASRForm().ShowDialog();
                    break;
                case "二维码识别":
                    new QRCodeForm().ShowDialog();
                    break;
                case "人脸年龄性别识别":
                    new FaceAgeGenderForm().ShowDialog();
                    break;
                case "InfoExtraction":
                    new InfoExtractionForm().ShowDialog();
                    break;
                case "语种识别":
                    new LanguageDetectForm().ShowDialog();
                    break;
                case "涉赌文本识别":
                    new DrugTextRecognitionForm().ShowDialog();
                    break;
                case "涉政文本识别":
                    new PoliticsTextRecognitionForm().ShowDialog();
                    break;
                case "涉黄图像识别":
                    new PornRecognitionForm().ShowDialog();
                    break;
                case "轨迹联通类图像识别":
                    new TrackRecognitionForm().ShowDialog();
                    break;
                case "红包转账图像识别":
                    new RedPocketRecognitionForm().ShowDialog();
                    break;
                case "二维码图像识别":
                    new QRCodeRecognitionForm().ShowDialog();
                    break;
                case "银行卡图像识别":
                    new BankCardRecognitionForm().ShowDialog();
                    break;
                case "卡证识别":
                    new CardRecognitionForm().ShowDialog();
                    break;
                case "红头文件识别":
                    new RedHeaderRecognitionForm().ShowDialog();
                    break;
                default:
                    openToolForm?.Invoke();
                    break;
            }
        }


    }
}
