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

        public void CoordinateConversion(string lcoation,string type)
        {
            //switch(type)
            //{ 
            //}
        }

        #region 6种坐标转换方法

        private List<double> GCJConvertToBD(double GCJLAT, double GCJLON)
        {
            double z = Math.Sqrt(Math.Pow(GCJLON, 2) + Math.Pow(GCJLAT, 2)) + 0.00002 * Math.Sin(GCJLAT * xPi);
            double theta = Math.Atan2(GCJLAT, GCJLON) + 0.000003 * Math.Cos(GCJLON * xPi);
            List<double> result = new List<double>() {
                z * Math.Cos(theta) + 0.0065,
                z * Math.Sin(theta) + 0.006 };
            return result;
        }
        private List<double> BDConvertToGCJ(double BDLON, double BDLAT)
        {
            // 这个还传参数干嘛？？？
            BDLON -= -0.0065;
            BDLAT -= -0.006;
            double z = Math.Sqrt(BDLON * BDLON + BDLAT * BDLAT) - 0.00002 * Math.Sin(BDLAT * xPi);
            double theta = Math.Atan2(BDLAT, BDLON) - 0.000003 * Math.Cos(BDLON * xPi);
            List<double> result = new List<double>() { z * Math.Cos(theta), z * Math.Sin(theta) };

            return result;
        }
        private double Distance(double ALAT, double ALON, double BLAT, double BLON)
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

        private List<double> XYTransform(double x, double y)
        {

            double absX = Math.Sqrt(Math.Abs(x));
            double d = (20.0 * Math.Sin(6.0 * x * pi) + 20.0 * Math.Sin(2.0 * x * pi)) * 2.0 / 3.0;
            double lat = -100.0 + 2.0 * x + 3.0 * y + 0.2 * y * y + 0.1 * x * y + 0.2 * absX + d;
            double lon = 300.0 + x + 2.0 * y + 0.1 * x * x + 0.1 * x * y + 0.1 * absX + d;
            lat += (20.0 * Math.Sin(y * pi) + 40.0 * Math.Sin(y / 3.0 * pi)) * 2.0 / 3.0;
            lon += (20.0 * Math.Sin(x * pi) + 40.0 * Math.Sin(x / 3.0 * pi)) * 2.0 / 3.0;
            lat += (160.0 * Math.Sin(y / 12.0 * pi) + 320 * Math.Sin(y / 30.0 * pi)) * 2.0 / 3.0;
            lon += (150.0 * Math.Sin(x / 12.0 * pi) + 300.0 * Math.Sin(x / 30.0 * pi)) * 2.0 / 3.0;
            List<double> result = new List<double>() { lat, lon };
            return result;
        }

        private List<double> CoordinateTransform(double x, double y)
        {

            double absX = Math.Sqrt(Math.Abs(x));
            double d = (20.0 * Math.Sin(6.0 * x * pi) + 20.0 * Math.Sin(2.0 * x * pi)) * 2.0 / 3.0;
            double lat = -100.0 + 2.0 * x + 3.0 * y + 0.2 * y * y + 0.1 * x * y + 0.2 * absX + d;
            double lon = 300.0 + x + 2.0 * y + 0.1 * x * x + 0.1 * x * y + 0.1 * absX + d;
            lat += (20.0 * Math.Sin(y * pi) + 40.0 * Math.Sin(y / 3.0 * pi)) * 2.0 / 3.0;
            lon += (20.0 * Math.Sin(x * pi) + 40.0 * Math.Sin(x / 3.0 * pi)) * 2.0 / 3.0;
            lat += (160.0 * Math.Sin(y / 12.0 * pi) + 320 * Math.Sin(y / 30.0 * pi)) * 2.0 / 3.0;
            lon += (150.0 * Math.Sin(x / 12.0 * pi) + 300.0 * Math.Sin(x / 30.0 * pi)) * 2.0 / 3.0;
            List<double> result = new List<double>() { lat, lon };
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
        private List<double> Delta(double lat, double log)
        {
            double a = 6378245.0;
            double ee = 0.00669342162296594323;
            // 下面公式对么？log log

            List<double> latLon = XYTransform(log - 105.0, lat - 35.0);

            //List<double> latLon = CoordinateTransform(log - 105.0, lat - 35.0);

            double radLat = lat / 180.0 * pi;
            double magic = 1 - ee * (Math.Sin(radLat) * 2);
            double sqrtMagic = Math.Sqrt(magic);
            latLon[0] = (latLon[0] * 180.0) / ((a * (1 - ee)) / (magic * sqrtMagic) * pi);
            latLon[1] = (latLon[1] * 180.0) / (a / sqrtMagic * Math.Cos(radLat) * pi);
            List<double> result = new List<double>() { latLon[0], latLon[1] };
            return result;
        }
        private List<double> WGSConvertToGCJ(double wgsLat, double wgsLon)
        {
            if (OutOfChina(wgsLat, wgsLon))
            {
                return new List<double>() { wgsLat, wgsLon };
            }
            else
            {
                List<double> GCJ = Delta(wgsLat, wgsLon);
                List<double> result = new List<double>() { GCJ[0] + wgsLat, GCJ[1] + wgsLon };
                return result;
            }
        }
        private List<double> EasyGCJWGS(double gcjLat, double gcjLon)
        {
            if (OutOfChina(gcjLat, gcjLon))
            {
                List<double> result = new List<double>() { gcjLat, gcjLon };
                return result;
            }
            else
            {
                List<double> WGS = Delta(gcjLat, gcjLon);
                List<double> result = new List<double>() { gcjLat - WGS[0], gcjLon - WGS[1] };
                return result;
            }
        }
        private List<double> GCJConvertToWGS(double gcjLat, double gcjLon)
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
            List<double> result = new List<double>() { wgsLat, wgsLon };
            return result;
        }
        private List<double> BDConvertToWGS(double bdLat, double bdLon)
        {
            List<double> gcj = BDConvertToGCJ(bdLat, bdLon);
            return GCJConvertToWGS(gcj[0], gcj[1]);
        }
        private List<double> WGConvertToSBD(double wgsLat, double wgsLon)
        {
            List<double> gcj = WGSConvertToGCJ(wgsLat, wgsLon);
            return GCJConvertToWGS(gcj[0], gcj[1]);
        }
        #endregion
    }
}
