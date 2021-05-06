using System.Linq;
using System.Web;
using IRepositories;

namespace Affidavit.Utils
{
    public class GAnalitycsHelper
    {
        private const string GANALITYCS_ID = "GAnalitycsID";

        /// <summary>
        /// Method for get the GetGAnalitycsID
        /// </summary>
        /// <returns>String with the GetGAnalitycsID</returns>
        public static string GetGAnalitycsID()
        {
            return System.Configuration.ConfigurationManager.AppSettings[GANALITYCS_ID].ToString();
        }

        public static string GetGAnalitycsURL()
        {
            return "https://www.googletagmanager.com/gtag/js?id=" + System.Configuration.ConfigurationManager.AppSettings[GANALITYCS_ID].ToString();
        }
    }
}