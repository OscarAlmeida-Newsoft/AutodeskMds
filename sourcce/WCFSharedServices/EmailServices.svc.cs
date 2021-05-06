using Microsoft.Practices.Unity;
using SharedEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFSharedServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EmailServices" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select EmailServices.svc or EmailServices.svc.cs at the Solution Explorer and start debugging.
    public class EmailServices : IEmailServices
    {
        private UnityContainer _unityContainer;
        private SharedServices.EmailServices _emailServices;

        public EmailServices()
        {
            //Unity container instance
            if (_unityContainer == null)
            {
                _unityContainer = new UnityContainer();
            }



            //Resolve Email Services class
            if (_emailServices == null)
            {
                _emailServices = _unityContainer.Resolve<SharedServices.EmailServices>();
            }
        }

        public void SendEmail(EmailQueue pEmailInfo)
        {
            _emailServices.SendEmail(pEmailInfo);
        }
    }
}
