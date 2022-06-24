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
using C2.Business.HIBU.GunDetection;
using C2.Business.HIBU.TerrorismDetection;
using C2.Business.HIBU.TibetanDetection;
using C2.Business.IAOLab.PostAndGet;
using C2.Dialogs.IAOLab;
using C2.Globalization;
using C2.Utils;
using System;
using System.Drawing;
using System.Windows.Forms;
using C2.Business.HIBU.InfoExtraction;
using C2.Business.HIBU.FaceDetector;
using C2.Business.HIBU.FaceBeauty;
using C2.Business.HIBU.FaceExpression;
using C2.Business.HIBU.FaceRecognizer;
using C2.Business.IAOLab.PwdGenerator;

namespace C2.Controls.Left
{
    public delegate void OpenToolFormDelegate();
    public partial class IAOButton : UserControl
    {

        private OpenToolFormDelegate openToolForm;
        private readonly string buttonType;

        public string Type { get => this.txtButton.Text; }
        public string Desc { get => toolTip.GetToolTip(this.rightPictureBox); }

        public OpenToolFormDelegate ShowDialogDelegate { get => openToolForm; set => openToolForm = value; }

        public void SetToolTip(string desc)
        {
            toolTip.SetToolTip(this.rightPictureBox, desc);
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
            toolTip.SetToolTip(this.txtButton, this.txtButton.Text);
            switch (ffp)
            {
                case "APK":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.Apk;
                    toolTip.SetToolTip(this.rightPictureBox, HelpUtil.ApkToolFormHelpInfo);
                    break;
                case "Visualization":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.Visualization;
                    toolTip.SetToolTip(this.rightPictureBox, HelpUtil.VisualizationFormHelpInfo);
                    break;
                case "Wifi":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.Wifi;
                    toolTip.SetToolTip(this.rightPictureBox, HelpUtil.WifiLocationFormHelpInfo);
                    break;
                case "InformationSearch":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.InformationSearch;
                    toolTip.SetToolTip(this.rightPictureBox, HelpUtil.WifiLocationFormHelpInfo);
                    break;
                case "Address":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.Wifi;
                    toolTip.SetToolTip(this.rightPictureBox, HelpUtil.WifiLocationFormHelpInfo);
                    break;
                case "PostAndGet":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.PostAndGet;
                    toolTip.SetToolTip(this.rightPictureBox, HelpUtil.PostAndGetFormHelpInfo);
                    break;
                case "GoldEyes": 
                    this.leftPictureBox.Image = global::C2.Properties.Resources.GoldEyes;
                    toolTip.SetToolTip(this.rightPictureBox, HelpUtil.GoldEyesFormHelpInfo);
                    break;
                case "DownloadTool":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.DownloadTool;
                    toolTip.SetToolTip(this.rightPictureBox, HelpUtil.DownloadToolFormHelpInfo);
                    break;
                case "Card":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.Card;
                    toolTip.SetToolTip(this.rightPictureBox, HelpUtil.BankToolFormHelpInfo);
                    break;
                case "Tude":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.Tude;
                    toolTip.SetToolTip(this.rightPictureBox, HelpUtil.GPSTransformFormHelpInfo);
                    break;
                case "BigAPK":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.BigAPK;
                    toolTip.SetToolTip(this.rightPictureBox, HelpUtil.BigAPKFormHelpInfo);
                    break;
                case "Fraud":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.fraud;
                    toolTip.SetToolTip(this.rightPictureBox, HelpUtil.FraudFormHelpInfo);
                    break;
                case "PwdGenerator":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.dictGenerator;
                    toolTip.SetToolTip(this.rightPictureBox, HelpUtil.PwdGeneratorHelpInfo);
                    break;
                case "图片文本识别":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.OCR;
                    toolTip.SetToolTip(this.rightPictureBox, HelpUtil.OCRFormHelpInfo);
                    break;
                case "命名实体识别":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.NER;
                    toolTip.SetToolTip(this.rightPictureBox, HelpUtil.NERFormHelpInfo);
                    break;
                case "语音转文本":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.ASR;
                    toolTip.SetToolTip(this.rightPictureBox, HelpUtil.ASRFormHelpInfo);
                    break;
                case "二维码识别":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.QRCode;
                    toolTip.SetToolTip(this.rightPictureBox, HelpUtil.QRCodeFormHelpInfo);
                    break;
                case "人脸年龄性别识别":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.FaceAgeGender;
                    toolTip.SetToolTip(this.rightPictureBox, HelpUtil.FaceAgeGenderFormHelpInfo);
                    break;
                case "信息抽取":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.InfoExtraction;
                    toolTip.SetToolTip(this.rightPictureBox, HelpUtil.InfoExtractionFormHelpInfo);
                    break;
                case "语种识别":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.LanguageDetect;
                    toolTip.SetToolTip(this.rightPictureBox, HelpUtil.LanguageDetectFormHelpInfo);
                    break;
                case "涉赌文本识别":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.DrugTextRecognition;
                    toolTip.SetToolTip(this.rightPictureBox, HelpUtil.DrugTextRecognitionFormHelpInfo);
                    break;
                case "涉政文本识别":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.PoliticsTextRecognition;
                    toolTip.SetToolTip(this.rightPictureBox, HelpUtil.PoliticsTextRecognitionFormHelpInfo);
                    break;
                case "涉黄图像识别":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.PornRecognition;
                    toolTip.SetToolTip(this.rightPictureBox, HelpUtil.PornRecognitionFormHelpInfo);
                    break;
                case "轨迹联通类图像识别":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.TrackRecognition;
                    toolTip.SetToolTip(this.rightPictureBox, HelpUtil.TrackRecognitionFormHelpInfo);
                    break;
                case "红包转账图像识别":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.RedPocketRecognition;
                    toolTip.SetToolTip(this.rightPictureBox, HelpUtil.RedPocketRecognitionFormHelpInfo);
                    break;
                case "二维码图像识别":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.QRCodeRecognition;
                    toolTip.SetToolTip(this.rightPictureBox, HelpUtil.QRCodeRecognitionFormHelpInfo);
                    break;
                case "银行卡图像识别":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.BankCardRecognition;
                    toolTip.SetToolTip(this.rightPictureBox, HelpUtil.BankCardRecognitionFormHelpInfo);
                    break;
                case "卡证识别":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.CardRecognition;
                    toolTip.SetToolTip(this.rightPictureBox, HelpUtil.CardRecognitionFormHelpInfo);
                    break;
                case "红头文件识别":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.RedHeaderRecognition;
                    toolTip.SetToolTip(this.rightPictureBox, HelpUtil.RedHeaderRecognitionFormHelpInfo);
                    break;
                case "涉枪图像检测":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.GunDetection;
                    toolTip.SetToolTip(this.rightPictureBox, HelpUtil.GunDetectionFormHelpInfo);
                    break;
                case "涉恐图像识别":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.TerrorismDetection;
                    toolTip.SetToolTip(this.rightPictureBox, HelpUtil.TerrorismDetectionFormHelpInfo);
                    break;
                case "涉藏图像检测":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.TibetanDetection;
                    toolTip.SetToolTip(this.rightPictureBox, HelpUtil.TibetanDetectionFormHelpInfo);
                    break;
                case "人脸检测":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.FaceDetector;
                    toolTip.SetToolTip(this.rightPictureBox, HelpUtil.FaceDetectorFormHelpInfo);
                    break;
                case "颜值打分":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.FaceBeauty;
                    toolTip.SetToolTip(this.rightPictureBox, HelpUtil.FaceBeautyFormHelpInfo);
                    break;
                case "表情识别":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.FaceExpression;
                    toolTip.SetToolTip(this.rightPictureBox, HelpUtil.FaceExpressionFormHelpInfo);
                    break;
                case "人脸识别":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.FaceRecognizer;
                    toolTip.SetToolTip(this.rightPictureBox, HelpUtil.FaceRecognizerFormHelpInfo);
                    break;
            }

        }
        private string Trans(string buttonType)
        {
            switch (buttonType)
            {
                case "BigAPK":
                    return "APK大眼睛";
                case "APK":
                    return "APK小眼睛";
                case "Wifi":
                    return "Wifi查询";
                case "InformationSearch":
                    return "银行卡号查询";
                case "Tude":
                    return "坐标经纬度转换";
                case "PostAndGet":
                    return "POST工具";
                case "PwdGenerator":
                    return "社工生成器";
                case "GoldEyes":
                    return "SEO综合查询";
                case "DownloadTool":
                    return "抖音视频下载";
                default:
                    return string.Empty;
            }
        }

