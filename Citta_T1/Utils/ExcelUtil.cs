using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.Util;
using OfficeOpenXml;
using System;
using System.Collections.Generic;

namespace C2.Utils
{
    public static class Constants {
		/**
		 * 星期 默认格式
		 */
		public static String COMMON_DATE_FORMAT_XQ = "星期";
		/**
		 * 周 默认格式
		 */
		public static String COMMON_DATE_FORMAT_Z = "周";
		/**
		* 07版时间=time的Cell.getCellStyle().getDataFormat()值
		* 
		*/
		public static List<short> EXCEL_FORMAT_INDEX_07_TIME = Arrays.AsList(new short[] { 18, 19, 20, 21, 32, 33, 45, 46, 47, 55, 56,
			176, 177, 178, 179, 180, 181, 182, 183, 184, 185, 186 });
			/**
			 * 07版日期date的Cell.getCellStyle().getDataFormat()值
			 */
		public static List<short> EXCEL_FORMAT_INDEX_07_DATE = Arrays.AsList(new short[] { 14, 15, 16, 17, 22, 30, 31, 57, 58, 187, 188,
			189, 190, 191, 192, 193, 194, 195, 196, 197, 198, 199, 200, 201, 202, 203, 204, 205, 206, 207, 208 });
		/**
		 * 03版时间time的Cell.getCellStyle().getDataFormat()值
		 */
		public static List<short> EXCEL_FORMAT_INDEX_03_TIME = Arrays.AsList(new short[] { 18, 19, 20, 21, 32, 33, 45, 46, 47, 55, 56,
			176, 177, 178, 179, 180, 181, 182, 183, 184, 185, 186 });
		/**
		 * 03版日期 总date的Cell.getCellStyle().getDataFormat()值
		 */
		public static List<short> EXCEL_FORMAT_INDEX_03_DATE = Arrays.AsList(new short[] { 14, 15, 16, 17, 22, 30, 31, 57, 58, 187, 188,
			189, 190, 191, 192, 193, 194, 195, 196, 197, 198, 199, 200, 201, 202, 203, 204, 205, 206, 207, 208 });
		/**
		 * date-年月日时分秒-Cell.getCellStyle().getDataFormatString()
		 */
		public static List<String> EXCEL_FORMAT_INDEX_DATE_NYRSFM_STRING = Arrays.AsList("yyyy/m/d\\ h:mm;@", "m/d/yy h:mm",
			"yyyy/m/d\\ h:mm\\ AM/PM", "[$-409]yyyy/m/d\\ h:mm\\ AM/PM;@", "yyyy/mm/dd\\ hh:mm:dd",
			"yyyy/mm/dd\\ hh:mm", "yyyy/m/d\\ h:m", "yyyy/m/d\\ h:m:s", "yyyy/m/d\\ h:mm", "m/d/yy h:mm;@",
			"yyyy/m/d\\ h:mm\\ AM/PM;@");
		/**
		 * date-年月日Cell.getCellStyle().getDataFormatString()
		 */
		public static List<String> EXCEL_FORMAT_INDEX_DATE_NYR_STRING = Arrays.AsList("m/d/yy", "[$-F800]dddd\\,\\ mmmm\\ dd\\,\\ yyyy",
			"[DBNum1][$-804]yyyy\"年\"m\"月\"d\"日\";@", "yyyy\"年\"m\"月\"d\"日\";@", "yyyy/m/d;@", "yy/m/d;@", "m/d/yy;@",
			"[$-409]d/mmm/yy", "[$-409]dd/mmm/yy;@", "reserved-0x1F", "reserved-0x1E", "mm/dd/yy;@", "yyyy/mm/dd",
			"d-mmm-yy", "[$-409]d\\-mmm\\-yy;@", "[$-409]d\\-mmm\\-yy", "[$-409]dd\\-mmm\\-yy;@",
			"[$-409]dd\\-mmm\\-yy", "[DBNum1][$-804]yyyy\"年\"m\"月\"d\"日\"", "yy/m/d", "mm/dd/yy", "dd\\-mmm\\-yy");
		/**
		 * date-年月-Cell.getCellStyle().getDataFormatString()
		 */
		public static List<String> EXCEL_FORMAT_INDEX_DATE_NY_STRING = Arrays.AsList("[DBNum1][$-804]yyyy\"年\"m\"月\";@",
			"[DBNum1][$-804]yyyy\"年\"m\"月\"", "yyyy\"年\"m\"月\";@", "yyyy\"年\"m\"月\"", "[$-409]mmm\\-yy;@",
			"[$-409]mmm\\-yy", "[$-409]mmm/yy;@", "[$-409]mmm/yy", "[$-409]mmmm/yy;@", "[$-409]mmmm/yy",
			"[$-409]mmmmm/yy;@", "[$-409]mmmmm/yy", "mmm-yy", "yyyy/mm", "mmm/yyyy", "[$-409]mmmm\\-yy;@",
			"[$-409]mmmmm\\-yy;@", "mmmm\\-yy", "mmmmm\\-yy");
		/**
		 * date-月日Cell.getCellStyle().getDataFormatString()
		 */
		public static List<String> EXCEL_FORMAT_INDEX_DATE_YR_STRING = Arrays.AsList("[DBNum1][$-804]m\"月\"d\"日\";@",
			"[DBNum1][$-804]m\"月\"d\"日\"", "m\"月\"d\"日\";@", "m\"月\"d\"日\"", "[$-409]d/mmm;@", "[$-409]d/mmm", "m/d;@",
			"m/d", "d-mmm", "d-mmm;@", "mm/dd", "mm/dd;@", "[$-409]d\\-mmm;@", "[$-409]d\\-mmm");
		/**
		 * date-星期X-Cell.getCellStyle().getDataFormatString()
		 */
		public static List<String> EXCEL_FORMAT_INDEX_DATE_XQ_STRING = Arrays.AsList("[$-804]aaaa;@", "[$-804]aaaa");
		/**
		 * date-周X-Cell.getCellStyle().getDataFormatString()
		 */
		public static List<String> EXCEL_FORMAT_INDEX_DATE_Z_STRING = Arrays.AsList("[$-804]aaa;@", "[$-804]aaa");
		/**
		 * date-月X-Cell.getCellStyle().getDataFormatString()
		 */
		public static List<String> EXCEL_FORMAT_INDEX_DATE_Y_STRING = Arrays.AsList("[$-409]mmmmm;@", "mmmmm", "[$-409]mmmmm");
		/**
		 * time - 时间-Cell.getCellStyle().getDataFormatString()
		 */
		public static List<String> EXCEL_FORMAT_INDEX_TIME_STRING = Arrays.AsList("mm:ss.0", "h:mm", "h:mm\\ AM/PM", "h:mm:ss",
			"h:mm:ss\\ AM/PM", "reserved-0x20", "reserved-0x21", "[DBNum1]h\"时\"mm\"分\"", "[DBNum1]上午/下午h\"时\"mm\"分\"",
			"mm:ss", "[h]:mm:ss", "h:mm:ss;@", "[$-409]h:mm:ss\\ AM/PM;@", "h:mm;@", "[$-409]h:mm\\ AM/PM;@",
			"h\"时\"mm\"分\";@", "h\"时\"mm\"分\"\\ AM/PM;@", "h\"时\"mm\"分\"ss\"秒\";@", "h\"时\"mm\"分\"ss\"秒\"_ AM/PM;@",
			"上午/下午h\"时\"mm\"分\";@", "上午/下午h\"时\"mm\"分\"ss\"秒\";@", "[DBNum1][$-804]h\"时\"mm\"分\";@",
			"[DBNum1][$-804]上午/下午h\"时\"mm\"分\";@", "h:mm AM/PM", "h:mm:ss AM/PM", "[$-F400]h:mm:ss\\ AM/PM");
		/**
		 * date-当formatString为空的时候-年月
		 */
		public static short EXCEL_FORMAT_INDEX_DATA_EXACT_NY = 57;
		/**
		 * date-当formatString为空的时候-月日
		 */
		public static short EXCEL_FORMAT_INDEX_DATA_EXACT_YR = 58;
		/**
		 * time-当formatString为空的时候-时间
		 */
		public static List<short> EXCEL_FORMAT_INDEX_TIME_EXACT = Arrays.AsList(new short[] { 55, 56 });
		/**
		 * 格式化星期或者周显示
		 */
		public static String[] WEEK_DAYS = { "日", "一", "二", "三", "四", "五", "六" };
}
	public class ExcelUtil
	{
		static LogUtil log = LogUtil.GetInstance("ExcelUtil");
		/**
		 * 年月日时分秒 默认格式
		 */
		public static SimpleDateFormat COMMON_DATE_FORMAT = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
		/**
		 * 时间 默认格式
		 */
		public static SimpleDateFormat COMMON_TIME_FORMAT = new SimpleDateFormat("HH:mm:ss");
		/**
		 * 年月日 默认格式
		 */
		public static SimpleDateFormat COMMON_DATE_FORMAT_NYR = new SimpleDateFormat("yyyy-MM-dd");
		/**
		 * 年月 默认格式
		 */
		public static SimpleDateFormat COMMON_DATE_FORMAT_NY = new SimpleDateFormat("yyyy-MM");
		/**
		 * 月日 默认格式
		 */
		public static SimpleDateFormat COMMON_DATE_FORMAT_YR = new SimpleDateFormat("MM-dd");
		/**
		 * 月 默认格式
		 */
		public static SimpleDateFormat COMMON_DATE_FORMAT_Y = new SimpleDateFormat("MM");
		/**
		 * 07版 excel dataformat
		 */
		public static DataFormatter EXCEL_07_DATA_FORMAT = new DataFormatter();

