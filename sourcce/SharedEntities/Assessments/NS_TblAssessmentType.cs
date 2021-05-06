
using System;
using System.ComponentModel.DataAnnotations;

namespace SharedEntities
{
    public class NS_TblAssessmentType
    {
        public int Id { get; set; }
        public int TranslatorAdministratorId { get; set; }
        public int TranslatorAdministratorDescriptionId { get; set; }

        [StringLength(50)]
        public string Description { get; set; }
        public int IndustryId { get; set; }
        public bool Status { get; set; }

        public DateTime CreatedDate { get; set; }
        public Guid CreatedById { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedById { get; set; }
        public string IconName { get; set; }

        public bool IsIndustryInsights { get; set; }

    }
}
