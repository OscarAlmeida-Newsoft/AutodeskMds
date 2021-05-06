using ISOMSightServices;
using SOMSightModels.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SOMSight.Utils.Extensions;
using SOMSight.Models;
using AutoMapper;
using SOMSightModels.DTOs;
using SOMSight.Utils;
using HiQPdf;
using System.IO;

namespace SOMSight.Controllers
{
    [AuthorizationFilter]
    public class AssessmentController : Controller
    {
        private ISessionState sessionState;
        private ITranslatorService translatorService;
        private IAssessmentService assessmentSummaryService;
        private IAssessmentQuestionService assessmentQuestionsService;

        public AssessmentController(ISessionState pSessionState, ITranslatorService pTranslatorService, IAssessmentService pAssessmentSummaryService
            , IAssessmentQuestionService pAssessmentQuestionsService)
        {
            sessionState = pSessionState;
            translatorService = pTranslatorService;
            assessmentSummaryService = pAssessmentSummaryService;
            assessmentQuestionsService = pAssessmentQuestionsService;
        }
        // GET: Assessment
        public ActionResult Index()
        {
            sessionState.Store(ConstantsStringKeys.SELECTED_LANGUAGE_ID__KEY, 1);

            translatorService.SaveTranslationFileObject(1);

            return View();
        }


        [HttpPost]
        public PartialViewResult GridListMyAssessmentCallback()
        {
            var _userData = Session.GetCurrentUser();



            IEnumerable<AssessmentGridViewModel> _assessmentSummary =
                Mapper.Map<IEnumerable<AssessmentSummaryDTO>, IEnumerable<AssessmentGridViewModel>>(assessmentSummaryService.GetAssessmentsSummary(_userData.Id));



            return PartialView("_myAssessmentsGrid", _assessmentSummary);
        }

        public ActionResult GetAssessmentRecommendations(int pAssessmentSummaryId)
        {
            FileResult fileResult = null;
            var _user = Session.GetCurrentUser();


            AssessmentSummaryDTO _summary = assessmentSummaryService.GetAssessmentsSummaryById(pAssessmentSummaryId);

            DateTime _date = DateTime.Now;

            string FileName = TranslatorHelper.TranslateTextById(_summary.AssessmentTypeTraslatorId) + "_" + _date.Month + "_" + _date.Day + "_" + _date.Year + ".pdf";

            FileName = FileName.Replace(" ", string.Empty);

            AssessmentRecommendationViewModel _model = new AssessmentRecommendationViewModel();
            
            AssessmentRecommendationsDTO _info = assessmentQuestionsService.AssessmentScoringModel(_summary.AssessmentTypeId, _user.Id);

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