using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Business.GlueWater.Settings
{
    class YellowGlueSetting : BaseGlueSetting
    {
        private static YellowGlueSetting YellowGlueSettingInstance;
        public static YellowGlueSetting GetInstance()
        {
            if (YellowGlueSettingInstance == null)
            {
                YellowGlueSettingInstance = new YellowGlueSetting();
            }
            return YellowGlueSettingInstance;
        }

        public YellowGlueSetting() : base()
        {

        }

        public override void InitDataTable()
        {
        }

        public override string RefreshHtmlTable(bool freshTitle = true)
        {
            StringBuilder sb = new StringBuilder();
            if (freshTitle)
                sb.Append("<tr name=\"title\">" +
                      "    <th>网站名称/域名/IP</th>" +
                      "    <th>Refer对应Title/Refer</th>" +
                      "    <th>涉案金额<a class=\"arrow desc\" onmousedown=\"SortCol(this)\"></a></th>" +
                      "    <th>涉黄人数<a class=\"arrow desc\" onmousedown=\"SortCol(this)\"></a></th>" +
                      "    <th>赌博类型/运营时间</th>" +
                      "    <th>发现地市/发现时间</th>" +
                      "</tr>"
                      );
            return sb.ToString();
        }

        public override DataTable SearchInfo(string item)
        {
            return base.SearchInfo(item);
        }

        public override string UpdateContent(string excelPath)
        {
            return base.UpdateContent(excelPath);
        }
    }
}
