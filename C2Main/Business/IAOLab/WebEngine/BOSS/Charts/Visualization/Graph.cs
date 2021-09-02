using C2.IAOLab.WebEngine.Boss.Charts;
using C2.IAOLab.WebEngine.Boss.Option;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType;
using C2.IAOLab.WebEngine.Boss.Option.BaseOption;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType.LineBaseOption;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;
using Label = C2.IAOLab.WebEngine.Boss.Option.SeriesType.SeriesBaseOption.Label;
using C2.Business.IAOLab.WebEngine.Boss.Option.SeriesType.SeriesBaseOption;
using System.Linq;

namespace C2.Business.IAOLab.WebEngine.Boss.Charts.Visualization
{
    class Graph : BaseCharts
    {
        public Graph(List<DataTable> dataList, CompleteOption option, Dictionary<string, string[]> chartOptionDict)
        {
            if (!chartOptionDict.ContainsKey("Graph") || chartOptionDict["Graph"].Length == 0)
                return;
           
            string[] chartOptions = chartOptionDict["Graph"];

            string link = Trans(dataList, chartOptions)[0] as string;
            string data = Trans(dataList, chartOptions)[1];
            
            string category = string.Empty;
            List<string> categoriesList = new List<string>() { };
            
            if (chartOptions[3] != "-1" && chartOptions[4] != "-1")//有节点数据和类型时
            {
                category = Trans(dataList, chartOptions)[2];
                for (int i = 0; i < dataList[1].Rows.Count; i++)
                {
                    string categories = dataList[1].Rows[i][int.Parse(chartOptions[4])].ToString();
                    if (categories != "" && categoriesList.Contains(categories) == false)
                    {
                        categoriesList.Add(categories);
                    }
                }
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    Dictionary<string, string> categorieDictionary = new Dictionary<string, string>();
                    categorieDictionary.Add("name", dataList[0].Columns[int.Parse(chartOptions[i])].ColumnName.ToString());
                    categoriesList.Add(dataList[0].Columns[int.Parse(chartOptions[i])].ColumnName.ToString());
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    string categorieData = jss.Serialize(categorieDictionary);
                    category = category + categorieData + ",";
                };
                category = '['+ category + ']';
            }

            string lengendList = string.Empty;
            for (int i=0; i < categoriesList.Count; i++)
            {
                lengendList = lengendList + "'"+ categoriesList[i]+ "',";
            }

            List <ISeries> series = new List<ISeries>();
            option.legend = new Legend()
            {
                data = "[" + lengendList + "]",
                textStyle = new TextStyle()
                {
                    color = "'#333'",
                }
            };

            option.tooltip = new Tooltip()
            {
                trigger = "'item'",
                triggerOn = TriggerOn.mousemove,
            };
            series.Add(new SeriesGraph()
            {
                symbolSize = "4",
                symbol = "'emptyCircle'",
                smooth = "false",
                type = "'graph'",
                layout = "'force'",
                draggable = "true",
                links = link,
                categories = category,
                roam = "true",
                lineStyle=new LineStyle() 
                {
                    width = 1,
                    color= "'#aaa'",
                },
                label = new Label()
                {
                    show = "true",
                    position = C2.IAOLab.WebEngine.Boss.Option.SeriesType.SeriesBaseOption.Position.right,
                    formatter = "'{b}'",
                    //color = "'#333'",
                },
                
                force = new Force() 
                {
                    edgeLength = 50,
                    repulsion = 100,
                    gravity = "0.05",
                    layoutAnimation = "true",
                },
                data = data,
            });
            option.series = new Series(series.ToArray());
            _initScript = option.ToString();
        }
    
