﻿using C2.Utils;

namespace C2.Business.CastleBravo.WebShellTool
{
    class AntSwordClient : CommonClient
    {
        public AntSwordClient(string password, string clientSetting)
            :base(password, clientSetting)
        {
            this.prefix = password + "=";
        }

        private string RandomSPL()
        {
            int len = RandomUtil.RandomInt(7, 10);
            return RandomUtil.RandomHexString(len, 0);
        }

        private string RandomSPR()
        {
            return RandomSPL(); 
        }
        
        private string RandomValueAB()
        {
            return RandomUtil.RandomString(2, 1); 
        }

        private string RandomTmDir()
        {
            return RandomUtil.RandomHexString(9, 0, "."); 
        }

        private string RandomParam1K()
        {
            return RandomUtil.RandomString(14, 0);
        }

        //private string IndexTemplate;
        //private string PhpinfoTemplate;
        //private string ReadDictTemplate;
        //private string ShellTemplate;

        private void SessionReset()
        {
            this.clientSetting.SPL      = RandomSPL();
            this.clientSetting.SPR      = RandomSPR();
            this.clientSetting.CODE     = RandomTmDir();        // 借用字段
            this.clientSetting.ACTION   = RandomParam1K();    // 借用字段
            this.clientSetting.PHP_MAKE = RandomValueAB();  // 借用字段存一下, 不想建新变量,
        }

        public override string PHPInfo()
        {
            // 1 根据payload模板生成各种随机参数
            // 2 将随机参数赋值到session中
            // 3 构造payload
            // 4 发包
            // 5 解析返回包
            // 6 清空本次session参数,因为antsword每次报文参数都不一样
            SessionReset();
            string payload = string.Empty;
            return payload;
        }


    }
}
