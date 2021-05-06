using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affidavit.Models
{
    public class AssessmentGridViewModel
    {
        public int AssessmentsProgress { get; set; }
        public int AvailableAssessments { get; set; }
        public int FinishedAssessments { get; set; }
        public bool IsFinishedMDS { get; set; }
        public List<AssessmentGridViewModelDetail> AssessmentDetails { get; set; }

    }


    public class AssessmentGridViewModelDetail
    {
        public int AssessmentSummaryId { get; set; }
        public int AssessmentTypeId { get; set; }
        public int AssessmentTypeTraslatorId { get; set; }
        public int AssessmentDescriptionTranslatorId { get; set; }
        public string AssessmentStatus { get; set; }
        public int? GlobalMaturityLevelTranslatorId { get; set; }
        public string EvaluationLevelDescription { get; set; }
        public bool IsMDSAssessment { get; set; }
        public bool IsInProgress { get; set; }
        public bool DownloadPDF { get; set; }
        public string IconName { get; set; }
        public bool IsIndustryInsights { get; set; }
    }
}