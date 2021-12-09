using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Business.GlueWater
{
    interface IGlueSetting
    {
        void InitDataTable(); //初始化表
        bool UpdateContent(string excelPath); //更新表
        string RefreshHtmlTable(); //更新html
        string SearchInfo(string item); //查询表内容
    }
}
