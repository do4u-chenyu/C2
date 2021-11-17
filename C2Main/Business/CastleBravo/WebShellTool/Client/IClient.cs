using System;
using System.Collections.Generic;

namespace C2.Business.CastleBravo.WebShellTool
{
    interface IClient
    {
        string FetchLog();             // 获取日志
        IClient AppendLog(string msg); // 有时候需要从外部补充日志
        string PHPInfo();              // 构造phpinfo命令的payload
        string PHPIndex();  
        string PHPReadDict(string dict);   // 带一个参数构造payload
        string PHPShell(string shellEnv, string command);   // 带二个参数构造payload
        string ExtractResponse(string response);
        List<string> ParseCurrentPath(string data);
        Tuple<string,string> GetShellParams();
        //
        string Suscide();                     // 一键自毁
        string DetailInfo(string PageData);   // 文件浏览
        string DownloadFile(string PageData); //文件下载
        string GetDatabaseInfo(string loginInfo, string database, string command);
    }
}
