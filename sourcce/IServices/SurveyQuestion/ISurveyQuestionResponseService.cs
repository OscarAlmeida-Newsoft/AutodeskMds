using DTOs.SurveyQuestionResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices.SurveyQuestion
{
    public interface ISurveyQuestionResponseService
    {
        /// <summary>
        /// Save a Survey Question Response
        /// </summary>
        /// <param name="pSurveyQuestionResponse">Survey Question Response object</param>
        void CreateSurveyQuestionResponse(SurveyQuestionResponseDTO pSurveyQuestionResponse);

        /// <summary>
        /// Get a survey question by LeadId
        /// </summary>
        /// <param name="pLeadID">Lead Id for the survey</param>
        /// <returns></returns>
        SurveyQuestionResponseDTO GetSurveyQuestionByLeadId(Guid pLeadID);

        /// <summary>
        /// Update the survey question response IsNotInterested camp
        /// </summary>
        /// <param name="pLeadID"></param>
        void UpdateIsNotInterested(int pIsNotInterested, Guid pLeadID);

        /// <summary>
        /// Update a survey question response
        /// </summary>
        /// <param name="pSurverResponse"></param>
        void UpdateSurveyQuestionResponse(SurveyQuestionResponseDTO pSurverResponse);
    }
}
