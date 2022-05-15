namespace v2rayN.HttpProxyHandler
{
    /// <summary>
    /// v2ray源码硬移植来的
    /// </summary>
    public enum ListenerType
    {
        noHttpProxy = 0,
        GlobalHttp = 1,
        GlobalPac = 2,
        HttpOpenAndClear = 3,
        PacOpenAndClear = 4,
        HttpOpenOnly = 5,
        PacOpenOnly = 6
    }
}
