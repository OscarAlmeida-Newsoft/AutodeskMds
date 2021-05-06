using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISOMSightServices;
using SOMSightModels.Users;
using System.Configuration;

namespace SOMSightServices
{
    public class ChooseAnActionService : IChooseAnActionService
    {
        public string Message { get; set; }

        public string GetUrlRedirectMeasureMyPlataform(UserDTO user)
        {
            MeasureMyPlataformServices.MeasureMyPlataformServicesClient _measureMyPlataformService = new MeasureMyPlataformServices.MeasureMyPlataformServicesClient();

            MeasureMyPlataformServices.UserSAMLive userSAMLive = new MeasureMyPlataformServices.UserSAMLive();
            userSAMLive.username = user.UserName;

            userSAMLive.Company = user.CompanyName ?? "Echez";
            userSAMLive.Email = user.UserName;
            userSAMLive.FirstName = user.FirstName;
            userSAMLive.LastName = user.LastName;
            userSAMLive.Telephone = user.Telephone;
            userSAMLive.Mobil = user.MobilePhone;
            userSAMLive.RefUserGroupID = ConfigurationManager.AppSettings["SAMLive_SmallPlan_GroupID"];
            userSAMLive.DefaultDashboard = ConfigurationManager.AppSettings["SAMLive_DefaultDashboardId"];

            MeasureMyPlataformServices.TokenResponse response = _measureMyPlataformService.GetUrlRedirectMeasureMyPlataform(userSAMLive);

            if (response.error)
            {
                Message = response.message;
            }

            return response.Url;
        }
    }
}
