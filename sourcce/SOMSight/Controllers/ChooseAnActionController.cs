using SOMSight.Models.Account;
using SOMSight.Utils.Extensions;
using SOMSightModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISOMSightServices;
using SOMSight.Utils;

namespace SOMSight.Controllers
{
    [AuthorizationFilter]
    public class ChooseAnActionController : BaseController
    {
        private IChooseAnActionService _chooseAnActionService;

        public ChooseAnActionController(IChooseAnActionService pChooseAnActionServie) {
            _chooseAnActionService = pChooseAnActionServie;
        }

        // GET: ChooseAnAction
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult MeasureMyPlataform()
        {
            UserSession _userData = Session.GetCurrentUser();         

            UserDTO user = new UserDTO();
            user.UserName = _userData.UserName;
            user.Email = _userData.UserName;               
            

            string theUrl = _chooseAnActionService.GetUrlRedirectMeasureMyPlataform(user);

            return Redirect(theUrl);
            
        }

    }
}