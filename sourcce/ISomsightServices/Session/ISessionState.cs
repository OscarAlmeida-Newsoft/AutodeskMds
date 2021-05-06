using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*
 * 
 * The solution was taken regarding the below post 
 * https://markgwilliamsblog.wordpress.com/2013/01/20/creating-a-wrapper-around-the-session-object/
 * 
*/

namespace ISOMSightServices
{


    public interface ISessionState
    {
        void Clear();
        void Delete(string pKey);
        object Get(string pKey);
        T Get<T>(string pKey) where T : class;
        ISessionState Store(string pKey, object value);


    }
}
