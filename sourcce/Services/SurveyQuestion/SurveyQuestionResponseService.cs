using AutoMapper;
using DTOs.SurveyQuestionResponse;
using Entities.SurveyQuestionResponse;
using IRepositories;
using IServices.SurveyQuestion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.SurveyQuestion
{
    public class SurveyQuestionResponseService : ISurveyQuestionResponseService
    {
        protected IUnitOfWork.IUnitOfWork unitOfWork;
        readonly ISurveyQuestionResponseRepository SurveyQuestionResponseRepository;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="pUnitOfWork"></param>
        /// <param name="pSurveyQuestionResponseRepository"></param>
        public SurveyQuestionResponseService(IUnitOfWork.IUnitOfWork pUnitOfWork, ISurveyQuestionResponseRepository pSurveyQuestionResponseRepository)
        {
            unitOfWork = pUnitOfWork;
            SurveyQuestionResponseRepository = pSurveyQuestionResponseRepository;
        }

        /// <summary>
        /// Save a landing page
        /// </summary>
        /// <param name="pLanding">Landing object</param>
        public void CreateSurveyQuestionResponse(SurveyQuestionResponseDTO pSurveyQuestionResponse)
        {

            pSurveyQuestionResponse.SurveyQuestion5Response = pSurveyQuestionResponse.SurveyQuestion5Response ?? "";
            //pLanding.Id = Guid.NewGuid();
            //pLanding.CreatedDate = DateTime.Now;
            //pLanding.EditedDate = DateTime.Now;
            //pLanding.CreatedById = Guid.Parse("B20D0289-21D2-4B12-9096-8ECFF2966E85");
            //pLanding.Status = true;
            //pLanding.LandingText = pLanding.LandingText ?? "";
            pSurveyQuestionResponse.IsNotInterested = true;
            SurveyQuestionResponseRepository.CreateSurveyQuestionResponse(Mapper.Map<SurveyQuestionResponseDTO, SurveyQuestionResponse>(pSurveyQuestionResponse));
            unitOfWork.Complete();
        }

        /// <summary>
        /// Get a survey question by LeadId
        /// </summary>
        /// <param name="pLeadID">Lead Id for the survey</param>
        /// <returns></returns>
        public SurveyQuestionResponseDTO GetSurveyQuestionByLeadId(Guid pLeadID)
        {
            var _data = SurveyQuestionResponseRepository.GetSurveyQuestionByLeadId(pLeadID);
            return Mapper.Map<SurveyQuestionResponse, SurveyQuestionResponseDTO>(_data);
        }

        /// <summary>
        /// Update the survey question response IsNotInterested camp
        /// </summary>
        /// <param name="pLeadID"></param>
        public void UpdateIsNotInterested(int pIsNotInterested, Guid pLeadID)
        {
            SurveyQuestionResponse _surveyQuestionResponse =  SurveyQuestionResponseRepository.GetSurveyQuestionByLeadId(pLeadID);

            _surveyQuestionResponse.IsNotInterested = (pIsNotInterested == 1) ? true : false;

            unitOfWork.Complete();
        }

        /// <summary>
        /// Update a survey question response
        /// </summary>
        /// <param name="pSurverResponse"></param>
        public void UpdateSurveyQuestionResponse(SurveyQuestionResponseDTO pSurverResponse)
        {
            SurveyQuestionResponse _surveyResponse = SurveyQuestionResponseRepository.GetSurveyQuestionByLeadId(pSurverResponse.LeadId);

            _surveyResponse.SurveyQuestion1OtherResponse = pSurverResponse.SurveyQuestion1OtherResponse;
            _surveyResponse.SurveyQuestion1Response = pSurverResponse.SurveyQuestion1Response;
            _surveyResponse.SurveyQuestion2OtherResponse = pSurverResponse.SurveyQuestion2OtherResponse;
            _surveyResponse.SurveyQuestion2Response = pSurverResponse.SurveyQuestion2Response;
            _surveyResponse.SurveyQuestion3OtherResponse = pSurverResponse.SurveyQuestion3OtherResponse;
            _surveyResponse.SurveyQuestion3Response = pSurverResponse.SurveyQuestion3Response;
            _surveyResponse.SurveyQuestion4Option1Value = pSurverResponse.SurveyQuestion4Option1Value;
            _surveyResponse.SurveyQuestion4Option2Value = pSurverResponse.SurveyQuestion4Option2Value;
            _surveyResponse.SurveyQuestion4Option3Value = pSurverResponse.SurveyQuestion4Option3Value;
            _surveyResponse.SurveyQuestion4Option4Value = pSurverResponse.SurveyQuestion4Option4Value;
            _surveyResponse.SurveyQuestion5Response = pSurverResponse.SurveyQuestion5Response;
            _surveyResponse.IsNotInterested = pSurverResponse.IsNotInterested;

            unitOfWork.Complete();
        }
    }
}
