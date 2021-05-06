using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class NS_TblAssessmentTypeDTO
    {
        public int Id { get; set; }
        public int TranslatorAdministratorId { get; set; }
        public int TranslatorAdministratorDescriptionId { get; set; }
        public int TranslatorAdministratorLandingText1Id { get; set; }
        public int TranslatorAdministratorLandingText2Id { get; set; }
        public int TranslatorAdministratorLandingText3Id { get; set; }

        public string Description { get; set; }
        public int IndustryId { get; set; }
        public bool Status { get; set; }

        public DateTime CreatedDate { get; set; }
        public Guid CreatedById { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedById { get; set; }
        public string IconName { get; set; }
        public string IconLanding { get; set; }
        public string BackgroudImage { get; set; }
        public bool IsFree { get; set; }

        public bool IsIndustryInsights { get; set; }
    }
}
