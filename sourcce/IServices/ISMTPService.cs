using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface ISMTPService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pTo"></param>
        /// <param name="pSubject"></param>
        /// <param name="pBody"></param>
        string EnviarCorreo(string pTo, string pSubject, string pBody);
    }
}
