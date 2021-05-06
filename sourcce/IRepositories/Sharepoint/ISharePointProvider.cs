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
    public interface ISharePointProvider
    {

        string GetSharePointUrl();

        string GetSharePointUser();

        string GetSharePointPassword();

        string GetSharePointFolder();
    }
}
