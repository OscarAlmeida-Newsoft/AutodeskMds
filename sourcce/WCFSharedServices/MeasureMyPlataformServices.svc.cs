using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SharedEntities;
using Microsoft.Practices.Unity;

namespace WCFSharedServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MeasureMyPlataformServices" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select MeasureMyPlataformServices.svc or MeasureMyPlataformServices.svc.cs at the Solution Explorer and start debugging.
    public class MeasureMyPlataformServices : IMeasureMyPlataformServices
    {
        private UnityContainer _unityContainer;
        private SharedServices.MeasureMyPlataformServices _measureMyPlataformService;

        public MeasureMyPlataformServices() {

            //Unity container instance
            if (_unityContainer == null)
            {
                _unityContainer = new UnityContainer();
            }

            if (_measureMyPlataformService == null) {
                _measureMyPlataformService = _unityContainer.Resolve<SharedServices.MeasureMyPlataformServices>();
            }
        }

        ////public TokenResponse GetUrlRedirectMeasureMyPlataform(UserSAMLive user)
        ////{
        ////    TokenResponse userCreation = _measureMyPlataformService.GetUrlRedirectMeasureMyPlataform(user.username);

        ////    if (string.IsNullOrEmpty(_measureMyPlataformService.Message))
        ////    {
        ////        if (!userCreation.error)
        ////        {
        ////            user.Token = userCreation.data.token;
        ////            bool respuesta = _measureMyPlataformService.UpdateUser(user);

        ////            if (string.IsNullOrEmpty(_measureMyPlataformService.Message) && respuesta)
        ////            {
        ////                userCreation = _measureMyPlataformService.GetUrlRedirectMeasureMyPlataform(user.username);
        ////            }
        ////            else {
        ////                userCreation.error = true;
        ////                userCreation.message = _measureMyPlataformService.Message;
        ////            }
        ////        }

        ////    }
        ////    else {
        ////        userCreation.error = true;
        ////        userCreation.message = _measureMyPlataformService.Message;
        ////    }



        ////    return userCreation;
        ////}

        public TokenResponse GetUrlRedirectMeasureMyPlataform(UserSAMLive user)
        {
            //SI NO VIENE HACE TODO NORMAL, SI VIENE cambia username por julian.giraldo
            TokenResponse userCreation = new TokenResponse();

            if (user.Company.Equals("EchezGroupT2") ||
                    user.Company.Equals("EchezGroupT4") ||
                    user.Company.Equals("EchezGroupT6") ||
                    user.Company.Equals("EchezGroupT8") ||                  
                   user.Company.Equals("EchezGroupT12"))
            {
                user.username = "julian.giraldo";
                userCreation = _measureMyPlataformService.GetUrlRedirectMeasureMyPlataform(user.username);
            }
            else {
                userCreation = _measureMyPlataformService.GetUrlRedirectMeasureMyPlataform(user.username);


                if (string.IsNullOrEmpty(_measureMyPlataformService.Message))
                {
                    if (!userCreation.error)
                    {
                        user.Token = userCreation.data.token;
                        bool respuesta = _measureMyPlataformService.UpdateUser(user);

                        if (string.IsNullOrEmpty(_measureMyPlataformService.Message) && respuesta)
                        {
                            userCreation = _measureMyPlataformService.GetUrlRedirectMeasureMyPlataform(user.username);
                        }
                        else
                        {
                            userCreation.error = true;
                            userCreation.message = _measureMyPlataformService.Message;
                        }
                    }

                }
                else
                {
                    userCreation.error = true;
                    userCreation.message = _measureMyPlataformService.Message;
                }
            }           


            return userCreation;
        }
    }
}
