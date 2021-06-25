using ICSharpCode.TextEditor.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C2.Utils;

namespace C2.IAOLab.Transform
{
    public class GPSTransform
    {

        private double xPi = Math.PI * 3000.0 / 180.0;
        private double earthR = 6371000;
        private string input;
        private string wrongInfo;
        private string[] inputArray;
        public GPSTransform(string input)
        {
            this.input = input;
            this.wrongInfo = string.Format("{0} 输入格式有误\n", input);
            this.inputArray = input.Split(OpUtil.Blank);
        }

       
        public static GPSTransform GetInstance(string input)
        {  
            return new GPSTransform(input);
        }

        public string CoordinateConversion(string type)
        {
            inputArray = inputArray.Where(s => !string.IsNullOrEmpty(s)).ToArray();
            double[] result;
            if(input.Trim() == string.Empty)
            {
                return "";
            }
            else
            {
                if (inputArray.Length != 2)
                    return wrongInfo;
                double lat = ConvertUtil.TryParseDouble(inputArray[0]);
                double lon = ConvertUtil.TryParseDouble(inputArray[1]);
                if (double.IsNaN(lat) || double.IsNaN(lon))
                    return wrongInfo;
                if (lat > 90 || lat < -90 || lon > 180 || lon < -180)
                    return wrongInfo;

                switch (type)
                {
                    case "bd_wgs":
                        result = BDConvertToWGS(lat, lon);
                        break;
                    case "wgs_bd":
                        result = WGConvertToBD(lat, lon);
                        break;
                    case "wgs_gcj":
                        result = WGSConvertToGCJ(lat, lon);
                        break;
                    case "gcj_wgs":
                        result = GCJConvertToWGS(lat, lon);
                        break;
                    case "bd_gcj":
                        result = BDConvertToGCJ(lat, lon);
                        break;
                    case "gcj_bd":
                        result = GCJConvertToBD(lat, lon);
                        break;
                    default:
                        result = new double[] { };
                        break;
                }
                if (result.Count() < 2)
                    return string.Format("{0}:{1}", input, wrongInfo);
                double deviation = Distance(lat, lon, result[0], result[1]);
                return string.Format("{0}\t输出坐标:{1:N5} {2:N5}，偏差:{3:N5}米\n", input, result[0], result[1], deviation);
            }
        }
        public string ComputeDistance()
        {
            inputArray = inputArray.Where(s => !string.IsNullOrEmpty(s)).ToArray();
            if (inputArray.Length != 4)
                return wrongInfo;
            double lat = ConvertUtil.TryParseDouble(inputArray[0]);
            double lon = ConvertUtil.TryParseDouble(inputArray[1]);
            double blat = ConvertUtil.TryParseDouble(inputArray[2]);
            double blon = ConvertUtil.TryParseDouble(inputArray[3]);
            bool illegalNum = double.IsNaN(lat) || double.IsNaN(lon) || double.IsNaN(blat) || double.IsNaN(blon);
            if (illegalNum)
                return wrongInfo;
            return string.Format("{0}\t距离:{1:N5}米\n", input, Distance(lat, lon, blat, blon));
        }
        #region 6种坐标转换方法

        private double[] GCJConvertToBD(double GCJLat, double GCJLon)
        {
            double z = Math.Sqrt(Math.Pow(GCJLon, 2) + Math.Pow(GCJLat, 2)) + 0.00002 * Math.Sin(GCJLat * xPi);
            double theta = Math.Atan2(GCJLat, GCJLon) + 0.000003 * Math.Cos(GCJLon * xPi);
            double[] result = { z * Math.Sin(theta) + 0.006, z * Math.Cos(theta) + 0.0065 };
            return result;
        }
        private double[] BDConvertToGCJ(double BDLAT, double BDLON)
        {          
            BDLON -= 0.0065;
            BDLAT -= 0.006;
            double z = Math.Sqrt(BDLON * BDLON + BDLAT * BDLAT) - 0.00002 * Math.Sin(BDLAT * xPi);
            double theta = Math.Atan2(BDLAT, BDLON) - 0.000003 * Math.Cos(BDLON * xPi);
            double[] result = { z * Math.Sin(theta), z * Math.Cos(theta) };
            return result;
        }
        public double Distance(double ALAT, double ALON, double BLAT, double BLON)
        {
            double x = Math.Cos(ALAT * Math.PI / 180) * Math.Cos(BLAT * Math.PI / 180) * Math.Cos((ALON - BLON) * Math.PI / 180);
            double y = Math.Sin(ALAT * Math.PI / 180) * Math.Sin(BLAT * Math.PI / 180);
            double s = x + y;
            double alpha = Math.Acos(Math.Max(-1.0, Math.Min(1.0, s)));
            return alpha * earthR;
        }


