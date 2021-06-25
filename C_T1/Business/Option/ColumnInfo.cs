﻿namespace C2.Business.Option
{
    public enum ColumnType
    {
        String,  // 字符串类型
        Int,     // 整数型
        Date,    // 日期型
        Float,    // 有理数
        ID,
        Phone,
    }

    class ColumnInfo
    {
        private int columnIndex;
        private string columnName;
        private ColumnType columnType;

        public int ColumnIndex { get => columnIndex; set => columnIndex = value; }
        public string ColumnName { get => columnName; set => columnName = value; }
        public ColumnType ColumnType { get => columnType; set => columnType = value; }
    }
}
