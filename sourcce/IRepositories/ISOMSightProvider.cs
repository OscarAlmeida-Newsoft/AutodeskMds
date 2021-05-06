using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface ISOMSightProvider
    {
        /// <summary>
        /// Method for get the SOM Sight Path
        /// </summary>
        /// <returns>String with the SOM Sight Path</returns>
        string GetSOMSightPath();
    }
}
