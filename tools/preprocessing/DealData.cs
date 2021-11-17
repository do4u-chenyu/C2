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
             * 0、如果输入文件：
             *        字段个数小于5，且（第一列名为说明），那么跳到指定文件夹路径读所有文件；
             *        字段个数小于5，不处理；
             *        大于5，取第五列（群聊内容列）
             * 
             * 1、解析类型，去广告群、去长文本、去图片，分三个方法实现
             * 2、结果写入指定输出路径文件（多个文件的话合并）
             * 
             */

            return;
        }
    }
}
