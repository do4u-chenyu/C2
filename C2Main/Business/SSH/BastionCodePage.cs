namespace C2.Business.SSH
{
    public class BastionCodePage
    {
        public const int Success          = 0;
        public const int NoHomeSearch     = 2;  // 没有全文环境
        public const int LoginBastionFail = 4;  // 堡垒机登陆失败
        public const int JumpUnknownFail  = 8;  // 跳转时出现未预期错误
        public const int JumpOneFail      = 16;  // 一阶跳转时失败
        public const int JumpTwoFail      = 32; // 二阶跳转时失败
        public const int JumpSearchFail   = 64; // 跳转全文机失败
        public const int DownloadFileCorrupted    = 128; // 下载文件损坏
        public const int DownloadTimeout  = 256;// 下载超时
        public const int DownloadCancel   = 512;// 下载取消
        public const int UploadScriptFail = 1024;// 上传脚本到全文机失败
        public const int RunTaskFail      = 2048; // 全文机运行脚本失败 
        public const int TaskDirectoryFail = 4096;// 任务目录创建失败或找不到
        public const int DownloadFileNotExists  = 8192; // 下载结果文件不存在
        public const int DownloadRemoteReadFail = 16384;// 下载文件时远端读失败
    }
}
