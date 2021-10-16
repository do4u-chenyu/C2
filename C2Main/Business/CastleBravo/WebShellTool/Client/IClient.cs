namespace C2.Business.CastleBravo.WebShellTool
{
    interface IClient
    {
        string FetchLog();  // 获取日志
        string PHPInfo();   // 构造phpinfo命令的payload
        string PHPIndex();  
        string PHPReadDict(string dict);   // 带一个参数构造payload
        string PHPShell(string shellEnv, string command);   // 带二个参数构造payload
        string ExtractResponse(string response);
    }
}
