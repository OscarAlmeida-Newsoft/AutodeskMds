using SOMSightModels.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISOMSightServices
{
    public interface IEmailService
    {

        void SendEmail(Email pModel);
    }
}
