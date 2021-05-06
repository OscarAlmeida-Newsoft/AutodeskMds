using DTOs;
using Entities;
using System;
using System.Collections.Generic;

namespace IServices
{
    public interface IAssessmentService
    {

        IEnumerable<AssessmentSummaryDTO> GetAssessmentsSummary(Guid pLeadID, Guid pCampaignID);
        AssessmentSummaryDTO GetAssessmentsSummaryById(int pAssessmentSummaryId);
        int GetActiveAssessmentsCount();
        IEnumerable<NS_TblAssessmentType> GetAssessmentType();
        void MarkAssessmentSummaryAsDraft(Guid pUserId, Guid pCampaignId, int pAssessmentSummaryId);
        void SaveNextSteps(int pAssessmentSummaryId, string pNextSteps);
        void UnlockAssessment(int pAssessmentSummaryId);
    }
}
