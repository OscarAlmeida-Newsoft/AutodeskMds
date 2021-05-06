using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOMSightModels.Users;

namespace ISOMSightServices
{
    public interface IChooseAnActionService
    {
        string Message { get; }
        string GetUrlRedirectMeasureMyPlataform(UserDTO user);
    }
}
