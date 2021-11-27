using analysis.Method;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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

            //this.inputFilePath1 = "C:\\FiberHomeIAOModelDocument\\IAO\\聚沙成塔\\群聊关键词分析\\_datas\\L16_20211117_041034.bcp";
            //this.inputFilePath2 = "C:\\FiberHomeIAOModelDocument\\IAO\\聚沙成塔\\群聊关键词分析\\_datas\\key.txt";
            //this.outputFilePath = "C:\\FiberHomeIAOModelDocument\\IAO\\聚沙成塔\\群聊关键词分析\\_datas\\1.txt";
            //this.analysisType = "同群分析";

            this.dataTable = new DataTable();
        }

        public void Deal()
        {
            switch (analysisType)
            {
                case "关键词分析":
                    dataTable = new KeyWordAnalysis(inputFilePath1, inputFilePath2).GenDataTable();
                    break;
                case "银行卡提取":
                    dataTable = new BankCardExtract(inputFilePath1).GenDataTable();
                    break;
                case "同群分析":
                    //认为2个QQ号在2个群同时出现，认为这2个QQ属于统一组织
                    dataTable = new OrganizationAnalysis(inputFilePath1).GenDataTable();
                    break;
                default:
                    break;


            }

            SaveResultToLocal();
        }

        private void SaveResultToLocal()
        {
            StreamWriter sw = null;
            FileStream fs = null;
            try
            {
                fs = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write);
                sw = new StreamWriter(fs, Encoding.UTF8);
                List<string> colList = new List<string>();
                for (int i = 0; i < dataTable.Columns.Count; i++)
                    colList.Add(dataTable.Columns[i].ColumnName);

                sw.WriteLine(string.Join("\t", colList));

                foreach (DataRow row in dataTable.Rows)
                {
                    sw.WriteLine(string.Join("\t", row.ItemArray));
                }
            }
            catch { }
            finally
            {
                if (sw != null)
                    sw.Close();
                if (fs != null)
                    fs.Close();
            }
        }
    }
}
