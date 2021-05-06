using SOMSightModels;
using SOMSightModels.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISOMSightRepositories
{
    public interface IAssessmentQuestionRepository 
    {
        IEnumerable<NS_TblAssessmentQuestion> GetAssessmentQuestions(int pAssessmentTypeId);

        IEnumerable<NS_TblAnswerXAssessment> GetAnswersXAssessment( Guid pUserId);

        void DeleteAllAnswersXAssessment(Guid pUserId, int pAssessmentTypeId);

        void SaveAnswersXAssessment(IEnumerable<NS_TblAnswerXAssessment> pAnswersXAssessment);

        AssessmentRecommendationsDTO AssessmentScoringModel(int pAssessmentTypeId, Guid pUserId, IEnumerable<NS_TblRecommendation> pAssessmentRecommendations);

    }
}
