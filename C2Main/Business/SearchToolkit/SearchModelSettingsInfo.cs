using System;

namespace C2.SearchToolkit
{
    public class SearchModelSettingsInfo
    {
        public String StartTime;
        public String EndTime;
        public static readonly SearchModelSettingsInfo Empty = new SearchModelSettingsInfo();

        public SearchModelSettingsInfo() : this(String.Empty, String.Empty)
        { }

        public SearchModelSettingsInfo(String startTime, String endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
