using IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affidavit.Utils
{
    public class SMTPConnectionProvider : ISMTPConnectionProvider
    {
        private const string SMTP_SERVEY_KEY = "SMTPServer";
        private const string SMTP_PORT_KEY = "SMTPPort";
        private const string SMTP_USER_KEY = "SMTPUser";
        private const string SMTP_SECURE_KEY = "SMTPSecure";
        private const string SMTP_SSL_KEY = "SMTPSSL";

        /// <summary>
        /// Method for get the SMTP Server
        /// </summary>
        /// <returns>String with the SMTP Server</returns>
        public string GetSMTPServerConnection()
        {
            return System.Configuration.ConfigurationManager.AppSettings[SMTP_SERVEY_KEY].ToString();
        }


        /// <summary>
        /// Method for get the SMTP port
        /// </summary>
        /// <returns>String with the SMTP Port</returns>
        public string GetPortConnection()
        {
            return System.Configuration.ConfigurationManager.AppSettings[SMTP_PORT_KEY].ToString();
        }


        /// <summary>
        /// Method for get the SMTP User
        /// </summary>
        /// <returns>String with the SMTP User</returns>
        public string GetUserConnection()
        {
            return System.Configuration.ConfigurationManager.AppSettings[SMTP_USER_KEY].ToString();
        }


        /// <summary>
        /// Method for get the SMTP connection Secure
        /// </summary>
        /// <returns>String with the SMTP connection Secure</returns>
        public string GetSecureConnection()
        {
            return System.Configuration.ConfigurationManager.AppSettings[SMTP_SECURE_KEY].ToString();
        }


        /// <summary>
        /// Method for get the SMTP connection Secure
        /// </summary>
        /// <returns>String with the SMTP connection Secure</returns>
        public string GetSSLConnection()
        {
            return System.Configuration.ConfigurationManager.AppSettings[SMTP_SSL_KEY].ToString();
        }

    }
}