using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace v2rayN.Handler
{
    class V2rayConfigHandler
    {

        public static string GenerateClientSpeedtestConfigString(List<ListViewItem> lv, int startPort)
        {
            _ = lv;
            _ = startPort;
            // 临时测试用
            StreamReader sr = new StreamReader(@"C:\Users\quixote\Desktop\熊猫网络5用户\验活测试.v2rayconfig.v1.txt");
            return sr.ReadToEnd();
        }
    }
}
