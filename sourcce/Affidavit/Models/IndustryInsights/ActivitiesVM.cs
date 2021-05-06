using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affidavit.Models
{
    public class ActivitiesVM
    {
        public int AssessmentSummaryId { get; set; }
        public bool AssessmentFinished { get; set; }
        public int IndustryTranslationAdministrationId { get; set; }
        public IEnumerable<ActivityDTO> Activities { get; set; }
    }
}