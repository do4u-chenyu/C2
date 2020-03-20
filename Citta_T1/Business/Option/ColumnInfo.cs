using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citta_T1.Business.Option
{
    public enum ColumnType
    {
        String,  // 字符串类型
        Int,     // 整数型
        Date,    // 日期型
        Float    // 有理数
    }

    class ColumnInfo
    {
        private int columnIndex;
        private string columnName;
        private ColumnType columnType;
    }
}
