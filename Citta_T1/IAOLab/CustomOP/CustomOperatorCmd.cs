using Citta_T1.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Citta_T1.Business.Schedule.Cmd
{
    class CustomOperatorCmd : OperatorCmd
    {
        private static int DefaultSleepSecond = 1;

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

            if(option.GetOption("fix").ToLower() == "true")
            {
                string tmpSec = option.GetOption("fixSecond");
                sleepSecond = tmpSec == "" || int.Parse(tmpSec) <= 0 ? DefaultSleepSecond : int.Parse(tmpSec);
            }
            else
            {
                string randomBegin = option.GetOption("randomBegin");
                string randomEnd = option.GetOption("randomEnd");
                Random rand = new Random();
                sleepSecond = randomBegin == "" || randomEnd == "" || int.Parse(randomBegin) <= 0 || int.Parse(randomEnd) <= 0 || int.Parse(randomEnd)-int.Parse(randomBegin)<0 ? DefaultSleepSecond : rand.Next(int.Parse(randomBegin), int.Parse(randomEnd));
            }

            Thread.Sleep(sleepSecond * 1000);
        }
    }
}
