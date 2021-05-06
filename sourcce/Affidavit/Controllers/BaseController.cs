using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using Affidavit.Helpers;
using Affidavit.Utils;
using IServices;
using DTOs.Utils;
using System.IO;

namespace Affidavit.Controllers
{
    public class BaseController : Controller
    {


        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {

            string cultureName = null;

            // Attempt to read the culture cookie from Request
            HttpCookie cultureCookie = Request.Cookies["_culture"];
            if (cultureCookie != null)
                cultureName = cultureCookie.Value;
            else
                cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ?
                        Request.UserLanguages[0] :  // obtain it from HTTP header AcceptLanguages
                        null;
            // Validate culture name
            cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe

            

            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);


            //Save the culture for new translator engine
            //ISessionState _sessionStateResolve = DependencyResolver.Current.GetService<ISessionState>();
            //var _lang =  _sessionStateResolve.Get(ConstantsStringKeys.SELECTED_LANGUAGE_ID__KEY);
            int cultureId = TranslatorHelper.GetCultureIdentifier(cultureName);
            //if (_lang != null)
            //{
            //    cultureId = (int) _lang;
            //}

            TranslatorHelper.SetCulture(cultureId);

            TranslatorHelper.UpdateTranslatorUtilityFileModel();

            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            return base.BeginExecuteCore(callback, state);
        }

        /// <summary>
        ///     action que cambia el idioma de la aplicación.
        /// </summary>
        /// <param name="culture">string que contiene la cultura en la que se va a mostrar la aplicación</param>
        /// <returns></returns>
        public ActionResult SetCulture(string culture, string returnUrl)
        {
            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);

            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else
            {

                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);

            ISessionState _sessionStateResolve = DependencyResolver.Current.GetService<ISessionState>();
            int cultureId = TranslatorHelper.GetCultureIdentifier(culture);
            TranslatorHelper.SetCulture(cultureId);

            return Redirect(returnUrl);
            //return RedirectToAction("Index", new { pLeadID = leadID, pLanguageID = culture });
        }

        public ActionResult SetCultureAdmin(string culture, string returnUrl)
        {
            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);

            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else
            {

                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);

            return Redirect(returnUrl);
            //return RedirectToAction("Index", new { pLeadID = leadID, pLanguageID = culture });
        }

        public void ChangeLanguageAdmin(int pLangageId, string pCultureCode)
        {
            TranslatorHelper.SetCulture(pLangageId);

            // Validate input
           var culture = CultureHelper.GetImplementedCulture(pCultureCode);

            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else
            {

                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
        }

        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
                                                                         viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View,
                                             ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

    }
}