using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class ProcessDTO
    {
        public int Id { get; set; }
        public int AssessmentSummaryId { get; set; }
        public int IndustryId { get; set; }
        public int ActivityId { get; set; }
        public int TranslatorAdministratorNameId { get; set; }
        public int TranslatorAdministratorDescriptionId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public Guid UserCreation { get; set; }
        public DateTime DateCreation { get; set; }
        public Guid UserLastModification { get; set; }
        public DateTime DateLastModification { get; set; }

        public int TranslatorAdministratorActivityDescriptionId { get; set; }

        public IEnumerable<ProblemDTO> Problems { get; set; }
        public IEnumerable<DigitalTransformationDTO> DigitalTransformations { get; set; }

        //Scoring
        public decimal Score { get; set; }
        public string ClassCss { get; set; }
    }
}
