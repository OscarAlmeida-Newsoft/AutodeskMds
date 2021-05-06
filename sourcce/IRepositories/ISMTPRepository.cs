using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface ISMTPRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pTo"></param>
        /// <param name="pSubject"></param>
        /// <param name="pBody"></param>
        string EnviarCorreo(String pTo, String pSubject, String pBody);
    }
}
