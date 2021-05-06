using SOMSightModels.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISOMSightServices
{
    public interface IAssessmentQuestionService
    {
        IEnumerable<AssessmentQuestionDTO> GetAssessmentQuestions(AssessmentSummaryDTO pAssessmentSummary);

        void SaveAssessmentQuestionsDraft(IEnumerable<AssessmentQuestionAnswerDetailsDTO> pAssessmentQuestionDetails, Guid pUserId, int pAssessmentSummaryId, int pAssessmentTypeId);
        void SaveAssessmentQuestionsFinal(IEnumerable<AssessmentQuestionAnswerDetailsDTO> pAssessmentQuestionDetails, Guid pUserId, int pAssessmentSummaryId, int pAssessmentTypeId, string pMessageTo, string pMessageSubject, string pMessageBody);
        AssessmentRecommendationsDTO AssessmentScoringModel(int pAssessmentTypeId, Guid pUserId);
    }
}