		//public static String GetFormatDateStringValue(short dataFormat, String dataFormatString, object value)
		//      {

		//      }
		public static String GetFormatTimeStringValue(short dataFormat, String dataFormatString, double value)
		{
			if (!DateUtil.IsValidExcelDate(value))
				return null;
			DateTime date = DateUtil.GetJavaDate(value);
			return ExcelUtil.COMMON_TIME_FORMAT.Format(date);
		}
		public static String GetFormatDateStringValue(short dataFormat, String dataFormatString, double value)
		{
			if (!DateUtil.IsValidExcelDate(value))
				return null;
			DateTime date = DateUtil.GetJavaDate(value);
			return ExcelUtil.COMMON_DATE_FORMAT.Format(date);
			/**
			 * 年月日时分秒
			 */
			//if (Constants.EXCEL_FORMAT_INDEX_DATE_NYRSFM_STRING.Contains(dataFormatString))
			//{
			//	return ExcelUtil.COMMON_DATE_FORMAT.Format(date);
			//}
			///**
			// * 年月日
			// */
			//if (Constants.EXCEL_FORMAT_INDEX_DATE_NYR_STRING.Contains(dataFormatString))
			//{
			//	return ExcelUtil.COMMON_DATE_FORMAT_NYR.Format(date);
			//}
			///**
			// * 年月
			// */
			//if (Constants.EXCEL_FORMAT_INDEX_DATE_NY_STRING.Contains(dataFormatString)
			//		|| Constants.EXCEL_FORMAT_INDEX_DATA_EXACT_NY.Equals(dataFormat))
			//{
			//	return ExcelUtil.COMMON_DATE_FORMAT_NY.Format(date);
			//}
			///**
			// * 月日
			// */
			//if (Constants.EXCEL_FORMAT_INDEX_DATE_YR_STRING.Contains(dataFormatString)
			//		|| Constants.EXCEL_FORMAT_INDEX_DATA_EXACT_YR.Equals(dataFormat))
			//{
			//	return ExcelUtil.COMMON_DATE_FORMAT_YR.Format(date);

			//}
			///**
			// * 月
			// */
			//if (Constants.EXCEL_FORMAT_INDEX_DATE_Y_STRING.Contains(dataFormatString))
			//{
			//	return ExcelUtil.COMMON_DATE_FORMAT_Y.Format(date);
			//}
			///**
			// * 星期X
			// */
			//if (Constants.EXCEL_FORMAT_INDEX_DATE_XQ_STRING.Contains(dataFormatString))
			//{
			//	return Constants.COMMON_DATE_FORMAT_XQ + date.DayOfWeek;
			//}
			///**
			// * 周X
			// */
			//if (Constants.EXCEL_FORMAT_INDEX_DATE_Z_STRING.Contains(dataFormatString))
			//{
			//	return Constants.COMMON_DATE_FORMAT_Z + date.DayOfWeek;
			//}
			///**
			// * 时间格式
			// */
			//if (Constants.EXCEL_FORMAT_INDEX_TIME_STRING.Contains(dataFormatString)
			//		|| Constants.EXCEL_FORMAT_INDEX_TIME_EXACT.Contains(dataFormat))
			//{
			//	return ExcelUtil.COMMON_TIME_FORMAT.Format(DateUtil.GetJavaDate(value));
			//}
			///**
			// * 单元格为其他未覆盖到的类型
			// */
			//if (DateUtil.IsADateFormat(dataFormat, dataFormatString))
			//{
			//	return ExcelUtil.COMMON_DATE_FORMAT.Format(date);
			//}

			//return null;
		}

