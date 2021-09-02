using System;

namespace C2.SearchToolkit
{
    public class SearchModelSettingsInfo
    {
        public String StartTime;
        public String EndTime;
        public String QueryStr;


        public SearchModelSettingsInfo() : this(String.Empty, String.Empty, String.Empty)
        { }

        public SearchModelSettingsInfo(String startTime, String endTime) : this (startTime, endTime, String.Empty)
        { }

        public SearchModelSettingsInfo(String startTime, String endTime, String queryStr)
        {
            StartTime = startTime;
            EndTime   = endTime;
            QueryStr  = queryStr;
        }

        public bool IsSetQueryTime()
        {
            // 限制条件从严
            return String.IsNullOrEmpty(StartTime) || String.IsNullOrEmpty(EndTime);
        }
        
        public bool IsSetQueryStr()
        {
            return !String.IsNullOrEmpty(QueryStr);
        }
    }
}
