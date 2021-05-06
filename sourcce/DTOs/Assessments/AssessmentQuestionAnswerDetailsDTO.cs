using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class AssessmentQuestionAnswerDetailsDTO
    {
        public bool IsFinal { get; set; }
        public Guid CampaignId { get; set; }
        public Guid CompanyId { get; set; }
        public int AssessmentId { get; set; }
        public string SelectedOption { get; set; }
    }
}
