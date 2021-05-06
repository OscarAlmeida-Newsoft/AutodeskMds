
using System;

namespace SharedEntities
{
    public class NS_TblAssessmentQuestionOption
    {
        public int Id { get; set; }
        public int AssessmentQuestionId { get; set; }
        public int TranslatorAdministratorId { get; set; }
        public string MaturityLevelId { get; set; }
        public bool Status { get; set; }

        public DateTime CreatedDate { get; set; }
        public Guid CreatedById { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedById { get; set; }
        
    }
}