        public List<string> Trans(List<DataTable> dataList, string[] chartOptions)
        {
            string edgeData = string.Empty;
            string nodeData = string.Empty;
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string linewidth = "1";
            List<string> returnList = new List<string>() { };
            for (int i = 0; i < dataList[0].Rows.Count; i++)
            {
                Dictionary<string, object> edgeDictionary = new Dictionary<string, object>();
                edgeDictionary.Add("source", dataList[0].Rows[i][int.Parse(chartOptions[0])].ToString());
                edgeDictionary.Add("target", dataList[0].Rows[i][int.Parse(chartOptions[1])].ToString());
                //添加权重，默认为1
                if (int.Parse(chartOptions[2]) != -1)
                {
                    linewidth = dataList[0].Rows[i][int.Parse(chartOptions[2])].ToString();
                }
                Dictionary<string, string> lineStyleDictionary = new Dictionary<string, string>();
                lineStyleDictionary.Add("width", linewidth);

                edgeDictionary.Add("lineStyle", lineStyleDictionary);
                string strEdgeData = jss.Serialize(edgeDictionary);
                edgeData = edgeData + strEdgeData + ",";
            }
            if (int.Parse(chartOptions[3]) != -1)//有节点数据
            {
                List<string> categoryList = new List<string>() { };
                string categories = string.Empty;
                for (int i = 0; i < dataList[1].Rows.Count; i++)
                {
                    Dictionary<string, object> nodeDictionary = new Dictionary<string, object>();
                    nodeDictionary.Add("id", dataList[1].Rows[i][int.Parse(chartOptions[3])].ToString());
                    nodeDictionary.Add("name", dataList[1].Rows[i][int.Parse(chartOptions[3])].ToString());
                    if (int.Parse(chartOptions[4]) != -1)//有节点类型
                    {
                        string category = dataList[1].Rows[i][int.Parse(chartOptions[4])].ToString();
                        if (category != "" && categoryList.Contains(category) == false)
                        {
                            categoryList.Add(category);
                            Dictionary<string, string> categorieDictionary = new Dictionary<string, string>();
                            categorieDictionary.Add("name", category);
                            string categorieData = jss.Serialize(categorieDictionary);
                            categories = categories + categorieData + ',';
                        }
                        for (int j = 0; j < categoryList.Count; j++)
                        {
                            if (category == categoryList[j])
                            {
                                nodeDictionary.Add("category", j);
                            }
                        }
                    }
                    else //有节点无类型
                    {
                        string sourceColName = dataList[0].Columns[int.Parse(chartOptions[0])].ColumnName;
                        string[] source = dataList[0].AsEnumerable().Select(peo => peo.Field<string>(sourceColName)).ToArray();
                        if (source.Contains(dataList[1].Rows[i][int.Parse(chartOptions[3])].ToString()))
                        {
                            nodeDictionary.Add("category", 0);
                        }
                        else
                        {
                            nodeDictionary.Add("category", 1);
                        }
                    }
                    string strNodeData = jss.Serialize(nodeDictionary);
                    nodeData = nodeData + strNodeData + ",";
                }
                returnList = new List<string>() { '[' + edgeData + ']', '[' + nodeData + ']', '[' + categories + ']' };
            }
            else //无节点数据
            {
                List<string> nodeList = new List<string>() { };
                for (int i = 0; i < dataList[0].Rows.Count; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        if (nodeList.Contains(dataList[0].Rows[i][int.Parse(chartOptions[j])].ToString()) == false)
                        {
                            nodeList.Add(dataList[0].Rows[i][int.Parse(chartOptions[j])].ToString());
                            Dictionary<string, object> nodeDictionary = new Dictionary<string, object>();
                            nodeDictionary.Add("id", dataList[0].Rows[i][int.Parse(chartOptions[j])].ToString());
                            nodeDictionary.Add("name", dataList[0].Rows[i][int.Parse(chartOptions[j])].ToString());
                            //string sourceColName = dataList[0].Columns[int.Parse(chartOptions[0])].ColumnName;
                            //string[] source = dataList[0].AsEnumerable().Select(peo => peo.Field<string>(sourceColName)).ToArray();
                            //if (source.Contains(dataList[0].Rows[i][int.Parse(chartOptions[j])].ToString()))
                            nodeDictionary.Add("category", j);
                            string strNodeData = jss.Serialize(nodeDictionary);
                            nodeData = nodeData + strNodeData + ",";
                        }
                    }
                }
                returnList = new List<string>() { '[' + edgeData + ']', '[' + nodeData + ']' };
            }
            return returnList;
        }
    }
}