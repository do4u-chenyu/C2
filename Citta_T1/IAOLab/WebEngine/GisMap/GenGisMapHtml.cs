using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.IAOLab.WebEngine.GisMap
{
    class GenGisMapHtml
    {

        private static GenGisMapHtml GenGisMapHtmlInstance;
        public static GenGisMapHtml GetInstance()
        {
            if (GenGisMapHtmlInstance == null)
            {
                GenGisMapHtmlInstance = new GenGisMapHtml();
            }
            return GenGisMapHtmlInstance;
        }

        public string TransDataToHtml(object[] args = null)//参数待设计
        {

            return "https://www.baidu.com";
        }
    }
}
