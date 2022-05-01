namespace C2.Business.CastleBravo.VPN.Client
{
    class ClientSetting
    {
        public static string[] I204List = 
        {
            @"http://connect.rom.miui.com/generate_204",                   /*小米*/
            @"http://wifi.vivo.com.cn/generate_204",                       /*Vivo*/
            @"http://connectivitycheck.platform.hicloud.com/generate_204", /*华为*/
        };

        public static string[] O204List = 
        {
            @"http://www.google.com/generate_204",
            @"http://clients3.google.com/generate_204",
        };

        public static string I_Generate_204 { get => I204List[0]; }
        public static string O_Generate_204 { get => O204List[0]; }
    }
}
