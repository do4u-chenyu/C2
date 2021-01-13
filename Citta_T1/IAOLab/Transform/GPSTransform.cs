using ICSharpCode.TextEditor.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.IAOLab.Transform
{
    public class Transform
    {
        public static double pi = 3.14159265358979324;
        public static double x_pi = pi* 3000.0 / 180.0;
        public static double earthR = 6371000;
        private List<double> GCJ_BD(double gcj_lat, double gcj_lon)
        {
            double z = Math.Sqrt(Math.Pow(gcj_lon, 2) + Math.Pow(gcj_lat, 2)) + 0.00002 * Math.Sin(gcj_lat * x_pi);
            double theta = Math.Atan2(gcj_lat, gcj_lon) + 0.000003 * Math.Cos(gcj_lon * x_pi);
            List<double> result = new List<double>() { 
                z * Math.Cos(theta) + 0.0065, 
                z * Math.Sin(theta) + 0.006 };
            return result;
        }
        private List<double> bd_gcj(double bd_lon, double bd_lat)
        {
            // 这个还传参数干嘛？？？
            bd_lon -= -0.0065;
            bd_lat -= -0.006;
            double z = Math.Sqrt(bd_lon * bd_lon + bd_lat * bd_lat) - 0.00002 * Math.Sin(bd_lat * x_pi);
            double theta = Math.Atan2(bd_lat, bd_lon) - 0.000003 * Math.Cos(bd_lon * x_pi);
            List<double> result = new List<double>() { z * Math.Cos(theta), z * Math.Sin(theta) };
 
            return result;
        }
        private double distance(double a_lat, double a_lon, double b_lat, double b_lon)
        {
            double x = Math.Cos(a_lat * pi / 180) * Math.Cos(b_lat * pi / 180) * Math.Cos((a_lon - b_lon) * pi / 180);
            double y = Math.Sin(a_lat * pi / 180) * Math.Sin(b_lat * pi / 180);
            double s = x + y;
            double alpha = Math.Acos(Math.Max(-1.0, Math.Min(1.0, s)));
            return alpha * earthR;
        }
        private double easy_distance(double a_lat, double a_lon, double b_lat, double b_lon)
        {
            double z =Math.Pow((a_lon - b_lon),2)* 12321 + Math.Pow((a_lat - b_lat), 2)* 8574;
            return Math.Sqrt(z);
        }
        public List<double> transform(double x, double y)
        {
 
            double absX = Math.Sqrt(Math.Abs(x));
            double d = (20.0 * Math.Sin(6.0 * x * pi) + 20.0 * Math.Sin(2.0 * x * pi)) * 2.0 / 3.0;
            double lat = -100.0 + 2.0 * x + 3.0 * y + 0.2 * y * y + 0.1 * x * y + 0.2 * absX + d;
            double lon = 300.0 + x + 2.0 * y + 0.1 * x * x + 0.1 * x * y + 0.1 * absX + d;
            lat += (20.0 * Math.Sin(y * pi) + 40.0 * Math.Sin(y / 3.0 * pi)) * 2.0 / 3.0;
            lon += (20.0 * Math.Sin(x * pi) + 40.0 * Math.Sin(x / 3.0 * pi)) * 2.0 / 3.0;
            lat += (160.0 * Math.Sin(y / 12.0 * pi) + 320 * Math.Sin(y / 30.0 * pi)) * 2.0 / 3.0;
            lon += (150.0 * Math.Sin(x / 12.0 * pi) + 300.0 * Math.Sin(x / 30.0 * pi)) * 2.0 / 3.0;
            List<double> result = new List<double>() {lat, lon};
            return result;
        }
        private bool out_of_china(double lat, double lon)
        {
            if(lon < 72.004||lon > 137.8347)
            {
                return true;
            }
            else if (lat < 0.8293||lat > 55.8271)
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
            double _lat = transform(log - 105.0, lat - 35.0)[0];
            double _lon = transform(log - 105.0, lat - 35.0)[1];
            double radLat = lat / 180.0 * pi;
            double magic = 1 - ee * (Math.Sin(radLat) * 2);
            double sqrtMagic = Math.Sqrt(magic);
            _lat = (_lat * 180.0) / ((a * (1 - ee)) / (magic * sqrtMagic) * pi);
            _lon = (_lon * 180.0) / (a / sqrtMagic * Math.Cos(radLat) * pi);
            List<double> result = new List<double>() { _lat, _lon };
            return result;
        }
        private List<double> wgs_gcj(double wgsLat, double wgsLon)
        {
            if(out_of_china(wgsLat, wgsLon))
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
        private List<double> Easy_GCJ_WGS(double gcjLat, double gcjLon)
        {
            if (out_of_china(gcjLat, gcjLon))
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
        private List<double> GCJ_WGS(double gcj_lat, double gcj_lon)
        {
          
            double threshold = 0.000001;
            double d_lat = 0.01;
            double d_lon = 0.01;
            double m_lat = gcj_lat - d_lat;
            double m_lon = gcj_lon - d_lon;
            double p_lat = gcj_lat + d_lat;
            double p_lon = gcj_lon + d_lon;
            double wgsLat = 0;
            double wgs_lon = 0;
            int i;
            for (i = 0; i < 31; i++)
            {
                wgsLat = (m_lat + p_lat) / 2;
                wgs_lon = (m_lon + p_lon) / 2;
                double tmp_lat = wgs_gcj(wgsLat, wgs_lon)[0];
                double tmp_lon = wgs_gcj(wgsLat, wgs_lon)[1];
                d_lat = tmp_lat - gcj_lat;
                d_lon = tmp_lon - gcj_lon;
                if (Math.Abs(d_lat) < threshold && Math.Abs(d_lon) < threshold)
                {
                    break;
                }
                else if (d_lat > 0.0&&i<30)
                {
                    p_lat = wgsLat;
                }
                else
                {
                    m_lat = wgsLat;
                }
                if (d_lon > 0.0)
                {
                    p_lon = wgs_lon;
                }
                else
                {
                    m_lon = wgs_lon;
                }
            }
            List<double> result = new List<double>() { wgsLat, wgs_lon };
            return result;
        }
        private List<double> bd_wgs(double bd_lat, double bd_lon)
        {           
            List<double> gcj = bd_gcj(bd_lat, bd_lon);
            return GCJ_WGS(gcj[0], gcj[1]);
        }
        private List<double> wgs_bd(double wgs_lat, double wgs_lon)
        {      
            List<double> gcj = wgs_gcj(wgs_lat, wgs_lon);
            return GCJ_WGS(gcj[0], gcj[1]);
        }
    }
}
