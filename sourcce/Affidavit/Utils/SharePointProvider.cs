using IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affidavit.Utils
{
    public class SharePointProvider : ISharePointProvider
    {
        private const string SHAREPOINT_URL_KEY = "SharePointUrl";
        private const string SHAREPOINT_USER_KEY = "SharePointUser";
        private const string SHAREPOINT_PASSWORD_KEY = "SharePointPassword";
        private const string SHAREPOINT_FOLDER_KEY = "SharePointFolder";

        public string GetSharePointUrl()
        {
            return System.Configuration.ConfigurationManager.AppSettings[SHAREPOINT_URL_KEY].ToString();
        }

        public string GetSharePointUser()
        {
            return System.Configuration.ConfigurationManager.AppSettings[SHAREPOINT_USER_KEY].ToString();
        }

        public string GetSharePointPassword()
        {
            return System.Configuration.ConfigurationManager.AppSettings[SHAREPOINT_PASSWORD_KEY].ToString();
        }

        public string GetSharePointFolder()
        {
            return System.Configuration.ConfigurationManager.AppSettings[SHAREPOINT_FOLDER_KEY].ToString();
        }
    }
}