using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOMSight.Models
{
    public class AssessmentGridViewModel
    {
        public int AssessmentSummaryId { get; set; }
        public int AssessmentTypeId { get; set; }
        public int AssessmentDescriptionTranslatorId { get; set; }
        public int AssessmentTypeTraslatorId { get; set; }
        public string AssessmentStatus { get; set; }
        public int? GlobalMaturityLevelTranslatorId { get; set; }
        public string EvaluationLevelDescription { get; set; }
        public bool IsMDSAssessment { get; set; }
        public bool IsInProgress { get; set; }
        public bool DownloadPDF { get; set; }
        public string IconName { get; set; }
    }
}