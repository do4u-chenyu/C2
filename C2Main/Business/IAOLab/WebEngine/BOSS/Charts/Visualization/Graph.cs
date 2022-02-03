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
using System.Text.RegularExpressions;
using System;

namespace C2.Business.IAOLab.WebEngine.Boss.Charts.Visualization
{
    class Graph : BaseCharts
    {
        public Graph(List<DataTable> dataList, CompleteOption option, Dictionary<string, string[]> chartOptionDict)
        {
            if (!chartOptionDict.ContainsKey("Graph") || chartOptionDict["Graph"].Length == 0)
                return;
           
            string[] chartOptions = chartOptionDict["Graph"];

            List<string> returnList = Trans(dataList, chartOptions);
            string link = returnList[0];
            string data = returnList[1];
            string category = returnList[2];

            JavaScriptSerializer jss = new JavaScriptSerializer();
            string categorie = category.Remove(category.Length - 2, 1);
            List<Dictionary<string, string>> categoriesList = jss.Deserialize<List<Dictionary<string, string>>>(categorie);
            float a = dataList[0].Rows.Count;
            float b = 1250;
            float gravities = a/b;

            int edgeLen = Math.Min(5000 / dataList[0].Rows.Count + 25, 125);
            int symbolSizes =11-dataList[0].Rows.Count/250;
            string lengendList = string.Empty;
            for (int i=0; i < categoriesList.Count; i++)
            {
                lengendList = lengendList + "'"+ categoriesList[i]["name"]+ "',";
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
                symbolSize = symbolSizes.ToString(),
                symbol = "'circle'",
                smooth = "false",
                type = "'graph'",
                layout = "'force'",
                draggable = "true",
                left = "'5%'",
                right = "'5%'",
                top ="'10%'",
                bottom ="'5%'",
                links = link,
                categories = category,
                roam = "true",
                animationDuration = 100,
                animationDurationUpdate = 100,
                animationEasing = "'cubicOut'",
                animationDelay= "0",
                animationEasingUpdate= "'cubicInOut'",
                animationDelayUpdate = "0",
                lineStyle =new LineStyle() 
                {
                    width = 1,
                    color= "'#aaa'",
                },
                label = new Label()
                {
                    show = "false",
                    position = C2.IAOLab.WebEngine.Boss.Option.SeriesType.SeriesBaseOption.Position.right,
                    formatter = "'{b}'",
                    //color = "'#333'",
                },
                
                force = new Force() 
                {
                    edgeLength = edgeLen,
                    repulsion = 50,
                    gravity = gravities.ToString(),
                    layoutAnimation = "true",
                },
                data = data,
            });
            option.series = new Series(series.ToArray());
            _initScript = option.ToString();
        }
    
