﻿using System.Collections.Generic;
using Citta_T1.Utils;
using Citta_T1.Business.Model;
using Citta_T1.Core;

namespace Citta_T1.Business.Option
{
    class BcpInfo
    {
        private string fullFilePath;  //BCP文件全路径
        private string fileName;     //BCP文件名
        private string name;         //对应的数据源名称
        private ElementType type;    //对应的类型:数据源 或 Result
        private OpUtil.Encoding encoding;     //BCP文件对应的编码
        private char[] separator;             //BCP文件对应的分隔符
        private string columnLine;
        private string[] columnArray;
        public string ColumnLine { get => columnLine; }
        public string[] ColumnArray { get => columnArray; }

        public BcpInfo(ModelElement me)
        {
            InitBcpInfo(me.FullFilePath, me.Description, me.Type, me.Encoding, new char[] { me.Separator }); 
        }

        public BcpInfo(string fullBcpPath, string name, ElementType type, OpUtil.Encoding encoding, char separator)
        {
            InitBcpInfo(fullBcpPath, name, type, encoding, new char[] { separator });
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
            this.columnLine = BCPBuffer.GetInstance().GetCacheColumnLine(this.fullFilePath, encoding);
            columnArray = this.columnLine.Split(this.separator);
        }
    }
}
