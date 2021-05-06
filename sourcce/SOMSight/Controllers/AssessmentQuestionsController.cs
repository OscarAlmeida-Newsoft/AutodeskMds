using AutoMapper;
using ISOMSightServices;
using ISOMSightServices.Users;
using SOMSight.Helpers;
using SOMSight.Models;
using SOMSight.Utils;
using SOMSightModels.DTOs;
using SOMSightModels.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOMSight.Controllers
{
    [AuthorizationFilter]
    public class AssessmentQuestionsController : Controller
    {
        private IAssessmentQuestionService assessmentQuestionService;
        private IAssessmentService assessmentService;
        private IUserService userService;
        private ITranslatorUtility translatorUtility;


        public AssessmentQuestionsController(IAssessmentQuestionService pAssessmentQuestionService, IAssessmentService pAssessmentService
            , IUserService pUserService, ITranslatorUtility pTranslatorUtility)
        {
            assessmentQuestionService = pAssessmentQuestionService;
            assessmentService = pAssessmentService;
            userService = pUserService;
            translatorUtility = pTranslatorUtility;
        }


        public ActionResult Index(int pAssessmentSummaryId)
        {

            AssessmentViewModel _model = new AssessmentViewModel();

            AssessmentSummaryDTO _summary = assessmentService.GetAssessmentsSummaryById(pAssessmentSummaryId);

            var _data = Mapper.Map<IEnumerable<AssessmentQuestionDTO>, IEnumerable<AssessmentQuestionsViewModel>>(assessmentQuestionService.GetAssessmentQuestions(_summary));

            _model.AssessmentQuestions = _data;
            _model.AssessmentTypeTranslatorId = _summary.AssessmentTypeTraslatorId;
            _model.UserId = _summary.UserId;
            _model.AssessmentSummaryId = _summary.Id;
            _model.IconName = _summary.IconName;
            _model.AssessmentTypeId = _summary.AssessmentTypeId;
            _model.TotalQuestion = _data.Count();
            _model.AnsweredQuestions = _data.Count(x => x.SelectedMaturityLevelId != null);
            _model.AssessmentMaturityLevelTranslatorId = _summary.GlobalMaturityLevelTranslatorId;

            return View(_model);
        }

        public ActionResult SaveAssessmentInfo(AssessmentQuestionAnswerViewModel pQuestionAnswersModel)
        {

            //Saves a draft 
            assessmentQuestionService.SaveAssessmentQuestionsDraft(
                    Mapper.Map<IEnumerable<AssessmentQuestionAnswerDetailsViewModel>, IEnumerable<AssessmentQuestionAnswerDetailsDTO>>(pQuestionAnswersModel.AnswerDetails)
                    , pQuestionAnswersModel.UserId, pQuestionAnswersModel.AssessmentSummaryId, pQuestionAnswersModel.AssessmentTypeId);

            //If is the final version, save it
            if (pQuestionAnswersModel.IsFinal)
            {
                AssessmentSummaryDTO _summary = assessmentService.GetAssessmentsSummaryById(pQuestionAnswersModel.AssessmentSummaryId);

                var _userSession = userService.GetbyUserId(pQuestionAnswersModel.UserId);
                string _messageBody = string.Format( "Hi {0}, <br/> <br/> The company <b> {1} </b> has submitted the <b> {2} </b> Assessment. <br/><br/> Regards, <br/><br/> SOMSight Automatically send."
                    ,ConfigHelper.GetAssignedConsultant(), _userSession.CompanyName, translatorUtility.TranslateTextById(_summary.AssessmentTypeTraslatorId, 1));
                string _messageSubject = string.Format("{0} Assessment has been Finalized by {1}"
                    , translatorUtility.TranslateTextById(_summary.AssessmentTypeTraslatorId, 1), _userSession.CompanyName);
                string _meesageTo = ConfigHelper.GetConsultantEmail();

                assessmentQuestionService.SaveAssessmentQuestionsFinal(Mapper.Map<IEnumerable<AssessmentQuestionAnswerDetailsViewModel>, IEnumerable<AssessmentQuestionAnswerDetailsDTO>>(pQuestionAnswersModel.AnswerDetails)
                    , pQuestionAnswersModel.UserId, pQuestionAnswersModel.AssessmentSummaryId, pQuestionAnswersModel.AssessmentTypeId, _meesageTo, _messageSubject, _messageBody);
            }

            return RedirectToAction("Index", "Assessment");
        }
    }
}