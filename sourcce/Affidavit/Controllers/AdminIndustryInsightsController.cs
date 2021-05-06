using Affidavit.Helpers;
using Affidavit.Models;
using AutoMapper;
using DTOs;
using IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Affidavit.Controllers
{
    public class AdminIndustryInsightsController : Controller
    {
        IIndustryInsightsService industryInsightsService;
        IActivityService activityService;
        IAssessmentService assessmentSummaryService;
        ILeadService leadService;


        public AdminIndustryInsightsController(IIndustryInsightsService pIndustryInsightsService,
            IActivityService pActivityService,
            IAssessmentService pAssessmentService,
            ILeadService pLeadService)
        {

            industryInsightsService = pIndustryInsightsService;
            activityService = pActivityService;
            assessmentSummaryService = pAssessmentService;
            leadService = pLeadService;
        }

        //[AdminAuthFilter(new string[] { RolesDef.ADMINISTRATOR_ROLE })]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public PartialViewResult GridListCallBack(int? pPage, int? pPageSize)
        {

            var _pageNumber = pPage ?? 1;
            var _pageSize = pPageSize ?? 2;

            AdminIndustryInsightsGridVM grid = new AdminIndustryInsightsGridVM();

            IEnumerable<IndustryInsightsAdminDTO> allIndustry = industryInsightsService.GetAllIndustryInsightsAssessments(pPage.Value, pPageSize.Value);

            grid.Details = Mapper.Map<IEnumerable<IndustryInsightsAdminDTO>, IEnumerable<AdminIndustryInsightsVM>>(allIndustry);

            grid.PageSettings = new NSPageSettings();
            grid.PageSettings.page = _pageNumber;
            grid.PageSettings.size = _pageSize;
            //_model.SortBy = pfilterModel.SortBy ?? "";

            grid.PageSettings.dataItems = industryInsightsService.GetNumbreAllIndustryInsightsAssessments();

            return PartialView("_Grid", grid);
        }

        //[AdminAuthFilter(new string[] { RolesDef.ADMINISTRATOR_ROLE })]
        public ActionResult ViewDetail(int Id)
        {

            if (Session["userId"] == null) {
                return RedirectToAction("../Home/LoginAdmin");
            }
            else { 

                ////Cargar las actividades
                ActivitiesVM _model = new ActivitiesVM();
                _model.Activities = activityService.GetAllActivities();


                //Obtengo informacion del assessment
                AssessmentSummaryDTO summary = assessmentSummaryService.GetAssessmentsSummaryById(Id);
                _model.AssessmentSummaryId = summary.Id;
                _model.AssessmentFinished = summary.ResponseEndDate != null ? true : false;

                LeadInformationDTO user = leadService.GetByLeadID(summary.CompanyId);
            
                //Get the industry translation
                var _industry = industryInsightsService.GetIndustryById(user.IndustryIndInsId ?? Id);
                _model.IndustryTranslationAdministrationId = _industry.TranslatorAdministratorDescriptionId;

                return View(_model);
            }
        }


        //[AdminAuthFilter(new string[] { RolesDef.ADMINISTRATOR_ROLE })]
        public ActionResult GetNextSteps(int pAssessmentSummaryId)
        {
            NextStepsVM _model = new NextStepsVM();
            var _assessment = assessmentSummaryService.GetAssessmentsSummaryById(pAssessmentSummaryId);

            _model.AssessmentSummaryId = pAssessmentSummaryId;
            _model.NextStepsComments = _assessment.NextSteps;



            return PartialView("_nextSteps", _model);
        }


        //[AdminAuthFilter(new string[] { RolesDef.ADMINISTRATOR_ROLE })]
        public void SaveNextSteps(NextStepsVM pModel)
        {
            assessmentSummaryService.SaveNextSteps(pModel.AssessmentSummaryId, pModel.NextStepsComments);
        }
    }
}