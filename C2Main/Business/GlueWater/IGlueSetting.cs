using System.Collections.Generic;
using System.Data;

namespace C2.Business.GlueWater
{
    interface IGlueSetting
    {
        void InitDataTable(); //初始化表
        string UpdateContent(string excelPath,bool isWrite); //更新表
        string RefreshHtmlTable(DataTable resTable,bool freshTitle, bool freshColumn,bool freshSort, bool clearAllData); //更新html
        DataTable SearchInfo(string item); //查询表内容
        DataTable DeleteInfo(string item); //删除表内容
        DataTable SearchInfoReply(string item); //查询表内容
        void SortDataTableByCol(string col, string sortType);//根据某列排序
    }
}
