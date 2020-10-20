using C2.Utils;
using System;
using System.Collections.Generic;
using System.Threading;


namespace C2.Business.Schedule.Cmd
{
    class CustomOperatorCmd : OperatorCmd
    {
        private static readonly int DefaultSleepSecond = 30; // 默认一个算子跑30秒

        public CustomOperatorCmd(Triple triple) : base(triple)
        {

        }

        public List<string> GenCmd()
        {
            List<string> cmds = new List<string>();
            int sleepSecond = DoSleepCommand();// 休眠指定时间
            cmds.Add(string.Format("sbin\\sleep.exe {0}s ",sleepSecond));
            return cmds;
        } 

        private int DoSleepCommand()
        {
            int sleepSecond = DefaultSleepSecond;

            if (option.GetOption("fix").ToLower() == "true")
            {
                int tmpSec = ConvertUtil.TryParseInt(option.GetOption("fixSecond"));
                sleepSecond = tmpSec <= 0 ? DefaultSleepSecond : tmpSec;
            }
            else
            {
                int randomBegin = ConvertUtil.TryParseInt(option.GetOption("randomBegin"));
                int randomEnd = ConvertUtil.TryParseInt(option.GetOption("randomEnd"));
                Random rand = new Random();
                sleepSecond = randomBegin <= 0 || randomEnd <= 0 || randomEnd - randomBegin < 0 ? DefaultSleepSecond : rand.Next(randomBegin, randomEnd);
            }
            //Thread.Sleep(sleepSecond * 1000);
            return sleepSecond;
        }
    }
}