        #region 定义弹窗

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
            if (buttonType == "BigAPK" || buttonType == "APK" || buttonType == "Wifi" || 
                buttonType == "InformationSearch" || buttonType == "Tude" || buttonType == "PostAndGet" || 
                buttonType == "PwdGenerator" || buttonType == "GoldEyes" || buttonType == "DownloadTool")
                new Log.Log().LogManualButton("实验楼" + "-" + Trans(buttonType), "打开");
            else
                new Log.Log().LogManualButton(buttonType, "打开");
            OpenToolForm();
        }

        private void TxtButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                if (buttonType == "BigAPK" || buttonType == "APK" || buttonType == "Wifi" || 
                    buttonType == "InformationSearch" || buttonType == "Tude" || buttonType == "PostAndGet" || 
                    buttonType == "PwdGenerator" || buttonType == "GoldEyes" || buttonType == "DownloadTool")
                    new Log.Log().LogManualButton("实验楼" + "-" + Trans(buttonType), "打开");
                else
                    new Log.Log().LogManualButton(buttonType, "打开");
                OpenToolForm();
            }
        }
        public void OpenToolForm()
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
                    break;
                case "InformationSearch":
                    new InformationSearch() { FormType = buttonType }.ShowDialog();
                    break;
                case "Card":
                    BankToolForm(buttonType).ShowDialog();
                    break;
                case "Tude":
                    GPSTransformForm(buttonType).ShowDialog();
                    break;
                case "PostAndGet":
                    new PostAndGetForm().ShowDialog();
                    break;
                case "GoldEyes":
                    new GoldEyesForm().ShowDialog();
                    break;
                case "DownloadTool":
                    new DownloadToolForm().ShowDialog();
                    break;
                case "Fraud":
                    new FraudDialog().ShowDialog();
                    break;
                case "PwdGenerator":
                    new PwdGeneratorForm().ShowDialog();
                    break;
                case "BigAPK":
                    BigAPKForm().ShowDialog();
                    break;
                case "图片文本识别":
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
                case "信息抽取":
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
                case "涉枪图像检测":
                    new GunDetectionForm().ShowDialog();
                    break;
                case "涉恐图像识别":
                    new TerrorismDetectionForm().ShowDialog();
                    break;
                case "涉藏图像检测":
                    new TibetanDetectionForm().ShowDialog();
                    break;
                case "人脸检测":
                    new FaceDetectorForm().ShowDialog();
                    break;
                case "颜值打分":
                    new FaceBeautyForm().ShowDialog();
                    break;
                case "表情识别":
                    new FaceExpressionForm().ShowDialog();  
                    break;
                case "人脸识别":
                    new FaceRecognizerForm().ShowDialog();
                    break;
                default:
                    openToolForm?.Invoke();
                    break;
            }
        }


    }
}
