using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace analysis
{
    class DealData
    {
        private string inputFilePath1;
        private string inputFilePath2;
        private string outputFilePath;
        private string analysisType;
        private DataTable dataTable;

        public DealData(string[] param)
        {
            this.inputFilePath1 = param[0];
            this.inputFilePath2 = param[1];
            this.outputFilePath = param[2];
            this.analysisType = param[3];

            //this.inputFilePath = "C:\\Users\\RedHat\\Desktop\\ql\\GROUPCODE_680692357_QQ.tsv";
            //this.outputFilePath = "C:\\Users\\RedHat\\Desktop\\ql\\1.txt";
            //this.analysisType = "7";

            this.dataTable = new DataTable();
        }

        public void Deal()
        {

        }
    }
}
