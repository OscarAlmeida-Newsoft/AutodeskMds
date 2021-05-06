using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    /// <summary>
    /// Interface for extract the implementation of the Connection with CRM Software
    /// </summary>
    public interface ICRMConnectionProvider
    {
        /// <summary>
        /// Method for get the connection Url
        /// </summary>
        /// <returns>String with the connection Url </returns>
        string GetUrlConnection();

        /// <summary>
        /// Method for get the connection User
        /// </summary>
        /// <returns>String with the connection User </returns>
        string GetUserConnection();

        /// <summary>
        /// Method for get the connection Secure
        /// </summary>
        /// <returns>String with the connection Secure </returns>
        string GetSecureConnection();

        /// <summary>
        /// Method for get connection sting dynamic CRM 365
        /// </summary>
        /// <returns>string with the connection string</returns>
        string GetCRMConnectionString();
    }
}