		/**
		 * 用户模式得到公式单元格的值
		 * @param formulaValue
		 * @return
		 */
		public static String GetCellValue(CellValue formulaValue)
		{
			String cellValue = "";
			if (formulaValue == null)
			{
				return cellValue;
			}

			switch (formulaValue.CellType)
			{
				case CellType.Numeric:
					cellValue = formulaValue.NumberValue.ToString();
					break;
				case CellType.String:
					cellValue = formulaValue.StringValue.ToString();
					break;
				case CellType.Boolean:
					cellValue = formulaValue.BooleanValue.ToString();
					break;
				case CellType.Blank:
					cellValue = "";
					break;
				case CellType.Error:
					cellValue = formulaValue.ErrorValue.ToString();
					break;
				case CellType.Unknown:
					cellValue = "";
					break;
				default:
					cellValue = "未知类型";
					break;
			}
			return cellValue;
		}

		/**
		 * 用户模式得到单元格的值
		 * @param workbook
		 * @param cell
		 * @return
		 */
		public static String GetCellValue(IWorkbook workbook, ICell cell)
		{
			String cellValue = "";
			try
			{
				if (cell == null)
					return cellValue;
				switch (cell.CellType)
				{
					case CellType.Numeric:
						short formatID = cell.CellStyle.DataFormat;
						string formatString = cell.CellStyle.GetDataFormatString();
						if (IsDateFormat(formatID, formatString))
						{
							cellValue = ExcelUtil.GetFormatDateStringValue(
								formatID,
								formatString,
								cell.NumericCellValue);
						}
						else if (IsTimeFormat(formatID, formatString))
						{
							cellValue = ExcelUtil.GetFormatTimeStringValue(
									formatID,
									formatString,
									cell.NumericCellValue);
						}
						else
							cellValue = cell.NumericCellValue.ToString() != null ? cell.NumericCellValue.ToString() : string.Empty;
						break;
					case CellType.Formula:
						/**
						 * 格式化单元格
						 */
						IFormulaEvaluator evaluator = workbook.GetCreationHelper().CreateFormulaEvaluator();
						cellValue = evaluator.Evaluate(cell).ToString();
						if (cell.CellStyle.DataFormat != 0)
							cellValue = GetFormatDateStringValue(cell.CellStyle.DataFormat, cell.CellStyle.GetDataFormatString(), evaluator.Evaluate(cell).NumberValue);
						else
							cellValue = cell.NumericCellValue.ToString();
						break;
					case CellType.String:
						cellValue = cell.StringCellValue.ToString();
						break;
					case CellType.Boolean:
						cellValue = cell.BooleanCellValue.ToString();
						break;
					case CellType.Blank:
						cellValue = "";
						break;
					case CellType.Error:
						cellValue = cell.ErrorCellValue.ToString();
						break;
					case CellType.Unknown:
						cellValue = "";
						break;
					default:
						cellValue = "未知类型";
						break;
				}
			}
			catch (Exception e)
			{
				log.Error("读取单元格失败, error: " + e.ToString());
			}
			return cellValue.Replace('\n', ' ').Replace('\t', ' ').Replace('\r', ' ');
		}

