using C2.Utils;
using System.Collections.Generic;

namespace C2.Model
{
    public class DataItem
    {
        public static readonly DataItem Empty = new DataItem();
        public enum ResultType
        {
            Null,
            SingleOp,
            ModelOp
        }

        public bool IsEmpty()
        {
            return FileType == OpUtil.ExtType.Unknow;
        }
        public DataItem()
        {

        }
        public DataItem(string filePath, string fileName, char fileSep, OpUtil.Encoding fileEncoding, OpUtil.ExtType fileType)
        {
            this.FilePath = filePath;
            this.FileName = fileName;
            this.FileSep = fileSep;
            this.FileEncoding = fileEncoding;
            this.FileType = fileType;
        }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public char FileSep { get; set; }
        public OpUtil.Encoding FileEncoding { get; set; }
        public OpUtil.ExtType FileType { get; set; }
        public string ChartType { get; set; }
        public List<int> SelectedIndexs { get; set; }
        public List<string> SelectedItems { get; set; }
        public ResultType ResultDataType { get; set; }
    }
}
