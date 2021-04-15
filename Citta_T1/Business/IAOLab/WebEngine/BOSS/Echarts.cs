using C2.IAOLab.WebEngine.Boss.Charts;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace C2.IAOLab.WebEngine.Boss
{
    public class Echarts
    {    
        Dictionary<string, Theme> themes;//支持主题 
        Dictionary<string, string> optionScript;//所有增加的图表

        public DataTable dataTable;

        public Echarts()
        {
            themes = new Dictionary<string, Theme>();
            optionScript = new Dictionary<string, string>();
        }

        public BaseCharts this[int index] {//添加图的索引器
            set {
                AddChart(index, value);
            }
        }

        /// <summary>
        /// 增加表到相应的行列
        /// </summary>
        public void AddChart(int index, BaseCharts baseCharts)
        {
            if (optionScript.ContainsKey("container_" + index))
                optionScript["container_" + index] = baseCharts._initScript;
            else
                optionScript.Add("container_" + index, baseCharts._initScript);
        }

        /// <summary>
        /// 显示已经添加的所有图
        /// </summary>
        public string Show()
        {
            string htmlContent = string.Empty;

            htmlContent += "$(function () {" + $"var datas =" + Common.GetDataSetSource(dataTable) + ";" + Environment.NewLine;

            foreach (var option in optionScript)
            {
                if (option.Value != null)
                {
                    htmlContent += "echart_" + option.Key + "();" + Environment.NewLine + GetScriptNode(option.Key, option.Value);
                }
            }

            htmlContent += "});";

            string tempName = Path.Combine(Application.StartupPath, "Business\\IAOLab\\WebEngine\\JS", "BossOptions.js");
            File.WriteAllText(tempName, htmlContent);
            return tempName;            
        }

        //增加主题 CSS JS 自定义节点
        public void AddTheme(string themeName, Theme theme)
        {
            if (themes.ContainsKey(themeName))
                themes[themeName] = theme;
            else
                themes.Add(themeName, theme);
        }
        public void AddTheme(Theme theme)
        {
            AddTheme(theme.Name, theme);
        }

        string GetScriptNode(string containerId, string script)
        {
            return new JSBeautify( TransToFunc(containerId,
                        InitID(containerId) + Environment.NewLine +
                            $"var {containerId}option = " + script + ";" +
                            SetOption(containerId)),
                        new JSBeautifyOptions()).GetResult() + Environment.NewLine;
        }
        
        private string TransToFunc(string id, string js)
        {
            return "function echart_" + id + "() {try {" + js + "}catch (err){alert(err)}}";
        }

        string InitID(string ContanerID)
        {
            return $@"var my{ContanerID}Chart = echarts.init("+
                $@"document.getElementById('{ContanerID}')" + GetTheme ()+ ");";
        }
        string SetOption(string ContanerID)
        {
            return $@"my{ContanerID}Chart.setOption({ContanerID}option);";
        }
        string GetTheme(string theme = null)
        {
            if (!EchartsInitialize.UseTheme || (themes == null || themes.Count <= 0))
                return "";
            if (string.IsNullOrWhiteSpace(theme))
                return ",'" + themes.First().Value.Name+"'";
            if(!themes.ContainsKey(theme))
                return ",'" + themes.First().Value.Name + "'";
            else
                return ",'" + themes[theme].Name + "'";
        }
    }
}
