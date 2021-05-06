using AutoMapper;
using DTOs;
using DTOs.Utils;
using Entities;
using IRepositories;
using IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Services
{
    public class AssessmentQuestionService : IAssessmentQuestionService
    {

        private readonly IAssessmentQuestionRepository assessmentQuestionRepository;
        private readonly IAssessmentSummaryRepository assessmentSummaryRepository;
        private readonly ISMTPService smtpService;
        private readonly IAssessmentService assessmentService;
        
        protected IUnitOfWork.IUnitOfWork unitOfWork;

        public AssessmentQuestionService(IAssessmentQuestionRepository pAssessmentQuestionRepository, IUnitOfWork.IUnitOfWork pUnitOfWork
            , IAssessmentSummaryRepository pAssessmentSummaryRepository, ISMTPService pSMTPService
            , IAssessmentService pAssessmentService)
        {
            assessmentQuestionRepository = pAssessmentQuestionRepository;
            assessmentSummaryRepository = pAssessmentSummaryRepository;
            smtpService = pSMTPService;
            assessmentService = pAssessmentService;
            unitOfWork = pUnitOfWork;
        }

        public IEnumerable<AssessmentQuestionDTO> GetAssessmentQuestions(AssessmentSummaryDTO pAssessmentSummary)
        {
            //Get the questions for the selected assessment
            IEnumerable<NS_TblAssessmentQuestion> _assessmentQuestions = GetAssessmentQuestions(pAssessmentSummary.AssessmentTypeId); //assessmentQuestionRepository.GetAssessmentQuestions(pAssessmentSummary.AssessmentTypeId);

            //Mapping the object
            IEnumerable<AssessmentQuestionDTO> _data = Mapper.Map<IEnumerable<NS_TblAssessmentQuestion>, IEnumerable<AssessmentQuestionDTO>>(_assessmentQuestions);

            //get the user answers
            var _clientAnswers = assessmentQuestionRepository.GetAnswersXAssessment(pAssessmentSummary.CompanyId, pAssessmentSummary.CampaignId);

            _data = _data
                .GroupJoin(_clientAnswers
                , AQO => AQO.Id
                , CA => CA.AssessmentId
                , (AQO, CA) => new { AQO, CA = CA.FirstOrDefault() })
                .Select(o => new AssessmentQuestionDTO
                {
                    AssessmentQuestionOptions = o.AQO.AssessmentQuestionOptions
                    ,
                    TranslatorAdministratorId = o.AQO.TranslatorAdministratorId
                    ,
                    DisplayOrder = o.AQO.DisplayOrder
                    ,
                    SelectedMaturityLevelId = o.CA?.Response
                    ,
                    Id = o.AQO.Id
                    ,
                    CampaignId = pAssessmentSummary.CampaignId
                    ,
                    CompanyId = pAssessmentSummary.CompanyId
                });

            return _data;
        }

        public void SaveAssessmentQuestionsDraft(IEnumerable<AssessmentQuestionAnswerDetailsDTO> pAssessmentQuestionDetails, Guid pCampaignId, Guid pCompanyId
            , int pAssessmentSummaryId, int pAssessmentTypeId)
        {

            List<NS_TblAnswerXAssessment> _datos = new List<NS_TblAnswerXAssessment>();
            _datos = GetAnswerQuestionsList(pAssessmentQuestionDetails, pAssessmentTypeId);
            try
            {
                //first delete all answers
                assessmentQuestionRepository.DeleteAllAnswersXAssessment(pCompanyId, pCampaignId, pAssessmentTypeId);

                //save answers
                assessmentQuestionRepository.SaveAnswersXAssessment(_datos);

                //Mark assessment summary as draft
                assessmentSummaryRepository.MarkAssessmentSummaryAsDraft(pCompanyId, pCampaignId, pAssessmentSummaryId);
                               

                //commit transaction
                unitOfWork.Complete();
            }
            catch (Exception)
            {

                throw;
            }


        }

        public void SaveAssessmentQuestionsFinal(IEnumerable<AssessmentQuestionAnswerDetailsDTO> pAssessmentQuestionDetails, Guid pCampaignId, Guid pCompanyId
            , int pAssessmentSummaryId, int pAssessmentTypeId, string pMessageTo, string pMessageSubject, string pMessageBody)
        {
            List<NS_TblAnswerXAssessment> _datos = new List<NS_TblAnswerXAssessment>();
            _datos = GetAnswerQuestionsList(pAssessmentQuestionDetails, pAssessmentTypeId);

            try
            {

                //Get Assessment Global Maturity Level
                string _maturityLevel = assessmentSummaryRepository.GetAssessmentGlobalMaturityLevel(pAssessmentTypeId, pCompanyId, pCampaignId);

                //Mark assessment summary as finished
                assessmentSummaryRepository.MarkAssessmentSummaryAsFinished(pCompanyId, pCampaignId, pAssessmentSummaryId, _maturityLevel);

                
                
                //TODO: Making EnviarCorreo function Async
                smtpService.EnviarCorreo(pMessageTo
                    ,pMessageSubject, 
                    pMessageBody
                );
                

                //commit transaction
                unitOfWork.Complete();
            }
            catch (Exception)
            {

                throw;
            }
        }
        

        private List<NS_TblAnswerXAssessment> GetAnswerQuestionsList(IEnumerable<AssessmentQuestionAnswerDetailsDTO> pData, int pAssessmentTypeId)
        {
            List<NS_TblAnswerXAssessment> _datos = new List<NS_TblAnswerXAssessment>();
            foreach (var item in pData.Where(x => x.SelectedOption != null))
            {
                _datos.Add(new NS_TblAnswerXAssessment
                {
                    AssessmentId = item.AssessmentId
                    ,CampaignId = item.CampaignId
                    ,CompanyId = item.CompanyId
                    ,UpdatedDate = DateTime.UtcNow
                    ,Response = item.SelectedOption
                    ,Points = ValidateAnswerPoints(item.SelectedOption)
                    ,AssessmentTypeId = pAssessmentTypeId
                });
            }

            return _datos;
        }

        private int ValidateAnswerPoints(string pSelectedAnswer)
        {
            int _points = 0;
            switch (pSelectedAnswer.ToUpper())
            {
                case "A":
                    _points = 1;
                    break;
                case "B":
                    _points = 2;
                    break;
                case "C":
                    _points = 3;
                    break;
                case "D":
                    _points = 4;
                    break;
                default:
                    break;
            }

            return _points;
        }

        public AssessmentRecommendationsDTO AssessmentScoringModel(int pAssessmentTypeId, Guid pLeadId, Guid pCampaingId)
        {
            try
            {
                return assessmentQuestionRepository.AssessmentScoringModel(pAssessmentTypeId, pLeadId, pCampaingId, GetAssessmentRecommendations());
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        private IEnumerable<NS_TblAssessmentQuestion> GetAssessmentQuestions(int pAssessmentTypeId)
        {
            AssessmentExternalService.AssessmentServicesClient _client = new AssessmentExternalService.AssessmentServicesClient();
            List<NS_TblAssessmentQuestion> _assessmentQuestions = new List<NS_TblAssessmentQuestion>();
            IEnumerable<AssessmentExternalService.NS_TblAssessmentQuestion> _assessmentQuestionsExternal = _client.GetAssessmentQuestions(pAssessmentTypeId);
            List<NS_TblAssessmentQuestionOption> _options;

            //Convert the object to internalClass
            foreach (var item in _assessmentQuestionsExternal)
            {
                _options = new List<NS_TblAssessmentQuestionOption>();

                //Get the questions options
                
                foreach (var option in item.AssessmentQuestionOptions)
                {
                    _options.Add(new NS_TblAssessmentQuestionOption
                    {
                        AssessmentQuestionId = option.AssessmentQuestionId,
                        CreatedById = option.CreatedById,
                        CreatedDate = option.CreatedDate,
                        Id = option.Id,
                        MaturityLevelId = option.MaturityLevelId,
                        Status = option.Status,
                        TranslatorAdministratorId = option.TranslatorAdministratorId,
                        UpdatedById = option.UpdatedById,
                        UpdatedDate = option.UpdatedDate
                    });
                }

                _assessmentQuestions.Add(new NS_TblAssessmentQuestion
                {
                    AssessmentTypeId = item.AssessmentTypeId,
                    CreatedById = item.CreatedById,
                    CreatedDate = item.CreatedDate,
                    DisplayOrder = item.DisplayOrder,
                    Id = item.Id,
                    Status = item.Status,
                    TranslatorAdministratorId = item.TranslatorAdministratorId,
                    UpdatedById = item.UpdatedById,
                    UpdatedDate = item.UpdatedDate,
                    AssessmentQuestionOptions = _options
                });
            }

            

            return _assessmentQuestions;
        }

        private IEnumerable<NS_TblRecommendation> GetAssessmentRecommendations()
        {
            AssessmentExternalService.AssessmentServicesClient _client = new AssessmentExternalService.AssessmentServicesClient();
            List<NS_TblRecommendation> _assessmentRecommendations = new List<NS_TblRecommendation>();
            IEnumerable<AssessmentExternalService.NS_TblRecommendation> _assessmentRecommendationsExternal = _client.GetAssessmentRecommendations();

            foreach (var recommendation in _assessmentRecommendationsExternal)
            {
                _assessmentRecommendations.Add(new NS_TblRecommendation
                {
                    AssessmentTypeId = recommendation.AssessmentTypeId,
                    AssessmentTypeTranslationAdministrationId = recommendation.AssessmentTypeTranslationAdministrationId,
                    Id = recommendation.Id,
                    MaturityLevelId = recommendation.MaturityLevelId,
                    TranslatorAdministratorNameId = recommendation.TranslatorAdministratorNameId,
                    TranslatorAdministratorTextId = recommendation.TranslatorAdministratorTextId,
                    AssessmentQuestionId = recommendation.AssessmentQuestionId
                    
                });
            }

            return _assessmentRecommendations;
        }
    }
}
