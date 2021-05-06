using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOMSightModels.DTOs
{
    public class AssessmentQuestionDTO
    {
        public int Id { get; set; }
        public int TranslatorAdministratorId { get; set; }
        public int DisplayOrder { get; set; }
        public string SelectedMaturityLevelId { get; set; }
        public Guid UserId { get; set; }

        public IEnumerable<AssessmentQuestionOptionsDTO> AssessmentQuestionOptions { get; set; }
    }
}
