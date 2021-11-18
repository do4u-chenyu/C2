using C2.Model.Widgets;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Business.Schedule.Cmd
{
    class PreprocessingOperatorCmd : OperatorCmd
    {
        public PreprocessingOperatorCmd(Triple triple) : base(triple)
        {
        }
        public PreprocessingOperatorCmd(OperatorWidget operatorWidget) : base(operatorWidget)
        {
        }

        public List<string> GenCmd()
        {
            List<string> cmds = new List<string>();
            string inputFilePath = GetInputFilePath(inputFilePaths.First());//输入文件
            string preType = option.GetOption("pretype");//取平均值字段

            cmds.Add(string.Format("sbin\\preprocessing.exe {0} {1} {2}", inputFilePath, this.outputFilePath, preType));
            return cmds;
        }

        private string GetInputFilePath(string path)
        {
            /*
             * 如果输入文件：
             *     字段个数为1，且（第一列名为说明），那么跳到指定文件夹路径读所有文件；
             *     字段个数小于5，不处理；
             */

            FileStream fs_dir = null;
            StreamReader reader = null;
            string inputFilePath = path;
            try
            {
                fs_dir = new FileStream(path, FileMode.Open, FileAccess.Read);
                reader = new StreamReader(fs_dir);
                string[] columnList = reader.ReadLine().TrimEnd(new char[] { '\r', '\n' }).Split('\t');
                if (columnList.Length == 1 && columnList[0] == "说明")
                    inputFilePath = reader.ReadLine().TrimEnd(new char[] { '\r', '\n' });
                else if (columnList.Length < 5)
                    inputFilePath = string.Empty;
            }
            catch { }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (fs_dir != null)
                    fs_dir.Close();
            }

            return inputFilePath;
        }
    }
}
