using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IRepositories;

namespace Affidavit.Utils
{
    public class SOMSightHelper
    {
        private const string SOMSIGHT_PATH_KEY = "SOMSightPath";

        /// <summary>
        /// Method for get the SOM Sight Path
        /// </summary>
        /// <returns>String with the SOM Sight Path</returns>
        public static string GetSOMSightPath()
        {
            return System.Configuration.ConfigurationManager.AppSettings[SOMSIGHT_PATH_KEY].ToString();
        }
    }
}