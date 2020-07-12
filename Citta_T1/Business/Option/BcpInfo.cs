using System.Collections.Generic;
using System.Linq;
using Citta_T1.Business.Model;
using Citta_T1.Core;
using Citta_T1.Utils;

namespace Citta_T1.Business.Option
{
    class BcpInfo
    {
        private string fullFilePath;          //BCP文件全路径
        private string fileName;              //BCP文件名
        private string name;                  //对应的数据源名称
        private ElementType type;             //对应的类型:数据源 或 Result
        private OpUtil.Encoding encoding;     //BCP文件对应的编码
        private char[] separator;             //BCP文件对应的分隔符

        public string ColumnLine { get; set; }
        public string[] ColumnArray { get; set; }

        public BcpInfo(ModelElement me)
        {
            InitBcpInfo(me.FullFilePath, me.Description, me.Type, me.Encoding, new char[] { me.Separator });
        }

        // 简易版本，很多情况下name和type都不需要用
        public BcpInfo(string fullBcpPath, OpUtil.Encoding encoding, char[] separator)
        {
            InitBcpInfo(fullBcpPath, string.Empty, ElementType.Empty, encoding, separator);
        }

        public BcpInfo(string fullBcpPath, string name, ElementType type, OpUtil.Encoding encoding, char[] separator)
        {
            InitBcpInfo(fullBcpPath, name, type, encoding, separator);
        }

        private void InitBcpInfo(string fullBcpPath, string name, ElementType type, OpUtil.Encoding encoding, char[] separator)
        {
            this.fullFilePath = fullBcpPath;
            fileName = System.IO.Path.GetFileName(this.fullFilePath);
            this.name = name;
            this.type = type;
            this.encoding = encoding;
            this.separator = separator;
            InitColumnInfo();
        }

        // 根据第一行初始化列信息
        private void InitColumnInfo()
        {
            this.ColumnLine = BCPBuffer.GetInstance().GetCacheColumnLine(this.fullFilePath, encoding);
            //暂定预览保持文件不变，下拉选项去掉尾部空表头
            List<string> tmpColumnArray = new List<string>(this.ColumnLine.Split(this.separator));
            int realColCount = tmpColumnArray.Count;
            for(int i = tmpColumnArray.Count-1 ; i>=0;i--) 
            {
                if (string.IsNullOrEmpty(tmpColumnArray[i]))
                    realColCount--;
                else
                    break;
            }
            ColumnArray = tmpColumnArray.Take(realColCount).ToArray();

        }
    }
}
