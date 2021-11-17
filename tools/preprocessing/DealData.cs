using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace preprocessing
{
    class DealData
    {
        private string inputFilePath;
        private string outputFilePath;
        private string preType;


        public DealData(string[] param)
        {
            this.inputFilePath = param[0];
            this.outputFilePath = param[1];
            this.preType = param[2];
        }

        public void Deal()
        {
            /*
             * 1、读指定路径文件夹下的所有txt文件
             * 2、解析类型，去广告群、去长文本、去图片，分三个方法实现
             * 3、结果写入指定输出路径文件（多个文件的话合并）
             * 
             */

            return;
        }
    }
}
