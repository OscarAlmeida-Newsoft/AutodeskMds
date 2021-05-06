using DTOs;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface IAssessmentQuestionRepository 
    {
        IEnumerable<NS_TblAssessmentQuestion> GetAssessmentQuestions(int pAssessmentTypeId);

        IEnumerable<NS_TblAnswerXAssessment> GetAnswersXAssessment( Guid pLeadId, Guid pCampaignID);

        void DeleteAllAnswersXAssessment(Guid pLeadId, Guid pCampaignID, int pAssessmentTypeId);

        void SaveAnswersXAssessment(IEnumerable<NS_TblAnswerXAssessment> pAnswersXAssessment);

        AssessmentRecommendationsDTO AssessmentScoringModel(int pAssessmentTypeId, Guid pLeadId, Guid pCampaingId, IEnumerable<NS_TblRecommendation> pAssessmentRecommendations);

    }
}
