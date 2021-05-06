using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affidavit.Models
{
    public class LoadProcessesVM
    {
        public int AssessmentSummaryId { get; set; }
        public int ActivityId { get; set; }
        public bool IsAdminUser { get; set; }
    }
}