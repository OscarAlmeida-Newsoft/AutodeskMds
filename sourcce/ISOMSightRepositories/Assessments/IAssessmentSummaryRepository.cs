
using SOMSightModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISOMSightRepositories
{
    public interface IAssessmentSummaryRepository
    {
        string Message { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pLeadID"></param>
        /// <param name="pCampaignID"></param>
        IEnumerable<NS_TblAssessmentSummary> GetAssessmentsSummary(Guid pUserId);

        /// <summary>
        /// Check if there are Assessments to insert for the lead in the indicated campaign
        /// </summary>
        /// <param name="pLeadID">LeadId</param>
        /// <param name="pCampaignID"> Campaign Id</param>
        /// <param name="pAssessmentTypes">pAssessmentTypes</param>
        bool CheckAssessmentsSummary(Guid pUserId, IEnumerable<NS_TblAssessmentType> pAssessmentTypes);

        NS_TblAssessmentSummary GetAssessmentsSummaryById(int pAssessmentSummaryId);

        void MarkAssessmentSummaryAsFinished(Guid pUserId, int pAssessmentSummaryId,string pGlobalMaturityLevel);
        void MarkAssessmentSummaryAsDraft(Guid pUserId, int pAssessmentSummaryId);

        string GetAssessmentGlobalMaturityLevel(int pAssessmentTypeId, Guid pUserId);
    }
}