		public static String GetCellValue(ExcelRange cell)
        {
			String cellValue = String.Empty;
			if (cell == null || cell.Value == null)
				return cellValue;
			try
            {

				short formatID = Convert.ToInt16(cell.Style.Numberformat.NumFmtID);
				string formatString = cell.Style.Numberformat.Format;
				if (IsDateFormat(formatID, formatString))
                {
					if (cell.Value is DateTime time)
						cellValue = ExcelUtil.COMMON_DATE_FORMAT.Format(time); // 补全的时候会出问题，可能只有时间差一个H或者一个M或者一个S
					else if (cell.Value is double)
						cellValue = ExcelUtil.GetFormatDateStringValue(
							formatID,
							formatString,
							Convert.ToDouble(cell.Value));
					else
						cellValue = cell.Value.ToString(); // 实在不行就直接转str吧，以后如果有其他更细致的解决方案再填上
				}
				else if (IsTimeFormat(formatID, formatString))
                {
					if (cell.Value is DateTime time)
						cellValue = ExcelUtil.COMMON_TIME_FORMAT.Format(time);
					else if (cell.Value is double)
						cellValue = ExcelUtil.GetFormatTimeStringValue(
							formatID,
							formatString,
							Convert.ToDouble(cell.Value));
				}
				else
					cellValue = cell.Value.ToString();
			}
			catch (Exception e)
            {
				log.Error("读取单元格失败, error: " + e.ToString());
			}
			return cellValue.Replace('\n', ' ').Replace('\t', ' ').Replace('\r', ' ');
		}
		public static bool IsDateFormat(short formatID, string formatString)
        {
			return formatString.Contains("y") && formatString.Contains("m");
				//Constants.EXCEL_FORMAT_INDEX_03_DATE.Contains(formatID) ||
				//Constants.EXCEL_FORMAT_INDEX_07_DATE.Contains(formatID);
		}
		public static bool IsTimeFormat(short formatID, string formatString)
        {
			return formatString.Contains("m") && formatString.Contains("s");
				//Constants.EXCEL_FORMAT_INDEX_03_TIME.Contains(formatID) ||
				//Constants.EXCEL_FORMAT_INDEX_07_TIME.Contains(formatID);
		}
	}
}
