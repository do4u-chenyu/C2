using System;

namespace C2.SearchToolkit
{
    public class SearchModelSettingsInfo
    {
        public String StartTime;
        public String EndTime;

        public SearchModelSettingsInfo() : this(String.Empty, String.Empty)
        { }

        public SearchModelSettingsInfo(String startTime, String endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
        }

        public bool IsEmpty()
        {
            // 限制条件从严
            return String.IsNullOrEmpty(StartTime) || String.IsNullOrEmpty(EndTime);
        }
    }
}
