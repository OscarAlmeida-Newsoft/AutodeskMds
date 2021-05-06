using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class NS_TblAssessmentSummary
    {
        public int Id { get; set; }
        public Guid CompanyId { get; set; }
        public Guid CampaignId { get; set; }
        public int AssessmentTypeId { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(1)]
        public string Code { get; set; }
        public string GlobalMaturityLevelId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ResponseStartDate { get; set; }
        public DateTime? ResponseEndDate { get; set; }
        public string NextSteps { get; set; }


    }
}
