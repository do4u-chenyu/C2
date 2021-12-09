using System.Data;

namespace C2.Business.GlueWater
{
    interface IGlueSetting
    {
        void InitDataTable(); //初始化表
        bool UpdateContent(string excelPath); //更新表
        string RefreshHtmlTable(); //更新html
        DataTable SearchInfo(string item); //查询表内容
    }
}
