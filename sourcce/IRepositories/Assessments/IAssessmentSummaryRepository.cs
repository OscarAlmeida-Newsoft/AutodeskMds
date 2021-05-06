using DTOs;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface IAssessmentSummaryRepository
    {
        string Message { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pLeadID"></param>
        /// <param name="pCampaignID"></param>
        IEnumerable<NS_TblAssessmentSummary> GetAssessmentsSummary(Guid pLeadID, Guid pCampaignID);

        /// <summary>
        /// Check if there are Assessments to insert for the lead in the indicated campaign
        /// </summary>
        /// <param name="pLeadID">LeadId</param>
        /// <param name="pCampaignID"> Campaign Id</param>
        /// <param name="pAssessmentTypes">pAssessmentTypes</param>
        bool CheckAssessmentsSummary(Guid pLeadID, Guid pCampaignID, IEnumerable<NS_TblAssessmentType> pAssessmentTypes);

        NS_TblAssessmentSummary GetAssessmentsSummaryById(int pAssessmentSummaryId);

        void MarkAssessmentSummaryAsFinished(Guid pLeadId, Guid pCampaignId, int pAssessmentSummaryId,string pGlobalMaturityLevel);
        void MarkAssessmentSummaryAsDraft(Guid pLeadId, Guid pCampaignId, int pAssessmentSummaryId);

        string GetAssessmentGlobalMaturityLevel(int pAssessmentTypeId, Guid pCompanyId, Guid pCampaignId);

        int GetFinishedAssessmentCount(Guid pCompanyId, Guid pCampaignId);

        void SaveNextSteps(int pAssessmentSummaryId, string pNextSteps);
        void UnlockAssessment(int pAssessmentSummaryId);
    }
}
