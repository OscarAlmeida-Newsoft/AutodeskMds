using IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affidavit.Utils
{
    public class SAM360Provider : ISAM360Provider
    {
        private const string SAM360_ON_KEY = "SAM360On";
        private const string SAM360_API_URL_KEY = "SAM360ApiUrl";
        private const string SAM360_ADMIN_USER_KEY = "SAM360AdminUser";
        private const string SAM360_ADMIN_PASSWORD_KEY = "SAM360AdminPassword";
        private const string SAM360_USER_SHARED_KEY = "SAM360UserSharedKey";
        private const string SAM360_PASSWORD_SHARED_KEY = "SAM360PasswordSharedKey";
        private const string SAM360_URL_AUTHENTICATION_KEY = "SAM360UrlAuthentication";


        public string GetSAM360On()
        {
            return System.Configuration.ConfigurationManager.AppSettings[SAM360_ON_KEY].ToString();
        }

        public string GetSAM360ApiUrl()
        {
            return System.Configuration.ConfigurationManager.AppSettings[SAM360_API_URL_KEY].ToString();
        }

        public string GetSAM360AdminUser()
        {
            return System.Configuration.ConfigurationManager.AppSettings[SAM360_ADMIN_USER_KEY].ToString();
        }

        public string GetSAM360AdminPassword()
        {
            return System.Configuration.ConfigurationManager.AppSettings[SAM360_ADMIN_PASSWORD_KEY].ToString();
        }

        public string GetSAM360UserSharedKey()
        {
            return System.Configuration.ConfigurationManager.AppSettings[SAM360_USER_SHARED_KEY].ToString();
        }

        public string GetSAM360PasswordSharedKey()
        {
            return System.Configuration.ConfigurationManager.AppSettings[SAM360_PASSWORD_SHARED_KEY].ToString();
        }

        public string GetSAM360UrlAuthentication()
        {
            return System.Configuration.ConfigurationManager.AppSettings[SAM360_URL_AUTHENTICATION_KEY].ToString();
        }

    }
}