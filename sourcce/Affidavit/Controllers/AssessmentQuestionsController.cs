using Affidavit.Models;
using AutoMapper;
using DTOs;
using DTOs.Utils;
using IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Affidavit.Utils;

namespace Affidavit.Controllers
{
#if !DEBUG
        [RequireHttps] //apply to all actions in controller
#endif
    [SessionExpire]
    public class AssessmentQuestionsController : BaseController
    {
        private IAssessmentQuestionService assessmentQuestionService;
        private IAssessmentService assessmentService;
        private ITranslatorUtility translatorUtility;

        public AssessmentQuestionsController(IAssessmentQuestionService pAssessmentQuestionService, IAssessmentService pAssessmentService
            , ITranslatorUtility pTranslatorUtility)
        {
            assessmentQuestionService = pAssessmentQuestionService;
            assessmentService = pAssessmentService;
            translatorUtility = pTranslatorUtility;
        }

        // GET: AssessmentQuestions
        public ActionResult Index(int pAssessmentSummaryId)
        {

            AssessmentViewModel _model = new AssessmentViewModel();

            AssessmentSummaryDTO _summary = assessmentService.GetAssessmentsSummaryById(pAssessmentSummaryId);

            var _data = Mapper.Map<IEnumerable<AssessmentQuestionDTO>, IEnumerable<AssessmentQuestionsViewModel>>(assessmentQuestionService.GetAssessmentQuestions(_summary));

            _model.AssessmentQuestions = _data;
            _model.AssessmentTypeTranslatorId = _summary.AssessmentTypeTraslatorId;
            _model.CompanyId = _summary.CompanyId;
            _model.CampaignId = _summary.CampaignId;
            _model.AssessmentSummaryId = _summary.Id;
            _model.IconName = _summary.IconName;
            _model.AssessmentTypeId = _summary.AssessmentTypeId;
            _model.TotalQuestion = _data.Count();
            _model.AnsweredQuestions= _data.Count(x => x.SelectedMaturityLevelId != null);
            _model.AssessmentMaturityLevelTranslatorId = _summary.GlobalMaturityLevelTranslatorId;

            _model.IsFinal = _summary.ResponseEndDate != null;

            return View(_model);
        }

        public ActionResult SaveAssessmentInfo(AssessmentQuestionAnswerViewModel pQuestionAnswersModel)
        {
            

            //Saves a draft 
            assessmentQuestionService.SaveAssessmentQuestionsDraft(
                    Mapper.Map<IEnumerable<AssessmentQuestionAnswerDetailsViewModel>, IEnumerable<AssessmentQuestionAnswerDetailsDTO>>(pQuestionAnswersModel.AnswerDetails)
                    , pQuestionAnswersModel.CampaignId, pQuestionAnswersModel.CompanyId, pQuestionAnswersModel.AssessmentSummaryId, pQuestionAnswersModel.AssessmentTypeId);

            //If is the final version, save it
            if (pQuestionAnswersModel.IsFinal)
            {
                //Data for compose the email notification
                CRMDataDTO data = (CRMDataDTO)Session["_leadCRM"];

                AssessmentSummaryDTO _summary = assessmentService.GetAssessmentsSummaryById(pQuestionAnswersModel.AssessmentSummaryId);

                string _messageSubject = string.Format(TranslatorHelper.TranslateTextByIdentifier("Old_FinishAssessmentNotificationMessageSubject")
                ,translatorUtility.TranslateTextById(_summary.AssessmentTypeTraslatorId,1), data.CompanyName);

                string _messageBody = string.Format(TranslatorHelper.TranslateTextByIdentifier("Old_FinishAssessmentNotificationMessageBody")
                    ,data.ConsultantName, data.CompanyName, translatorUtility.TranslateTextById(_summary.AssessmentTypeTraslatorId, 1));
                string _messageTo = data.MicrosoftConsultantEmail;

                assessmentQuestionService.SaveAssessmentQuestionsFinal(Mapper.Map<IEnumerable<AssessmentQuestionAnswerDetailsViewModel>, IEnumerable<AssessmentQuestionAnswerDetailsDTO>>(pQuestionAnswersModel.AnswerDetails)
                    , pQuestionAnswersModel.CampaignId, pQuestionAnswersModel.CompanyId, pQuestionAnswersModel.AssessmentSummaryId, pQuestionAnswersModel.AssessmentTypeId,
                    _messageTo,_messageSubject,_messageBody);
            }

            return RedirectToAction("Index", "ChooseAnAction");
        }




    }
}