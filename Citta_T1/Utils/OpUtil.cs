using Citta_T1.Business.Model;
using Citta_T1.Core;
using NPOI.SS.Formula;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;

namespace Citta_T1.Utils
{
    public class OpUtil
    {
        public static readonly char DefaultSeparator = '\t';
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
            Global.GetCurrentDocument().ModelElements.ForEach(me => me.InnerControl.ChangeLoc(dx, dy));
            Global.GetCurrentDocument().UpdateAllLines();
            Global.GetCanvasPanel().Invalidate(false);
        }


        public static ElementSubType SEType(string subType)
        {
            string type;
            switch (subType)
            {
                case "关联算子":
                    type = "RelateOperator";
                    break;
                case "碰撞算子":
                    type = "CollideOperator";
                    break;
                case "取并集":
                    type = "UnionOperator";
                    break;
                case "取差集":
                    type = "DifferOperator";
                    break;
                case "随机采样":
                    type = "RandomOperator";
                    break;
                case "条件筛选":
                    type = "FilterOperator";
                    break;
                case "取最大值":
                    type = "MaxOperator";
                    break;
                case "取最小值":
                    type = "MinOperator";
                    break;
                case "取平均值":
                    type = "AvgOperator";
                    break;
                case "频率算子":
                    type = "FreqOperator";
                    break;
                case "排序算子":
                    type = "SortOperator";
                    break;
                case "分组算子":
                    type = "GroupOperator";
                    break;
                case "AI实践":
                    type = "CustomOperator1";
                    break;
                case "多源算子":
                    type = "CustomOperator2";
                    break;
                case "Python算子":
                    type = "PythonOperator";
                    break;
                case "关键词过滤":
                    type = "KeyWordOperator";
                    break;
                case "数据标准化":
                    type = "DataFormatOperator";
                    break;
                default:
                    type = "Null";
                    break;
            }
            return (ElementSubType)Enum.Parse(typeof(ElementSubType), type);
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
                    type = "Python算子";
                    break;
                case "KeyWordOperator":
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
            Excel,
            Text,
            Unknow
        }

        public static Encoding EncodingEnum(string type)
        { return (Encoding)Enum.Parse(typeof(Encoding), type); }

        public static ExtType ExtTypeEnum(string type)
        { return (ExtType)Enum.Parse(typeof(ExtType), type); }

        public static ElementStatus EStatus(string status)
        { return (ElementStatus)Enum.Parse(typeof(ElementStatus), status); }


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
    }
}
