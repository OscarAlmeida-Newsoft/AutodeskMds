using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affidavit.Models
{
    public class AssessmentQuestionAnswerViewModel
    {
        public bool IsFinal { get; set; }
        public Guid CompanyId { get; set; }
        public Guid CampaignId { get; set; }
        public int AssessmentSummaryId { get; set; }
        public int AssessmentTypeId { get; set; }
        public IEnumerable<AssessmentQuestionAnswerDetailsViewModel> AnswerDetails { get; set; }
    }


    public class AssessmentQuestionAnswerDetailsViewModel
    {
        public bool IsFinal { get; set; }
        public Guid CampaignId { get; set; }
        public Guid CompanyId { get; set; }
        public int AssessmentId { get; set; }
        public string SelectedOption { get; set; }
    }
}