using IRepositories;

namespace Affidavit.Utils
{   

    public class CRMConnectionProvider : ICRMConnectionProvider
    {
        private const string CRM_URL_KEY = "CRMUrl";
        private const string CRM_USER_KEY = "CRMUser";
        private const string CRM_SECURE_KEY = "CRMSecure";
        private const string CRM_CONNECTION_STRING_KEY = "MyCRMServer";


        /// <summary>
        /// Method for get the connection Secure
        /// </summary>
        /// <returns>String with the connection Secure </returns>
        public string GetSecureConnection()
        {
            return System.Configuration.ConfigurationManager.AppSettings[CRM_SECURE_KEY].ToString();
        }


        /// <summary>
        /// Method for get the connection Url
        /// </summary>
        /// <returns>String with the connection Url </returns>
        public string GetUrlConnection()
        {
            return System.Configuration.ConfigurationManager.AppSettings[CRM_URL_KEY].ToString();
        }


        /// <summary>
        /// Method for get the connection User
        /// </summary>
        /// <returns>String with the connection User </returns>
        public string GetUserConnection()
        {
            return System.Configuration.ConfigurationManager.AppSettings[CRM_USER_KEY].ToString();

        }

        public string GetCRMConnectionString()
        {
            string _connString = System.Configuration.ConfigurationManager.ConnectionStrings[CRM_CONNECTION_STRING_KEY].ConnectionString;

            return _connString;
        }
    }
}