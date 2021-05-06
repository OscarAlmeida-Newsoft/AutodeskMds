using Affidavit.Models;
using AutoMapper;
using DTOs;
using DTOs.Utils;
using Entities;
using IServices;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.html.table;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Affidavit.Controllers
{
    //[SessionExpire]
    public class IndustryInsightsController : BaseController
    {

        private readonly IIndustryInsightsService industryInsightsService;
        private readonly ISessionState sessionState;
        private readonly ILeadService leadService;
        private readonly IActivityService activityService;
        private readonly IAssessmentService assessmentSummaryService;
        private readonly IProcessService processService;
        private readonly IProblemService problemService;
        private readonly IDigitalTransformationService digitalTransformationService;

        public IndustryInsightsController(IIndustryInsightsService pIndustryInsightsService,
            ISessionState pSessionState,
            ILeadService pleadService,
            IActivityService pActivityService,
            IAssessmentService pAssessmentSummaryService,
            IProcessService pProcessService,
            IProblemService pProblemService,
            IDigitalTransformationService pDigitalTransformationService
            )
        {

            industryInsightsService = pIndustryInsightsService;
            sessionState = pSessionState;
            leadService = pleadService;
            activityService = pActivityService;
            assessmentSummaryService = pAssessmentSummaryService;
            processService = pProcessService;
            problemService = pProblemService;
            digitalTransformationService = pDigitalTransformationService;
        }

        [SessionExpire]
        public ActionResult Index(int Id)
        {
            Guid LeadId = (Guid)Session["lead"];
            LeadInformationDTO _userData = leadService.GetByLeadID(LeadId);

            int LanguageId = (int)(sessionState.Get(ConstantsStringKeys.SELECTED_LANGUAGE_ID__KEY));

            if (_userData.IndustryIndInsId == null)
            {

                _userData.IndustryIndInsId = Id;
                //Asingar variable de sesion adicionandole el IdIndustry
                Session["IndustryId"] = _userData.IndustryIndInsId;

                //Guardo registro de la industria para el usuario
                leadService.UpdateLeadInformationIndustryIndInsId(_userData.LeadId, _userData.IndustryIndInsId.Value);

            }

            ////Cargar las actividades
            ActivitiesVM _model = new ActivitiesVM();
            _model.Activities = activityService.GetAllActivities();

            //Para obtenr campaña
            CRMDataDTO data = (CRMDataDTO)Session["_leadCRM"];

            //Busco información de asessment summary
            var _assessments = assessmentSummaryService.GetAssessmentsSummary(_userData.LeadId, Guid.Parse(data.CRMCampaignID));

            IEnumerable<NS_TblAssessmentType> assessments = assessmentSummaryService.GetAssessmentType();
            NS_TblAssessmentType assessmentIndIns = assessments.First(d => d.IsIndustryInsights);

            //Obtengo el asessment de industry insights del usuario
            AssessmentSummaryDTO _assessment = _assessments.Where(x => x.AssessmentTypeId == assessmentIndIns.Id).First();
            AssessmentSummaryVM asessmentSum = Mapper.Map<AssessmentSummaryDTO, AssessmentSummaryVM>(_assessment);

            //Download PDF es finalizado
            if (!asessmentSum.IsInProgress && !asessmentSum.DownloadPDF)
            {

                //Marcar el assessment como iniciado
                assessmentSummaryService.MarkAssessmentSummaryAsDraft(asessmentSum.CompanyId, asessmentSum.CampaignId, asessmentSum.Id);

                //Segun la industria cargar la info predeterminada
                industryInsightsService.GeneratePreLoadInfo(asessmentSum.Id, Id, LanguageId, _userData.Id);
            }

            _model.AssessmentSummaryId = asessmentSum.Id;
            _model.AssessmentFinished = asessmentSum.DownloadPDF;

            //Get the industry translation
            var _industry = industryInsightsService.GetIndustryById(_userData.IndustryIndInsId ?? Id);
            _model.IndustryTranslationAdministrationId = _industry.TranslatorAdministratorDescriptionId;


            return View(_model);
        }

        [HttpGet]
        public ActionResult ListIndustries()
        {

            IEnumerable<IndustryIndInsDTO> data = industryInsightsService.GetAllIndustries();
            IEnumerable<IndustryVM> industries = Mapper.Map<IEnumerable<IndustryIndInsDTO>, IEnumerable<IndustryVM>>(data);

            return PartialView("_ListIndustries", industries);
        }

        public ActionResult LoadProcesses(LoadProcessesVM pModel)
        {
            ProcessesVM _model = new ProcessesVM();
            IEnumerable<ProcessDTO> _processes = new List<ProcessDTO>();
            var _activity = activityService.GetActivityById(pModel.ActivityId);


            AssessmentSummaryVM asessmentSum = Mapper.Map<AssessmentSummaryDTO, AssessmentSummaryVM>(assessmentSummaryService.GetAssessmentsSummaryById(pModel.AssessmentSummaryId));

            _model.IsAssessmentFinished = asessmentSum.DownloadPDF;


            var processesList = processService.GetProcessesByActivity(pModel.ActivityId, pModel.AssessmentSummaryId);

            if (processesList != null)
            {
                _processes = processesList;
            }

            _model.ActivityId = _activity.Id;
            _model.AssessmentSummaryId = pModel.AssessmentSummaryId;
            _model.ActivityTranslatorAdministratorId = _activity.TranslatorAdministratorDescriptionId;
            _model.Processes = _processes;
            _model.IsAdminUser = pModel.IsAdminUser;

            return PartialView("_activityProcesses", _model);
        }

        public ActionResult LoadProblemsAndDigitalTransformation(int pProcessId, int pAssessmentSummaryId, bool pIsAdminUser)
        {
            ProblemsAndDigitalTransformationVM _model = new ProblemsAndDigitalTransformationVM();
            _model.IsAdminUser = pIsAdminUser;

            IEnumerable<ProblemDTO> _problems = new List<ProblemDTO>();
            IEnumerable<DigitalTransformationDTO> _digitalTransformations = new List<DigitalTransformationDTO>();

            AssessmentSummaryVM asessmentSum = Mapper.Map<AssessmentSummaryDTO, AssessmentSummaryVM>(assessmentSummaryService.GetAssessmentsSummaryById(pAssessmentSummaryId));
            _model.IsAssessmentFinished = asessmentSum.DownloadPDF;

            var problemsList = problemService.GetAllProblemByProcess(pProcessId);
            var digitalTransformationList = digitalTransformationService.GetAllDigitalTransformationByProcess(pProcessId);

            if (problemsList.Count() > 0)
            {
                _problems = problemsList;
            }

            if (digitalTransformationList.Count() > 0)
            {
                _digitalTransformations = digitalTransformationList;
            }

            _model.IdProcess = pProcessId;
            _model.DigitalTransformations = _digitalTransformations;
            _model.Problems = _problems;

            return PartialView("_problemsAndDigitalTransformation", _model);
        }

        public ActionResult LoadAboutThisActivities(int pActivityId)
        {
            var _activity = activityService.GetActivityById(pActivityId);

            AboutThisActivitiesVM _model = new AboutThisActivitiesVM();

            if (_activity != null)
            {
                _model.DefinitionTranslationId = _activity.TranslatorAdministratorDefinitionId;

                _model.ExampleTranlationId = _activity.TranslatorAdministratorExampleId;

                //TODO: Cambiar por la imagen real
                _model.Image = string.Format("{0}.png", _activity.ImageName);
            }

            return PartialView("_aboutThisActivities", _model);
        }

        public ActionResult CreateProcess(CreateProcessVM pModel)
        {
            Guid _userData;

            if (Session["lead"] != null)
            {
                _userData = (Guid)Session["lead"];
            }
            else {
                _userData = (Guid)Session["userId"];
            }

            

            pModel.UserCreation = _userData;
            pModel.DateCreation = DateTime.Now;
            pModel.UserLastModification = pModel.UserCreation;
            pModel.DateLastModification = pModel.DateCreation;

            processService.InsertProcess(Mapper.Map<CreateProcessVM, ProcessDTO>(pModel));
            return Json(new { Error = false });
        }

        public ActionResult CreateProcessModal(CreateProcessVM pModel)
        {
            //Clear the model state for hiding validation error messages
            ModelState.Clear();
            return PartialView("_createProcess", pModel);
        }

        public ActionResult UpdateProcessModal(int pProcessId)
        {
            var _process = processService.GetProcessById(pProcessId);
            var pModel = Mapper.Map<ProcessDTO, CreateProcessVM>(_process);

            return PartialView("_editProcess", pModel);
        }

        public ActionResult UpdateProcess(CreateProcessVM pModel)
        {
            Guid _userData;

            if (Session["lead"] != null)
            {
                _userData = (Guid)Session["lead"];
            }
            else
            {
                _userData = (Guid)Session["userId"];
            }

            pModel.UserLastModification = _userData;
            pModel.DateLastModification = DateTime.Now;
            processService.UpdateProcess(Mapper.Map<CreateProcessVM, ProcessDTO>(pModel));
            return Json(new { Error = false });
        }

        public ActionResult DeleteProcess(int pProcessId)
        {
            processService.DeleteProcess(pProcessId);
            return Json(new { Error = false });
        }

        public ActionResult SendFinalIndustryInsight(int pAssessmentSummaryId)
        {
            CRMDataDTO data = (CRMDataDTO)Session["_leadCRM"];
            industryInsightsService.MarkIndustryInsightAsFinished(data.LeadID, Guid.Parse(data.CRMCampaignID), pAssessmentSummaryId);

            return RedirectToAction("Index", "ChooseAnAction");
        }

        public JsonResult ValidateFinalIndustryInsigths(int pAssessmentSummaryId)
        {
            if (industryInsightsService.ValidateFinalIndustryInsigth(pAssessmentSummaryId))
            {
                return Json(new { error = false });
            }else
            {
                return Json(new { error = true });
            }


        }

        public ActionResult GetRecommendations(int Id)
        {
            RecommendationsDocumentDTO recommendations = industryInsightsService.GetRecommendationInfo(Id);

            string _viewString = RenderRazorViewToString("Recommendations", recommendations);

            FileResult fileResult = null;
            string FileName = "Recommendations.pdf";

            Rectangle pageSize = new Rectangle(595, 800);

            Document document = new Document(pageSize, 0, 0, 0, 0);
            byte[] pdfBytes = null;
            using (MemoryStream fileS = new MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(document, fileS);
                document.Open();
                //writer.PageEvent = new Footer(wm);
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, new StringReader(_viewString));

                document.Close();
                pdfBytes = fileS.ToArray();
            }

            HttpContext.Response.ContentType = "application/pdf";
            HttpContext.Response.ContentEncoding = System.Text.Encoding.UTF8;
            HttpContext.Response.AppendHeader("content-disposition", string.Format("attachment; filename={0}", FileName));

            fileResult = new FileContentResult(pdfBytes, "application/pdf");

            fileResult.FileDownloadName = FileName;

            return fileResult;
        }

        public ActionResult GetRecommendationsHtml(int Id)
        {
            RecommendationsDocumentDTO recommendations = industryInsightsService.GetRecommendationInfo(Id);

            return View("Recommendations", recommendations);
        }
    }
}

