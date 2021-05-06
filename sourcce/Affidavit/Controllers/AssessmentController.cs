using Affidavit.Models;
using Affidavit.Utils;
using AutoMapper;
using DTOs;
using DTOs.Utils;
using HiQPdf;
using IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace Affidavit.Controllers
{
#if !DEBUG
        [RequireHttps] //apply to all actions in controller
#endif
    [SessionExpire]
    public class AssessmentController : BaseController
    {
        private IAssessmentService assessmentSummaryService;
        private ITranslatorService translatorService;
        private ISessionState sessionState;
        private IAssessmentQuestionService assessmentQuestionsService;


        public AssessmentController(IAssessmentService pAssessmentSummaryService, ITranslatorService pTranslatorService
            ,ISessionState pSessionState, IAssessmentQuestionService pAssessmentQuestionsService) 
        {
            assessmentSummaryService = pAssessmentSummaryService;
            translatorService = pTranslatorService;
            sessionState = pSessionState;
            assessmentQuestionsService = pAssessmentQuestionsService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var data = Session["_leadCRM"];
            var LeadId  = sessionState.Get<string>(ConstantsStringKeys.CURRENT_LEADID__KEY);

            sessionState.Store(ConstantsStringKeys.SELECTED_LANGUAGE_ID__KEY, 1);

            //if (Session["lead"] == null || Session["lead"].ToString() != pLeadID.ToString())
            //{
            //    return RedirectToAction("../Home/Login", new { pLeadId = pLeadID });
            //}

            //List<TranslatorFileModelDTO> _data = new List<TranslatorFileModelDTO>();

            //_data.Add(new TranslatorFileModelDTO
            //{
            //    Id = 1,
            //    TextIdentifier = "industria",
            //    TranslatedText = "Industry"
            //});

            
            //TranslatorHelper.SaveJsonLanguageFile(1,_data);

            translatorService.SaveTranslationFileObject(1);


            //string _trans = TranslatorHelper.TranslateTextByIdentifier("industria", 1);
            //translatorService.GenerateTranslationFile();

            return View();
        }

        //[HttpPost]
        //public PartialViewResult GridListMyAssessmentCallback()
        //{
        //    CRMDataDTO data = (CRMDataDTO) Session["_leadCRM"];

            
                
        //    IEnumerable<AssessmentGridViewModel>  _assessmentSummary =  
        //        Mapper.Map<IEnumerable<AssessmentSummaryDTO>, IEnumerable<AssessmentGridViewModel>>( assessmentSummaryService.GetAssessmentsSummary(data.LeadID, Guid.Parse(data.CRMCampaignID)));

            

        //    return PartialView("_myAssessmentsGrid", _assessmentSummary);
        //}

        public ActionResult GetAssessmentRecommendations(int pAssessmentSummaryId)
        {
            FileResult fileResult = null;
            

            AssessmentSummaryDTO _summary = assessmentSummaryService.GetAssessmentsSummaryById(pAssessmentSummaryId);

            DateTime _date = DateTime.Now;

            string FileName = TranslatorHelper.TranslateTextById(_summary.AssessmentTypeTraslatorId) +"_"+ _date.Month +"_" +_date.Day+ "_"+_date.Year+".pdf";

            FileName = FileName.Replace(" ", string.Empty);

            AssessmentRecommendationViewModel _model = new AssessmentRecommendationViewModel();

            CRMDataDTO data = (CRMDataDTO)Session["_leadCRM"];
            AssessmentRecommendationsDTO _info = assessmentQuestionsService.AssessmentScoringModel(_summary.AssessmentTypeId, data.LeadID, Guid.Parse(data.CRMCampaignID));

            //_model.AssessmentTypeDescription = _summary.AssessmentTypeDescription;
            _model.Recomendations = new AssessmentRecommendationsDTO();
            _model.Recomendations = _info;
            _model.IconName = _summary.IconName;
            _model.AssessmentTypeTranslationAdministrationId = _summary.AssessmentTypeTraslatorId;
            _model.GlobalMaturityLevelTranslatorId = _summary.GlobalMaturityLevelTranslatorId;
            _model.GlobalMaturityLevelId = _summary.GlobalMaturityLevelId == "A" ? 1 :
                _summary.GlobalMaturityLevelId == "B" ? 2 :
                _summary.GlobalMaturityLevelId == "C" ? 3 : 4;

            string _viewString = RenderRazorViewToString("AssessmentRecommendations", _model);

            // create the HTML to PDF converter
            HtmlToPdf htmlToPdfConverter = new HtmlToPdf();

            htmlToPdfConverter.BrowserWidth = 1200;

            // set HTML Load timeout
            htmlToPdfConverter.HtmlLoadedTimeout = 120;

            // set PDF page size and orientation
            htmlToPdfConverter.Document.PageSize = PdfPageSize.Letter;
            htmlToPdfConverter.Document.PageOrientation = PdfPageOrientation.Portrait;

            // set PDF page margins
            htmlToPdfConverter.Document.Margins = new PdfMargins(60);

            // set a wait time before starting the conversion
            htmlToPdfConverter.WaitBeforeConvert = 2;

            // convert HTML to PDF
            byte[] pdfBytes = null;

            pdfBytes = htmlToPdfConverter.ConvertHtmlToMemory(_viewString, "");

            HttpContext.Response.ContentType = "application/pdf";
            HttpContext.Response.ContentEncoding = System.Text.Encoding.UTF8;
            HttpContext.Response.AppendHeader("content-disposition", string.Format("attachment; filename={0}", FileName));

            fileResult = new FileContentResult(pdfBytes, "application/pdf");
            fileResult.FileDownloadName = FileName;

            return fileResult;



            //return View("AssessmentRecommendations", _model);



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
