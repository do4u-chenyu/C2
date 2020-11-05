using C2.Utils;
using System.Collections.Generic;

namespace C2.Model
{
    public class DataItem
    {
        string fullFilePath;
        string fileName;
        char fileSep;
        OpUtil.Encoding fileEncoding;
        OpUtil.ExtType fileType;

        public static readonly DataItem Empty = new DataItem();

        public bool IsEmpty()
        {
            return fileType == OpUtil.ExtType.Unknow;
        }
        private DataItem()
        {

        }
        public DataItem(string filePath, string fileName, char fileSep, OpUtil.Encoding fileEncoding, OpUtil.ExtType fileType)
        {
            this.fullFilePath = filePath;
            this.fileName = fileName;
            this.fileSep = fileSep;
            this.fileEncoding = fileEncoding;
            this.fileType = fileType;
        }
        public string FilePath { get => fullFilePath; set => fullFilePath = value; }
        public string FileName { get => fileName; set => fileName = value; }
        public char FileSep { get => fileSep; set => fileSep = value; }
        public OpUtil.Encoding FileEncoding { get => fileEncoding; set => fileEncoding = value; }
        public OpUtil.ExtType FileType { get => fileType; set => fileType = value; }
        public string ChartType { get; set; }
        public int SelectedIndexs { get; set; }

    }
}
