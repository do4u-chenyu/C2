using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Citta_T1.Business.Schedule.Cmd
{
    class KeywordOperatorCmd : OperatorCmd
    {
        public KeywordOperatorCmd(Triple triple) : base(triple)
        {
        }
        public List<string> GenCmd()
        {
            List<string> cmds = new List<string>();
            string inputFilePath = inputFilePaths.First();//输入文件
            string inputField = TransInputLine(option.GetOption("dataSelectIndex"));//待匹配字段
            string outField = TransOutputField(option.GetOptionSplit("outfield"));//输出字段
            string invert = option.GetOption("conditionSlect").ToLower() == "0" ? String.Empty : "-v"; //是否包含，0包含，1不包含
            string[] keyList = option.GetOption("keyWordText").Split('\t');
            string codeConvert = JudgeInputFileEncoding(inputFilePath) == OpUtil.Encoding.GBK ? " | sbin\\iconv.exe -f gbk -t utf-8 -c" : string.Empty;

            //关键词写入临时配置文件，解决关键词为中文时的编码问题，文件统一为utf-8
            string keyPath = System.IO.Path.GetDirectoryName(this.outputFilePath) + "\\O" + this.operatorId + "_key.bat";
            UTF8Encoding utf8 = new UTF8Encoding(false);
            StreamWriter streamWriter = new StreamWriter(keyPath, false, utf8);
            foreach(string key in keyList)
            {
                streamWriter.Write(key + "\n");
            }
            streamWriter.Close();

            ReWriteBCPFile();
            cmds.Add(String.Format("{0}|sbin\\awk.exe -F\"{1}\"  '{{print ${2}}}' | sbin\\grep.exe -E {3} -f {4} -n | sbin\\awk.exe -F':' '{{print $1}}' | sbin\\xargs.exe -n1 -i sbin\\awk.exe  -F\"{5}\" -v OFS='\\t' '{{if(NR=={{}}+1) print {6}}}' {7} {8} >> {9}", TransInputfileToCmd(inputFilePath),this.separators[0],inputField,invert,keyPath, this.separators[0], outField, inputFilePath, codeConvert, this.outputFilePath));

            return cmds;
        }
    }
}
