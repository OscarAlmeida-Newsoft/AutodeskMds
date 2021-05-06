using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOMSightModels.DTOs
{
    public class AssessmentSummaryDTO
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int AssessmentTypeId { get; set; }
        public int AssessmentTypeTraslatorId { get; set; }
        public int AssessmentDescriptionTranslatorId { get; set; }
        public string GlobalMaturityLevelId { get; set; }
        public int? GlobalMaturityLevelTranslatorId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ResponseStartDate { get; set; }
        public DateTime? ResponseEndDate { get; set; }

        public string IconName { get; set; }
    }
}
