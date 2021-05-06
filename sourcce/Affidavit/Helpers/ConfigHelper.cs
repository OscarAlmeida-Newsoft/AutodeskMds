using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affidavit.Helpers
{
    public static class ConfigHelper
    {
        private const string BASE_PATH_KEY = "BasePath";

        public static string GetSiteBasePath()
        {
            return System.Configuration.ConfigurationManager.AppSettings[BASE_PATH_KEY].ToString();
        }
    }
}