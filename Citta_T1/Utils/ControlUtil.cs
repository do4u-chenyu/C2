using C2.Business.Model;
using C2.Business.Model.World;
using C2.Controls.Move;
using C2.Core;
using C2.Dialogs.WidgetChart;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace C2.Utils
{
    class ControlUtil
    {
        //
        // 沿着控件向上的返回控件的根节点
        // 如果控件自己就是根节点，返回ct
        //
        public static Control FindRootConrtol(Control ct)
        {
            Control ret = ct;
            while (ret.Parent != null)
                ret = ret.Parent;
            return ret;
        }

        //
        // 递归遍历子控件，根据name寻找子控件
        // 找不到返回null
        //
        public static Control FindControlByName(Control root, string name)
        {
            foreach (Control ct in root.Controls)
            {
                if (ct.Name == name)
                    return ct;
                if (ct.Controls.Count > 0)
                {
                    Control ret = FindControlByName(ct, name);
                    if (ret != null)
                        return ret;
                }
            }
            return null;
        }       

        public static void DisableOrder(DataGridView dgv)
        {
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                dgv.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        public static Dictionary<int, Point> SaveElesWorldCord(List<ModelElement> mes)
        {
            Dictionary<int, Point> eleWorldCordDict = new Dictionary<int, Point>();
            WorldMap wm = Global.GetCurrentModelDocument().WorldMap;
            foreach (ModelElement me in mes)
            {
                if (me == null)
                    continue;
                MoveBaseControl mbc = (me.InnerControl as MoveBaseControl);
                eleWorldCordDict.Add(mbc.ID, wm.ScreenToWorld(mbc.Location, true));
            }
            return eleWorldCordDict;
        }
        public static void UpdateElesWorldCord(Dictionary<int, Point> eleWorldCordDict, bool onlyUpdateRs=false)
        {
            ModelDocument doc = Global.GetCurrentModelDocument();
            WorldMap wm = doc.WorldMap;
            foreach(int eleID in eleWorldCordDict.Keys)
            {
                ModelElement me = doc.SearchElementByID(eleID);
                if (onlyUpdateRs && me.Type != ElementType.Result)
                    continue;
                me.Location = wm.WorldToScreen(eleWorldCordDict[eleID]);
            }
        }
        public static void PaintChart(List<List<string>> xyValues, List<string> titles,string chartType)
        {
            WidgetChartDialog chartDialog = new WidgetChartDialog(xyValues, titles);
            switch (chartType)
            {
                case "柱状图":
                    chartDialog.GetbarChart();
                    break;
                case "饼图":
                    chartDialog.GetPieChart();
                    break;
                case "折线图":
                    chartDialog.GetLineChart();
                    break;
                case "雷达图":
                    chartDialog.GetRadarChart();
                    break;
                case "圆环图":
                    chartDialog.GetRingChart();
                    break;
            }
            chartDialog.ShowDialog();
        }
    }
}
