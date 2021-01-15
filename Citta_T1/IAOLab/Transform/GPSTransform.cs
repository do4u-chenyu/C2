using ICSharpCode.TextEditor.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.IAOLab.Transform
{
    public class GPSTransform
    {
        private double pi = 3.14159265358979324;
        private double xPi = 3.14159265358979324 * 3000.0 / 180.0;
        private double earthR = 6371000;
        private static GPSTransform instance;
        public static GPSTransform GetInstance()
        {
            if (instance == null)
                instance = new GPSTransform();
            return instance;
        }

        public string CoordinateConversion(string location, string type)
        {
            string[] locationArray = location.Split(' ');
            string wrong = "输入格式有误\n";
            double[] result;
            double deviation;
            double distance;
            if (locationArray.Length == 2)
            {
                try
                {
                    double lat = double.Parse(locationArray[0]);
                    double lon = double.Parse(locationArray[1]);
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
                            result = null;
                            break;
                    }
                    if (result != null)
                    {
                        deviation = Distance(lat, lon, result[0], result[1]);
                        return ("结果坐标为" + String.Join(" ", result) + "，偏差为" + deviation.ToString() + "\r\n");
                    }
                    else
                    {
                        return wrong;
                    }
                }
                catch
                {
                    return wrong;
                }

            }
            if (locationArray.Length == 4)
            {
                try
                {
                    double lat = double.Parse(locationArray[0]);
                    double lon = double.Parse(locationArray[1]);
                    double blat = double.Parse(locationArray[2]);
                    double blon = double.Parse(locationArray[3]);
                    switch (type)
                    {
                        case "distance":
                            distance = Distance(lat, lon, blat, blon);
                            return ("距离为" + distance.ToString() + "\r\n");
                        default:
                            return wrong;
                    }
                }
                catch
                {
                    return wrong;
                }
            }
            else
            {
                return wrong;
            }
        }

        #region 6种坐标转换方法

        private double[] GCJConvertToBD(double GCJLAT, double GCJLON)
        {
            double z = Math.Sqrt(Math.Pow(GCJLON, 2) + Math.Pow(GCJLAT, 2)) + 0.00002 * Math.Sin(GCJLAT * xPi);
            double theta = Math.Atan2(GCJLAT, GCJLON) + 0.000003 * Math.Cos(GCJLON * xPi);
            //double[] result = { z * Math.Cos(theta) + 0.0065, z * Math.Sin(theta) + 0.006 };
            double[] result = { z * Math.Sin(theta) + 0.006, z * Math.Cos(theta) + 0.0065, };
            return result;
        }
        private double[] BDConvertToGCJ(double BDLON, double BDLAT)
        {
            // 这个还传参数干嘛？？？
            BDLON -= -0.0065;
            BDLAT -= -0.006;
            double z = Math.Sqrt(BDLON * BDLON + BDLAT * BDLAT) - 0.00002 * Math.Sin(BDLAT * xPi);
            double theta = Math.Atan2(BDLAT, BDLON) - 0.000003 * Math.Cos(BDLON * xPi);
            double[] result = { z * Math.Cos(theta), z * Math.Sin(theta) };
            return result;
        }
        public double Distance(double ALAT, double ALON, double BLAT, double BLON)
        {
            double x = Math.Cos(ALAT * pi / 180) * Math.Cos(BLAT * pi / 180) * Math.Cos((ALON - BLON) * pi / 180);
            double y = Math.Sin(ALAT * pi / 180) * Math.Sin(BLAT * pi / 180);
            double s = x + y;
            double alpha = Math.Acos(Math.Max(-1.0, Math.Min(1.0, s)));
            return alpha * earthR;
        }
        private double EasyDistance(double ALAT, double ALON, double BLAT, double BLON)
        {
            double z = Math.Pow((ALON - BLON), 2) * 12321 + Math.Pow((ALAT - BLAT), 2) * 8574;
            return Math.Sqrt(z);
        }

        private double[] XYTransform(double x, double y)
        {

            double absX = Math.Sqrt(Math.Abs(x));
            double d = (20.0 * Math.Sin(6.0 * x * pi) + 20.0 * Math.Sin(2.0 * x * pi)) * 2.0 / 3.0;
            double lat = -100.0 + 2.0 * x + 3.0 * y + 0.2 * y * y + 0.1 * x * y + 0.2 * absX + d;
            double lon = 300.0 + x + 2.0 * y + 0.1 * x * x + 0.1 * x * y + 0.1 * absX + d;
            lat += (20.0 * Math.Sin(y * pi) + 40.0 * Math.Sin(y / 3.0 * pi)) * 2.0 / 3.0;
            lon += (20.0 * Math.Sin(x * pi) + 40.0 * Math.Sin(x / 3.0 * pi)) * 2.0 / 3.0;
            lat += (160.0 * Math.Sin(y / 12.0 * pi) + 320 * Math.Sin(y / 30.0 * pi)) * 2.0 / 3.0;
            lon += (150.0 * Math.Sin(x / 12.0 * pi) + 300.0 * Math.Sin(x / 30.0 * pi)) * 2.0 / 3.0;
            double[] result = { lat, lon };
            return result;
        }

        private double[] CoordinateTransform(double x, double y)
        {

            double absX = Math.Sqrt(Math.Abs(x));
            double d = (20.0 * Math.Sin(6.0 * x * pi) + 20.0 * Math.Sin(2.0 * x * pi)) * 2.0 / 3.0;
            double lat = -100.0 + 2.0 * x + 3.0 * y + 0.2 * y * y + 0.1 * x * y + 0.2 * absX + d;
            double lon = 300.0 + x + 2.0 * y + 0.1 * x * x + 0.1 * x * y + 0.1 * absX + d;
            lat += (20.0 * Math.Sin(y * pi) + 40.0 * Math.Sin(y / 3.0 * pi)) * 2.0 / 3.0;
            lon += (20.0 * Math.Sin(x * pi) + 40.0 * Math.Sin(x / 3.0 * pi)) * 2.0 / 3.0;
            lat += (160.0 * Math.Sin(y / 12.0 * pi) + 320 * Math.Sin(y / 30.0 * pi)) * 2.0 / 3.0;
            lon += (150.0 * Math.Sin(x / 12.0 * pi) + 300.0 * Math.Sin(x / 30.0 * pi)) * 2.0 / 3.0;
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
            // 下面公式对么？log log

            double[] latLon = XYTransform(log - 105.0, lat - 35.0);

            //List<double> latLon = CoordinateTransform(log - 105.0, lat - 35.0);

            double radLat = lat / 180.0 * pi;
            double magic = 1 - ee * (Math.Sin(radLat) * 2);
            double sqrtMagic = Math.Sqrt(magic);
            latLon[0] = (latLon[0] * 180.0) / ((a * (1 - ee)) / (magic * sqrtMagic) * pi);
            latLon[1] = (latLon[1] * 180.0) / (a / sqrtMagic * Math.Cos(radLat) * pi);
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
        private double[] EasyGCJWGS(double gcjLat, double gcjLon)
        {
            if (OutOfChina(gcjLat, gcjLon))
            {
                double[] result = { gcjLat, gcjLon };
                return result;
            }
            else
            {
                double[] WGS = Delta(gcjLat, gcjLon);
                double[] result = { gcjLat - WGS[0], gcjLon - WGS[1] };
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
            return GCJConvertToWGS(gcj[0], gcj[1]);
        }
        #endregion
    }
}
