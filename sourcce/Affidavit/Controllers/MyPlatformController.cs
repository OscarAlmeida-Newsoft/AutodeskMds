using DTOs;
using DTOs.Utils;
using IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Affidavit.Utils;
using IRepositories;

namespace Affidavit.Controllers
{
#if !DEBUG
        [RequireHttps] //apply to all actions in controller
#endif
    public class MyPlatformController : BaseController
    {
        private ISessionState sessionState;
        private ILeadService leadService;
        private ISMTPService SMTPService;
        private IChooseAnActionService chooseAnActionService;
        private ISAM360Provider SAM360Provider;


        public MyPlatformController(ISessionState pSessionState, ILeadService pLeadService, ISMTPService pSMTPService, IChooseAnActionService pChooseAnActionService,
            ISAM360Provider pSAM360Provider)
        {
            sessionState = pSessionState;
            leadService = pLeadService;
            SMTPService = pSMTPService;
            chooseAnActionService = pChooseAnActionService;
            SAM360Provider = pSAM360Provider;
        }

        // GET: MyPlatform
        public ActionResult Index(bool MyPlatformModal = false)
        {
            string theUrl;
            string IsSAM360Mode = SAM360Provider.GetSAM360On();

            string[] _sam360AvailableUsers = IsSAM360Mode.ToUpper().Split(',');

            Guid _leadId = Guid.Parse(sessionState.Get<string>(ConstantsStringKeys.CURRENT_LEADID__KEY));

            CRMDataDTO data = (CRMDataDTO)Session["_leadCRM"];

            LeadInformationDTO leadDTO = leadService.GetByLeadID(_leadId);

            //Send Email notification
            SMTPService.EnviarCorreo(leadDTO.MicrosoftConsultantEmail
                , string.Format(TranslatorHelper.TranslateTextByIdentifier("Old_MeasureMyPlatformEmailSubject"), data.CompanyName)
                , string.Format(TranslatorHelper.TranslateTextByIdentifier("Old_MeasureMyPlatformEmailMessage"), data.ConsultantName, data.CompanyName)
                );

            //Add cookie for indicate if modal is shwing next time
            HttpCookie _cookie = new HttpCookie(ConstantsStringKeys.MEASURE_MY_PLATFORM_COOKIE_KEY);
            _cookie.Expires = DateTime.Now.AddYears(1);
            _cookie.Value = MyPlatformModal.ToString();
            ControllerContext.HttpContext.Response.Cookies.Add(_cookie);
            
            if (_sam360AvailableUsers.Contains(_leadId.ToString().ToUpper()))
            {
                ViewBag.IsSAM360Mode = true;

                theUrl = chooseAnActionService.GetUrlRedirectMeasureMyPlataformSAM360(leadDTO, data);

                if (theUrl.Equals(""))
                {
                    return RedirectToAction("Index", "ChooseAnAction");
                }
            }
            else
            {
                ViewBag.IsSAM360Mode = false;

                leadDTO.FirstName = data.FirstName;
                leadDTO.LastName = data.LastName;
                leadDTO.Email = data.Email;
                leadDTO.Telephone = data.PhoneNumber;
                leadDTO.MobilePhone = data.MobilePhone;

                theUrl = chooseAnActionService.GetUrlRedirectMeasureMyPlataform(leadDTO);                
            }



            ViewBag.MeasureMyPlatformUrl = theUrl ?? "http://1";


            return View();
        }
    }
}