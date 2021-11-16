﻿using C2.Model.Widgets;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Business.Schedule.Cmd
{
    class PreprocessingOperatorCmd : OperatorCmd
    {
        private static readonly int DefaultSleepSecond = 5; // 默认一个算子跑30秒

        public PreprocessingOperatorCmd(Triple triple) : base(triple)
        {
        }
        public PreprocessingOperatorCmd(OperatorWidget operatorWidget) : base(operatorWidget)
        {
        }

        public List<string> GenCmd()
        {
            List<string> cmds = new List<string>();
            int sleepSecond = DoSleepCommand();// 休眠指定时间
            cmds.Add(string.Format("sbin\\sleep.exe {0}s ", sleepSecond));
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
