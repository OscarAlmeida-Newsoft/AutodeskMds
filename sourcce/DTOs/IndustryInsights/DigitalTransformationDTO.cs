using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class DigitalTransformationDTO
    {
        public int Id { get; set; }
        public int ProcessPreLoadedId { get; set; }
        public int ProcessId { get; set; }
        public string ProcessName { get; set; }
        public int TranslatorAdministratorPillarId { get; set; }
        public int TranslatorAdministratorTechnologyId { get; set; }
        public int TranslatorAdministratorBrandId { get; set; }
        public int TranslatorAdministratorCommentId { get; set; }
        public int? TranslatorAdministratorSolutionId { get; set; }

        public int Priority { get; set; }
        public bool Exist { get; set; }

        public string Pillar { get; set; }
        public string Technology { get; set; }
        public string Brand { get; set; }
        public string Comment { get; set; }
        public string Solution { get; set; }
        public bool Solved { get; set; }

        public Guid UserCreation { get; set; }
        public DateTime DateCreation { get; set; }
        public Guid UserLastModification { get; set; }
        public DateTime DateLastModification { get; set; }
    }
}
