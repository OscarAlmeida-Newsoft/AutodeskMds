using Affidavit.Helpers;
using Affidavit.Models;
using Affidavit.Models.ChooseAnAction;
using Affidavit.Utils;
using AutoMapper;
using DTOs;
using DTOs.Utils;
using IServices;
using IServices.Landing;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Affidavit.Controllers
{
#if !DEBUG
        [RequireHttps] //apply to all actions in controller
#endif
    [SessionExpire]
    public class ChooseAnActionController : BaseController
    {

        private ISessionState sessionState;
        private ILandingCampaignService landingCampaignService;
        private ILeadService leadService;
        private ICRMService CRMService;
        private ISMTPService SMTPService;
        private IChooseAnActionService _chooseAnActionService;
        private IAssessmentService assessmentSummaryService;
        private ITranslatorService translatorService;
        private IMDSService MDSService;
        private ICampaignService campaignService;
        private ISAM360Service sam360Service;
       

        public ChooseAnActionController(ISessionState pSessionState, ILandingCampaignService pLandingCampaignService, ILeadService pLeadService
            , ICRMService pCRMService, ISMTPService pSMTPService, IChooseAnActionService pChooseAnActionService, IAssessmentService pAssessmentSummaryService, IMDSService pMDSService
            , ITranslatorService pTranslatorService, ICampaignService pCampaignService, ISAM360Service pSam360Service)
        {
            sessionState = pSessionState;
            landingCampaignService = pLandingCampaignService;
            leadService = pLeadService;
            CRMService = pCRMService;
            SMTPService = pSMTPService;
            _chooseAnActionService = pChooseAnActionService;
            assessmentSummaryService = pAssessmentSummaryService;
            translatorService = pTranslatorService;
            MDSService = pMDSService;

            campaignService = pCampaignService;
            sam360Service = pSam360Service;
        }

        // GET: ChooseAnAction
        public ActionResult Index(Guid? pLandingID, bool pAccept = false)
        {
            Guid _leadId = Guid.Parse(sessionState.Get<string>(ConstantsStringKeys.CURRENT_LEADID__KEY));

            ChooseAnActionVM _model = new ChooseAnActionVM();
            _model.IsMDSFinished = _chooseAnActionService.GetLeadMDSStatus(_leadId);
            _model.ShowMyplatformManualModal = true;


            CRMDataDTO _leadCRM = (CRMDataDTO)Session["_leadCRM"];
            if(_leadCRM!= null)
            {
                CampaignDTO _CampDTO = campaignService.GetByCRMCampaignID(_leadCRM.CRMCampaignID);
                if(_CampDTO!=null)
                {
                    _model.MDSPercentage = _chooseAnActionService.GetLeadMDSPercentage(_leadId, _CampDTO.CampaignID);
                }

            }
            else
            {
                _model.MDSPercentage = 0;
            }

            



            //Check if My platform modal need to be displayed
            if (ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains(ConstantsStringKeys.MEASURE_MY_PLATFORM_COOKIE_KEY))
            {
                _model.ShowMyplatformManualModal = Convert.ToBoolean(ControllerContext.HttpContext.Request.Cookies[ConstantsStringKeys.MEASURE_MY_PLATFORM_COOKIE_KEY].Value);
            }


            if (Session["lead"] != null && Session["lead"].ToString() == _leadId.ToString())
            {
                if (pLandingID != null)
                {
                    string _AcceptedLandingText = landingCampaignService.GetLandingById(pLandingID.Value).LandingText;
                    leadService.UpdateLeadInformation(_leadId, pLandingID.Value, _AcceptedLandingText, "landingUpdate");
                    MDSService.CommitChangesAffidavit();
                }

                if (pAccept == true)
                {
                    CRMDataDTO _leadCRMAccept = CRMService.GetLeadByID(_leadId);

                    string MicrosoftConsultantEmail = _leadCRMAccept.MicrosoftConsultantEmail;

                    string EmailSubject = TranslatorHelper.TranslateTextByIdentifier("Old_SubjectTextAffidavitParticipation");

                    string EmailBody = TranslatorHelper.TranslateTextByIdentifier("Old_BodyTextAffidavitParticipation");

                    EmailSubject = EmailSubject.Replace("{ConsultantName}", _leadCRMAccept.ConsultantName).Replace("{CompanyName}", _leadCRMAccept.CompanyName);

                    EmailBody = EmailBody.Replace("{CompanyName}", _leadCRMAccept.CompanyName);

                    EmailBody = EmailBody.Replace("{ConsultantName}", _leadCRMAccept.ConsultantName).Replace("{LeadId}", "'" + _leadCRMAccept.LeadID.ToString() + "'");

                    EmailBody = EmailBody.Replace("{SourceCampaign}", _leadCRMAccept.CampaignName).Replace("{Hora}", DateTime.Now.ToString("hh:mm tt")).Replace("{Fecha}", DateTime.Now.ToString("dd-MM-yyyy"));

                    SMTPService.EnviarCorreo(_leadCRMAccept.MicrosoftConsultantEmail, EmailSubject, EmailBody);

                    leadService.UpdateLeadInformation(_leadId, null, null, "acceptLandingDateUpdate");
                    MDSService.CommitChangesAffidavit();

                    if (!ModularityHelper.CanISeeV3(_leadCRMAccept.LeadID))
                    {
                        return RedirectToAction("Index", "MDS");                        
                    }

                }
            }

            _model.IdIndustry = (int?)Session["IndustryId"];



            return View(_model);
        }


        [HttpPost]
        public PartialViewResult GridListMyAssessmentCallback()
        {
            CRMDataDTO data = (CRMDataDTO)Session["_leadCRM"];

            Guid _leadId = Guid.Parse(sessionState.Get<string>(ConstantsStringKeys.CURRENT_LEADID__KEY));
            CRMDataDTO _crmData = sessionState.Get<CRMDataDTO>(ConstantsStringKeys.CRM_DATA__KEY);
            ChooseAnActionSummaryDTO _info = _chooseAnActionService.GetLeadGeneralProgress(_leadId, Guid.Parse(_crmData.CRMCampaignID));

            AssessmentGridViewModel _model = new AssessmentGridViewModel();

            
            _model.AssessmentDetails = Mapper.Map<IEnumerable<AssessmentSummaryDTO>, IEnumerable<AssessmentGridViewModelDetail>>(assessmentSummaryService.GetAssessmentsSummary(data.LeadID, Guid.Parse(data.CRMCampaignID))).ToList();

            if (_leadId != Guid.Parse("C3C7EAA1-2E92-E611-80F7-5065F38B0201") &&
                _leadId != Guid.Parse("C5C7EAA1-2E92-E611-80F7-5065F38B0201")  &&
                _leadId != Guid.Parse("C7C7EAA1-2E92-E611-80F7-5065F38B0201") &&
                _leadId != Guid.Parse("C9C7EAA1-2E92-E611-80F7-5065F38B0201") &&
                _leadId != Guid.Parse("CBC7EAA1-2E92-E611-80F7-5065F38B0201") &&
                _leadId != Guid.Parse("CDC7EAA1-2E92-E611-80F7-5065F38B0201") &&
                _leadId != Guid.Parse("CFC7EAA1-2E92-E611-80F7-5065F38B0201") &&
                _leadId != Guid.Parse("D1C7EAA1-2E92-E611-80F7-5065F38B0201") &&
                _leadId != Guid.Parse("D3C7EAA1-2E92-E611-80F7-5065F38B0201") &&
                _leadId != Guid.Parse("D5C7EAA1-2E92-E611-80F7-5065F38B0201") &&
                _leadId != Guid.Parse("D7C7EAA1-2E92-E611-80F7-5065F38B0201") &&
                _leadId != Guid.Parse("D9C7EAA1-2E92-E611-80F7-5065F38B0201")
                ) {

                int numberIndIns = _model.AssessmentDetails.Count(d => d.IsIndustryInsights);

                _model.AssessmentDetails.RemoveAll(d => d.IsIndustryInsights);
                _info.AvailableAssessments = _info.AvailableAssessments - numberIndIns;
            }



            _model.AssessmentsProgress = _info.AssessmentsProgress;
            _model.AvailableAssessments = _info.AvailableAssessments;
            _model.FinishedAssessments = _info.FinishedAssessments;
            _model.IsFinishedMDS = _info.MDSProgressValue == 100;     

            
            return PartialView("_myAssessmentsGrid", _model);
        }

        public FileResult DownloadManual()
        {
            string IsSAM360Mode = sam360Service.GetSAM360On();

            string[] _sam360AvailableUsers = IsSAM360Mode.ToUpper().Split(',');

            Guid _leadId = Guid.Parse(sessionState.Get<string>(ConstantsStringKeys.CURRENT_LEADID__KEY));

            CRMDataDTO data = (CRMDataDTO)Session["_leadCRM"];

            LeadInformationDTO leadDTO = leadService.GetByLeadID(_leadId);

            string filePath = "";
            if (_sam360AvailableUsers.Contains(_leadId.ToString().ToUpper()))
            {
                filePath = Server.MapPath("~/App_Data/Scanner Install Guide SOMSight 1.5 SAM360.pdf");
            }
            else {
                if (CultureHelper.GetCurrentCulture() == "es" || CultureHelper.GetCurrentCulture() == "es-ES")
                {
                    filePath = Server.MapPath("~/App_Data/Scanner Install Guide MDS 1.5 Español.pdf");
                }
                else
                {
                    filePath = Server.MapPath("~/App_Data/Scanner Install Guide SOMSight 1.5 English.pdf");
                }
            }
                             
            

            return File(filePath, MimeMapping.GetMimeMapping(filePath), "Scanner Install Guide SOMSight 1.5.pdf");

        }

        public ActionResult DownloadAgent()
        {
            string IsSAM360Mode = sam360Service.GetSAM360On();

            string[] _sam360AvailableUsers = IsSAM360Mode.ToUpper().Split(',');

            Guid _leadId = Guid.Parse(sessionState.Get<string>(ConstantsStringKeys.CURRENT_LEADID__KEY));

            CRMDataDTO data = (CRMDataDTO)Session["_leadCRM"];

            LeadInformationDTO leadDTO = leadService.GetByLeadID(_leadId);


            if (_sam360AvailableUsers.Contains(_leadId.ToString().ToUpper()))
            {
                string theUrlDownload = _chooseAnActionService.GetUrlManagmentPoint_SAM360(leadDTO, data);

                //return Redirect(theUrlDownload);
                using (WebClient wc = new WebClient())
                {
                    var byteArr = wc.DownloadData(theUrlDownload);

                    string fileName = "ManagmentPoint.exe";
                    string contentType = "application/x-msi";

                    if (!String.IsNullOrEmpty(wc.ResponseHeaders["Content-Disposition"]))
                    {
                        fileName = wc.ResponseHeaders["Content-Disposition"].Substring(wc.ResponseHeaders["Content-Disposition"].IndexOf("filename=") + 9).Replace("\"", "");
                    }

                    if (!String.IsNullOrEmpty(wc.ResponseHeaders["content-type"]))
                    {
                        contentType = wc.ResponseHeaders["content-type"];
                    }

                    return File(byteArr, contentType, fileName);
                }
            }
            else {

                string filePath = Server.MapPath("~/App_Data/SLScanner.zip");

                return File(filePath, MimeMapping.GetMimeMapping(filePath), "SLScanner.zip");
            }

                
        }
    }
}