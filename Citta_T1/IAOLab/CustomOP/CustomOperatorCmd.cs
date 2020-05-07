using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citta_T1.Business.Schedule.Cmd
{
    class CustomOperatorCmd : OperatorCmd
    {
        private static int DefaultSleepSecond = 30;

        public CustomOperatorCmd(Triple triple) : base(triple)
        {

        }

        public List<string> GenCmd()
        {
            List<string> cmds = new List<string>();
            // 休眠指定时间
            DoSleepCommand();
            return cmds;
        }

        private void DoSleepCommand()
        {
            int sleepSecond = DefaultSleepSecond;
            try
            {
                sleepSecond = Convert.ToInt32(option.GetOption("sleepSec", DefaultSleepSecond.ToString()));
            }
            catch
            {
                sleepSecond = DefaultSleepSecond;
            }
            System.Threading.Thread.Sleep(sleepSecond * 1000);
        }
    }
}
