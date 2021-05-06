using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs;

namespace IServices
{
    public interface IIndustryInsightsService
    {
        IEnumerable<IndustryIndInsDTO> GetAllIndustries();

        IndustryIndInsDTO GetIndustryById(int pId);

        void GeneratePreLoadInfo(int AssesmentSummaryId, int IndustryId, int LanguageId, Guid LeadId);

        void MarkIndustryInsightAsFinished(Guid LeadId, Guid CampaignId, int pAssessmentSumaryId);

        IEnumerable<IndustryInsightsAdminDTO> GetAllIndustryInsightsAssessments(int pPageNumber, int pPageSize);

        int GetNumbreAllIndustryInsightsAssessments();

        RecommendationsDocumentDTO GetRecommendationInfo(int pAssessmentSumaryId);

        bool ValidateFinalIndustryInsigth(int pAssessmentSummaryId);
    }
}
