
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOMSightModels
{
    public class NS_TblAssessmentQuestionOption
    {
        public int Id { get; set; }
        public int AssessmentQuestionId { get; set; }
        public int TranslatorAdministratorId { get; set; }
        public string Description { get; set; }
        public string MaturityLevelId { get; set; }
        public bool Status { get; set; }

        public DateTime CreatedDate { get; set; }
        public Guid CreatedById { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedById { get; set; }
    }
}
