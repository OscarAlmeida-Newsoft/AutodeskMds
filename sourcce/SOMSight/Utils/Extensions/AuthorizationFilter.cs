using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SOMSight.Utils.Extensions;
using SOMSight.Models.Account;


namespace SOMSight.Utils
{
    public class AuthorizationFilter : AuthorizeAttribute, IAuthorizationFilter
    {
        private const string USUARIO_KEY = "USUARIO_KEY";

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                // Don't check for authorization as AllowAnonymous filter is applied to the action or controller
                return;
            }

            // Check for authorization

            if (HttpContext.Current.Session[USUARIO_KEY] == null)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}