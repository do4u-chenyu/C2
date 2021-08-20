namespace C2.Business.SSH
{
    public class BastionCodePage
    {
        public const int NoHomeSearch     = 0;  // 没有全文环境
        public const int LoginBastionFail = 2;  // 堡垒机登陆失败
        public const int JumpUnknownFail  = 4;  // 跳转时出现未预期错误
        public const int JumpOneFail      = 8;  // 一阶跳转时失败
        public const int JumpTwoFail      = 16; // 二阶跳转时失败
        public const int JumpSearchFail   = 32; // 跳转全文机失败
        public const int FileCorrupted    = 64; // 下载文件损坏
        public const int DownloadTimeout  = 128;// 下载超时
        public const int DownloadCancel   = 256;// 下载取消
        public const int UploadScriptFail = 512;// 上传脚本到全文机失败
        public const int RunTaskFail      = 1024; // 全文机运行脚本失败 
        public const int TaskDirectoryFail = 2048;// 任务目录创建失败或找不到
        public const int DownloadFileNotExists = 4096; // 下载结果文件不存在
        public const int DownloadRemoteReadFail = 8192;// 下载文件时远端读失败
    }
}
