using System;
using System.Collections.Generic;
namespace SOMSight.Models
{
    public class AssessmentViewModel
    {
        public int AssessmentTypeId { get; set; }
        public int AssessmentTypeTranslatorId { get; set; }
        public bool Status { get; set; }
        public bool IsFinal { get; set; }
        public Guid UserId { get; set; }
        public int AssessmentSummaryId { get; set; }
        public int? AssessmentMaturityLevelTranslatorId { get; set; }
        public string IconName { get; set; }
        public int TotalQuestion { get; set; }
        public int AnsweredQuestions { get; set; }

        public IEnumerable<AssessmentQuestionsViewModel> AssessmentQuestions { get; set; }
    }
}