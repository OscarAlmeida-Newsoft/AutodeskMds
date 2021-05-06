using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOMSightModels.DTOs
{
    public class AssessmentQuestionAnswerDetailsDTO
    {
        public bool IsFinal { get; set; }
        public Guid UserId { get; set; }
        public int AssessmentId { get; set; }
        public string SelectedOption { get; set; }
    }
}
