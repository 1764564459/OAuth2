using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameWork.Server.Helper
{
    public class GPS_To_Baidu
    {
        // const double pi = 3.14159265358979324;
        // //  
        // // Krasovsky 1940  
        // //   
        // // a = 6378245.0, 1/f = 298.3   
        // // b = a * (1 - f)  
        // // ee = (a^2 - b^2) / a^2;   
        // const double a = 6378245.0;
        // const double ee = 0.00669342162296594323;
        // static bool outOfChina(double lat, double lon)
        // {
        //     if (lon < 72.004 || lon > 137.8347)
        //         return true;
        //     if (lat < 0.8293 || lat > 55.8271)
        //         return true;
        //     return false;
        // }
        // static double transformLat(double x, double y)
        // {
        //     double ret = -100.0 + 2.0 * x + 3.0 * y + 0.2 * y * y + 0.1 * x * y + 0.2 * Math.Sqrt(Math.Abs(x));
        //     ret += (20.0 * Math.Sin(6.0 * x * pi) + 20.0 * Math.Sin(2.0 * x * pi)) * 2.0 / 3.0;
        //     ret += (20.0 * Math.Sin(y * pi) + 40.0 * Math.Sin(y / 3.0 * pi)) * 2.0 / 3.0;
        //     ret += (160.0 * Math.Sin(y / 12.0 * pi) + 320 * Math.Sin(y * pi / 30.0)) * 2.0 / 3.0;
        //     return ret;
        // }
        // static double transformLon(double x, double y)
        // {
        //     double ret = 300.0 + x + 2.0 * y + 0.1 * x * x + 0.1 * x * y + 0.1 * Math.Sqrt(Math.Abs(x));
        //     ret += (20.0 * Math.Sin(6.0 * x * pi) + 20.0 * Math.Sin(2.0 * x * pi)) * 2.0 / 3.0;
        //     ret += (20.0 * Math.Sin(x * pi) + 40.0 * Math.Sin(x / 3.0 * pi)) * 2.0 / 3.0;
        //     ret += (150.0 * Math.Sin(x / 12.0 * pi) + 300.0 * Math.Sin(x / 30.0 * pi)) * 2.0 / 3.0;
        //     return ret;
        // }
        // /* 
        // 参数 
        // wgLat:WGS-84纬度wgLon:WGS-84经度 
        // 返回值： 
        // mgLat：GCJ-02纬度mgLon：GCJ-02经度 
        // */
        //public static double[] gps_transform(double wgLat, double wgLon, double mgLat=0.0, double mgLon=0.0)
        // {
        //     if (outOfChina(wgLat, wgLon))
        //     {
        //         mgLat = wgLat;
        //         mgLon = wgLon;
        //         //return [0.0];
        //     }
        //     double dLat = transformLat(wgLon - 105.0, wgLat - 35.0);
        //     double dLon = transformLon(wgLon - 105.0, wgLat - 35.0);
        //     double radLat = wgLat / 180.0 * pi; double magic = Math.Sin(radLat);
        //     magic = 1 - ee * magic * magic; double sqrtMagic = Math.Sqrt(magic);
        //     dLat = (dLat * 180.0) / ((a * (1 - ee)) / (magic * sqrtMagic) * pi);
        //     dLon = (dLon * 180.0) / (a / sqrtMagic * Math.Cos(radLat) * pi);
        //     mgLat = wgLat + dLat; mgLon = wgLon + dLon;
        //     return bd_encrypt(mgLat, mgLon);
        // }

        // const double x_pi = 3.14159265358979324 * 3000.0 / 180.0;
        // //将 GCJ-02 坐标转换成 BD-09 坐标  
        //public static  double[] bd_encrypt(double gg_lat, double gg_lon, double bd_lat=0.0, double bd_lon=0.0)
        // {
        //     double x = gg_lon, y = gg_lat;
        //     double z = Math.Sqrt(x * x + y * y) + 0.00002 * Math.Sin(y * x_pi);
        //     double theta = Math.Atan2(y, x) + 0.000003 * Math.Cos(x * x_pi);
        //     bd_lon = z *Math.Cos(theta) + 0.0065;
        //     bd_lat = z * Math.Sin(theta) + 0.006;
        //     double[] point = new double[] { bd_lon, bd_lat };
        //     return   point;
        // }

        // void bd_decrypt(double bd_lat, double bd_lon, double gg_lat, double gg_lon)
        // {
        //     double x = bd_lon - 0.0065, y = bd_lat - 0.006;
        //     double z =Math.Sqrt(x * x + y * y) - 0.00002 * Math.Sin(y * x_pi);
        //     double theta =Math.Atan2(y, x) - 0.000003 * Math.Cos(x * x_pi);
        //     gg_lon = z * Math.Cos(theta);
        //     gg_lat = z * Math.Sin(theta);
        // }

        static double pi = 3.14159265358979324;
        static double a = 6378245.0;
        static double ee = 0.00669342162296594323;
        public const double x_pi = 3.14159265358979324 * 3000.0 / 180.0;

        /// <summary>
        /// gps坐标转换成百度坐标，小数点前4位为准确坐标
        /// </summary>
        /// <param name="lat">纬度</param>
        /// <param name="lon">经度</param>
        /// <returns></returns>
        public static double[] wgs2bd(double lat, double lon)
        {
            double[] dwgs2gcj = wgs2gcj(lat, lon);
            double[] dgcj2bd = gcj2bd(dwgs2gcj[0], dwgs2gcj[1]);
            return dgcj2bd;
        }

        public static double[] gcj2bd(double lat, double lon)
        {
            double x = lon, y = lat;
            double z = Math.Sqrt(x * x + y * y) + 0.00002 * Math.Sin(y * x_pi);
            double theta = Math.Atan2(y, x) + 0.000003 * Math.Cos(x * x_pi);
            double bd_lon = z * Math.Cos(theta) + 0.0065;
            double bd_lat = z * Math.Sin(theta) + 0.006;
            return new double[] { bd_lat, bd_lon };
        }

        public static double[] bd2gcj(double lat, double lon)
        {
            double x = lon - 0.0065, y = lat - 0.006;
            double z = Math.Sqrt(x * x + y * y) - 0.00002 * Math.Sin(y * x_pi);
            double theta = Math.Atan2(y, x) - 0.000003 * Math.Cos(x * x_pi);
            double gg_lon = z * Math.Cos(theta);
            double gg_lat = z * Math.Sin(theta);
            return new double[] { gg_lat, gg_lon };
        }

        public static double[] wgs2gcj(double lat, double lon)
        {
            double dLat = transformLat(lon - 105.0, lat - 35.0);
            double dLon = transformLon(lon - 105.0, lat - 35.0);
            double radLat = lat / 180.0 * pi;
            double magic = Math.Sin(radLat);
            magic = 1 - ee * magic * magic;
            double sqrtMagic = Math.Sqrt(magic);
            dLat = (dLat * 180.0) / ((a * (1 - ee)) / (magic * sqrtMagic) * pi);
            dLon = (dLon * 180.0) / (a / sqrtMagic * Math.Cos(radLat) * pi);
            double mgLat = lat + dLat;
            double mgLon = lon + dLon;
            double[] loc = { mgLat, mgLon };
            return loc;
        }

        private static double transformLat(double lat, double lon)
        {
            double ret = -100.0 + 2.0 * lat + 3.0 * lon + 0.2 * lon * lon + 0.1 * lat * lon + 0.2 * Math.Sqrt(Math.Abs(lat));
            ret += (20.0 * Math.Sin(6.0 * lat * pi) + 20.0 * Math.Sin(2.0 * lat * pi)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(lon * pi) + 40.0 * Math.Sin(lon / 3.0 * pi)) * 2.0 / 3.0;
            ret += (160.0 * Math.Sin(lon / 12.0 * pi) + 320 * Math.Sin(lon * pi / 30.0)) * 2.0 / 3.0;
            return ret;
        }

        private static double transformLon(double lat, double lon)
        {
            double ret = 300.0 + lat + 2.0 * lon + 0.1 * lat * lat + 0.1 * lat * lon + 0.1 * Math.Sqrt(Math.Abs(lat));
            ret += (20.0 * Math.Sin(6.0 * lat * pi) + 20.0 * Math.Sin(2.0 * lat * pi)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(lat * pi) + 40.0 * Math.Sin(lat / 3.0 * pi)) * 2.0 / 3.0;
            ret += (150.0 * Math.Sin(lat / 12.0 * pi) + 300.0 * Math.Sin(lat / 30.0 * pi)) * 2.0 / 3.0;
            return ret;
        }
    }
}
