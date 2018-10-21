using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAnalysis.Helper
{
    public class DataAnalysisHelper
    {
        public string Error { get; set; }

        public DataAnalysisHelper()
        {
            Error = string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lat1"></param>
        /// <param name="lon1"></param>
        /// <param name="lat2"></param>
        /// <param name="lon2"></param>
        /// <returns></returns>
        public double? GetDistanceBetweenCoordinates(double lat1, double lon1, double lat2, double lon2)
        {
            double? distanceBetweenCoordinates = null;

            try
            {
                Error = string.Empty;
                double rlat1 = Math.PI * lat1 / 180;
                double rlat2 = Math.PI * lat2 / 180;
                double theta = lon1 - lon2;
                double rtheta = Math.PI * theta / 180;
                double dist =
                    Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                    Math.Cos(rlat2) * Math.Cos(rtheta);
                dist = Math.Acos(dist);
                dist = dist * 180 / Math.PI;
                dist = dist * 60 * 1.1515;
                distanceBetweenCoordinates= dist * 1.609344;
            }
            catch (Exception ex)
            {
                Error = string.Format("An unknown error occurred while calculating distance between LAT{0} LON{1} ::: LAT{2} LON{3}. Exception Details::: {5}.", lat1,ToString(), lon1.ToString(), lat2, ToString(), lon2.ToString(), ex.InnerException);
            }

            return distanceBetweenCoordinates;
        }

        /// <summary>
        /// Returns Current Directory
        /// </summary>
        /// <returns></returns>
        public string GetCurrentDirectory()
        {
            try
            {
                Error = string.Empty;
                string strCurrentDirectory = Directory.GetCurrentDirectory();
                return strCurrentDirectory;
            }
            catch (Exception ex)
            {
                Error = string.Format("An unknown error occurred while getting the current directory {0}" , ex.InnerException);
                return "";
            }
        }
    }

   
}