        public List<string> Trans(List<DataTable> dataList, string[] chartOptions)
        {
            string sourceColName = dataList[0].Columns[int.Parse(chartOptions[0])].ColumnName;
            string targetColName = dataList[0].Columns[int.Parse(chartOptions[1])].ColumnName;
            string[] source = dataList[0].AsEnumerable().Select(peo => peo.Field<string>(sourceColName)).ToArray();
            string[] target = dataList[0].AsEnumerable().Select(peo => peo.Field<string>(targetColName)).ToArray();
            var edgeUnionList = source.Union(target);

            string edgeData = string.Empty;
            string nodeData = string.Empty;
            string categories = string.Empty;
            List<string> categoryList = new List<string>() { };
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string linewidth = "1";
            List<string> returnList = new List<string>() { };
            for (int i = 0; i < dataList[0].Rows.Count; i++)
            {
                Dictionary<string, object> edgeDictionary = new Dictionary<string, object>();
                edgeDictionary.Add("source", dataList[0].Rows[i][int.Parse(chartOptions[0])].ToString());
                edgeDictionary.Add("target", dataList[0].Rows[i][int.Parse(chartOptions[1])].ToString());
                //添加权重，默认为1
                Dictionary<string, string> lineStyleDictionary = new Dictionary<string, string>();
                if (int.Parse(chartOptions[2]) != -1)
                {
                    string weight = dataList[0].Rows[i][int.Parse(chartOptions[2])].ToString();
                    Regex rx = new Regex("^\\d+(\\.\\d+)?$");
                    if (rx.IsMatch(weight))
                    {
                        linewidth = weight;
                    }
                    else
                    {
                        linewidth = "1";
                    }
                }         
                lineStyleDictionary.Add("width", linewidth);
                edgeDictionary.Add("lineStyle", lineStyleDictionary);
                string strEdgeData = jss.Serialize(edgeDictionary);
                edgeData = edgeData + strEdgeData + ",";
            }
           
            if (int.Parse(chartOptions[3]) == -1) //无节点数据
            {
                List<string> edgeNodeList = new List<string>() { };
                for (int i = 0; i < dataList[0].Rows.Count; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        string edgeNode = dataList[0].Rows[i][int.Parse(chartOptions[j])].ToString();
                        if (!edgeNodeList.Contains(edgeNode) && !string.IsNullOrEmpty(edgeNode))
                        {
                            edgeNodeList.Add(edgeNode);
                            Dictionary<string, object> nodeDictionary = new Dictionary<string, object>();
                            nodeDictionary.Add("id", edgeNode);
                            nodeDictionary.Add("name", edgeNode);
                            nodeDictionary.Add("category", j);
                            string strNodeData = jss.Serialize(nodeDictionary);
                            nodeData = nodeData + strNodeData + ",";

                            Dictionary<string, string> categorieDictionary = new Dictionary<string, string>();
                            if (!categoryList.Contains(dataList[0].Columns[int.Parse(chartOptions[j])].ColumnName.ToString()))
                            {
                                categoryList.Add(dataList[0].Columns[int.Parse(chartOptions[j])].ColumnName.ToString());
                                categorieDictionary.Add("name", dataList[0].Columns[int.Parse(chartOptions[j])].ColumnName.ToString());
                                string categorieData = jss.Serialize(categorieDictionary);
                                categories = categories + categorieData + ",";
                            }
                        }
                    }
                }
            }
            else//有节点数据
            {
                
                string nodeColName = dataList[1].Columns[int.Parse(chartOptions[3])].ColumnName;
                string[] nodeList = dataList[1].AsEnumerable().Select(peo => peo.Field<string>(nodeColName)).ToArray();
                List<string> allNodeList = new List<string>();
                foreach(string item in edgeUnionList.Union(nodeList))
                {
                    if(!string.IsNullOrEmpty(item))
                    {
                        allNodeList.Add(item);
                    }   
                }
                if (int.Parse(chartOptions[4]) == -1)//有节点无类型
                {
                    for (int i = 0; i < allNodeList.Count; i++)
                    {
                        Dictionary<string, object> nodeDictionary = new Dictionary<string, object>();
                        if (source.Contains(allNodeList[i]))
                        {
                            nodeDictionary.Add("category", 0);         
                        }
                        else if (target.Contains(allNodeList[i]))
                        {
                            nodeDictionary.Add("category", 1);                
                        }
                        else
                        {
                            nodeDictionary.Add("category", 2); 
                        }
                        nodeDictionary.Add("id", allNodeList[i]);
                        nodeDictionary.Add("name", allNodeList[i]);
                        string strNodeData = jss.Serialize(nodeDictionary);
                        nodeData = nodeData + strNodeData + ",";
                        
                    }
                    for (int i = 0; i < allNodeList.Count; i++)
                    {
                        Dictionary<string, string> categorieDictionary = new Dictionary<string, string>();
                        string categoryName = string.Empty;
                        if (!categoryList.Contains(sourceColName))
                        {
                            categoryList.Add(sourceColName);
                            categorieDictionary.Add("name", sourceColName);
                            string categorieData = jss.Serialize(categorieDictionary);
                            categories = categories + categorieData + ",";
                        }
                        else if (!categoryList.Contains(targetColName))
                        {
                            categoryList.Add(targetColName);
                            categorieDictionary.Add("name", targetColName);
                            string categorieData = jss.Serialize(categorieDictionary);
                            categories = categories + categorieData + ",";
                        }
                        else if (!categoryList.Contains("其他"))
                        {
                            categoryList.Add("其他");
                            categorieDictionary.Add("name", "其他");
                            string categorieData = jss.Serialize(categorieDictionary);
                            categories = categories + categorieData + ",";
                        }
                    }
                }
                else //有节点类型
                {
                    for (int i = 0; i < allNodeList.Count; i++)
                    {
                        Dictionary<string, object> nodeDictionary = new Dictionary<string, object>();
                        nodeDictionary.Add("id", allNodeList[i]);
                        nodeDictionary.Add("name", allNodeList[i]);
                        Dictionary<string, string> categorieDictionary = new Dictionary<string, string>();
                        if (nodeList.Contains(allNodeList[i]))
                        {
                            for (int j = 0; j < dataList[1].Rows.Count; j++)
                            {
                                if (nodeList[j] == allNodeList[i])
                                {
                                    string category = dataList[1].Rows[j][int.Parse(chartOptions[4])].ToString();
                                    if (category == "")
                                    {
                                        category = "其他";
                                    }
                                    nodeDictionary.Add("category", category);
                                    if (!categoryList.Contains(category))
                                    {
                                        categoryList.Add(category);
                                        categorieDictionary.Add("name", category);
                                        string categorieData = jss.Serialize(categorieDictionary);
                                        categories = categories + categorieData + ',';
                                    }
                                    break;
                                }
                            }
                        }
                        else
                        {
                            nodeDictionary.Add("category", "其他");
                            if (!categoryList.Contains("其他"))
                            {
                                categoryList.Add("其他");
                                categorieDictionary.Add("name", "其他");
                                string categorieData = jss.Serialize(categorieDictionary);
                                categories = categories + categorieData + ',';
                            }
                        }
                        string strNodeData = jss.Serialize(nodeDictionary);
                        nodeData = nodeData + strNodeData + ",";
                        
                    }

                }
            }
            returnList = new List<string>() { '[' + edgeData + ']', '[' + nodeData + ']', '[' + categories + ']' };
            return returnList;
        }
    }
}