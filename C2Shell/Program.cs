using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2Shell
{
    class Program
    {
        static void Main(string[] args)
        {

            IsNeedUpdate();
   
        

        }
        private static void IsNeedUpdate()
        {
            // update路径是否为空
            if (Directory.Exists(""))
                return;
            if (!ExecuteUpdate())
            {
                Rollback();
            }

        }
        private static bool ExecuteUpdate()
        {
            // 解压update目录

            // 执行 setup.bat脚本
            return true;
        }
        private static void  Rollback()
        {
            // 执行 rollback.bat脚本
        }
    }
}
