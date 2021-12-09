using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Business.GlueWater
{
    interface ISetting
    {
        void InitDataTable();//初始化表格
        bool UpdateContent(string excelPath);//更新表格
        string RefreshHtmlTable();//更新html内容
        string SearchInfo(string item);//查询
    }
}
