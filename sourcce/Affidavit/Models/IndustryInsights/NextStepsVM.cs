using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Affidavit.Models
{
    public class NextStepsVM
    {
        public int AssessmentSummaryId { get; set; }


        [Required(ErrorMessage = "Next Steps field is required")]
        [MaxLength(200, ErrorMessage = "Next Steps must have at least {1} characters")]
        public string NextStepsComments { get; set; }
    }
}