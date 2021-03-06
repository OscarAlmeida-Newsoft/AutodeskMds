using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affidavit.Models
{
    public class AdminIndustryInsightsVM
    {
        public int IdAssessmentSummary { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string IndustryName { get; set; }
        public string ContactName { get; set; }
        public string Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}