using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOMSight.Helpers
{
    public static class ConfigHelper
    {
        private const string BASE_PATH_KEY = "BasePath";
        private const string BASE_PATH_IMAGES_KEY = "BasePathImages";
        private const string ASSIGNED_CONSULTANT = "AssignedConsultant";
        private const string CONSULTAT_EMAIL = "ConsultantEmail";


        public static string GetSiteBasePath()
        {
            return System.Configuration.ConfigurationManager.AppSettings[BASE_PATH_KEY].ToString();
        }

        public static string GetSiteBasePathImages()
        {
            return System.Configuration.ConfigurationManager.AppSettings[BASE_PATH_IMAGES_KEY].ToString();
        }

        public static string GetAssignedConsultant()
        {
            return System.Configuration.ConfigurationManager.AppSettings[ASSIGNED_CONSULTANT].ToString();
        }

        public static string GetConsultantEmail()
        {
            return System.Configuration.ConfigurationManager.AppSettings[CONSULTAT_EMAIL].ToString();
        }
    }
}