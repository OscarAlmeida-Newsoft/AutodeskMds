using Entities.SurveyQuestionResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface ISurveyQuestionResponseRepository
    {
        /// <summary>
        /// Save a Survey Question Response
        /// </summary>
        /// <param name="pSurveyQuestionResponse">Survey Question Response Object</param>
        void CreateSurveyQuestionResponse(SurveyQuestionResponse pSurveyQuestionResponse);

        /// <summary>
        /// Get a survey question by LeadId
        /// </summary>
        /// <param name="pLeadID">Lead Id for the survey</param>
        /// <returns></returns>
        SurveyQuestionResponse GetSurveyQuestionByLeadId(Guid pLeadID);
    }
}
