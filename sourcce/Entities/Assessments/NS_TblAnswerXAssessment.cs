using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class NS_TblAnswerXAssessment
    {
        public int Id { get; set; }
        public Guid CompanyId { get; set; }
        public Guid CampaignId { get; set; }
        public int AssessmentId { get; set; }
        public int AssessmentTypeId { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(1)]
        public string Response { get; set; }
        public int Points { get; set; }
        public DateTime UpdatedDate { get; set; }
        
    }
}
