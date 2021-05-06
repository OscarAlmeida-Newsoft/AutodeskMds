using DTOs;
using DTOs.Utils;
using IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Affidavit.Controllers
{
#if !DEBUG
        [RequireHttps] //apply to all actions in controller
#endif
    [SessionExpire]
    public class MDSAccountController : BaseController
    {
        private ISessionState sessionState;
        private IMDSAccountService MDSaccountService;

        public MDSAccountController(ISessionState pSessionState, IMDSAccountService pMDSAccountService)
        {
            sessionState = pSessionState;
            MDSaccountService = pMDSAccountService;
        }

        // GET: MDSAccount
        public ActionResult Index()
        {

            var LeadId = sessionState.Get<string>(ConstantsStringKeys.CURRENT_LEADID__KEY);
            CRMDataDTO _leadCRMData = sessionState.Get<CRMDataDTO>(ConstantsStringKeys.CRM_DATA__KEY);

            var _model = MDSaccountService.GetMDSAccountInformation(Guid.Parse(LeadId));
            _model.ConsultantContact = _leadCRMData.ConsultantName;
            return View("Index", _model);
        }
    }
}