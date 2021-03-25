using C2.Business.Model;
using C2.Controls.Common;
using C2.Core;
using C2.Model;
using C2.Model.Widgets;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static C2.Model.DataItem;

namespace C2.Utils
{
    public class OpUtil
    {
        public static readonly string TabSeparatorString = "\t";

        public static readonly char TabSeparator = '\t';
        public static readonly char DefaultLineSeparator = '\n';
        public static readonly char Blank = ' ';
        public const int PreviewMaxNum = 1000;
        public static float IOU(Rectangle rect1, Rectangle rect2)
        {
            // [top, left, bottom, right]
            List<int> r1 = new List<int> { rect1.Location.Y, rect1.Location.X, rect1.Location.Y + rect1.Height, rect1.Location.X + rect1.Width };
            List<int> r2 = new List<int> { rect2.Location.Y, rect2.Location.X, rect2.Location.Y + rect2.Height, rect1.Location.X + rect1.Width };
            int inHeight = Math.Min(r1[2], r2[2]) - Math.Max(r1[0], r2[0]);
            int inWidth = Math.Min(r1[3], r2[3]) - Math.Max(r1[1], r2[1]);
            return inHeight > 0 && inWidth > 0 ? inHeight * inWidth : 0;
        }

        public static Rectangle GetAreaByLine(Bezier line)
        {
            return new Rectangle(
                new Point((int)line.StartP.X, (int)line.StartP.Y),
                new Size((int)Math.Abs(line.StartP.X - line.EndP.X), (int)Math.Abs(line.StartP.Y - line.EndP.Y))
                );
        }

        // 当前文档在canvas里整体拖动dx, dy
        public static void CanvasDragLocation(float dx, float dy)
        {
            Global.GetCurrentModelDocument().ModelElements.ForEach(me => me.InnerControl.ChangeLoc(dx, dy));
            Global.GetCurrentModelDocument().UpdateAllLines();
            Global.GetCanvasPanel().Invalidate(false);
        }


        public static ElementSubType SEType(string subType)
        {
            ElementSubType type;
            switch (subType)
            {
                case "关联算子":
                    type = ElementSubType.RelateOperator;
                    break;
                case "碰撞算子":
                    type = ElementSubType.CollideOperator;
                    break;
                case "取并集":
                    type = ElementSubType.UnionOperator;
                    break;
                case "取差集":
                    type = ElementSubType.DifferOperator;
                    break;
                case "随机采样":
                    type = ElementSubType.RandomOperator;
                    break;
                case "条件筛选":
                    type = ElementSubType.FilterOperator;
                    break;
                case "取最大值":
                    type = ElementSubType.MaxOperator;
                    break;
                case "取最小值":
                    type = ElementSubType.MinOperator;
                    break;
                case "取平均值":
                    type = ElementSubType.AvgOperator;
                    break;
                case "频率算子":
                    type = ElementSubType.FreqOperator;
                    break;
                case "排序算子":
                    type = ElementSubType.SortOperator;
                    break;
                case "分组算子":
                    type = ElementSubType.GroupOperator;
                    break;
                case "AI实践":
                    type = ElementSubType.CustomOperator1;
                    break;
                case "多源算子":
                    type = ElementSubType.CustomOperator2;
                    break;
                case "Py算子":
                    type = ElementSubType.PythonOperator;
                    break;
                case "关键词过滤":
                    type = ElementSubType.KeywordOperator;
                    break;
                case "数据标准化":
                    type = ElementSubType.DataFormatOperator;
                    break;
                default:
                    type = ElementSubType.Null;
                    break;
            }
            return type;
        }

