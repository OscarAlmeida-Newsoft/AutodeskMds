
using DTOs.Utils;
using System.Web;
using System.Web.Mvc;

namespace Affidavit
{
    public class SessionExpireAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ct = HttpContext.Current;
            if (HttpContext.Current.Session[ConstantsStringKeys.CURRENT_LEADID__KEY] == null)
            {
                var _leadId = filterContext.RouteData.Values["pLeadID"];
                if (_leadId !=null)
                {
                    filterContext.Result = new RedirectResult("~/Home/Login?pLEadID="+_leadId);
                }else
                {
                    filterContext.Result = new RedirectResult("~/Home/Login");
                }
                
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}