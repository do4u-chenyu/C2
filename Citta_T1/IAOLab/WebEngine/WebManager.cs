using C2.IAOLab.WebEngine.Dialogs;
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
        private WebBrowserDialog wbDialog;
        private string defaultMapUrl = Path.Combine(Application.StartupPath, "IAOLab\\WebEngine\\Html", "StartMap.html");
        private string defaultBossUrl = "https://www.google.com";

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
                    wbDialog = new WebBrowserDialog()
                    {
                        Title = "数据大屏",
                        WebUrl = string.IsNullOrEmpty(WebUrl) ? defaultBossUrl : WebUrl
                    };
                    wbDialog.InitializeBossToolStrip();
                    break;
                case WebType.Map:
                    wbDialog = new WebBrowserDialog()
                    {
                        Title = "图上作战",
                        WebUrl = string.IsNullOrEmpty(WebUrl) ? defaultMapUrl : WebUrl
                    };
                    wbDialog.InitializeMapToolStrip();
                    break;
                default:
                    return;
            }

            wbDialog.Show();
        }
    }
}