        public static string SubTypeName(string subType)
        {
            string type;
            switch (subType)
            {
                case "CollideOperator":
                    type = "碰撞算子";
                    break;
                case "RelateOperator":
                    type = "关联算子";
                    break;
                case "UnionOperator":
                    type = "取并集";
                    break;
                case "DifferOperator":
                    type = "取差集";
                    break;
                case "RandomOperator":
                    type = "随机采样";
                    break;
                case "FilterOperator":
                    type = "条件筛选";
                    break;
                case "MaxOperator":
                    type = "取最大值";
                    break;
                case "MinOperator":
                    type = "取最小值";
                    break;
                case "AvgOperator":
                    type = "取平均值";
                    break;
                case "FreqOperator":
                    type = "频率算子";
                    break;
                case "SortOperator":
                    type = "排序算子";
                    break;
                case "GroupOperator":
                    type = "分组算子";
                    break;
                case "CustomOperator1":
                    type = "AI实践";
                    break;
                case "CustomOperator2":
                    type = "多源算子";
                    break;
                case "PythonOperator":
                    type = "Py算子";
                    break;
                case "KeywordOperator":
                    type = "关键词过滤";
                    break;
                case "DataFormatOperator":
                    type = "数据标准化";
                    break;
                default:
                    type = "Null";
                    break;
            }
            return type;
        }

        public enum Encoding
        {
            UTF8,
            GBK,
            NoNeed
        }
        public enum ExtType
        {
            Unknow = 01,  // 默认值放在第一位
            Excel  = 02,
            Text   = 04,  
            Hive   = 08,
            Oracle = 16,
            Database = Hive | Oracle,
        }

        public static Encoding EncodingEnum(string encoding,Encoding defaultEncoding = Encoding.GBK)
        {
            if (!Enum.TryParse(encoding, true, out Encoding outEncoding))
                return defaultEncoding;
            return outEncoding;
        }

        public static ExtType ExtTypeEnum(string type, ExtType defaultType = ExtType.Unknow) 
        {
            if (!Enum.TryParse(type, true, out ExtType outType))
                return defaultType;
            return outType;
        }

        public static ResultType ResultTypeEnum(string type, ResultType defaultType = ResultType.Null)
        {
            if (!Enum.TryParse(type, true, out ResultType outType))
                return defaultType;
            return outType;
        }

        public static ElementStatus EStatus(string status, ElementStatus defaultStatus = ElementStatus.Null)
        {
            if (!Enum.TryParse(status, true, out ElementStatus outStatus))
                return defaultStatus;
            return outStatus;
        }
        public static DatabaseType DBTypeEnum(string type, DatabaseType defaultStatus = DatabaseType.Null)
        {
            if (!Enum.TryParse(type, true, out DatabaseType outStatus))
                return defaultStatus;
            return outStatus;
        }

        public static OpStatus OpStatus(string status, OpStatus defaultStatus = C2.Model.Widgets.OpStatus.Null)
        {
            if (!Enum.TryParse(status, true, out OpStatus outStatus))
                return defaultStatus;
            return outStatus;
        }
        public static OpType OpType(string status, OpType defaultStatus = C2.Model.Widgets.OpType.Null)
        {
            if (!Enum.TryParse(status, true, out OpType outType))
                return defaultStatus;
            return outType;
        }
        public static PointF ToPointFType(string point)
        {
            PointF location = new PointF();
            try
            {
                if (point == "")
                    return location;
                string coordinate = Regex.Replace(point, @"[^\d,-]*", "");
                string[] xy = coordinate.Split(',');
                location = new PointF(Convert.ToSingle(xy[0]), Convert.ToSingle(xy[1]));
            }
            catch { }
            return location;
        }
        public static Point ToPointType(string point)
        {
            return Point.Round(ToPointFType(point));
        }
        public static bool IsArrayIndexOutOfBounds(Control control,int index)
        {
            if (control is ComboBox)
                return (index >= (control as ComboBox).Items.Count || index < 0);
            else if (control is ComCheckBoxList)
                return (index >= (control as ComCheckBoxList).Items.Count || index < 0);
            else
                return true;

        }
    }
}
