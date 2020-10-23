using C2.Business.Model;
using C2.Controls;
using C2.Controls.Flow;
using C2.Controls.Move;
using C2.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace C2.Forms
{
    public partial class CanvasForm : BaseForm
    {
        public ModelDocument Document { get; set; }
        private readonly string userInfoPath = Path.Combine(Global.WorkspaceDirectory, "UserInformation.xml");
        public CanvasForm(ModelDocument modelDoc)
        {
            InitializeComponent();
            this.Document = modelDoc;
            this.canvasPanel.Document = modelDoc;
            this.SizeChanged += new EventHandler(CanvasForm_SizeChanged);
        }
        private void CanvasForm_SizeChanged(object sender, System.EventArgs e)
        {
            Point rightTop = new Point(this.canvasPanel.Width, 0);
            Point leftBottom = new Point(0, this.canvasPanel.Height);
            int x = rightTop.X - 10 - this.naviViewControl.Width;
            int y = leftBottom.Y - 5 - this.naviViewControl.Height;
            // 缩略图定位
            this.naviViewControl.Location = new Point(x, y);

            this.naviViewControl.Invalidate();
            // 底层工具按钮定位
            int delta = 50;
            x = x - (this.canvasPanel.Width) / 2 + 100;
            this.runButton.Location = new Point(x, y + delta);
            this.stopButton.Location = new Point(x + delta, y + delta);
            this.resetButton.Location = new Point(x + delta * 2, y + delta);

            // 顶层浮动工具栏和右侧工具及隐藏按钮定位
            Point loc = new Point(rightTop.X - 70 - this.canvasPanel.flowControl1.Width, rightTop.Y + 50);
            Point loc_flowcontrol2 = new Point(rightTop.X - this.rightShowButton.Width, loc.Y);
            Point loc_flowcontrol3 = new Point(loc_flowcontrol2.X, loc.Y + this.rightHideButton.Width + 10);
            Point loc_panel3 = new Point(loc.X, loc.Y + this.canvasPanel.flowControl1.Height + 10);
            this.canvasPanel.flowControl1.Location = loc;
            this.rightShowButton.Location = loc_flowcontrol2;
            this.rightHideButton.Location = loc_flowcontrol3;
            this.remarkControl.Location = loc_panel3;

        }

        public string SaveCurrentDocument()
        {
            Document.Save();
            Document.Dirty = false;
            return Document.ModelTitle;
        }
        public ModelElement AddDocumentOperator(MoveBaseControl ct)
        {
            ct.ID = this.Document.ElementCount++;
            ModelElement e = ModelElement.CreateModelElement(ct);
            Document.AddModelElement(e);
            return e;
        }

        public void UpdateRemark(RemarkControl remarkControl)
        {
            if (this.Document != null)
                this.Document.RemarkDescription = remarkControl.RemarkDescription;
        }

        public string RemarkDescription => Document == null ? String.Empty : Document.RemarkDescription;

        public bool WithoutDocumentLogin(string userName)
        {
            //新用户登录
            string userDir = Path.Combine(Global.WorkspaceDirectory, userName);
            if (!Directory.Exists(userDir))
                return true;
            //非新用户但无模型文档
            DirectoryInfo di = new DirectoryInfo(userDir);
            DirectoryInfo[] directoryInfos = di.GetDirectories();
            return (directoryInfos.Length == 0);
        }
        public string[] LoadSaveModelTitle(string userName)
        {
            List<string> modelTitleList = new List<string>();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(userInfoPath);

            XmlNodeList userNode = xDoc.GetElementsByTagName("user");
            foreach (XmlNode xn in userNode)
            {
                if (xn.SelectSingleNode("name") != null && xn.SelectSingleNode("name").InnerText == userName)
                {
                    XmlNodeList childNodes = xn.SelectNodes("modeltitle");
                    foreach (XmlNode xn2 in childNodes)
                    {
                        string modelTitle = xn2.InnerText;
                        if (Directory.Exists(System.IO.Path.Combine(Global.WorkspaceDirectory, userName, modelTitle)))
                            modelTitleList.Add(modelTitle);
                    }
                    if (modelTitleList.Count > 0)
                        return modelTitleList.Distinct().ToArray();
                }
            }
            return LoadAllModelTitle(userName);
        }
        public string[] LoadAllModelTitle(string userName)
        {
            string[] modelTitles = new string[0];
            try
            {
                DirectoryInfo userDir = new DirectoryInfo(Path.Combine(Global.WorkspaceDirectory, userName));
                DirectoryInfo[] dir = userDir.GetDirectories();
                modelTitles = Array.ConvertAll(dir, value => Convert.ToString(value));
            }
            catch { }
            return modelTitles;
        }
    }
}
