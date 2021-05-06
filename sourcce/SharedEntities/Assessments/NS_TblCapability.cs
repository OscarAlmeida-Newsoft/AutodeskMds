using System;
using System.ComponentModel.DataAnnotations;

namespace SharedEntities
{
    public class NS_TblCapability
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        public int AssessmentTypeId { get; set; }
        public int TranslatorAdministratorId { get; set; }
        public int? ParentCapabilityId { get; set; }
        public float Weight { get; set; }
        public bool Status { get; set; }

        public DateTime CreatedDate { get; set; }
        public Guid CreatedById { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedById { get; set; }

        public virtual NS_TblTranslatorAdministrator TranslatorAdministrator { get; set; }
        public virtual NS_TblAssessmentType AssessmentType { get; set; }
        public virtual NS_TblCapability ParentCapability { get; set; }
    }
}
