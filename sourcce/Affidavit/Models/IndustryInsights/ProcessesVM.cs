using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affidavit.Models
{
    public class ProcessesVM
    {
        public int ActivityId { get; set; }
        public int AssessmentSummaryId { get; set; }
        public bool IsAssessmentFinished { get; set; }
        public int ActivityTranslatorAdministratorId { get; set; }
        public bool IsAdminUser { get; set; }
        public IEnumerable<ProcessDTO> Processes { get; set; }
    }
}