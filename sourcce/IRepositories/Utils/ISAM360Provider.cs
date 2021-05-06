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
    public interface ISAM360Provider
    {
        string GetSAM360On();
        string GetSAM360ApiUrl();
        string GetSAM360AdminUser();
        string GetSAM360AdminPassword();
        string GetSAM360UserSharedKey();
        string GetSAM360PasswordSharedKey();
        string GetSAM360UrlAuthentication();

    }
}
