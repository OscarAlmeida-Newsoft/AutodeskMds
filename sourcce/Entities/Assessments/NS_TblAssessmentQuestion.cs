
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class NS_TblAssessmentQuestion
    {

        public int Id { get; set; }
        public int TranslatorAdministratorId { get; set; }
        public int AssessmentTypeId { get; set; }
        public int DisplayOrder { get; set; }
        public bool Status { get; set; }

        public DateTime CreatedDate { get; set; }
        public Guid CreatedById { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedById { get; set; }

        public IEnumerable<NS_TblAssessmentQuestionOption> AssessmentQuestionOptions { get; set; }
    }
}
