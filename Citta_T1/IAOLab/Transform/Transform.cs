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
        private List<double> gcj_bd(double gcj_lat, double gcj_lon)
        {
            double x = gcj_lon;
            double y = gcj_lat;
            double z = System.Math.Sqrt(x * x + y * y) + 0.00002 * System.Math.Sin(y * x_pi);
            double theta = System.Math.Atan2(y, x) + 0.000003 * System.Math.Cos(x * x_pi);
            double bd_lon = z * System.Math.Cos(theta) + 0.0065;
            double bd_lat = z * System.Math.Sin(theta) + 0.006;
            List<double> result = new List<double>() { bd_lat, bd_lon };
            return result;
        }
        private List<double> bd_gcj(double bd_lon, double bd_lat)
        {           
            double x = bd_lon - 0.0065;
            double y = bd_lat - 0.006;
            double z = System.Math.Sqrt(x * x + y * y) - 0.00002 * System.Math.Sin(y * x_pi);
            double theta = System.Math.Atan2(y, x) - 0.000003 * System.Math.Cos(x * x_pi);
            double gcj_lon = z * System.Math.Cos(theta);
            double gcj_lat = z * System.Math.Sin(theta);
            List<double> result = new List<double>() { gcj_lat , gcj_lon };
            return result;
        }
        private double distance(double a_lat, double a_lon, double b_lat, double b_lon)
        {
            double x = System.Math.Cos(a_lat * pi / 180) * System.Math.Cos(b_lat * pi / 180) * System.Math.Cos((a_lon - b_lon) * pi / 180);
            double y = System.Math.Sin(a_lat * pi / 180) * System.Math.Sin(b_lat * pi / 180);
            double s = x + y;
            s = System.Math.Min(1.0, s);
            s = System.Math.Max(-1.0, s);
            double alpha = System.Math.Acos(s);
            return alpha * earthR;
        }
        private double easy_distance(double a_lat, double a_lon, double b_lat, double b_lon)
        {
            double z = (a_lon - b_lon) * (a_lon - b_lon) * 12321 + (a_lat - b_lat) * (a_lat - b_lat) * 8574;
            return System.Math.Sqrt(z);
        }
        public List<double> transform(double x, double y)
        {
            double xy = x * y;
            double absX = System.Math.Sqrt(System.Math.Abs(x));
            double d = (20.0 * System.Math.Sin(6.0 * x * pi) + 20.0 * System.Math.Sin(2.0 * x * pi)) * 2.0 / 3.0;
            double lat = -100.0 + 2.0 * x + 3.0 * y + 0.2 * y * y + 0.1 * xy + 0.2 * absX + d;
            double lon = 300.0 + x + 2.0 * y + 0.1 * x * x + 0.1 * xy + 0.1 * absX + d;
            lat += (20.0 * System.Math.Sin(y * pi) + 40.0 * System.Math.Sin(y / 3.0 * pi)) * 2.0 / 3.0;
            lon += (20.0 * System.Math.Sin(x * pi) + 40.0 * System.Math.Sin(x / 3.0 * pi)) * 2.0 / 3.0;
            lat += (160.0 * System.Math.Sin(y / 12.0 * pi) + 320 * System.Math.Sin(y / 30.0 * pi)) * 2.0 / 3.0;
            lon += (150.0 * System.Math.Sin(x / 12.0 * pi) + 300.0 * System.Math.Sin(x / 30.0 * pi)) * 2.0 / 3.0;
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
        private List<double> delta(double lat, double log)
        {
            double a = 6378245.0;
            double ee = 0.00669342162296594323;            
            double _lat = transform(log - 105.0, lat - 35.0)[0];
            double _lon = transform(log - 105.0, lat - 35.0)[1];
            double radLat = lat / 180.0 * pi;
            double magic = 1 - ee * (System.Math.Sin(radLat) * 2);
            double sqrtMagic = System.Math.Sqrt(magic);
            _lat = (_lat * 180.0) / ((a * (1 - ee)) / (magic * sqrtMagic) * pi);
            _lon = (_lon * 180.0) / (a / sqrtMagic * System.Math.Cos(radLat) * pi);
            List<double> result = new List<double>() { _lat, _lon };
            return result;
        }
        private List<double> wgs_gcj(double wgs_lat, double wgs_lon)
        {
            if(out_of_china(wgs_lat, wgs_lon))
            {
                List<double> result = new List<double>() { wgs_lat, wgs_lon };
                return result;
            }
            else
            {
                double gcj_lat = delta(wgs_lat, wgs_lon)[0];
                double gcj_lon = delta(wgs_lat, wgs_lon)[1];
                List<double> result = new List<double>() { gcj_lat + wgs_lat, gcj_lon + wgs_lon };
                return result;
            }
        }
        private List<double> easy_gcj_wgs(double gcj_lat, double gcj_lon)
        {
            if (out_of_china(gcj_lat, gcj_lon))
            {
                List<double> result = new List<double>() { gcj_lat, gcj_lon };
                return result;
            }
            else
            {
                double wgs_lat = delta(gcj_lat, gcj_lon)[0];
                double wgs_lon = delta(gcj_lat, gcj_lon)[1];
                List<double> result = new List<double>() { gcj_lat - wgs_lat, gcj_lon - wgs_lon };
                return result;
            }
        }
        private List<double> gcj_wgs(double gcj_lat, double gcj_lon)
        {
            double initDelta = 0.01;
            double threshold = 0.000001;
            double d_lat = initDelta;
            double d_lon = initDelta;
            double m_lat = gcj_lat - d_lat;
            double m_lon = gcj_lon - d_lon;
            double p_lat = gcj_lat + d_lat;
            double p_lon = gcj_lon + d_lon;
            double wgs_lat = 0;
            double wgs_lon = 0;
            int i;
            for (i = 0; i < 31; i++)
            {
                wgs_lat = (m_lat + p_lat) / 2;
                wgs_lon = (m_lon + p_lon) / 2;
                double tmp_lat = wgs_gcj(wgs_lat, wgs_lon)[0];
                double tmp_lon = wgs_gcj(wgs_lat, wgs_lon)[1];
                d_lat = tmp_lat - gcj_lat;
                d_lon = tmp_lon - gcj_lon;
                if (System.Math.Abs(d_lat) < threshold && System.Math.Abs(d_lon) < threshold)
                {
                    break;
                }
                else if (d_lat > 0.0&&i<30)
                {
                    p_lat = wgs_lat;
                }
                else
                {
                    m_lat = wgs_lat;
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
            List<double> result = new List<double>() { wgs_lat, wgs_lon };
            return result;
        }
        private List<double> bd_wgs(double bd_lat, double bd_lon)
        {
            double gcj_lat = bd_gcj(bd_lat, bd_lon)[0];
            double gcj_lon = bd_gcj(bd_lat, bd_lon)[1];
            List<double> result = new List<double>() { gcj_wgs(gcj_lat, gcj_lon)[0], gcj_wgs(gcj_lat, gcj_lon)[1] };
            return result;
        }
        private List<double> wgs_bd(double wgs_lat, double wgs_lon)
        {
            double gcj_lat = wgs_gcj(wgs_lat, wgs_lon)[0];
            double gcj_lon = wgs_gcj(wgs_lat, wgs_lon)[1];
            List<double> result = new List<double>() { gcj_wgs(gcj_lat, gcj_lon)[0], gcj_wgs(gcj_lat, gcj_lon)[1] };
            return result;
        }
    }
}
