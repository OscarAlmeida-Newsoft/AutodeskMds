using ISOMSightServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOMSightModels.Email;

namespace SOMSightServices
{
    public class EmailService : IEmailService
    {
        public void SendEmail(Email pModel)
        {
            EmailServicesReference.EmailServicesClient _emailExternalServiceClient = new EmailServicesReference.EmailServicesClient();

            EmailServicesReference.EmailQueue _emailExternalObject = new EmailServicesReference.EmailQueue {
                Body = pModel.Body,
                EmailTo = pModel.EmailTo,
                Subject = pModel.Subject
            };

            try
            {
                _emailExternalServiceClient.SendEmail(_emailExternalObject);
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
    }
}