        private double[] XYTransform(double x, double y)
        {

            double absX = Math.Sqrt(Math.Abs(x));
            double d = (20.0 * Math.Sin(6.0 * x * Math.PI) + 20.0 * Math.Sin(2.0 * x * Math.PI)) * 2.0 / 3.0;
            double lat = -100.0 + 2.0 * x + 3.0 * y + 0.2 * y * y + 0.1 * x * y + 0.2 * absX + d;
            double lon = 300.0 + x + 2.0 * y + 0.1 * x * x + 0.1 * x * y + 0.1 * absX + d;
            lat += (20.0 * Math.Sin(y * Math.PI) + 40.0 * Math.Sin(y / 3.0 * Math.PI)) * 2.0 / 3.0;
            lon += (20.0 * Math.Sin(x * Math.PI) + 40.0 * Math.Sin(x / 3.0 * Math.PI)) * 2.0 / 3.0;
            lat += (160.0 * Math.Sin(y / 12.0 * Math.PI) + 320.0 * Math.Sin(y / 30.0 * Math.PI)) * 2.0 / 3.0;
            lon += (150.0 * Math.Sin(x / 12.0 * Math.PI) + 300.0 * Math.Sin(x / 30.0 * Math.PI)) * 2.0 / 3.0;
            double[] result = { lat, lon };
            return result;
        }

        private bool OutOfChina(double lat, double lon)
        {
            if (lon < 72.004 || lon > 137.8347)
            {
                return true;
            }
            else if (lat < 0.8293 || lat > 55.8271)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private double[] Delta(double lat, double log)
        {
            double a = 6378245.0;
            double ee = 0.00669342162296594323;

            double[] latLon = XYTransform(log - 105.0, lat - 35.0);

            //List<double> latLon = CoordinateTransform(log - 105.0, lat - 35.0);

            double radLat = lat / 180.0 * Math.PI;
            double magic = 1 - ee * (Math.Pow(Math.Sin(radLat),2));
            double sqrtMagic = Math.Sqrt(magic);
            latLon[0] = (latLon[0] * 180.0) / ((a * (1 - ee)) / (magic * sqrtMagic) * Math.PI);
            latLon[1] = (latLon[1] * 180.0) / (a / sqrtMagic * Math.Cos(radLat) * Math.PI);
            double[] result = { latLon[0], latLon[1] };
            return result;
        }
        private double[] WGSConvertToGCJ(double wgsLat, double wgsLon)
        {
            if (OutOfChina(wgsLat, wgsLon))
            {
                double[] result = { wgsLat, wgsLon };
                return result;
            }
            else
            {
                double[] GCJ = Delta(wgsLat, wgsLon);
                double[] result = { GCJ[0] + wgsLat, GCJ[1] + wgsLon };
                return result;
            }
        }

        private double[] GCJConvertToWGS(double gcjLat, double gcjLon)
        {

            double threshold = 0.000001;
            double dLat = 0.01;
            double dLon = 0.01;
            double mLat = gcjLat - dLat;
            double mLon = gcjLon - dLon;
            double pLat = gcjLat + dLat;
            double pLon = gcjLon + dLon;
            double wgsLat = 0;
            double wgsLon = 0;
            int i;
            for (i = 0; i < 31; i++)
            {
                wgsLat = (mLat + pLat) / 2;
                wgsLon = (mLon + pLon) / 2;
                double tmp_lat = WGSConvertToGCJ(wgsLat, wgsLon)[0];
                double tmp_lon = WGSConvertToGCJ(wgsLat, wgsLon)[1];
                dLat = tmp_lat - gcjLat;
                dLon = tmp_lon - gcjLon;
                if (Math.Abs(dLat) < threshold && Math.Abs(dLon) < threshold)
                {
                    break;
                }
                else if (dLat > 0.0 && i < 30)
                {
                    pLat = wgsLat;
                }
                else
                {
                    mLat = wgsLat;
                }
                if (dLon > 0.0)
                {
                    pLon = wgsLon;
                }
                else
                {
                    mLon = wgsLon;
                }
            }
            double[] result = { wgsLat, wgsLon };
            return result;
        }
        private double[] BDConvertToWGS(double bdLat, double bdLon)
        {
            double[] gcj = BDConvertToGCJ(bdLat, bdLon);
            return GCJConvertToWGS(gcj[0], gcj[1]);
        }
        private double[] WGConvertToBD(double wgsLat, double wgsLon)
        {
            double[] gcj = WGSConvertToGCJ(wgsLat, wgsLon);
            return GCJConvertToBD(gcj[0], gcj[1]);
        }
        #endregion
    }
}
