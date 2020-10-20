using C2.OperatorViews;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace C2.Business.Schedule.Cmd
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
            string outField = TransDifferOutputField(option.GetOptionSplit("outfield0"));//输出字段
            string invert = option.GetOption("conditionSlect").ToLower() == "0" ? String.Empty : "-v"; //是否包含，0包含，1不包含

            //每次运行从缓存中读一份关键词
            string keywordXml = new KeywordCombine().KeywordPreView(inputFilePaths[1],this.separators[1],int.Parse(option.GetOption("keySelectIndex")),triple.DataElements[1].ExtType.ToString(),triple.DataElements[1].Encoding.ToString());
            string[] keyList = keywordXml.Split('\t');
           
            //关键词写入临时配置文件，解决关键词为中文时的编码问题，文件统一为utf-8
            string keyPath = System.IO.Path.GetDirectoryName(this.outputFilePath) + "\\O" + this.operatorId + "_key.bat";
            UTF8Encoding utf8 = new UTF8Encoding(false);
            StreamWriter streamWriter = new StreamWriter(keyPath, false, utf8);
            foreach(string key in keyList)
            {
                streamWriter.Write(key + "\n");
            }
            streamWriter.Close();
            string keyTmpPath1 = System.IO.Path.GetDirectoryName(this.outputFilePath) + "\\O" + this.operatorId + "_keyWord1.tmp";
            string keyTmpPath2 = System.IO.Path.GetDirectoryName(this.outputFilePath) + "\\O" + this.operatorId + "_keyWord2.tmp";



            ReWriteBCPFile();
            cmds.Add(string.Format("{0}|sbin\\awk.exe -F\"{1}\"  '{{print ${2}}}' | sbin\\grep.exe -a -E {3} -f {4} -n | sbin\\awk.exe -F':' '{{print $1}}' >  {5} ",TransInputfileToCmd(inputFilePath),this.separators[0],inputField,invert,keyPath,keyTmpPath1));
            cmds.Add(string.Format("{0}|sbin\\awk.exe -v OFS=\"{1}\" '{{print NR,$0}}' > {2}", TransInputfileToCmd(inputFilePath),this.separators[0],keyTmpPath2));
            cmds.Add(string.Format("sbin\\awk.exe  -F\"{0}\"  '{{if(NR==FNR){{S[$1]=$0;next}}else{{print S[$1]}} }}' {1} {2} | sbin\\awk.exe -F\"{0}\" -v OFS='\\t' '{{print {3}}}' >> {4}", this.separators[0],keyTmpPath2,keyTmpPath1, outField, this.outputFilePath));
            //cmds.Add(string.Format("sbin\\join.exe  -t\"{4}\" {0} {1} | sbin\\awk.exe -F\"{4}\" -v OFS='\\t' '{{print {2}}}' >> {3}", keyTmpPath1, keyTmpPath2, outField, this.outputFilePath, this.separators[0]));
            cmds.Add(string.Format("sbin\\rm.exe - f {0} {1} {2}", keyPath, keyTmpPath1, keyTmpPath2));

            return cmds;
        }
    }
}