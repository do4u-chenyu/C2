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

        public bool IsDatabase()
        {
            return FileType == OpUtil.ExtType.Database;
        }
        public DataItem()
        {

        }
        public DataItem(string filePath, string fileName, char fileSep, OpUtil.Encoding fileEncoding, OpUtil.ExtType fileType, DatabaseType databaseType = DatabaseType.Null)
        {
            this.FilePath = filePath;
            this.FileName = fileName;
            this.FileSep = fileSep;
            this.FileEncoding = fileEncoding;
            this.FileType = fileType;
            this.DataType = databaseType;
        }

        public DataItem Clone()
        {
            return new DataItem
            {
                FilePath = this.FilePath,
                FileName = this.FileName,
                FileSep = this.FileSep,
                FileEncoding = this.FileEncoding,
                FileType = this.FileType,
                DataType = this.DataType,
                ResultDataType = this.ResultDataType,
                ChartType = this.ChartType,
                SelectedIndexs = this.SelectedIndexs,  // 浅拷贝
                SelectedItems = this.SelectedItems,    // 浅拷贝
                DBItem = this.DBItem,                  // 浅拷贝     
            }; 
        }

        public DataItem(DatabaseType dataType, DatabaseItem database)
        {
            DataType = dataType;
            DBItem = database;
            FileSep = OpUtil.DefaultFieldSeparator;
            FilePath = database.AllDatabaseInfo;
            FileName = database.DataTable.Name; 
            FileType = OpUtil.ExtType.Database;
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
        public DatabaseType DataType { get; set; }
        public DatabaseItem DBItem { get; set; }

    }
}
