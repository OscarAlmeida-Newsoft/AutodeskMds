using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface ISMTPConnectionProvider
    {
        /// <summary>
        /// Method for get the SMTP Server
        /// </summary>
        /// <returns>String with the SMTP Server</returns>
        string GetSMTPServerConnection();


        /// <summary>
        /// Method for get the SMTP port
        /// </summary>
        /// <returns>String with the SMTP Port</returns>
        string GetPortConnection();


        /// <summary>
        /// Method for get the SMTP User
        /// </summary>
        /// <returns>String with the SMTP User</returns>
        string GetUserConnection();


        /// <summary>
        /// Method for get the SMTP connection Secure
        /// </summary>
        /// <returns>String with the SMTP connection Secure</returns>
        string GetSecureConnection();


        /// <summary>
        /// Method for get the SMTP connection SSL
        /// </summary>
        /// <returns>String with the SMTP connection Secure</returns>
        string GetSSLConnection();
    }
}
