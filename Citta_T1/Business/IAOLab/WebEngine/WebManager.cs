using C2.Core;
using C2.IAOLab.WebEngine.Dialogs;
using C2.Model;
using C2.Model.MindMaps;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.IAOLab.WebEngine
{
    class WebManager
    {
        public enum WebType
        {
            Null,
            Map,   //图上作战
            Boss   //数据大屏
        }

        public WebType Type;
        public string WebUrl;
        public Topic HitTopic;
        private WebBrowserDialog wbDialog;
        private string defaultMapUrl = Path.Combine(Application.StartupPath, "Business\\IAOLab\\WebEngine\\Html", "StartMap.html");
        private string defaultBossUrl = "";

        public WebManager()
        {
            Type = WebType.Null;
            WebUrl = string.Empty;
        }

        public void OpenWebBrowser()
        {
            switch (Type)
            {
                case WebType.Boss:
                    wbDialog = new WebBrowserDialog(HitTopic, Type)
                    {
                        Title = "数据大屏",
                        WebUrl = string.IsNullOrEmpty(WebUrl) ? defaultBossUrl : WebUrl,
                    };
                    wbDialog.InitializeBossToolStrip();
                    break;
                case WebType.Map:
                    wbDialog = new WebBrowserDialog(HitTopic, Type)
                    {
                        Title = "图上作战",
                        WebUrl = string.IsNullOrEmpty(WebUrl) ? defaultMapUrl : WebUrl,
                    };
                    wbDialog.InitializeMapToolStrip();
                    break;
                default:
                    return;
            }
            if (wbDialog.ShowDialog() == DialogResult.OK)
                Global.OnModifiedChange(); //数据大屏生成图片挂件，地图生成打标文档，需要置dirty
        }
    }
}
