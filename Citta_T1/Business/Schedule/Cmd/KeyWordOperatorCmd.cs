using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Citta_T1.Business.Schedule.Cmd
{
    class KeyWordOperatorCmd : OperatorCmd
    {
        public KeyWordOperatorCmd(Triple triple) : base(triple)
        {
        }
        public List<string> GenCmd()
        {
            List<string> cmds = new List<string>();
            string inputFilePath = inputFilePaths.First();//输入文件
            string inputField = TransInputLine(option.GetOption("dataSelectIndex"));//待匹配字段
            string outField = TransOutputField(option.GetOptionSplit("outfield"));//输出字段
            string invert = option.GetOption("conditionSlect").ToLower() == "0" ? String.Empty : "-v"; //是否包含，0包含，1不包含
            string[] keyList = Regex.Split(option.GetOption("keyWordText")," OR ");

            //关键词写入临时配置文件，解决关键词为中文时的编码问题，文件统一为utf-8
            string keyPath = System.IO.Path.GetDirectoryName(this.outputFilePath) + "\\O" + this.operatorId + "_key.bat";
            UTF8Encoding utf8 = new UTF8Encoding(false);
            StreamWriter streamWriter = new StreamWriter(keyPath, false, utf8);
            foreach(string key in keyList)
            {
                streamWriter.WriteLine(key);
            }
            streamWriter.Close();

            ReWriteBCPFile();
            cmds.Add(String.Format("{0}|sbin\\awk.exe -F\"{1}\"  '{{print ${2}}}' | sbin\\grep.exe -E {3} -f {4} -n | sbin\\awk.exe -F':' '{{print $1}}' | sbin\\xargs.exe -n1 -i sbin\\awk.exe '{{if(NR=={{}}+1) print $0}}' {5} >> {6}",TransInputfileToCmd(inputFilePath),this.separators[0],inputField,invert,keyPath,inputFilePath,this.outputFilePath));

            return cmds;
        }
    }
}
