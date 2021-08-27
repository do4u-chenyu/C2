using C2.IAOLab.WebEngine.Boss.Charts;
using C2.IAOLab.WebEngine.Boss.Option;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType;
using C2.IAOLab.WebEngine.Boss.Option.BaseOption;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace C2.Business.IAOLab.WebEngine.Boss.Charts.Visualization
{
    class Graph : BaseCharts
    {
        public Graph(List<DataTable> dataList, CompleteOption option, Dictionary<string, string[]> chartOptionDict)
        {
            if (!chartOptionDict.ContainsKey("Graph") || chartOptionDict["Graph"].Length == 0)
                return;
            /*
             * chartOptions数组索引对应关系
             * 0     ：下级所在列
             * 1     ：上级所在列
             * 2-len ：信息所在列
             * 
             * 注：chartOptions[i] 得到的列为string，需要int.Parse转换后再去dataTable取值
             */
            string[] chartOptions = chartOptionDict["Graph"];
            string link = EdgeTrans(dataList[0], chartOptions);
            string data = NodeTrans(dataList[1], chartOptions);

            List<ISeries> series = new List<ISeries>();
            option.tooltip = new Tooltip()
            {
                trigger = "'item'",
                triggerOn = TriggerOn.mousemove,
            };
            series.Add(new SeriesGraph()
            { 
                type = "'graph'",
                layout = "'force'",
                draggable = "true",
                links = link,
                data = data,
            });
            option.series = new Series(series.ToArray());
            _initScript = option.ToString();
        }

        JavaScriptSerializer jss = new JavaScriptSerializer();
        
        public string EdgeTrans(DataTable dataTable, string[] chartOptions)
        {
            string edgeData = "[";
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Dictionary<string, string> edgeDictionary = new Dictionary<string, string>();
                edgeDictionary.Add("source", dataTable.Rows[i][int.Parse(chartOptions[0])].ToString());
                edgeDictionary.Add("target", dataTable.Rows[i][int.Parse(chartOptions[1])].ToString());
                string strData = jss.Serialize(edgeDictionary);
                edgeData = edgeData + strData + ",";
            }
            return edgeData + "]";
        }
        public string NodeTrans(DataTable dataTable, string[] chartOptions)
        {
            string nodeData = "[";
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Dictionary<string, string> nodeDictionary = new Dictionary<string, string>();
                nodeDictionary.Add("id", dataTable.Rows[i][int.Parse(chartOptions[2])].ToString());
                string strData = jss.Serialize(nodeDictionary);
                nodeData = nodeData + strData + ",";
            }
            return nodeData + "]";
        }
    }
}