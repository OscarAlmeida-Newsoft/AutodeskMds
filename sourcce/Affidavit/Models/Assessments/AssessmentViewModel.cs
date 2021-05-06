using System;
using System.Collections.Generic;
namespace Affidavit.Models
{
    public class AssessmentViewModel
    {
        public int AssessmentTypeId { get; set; }
        public int AssessmentTypeTranslatorId { get; set; }
        public bool Status { get; set; }
        public bool IsFinal { get; set; }
        public Guid CompanyId { get; set; }
        public Guid CampaignId { get; set; }
        public int AssessmentSummaryId { get; set; }
        public int? AssessmentMaturityLevelTranslatorId { get; set; }
        public string IconName { get; set; }
        public int TotalQuestion { get; set; }
        public int AnsweredQuestions { get; set; }
        public bool IsIndustryInsights { get; set; }


        public IEnumerable<AssessmentQuestionsViewModel> AssessmentQuestions { get; set; }
    }

    public class AssessmentAdminViewModel
    {
        public int IdCompany { get; set; }
        public int IdCampaign { get; set; }
        public Guid CompanyID { get; set; }
        public Guid CampaignID { get; set; }
        public int LanguageID { get; set; }
        public IEnumerable<AssessmentViewModel> AssessmentList { get; set; }
    }
}