using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class AssessmentQuestionOptionsDTO
    {
        public int Id { get; set; }
        public int AssessmentQuestionId { get; set; }
        public int TranslatorAdministratorId { get; set; }
        public string MaturityLevelId { get; set; }
        
    }
}
