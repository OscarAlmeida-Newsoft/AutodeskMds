using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class ProblemDTO
    {
        public int Id { get; set; }
        public int ProcessPreLoadedId { get; set; }
        public int ProcessId { get; set; }
        public string ProcessName { get; set; }
        public int TranslatorAdministratorProblemId { get; set; }
        public int TranslatorAdministratorOpportunityId { get; set; }
        public int TranslatorAdministratorTechnologyId { get; set; }
        public int TranslatorAdministratorInventoryId { get; set; }
        public int? TranslatorAdministratorSolutionId { get; set; }

        public string ProblemDescription { get; set; }
        public string Opportunity { get; set; }
        public string Technology { get; set; }
        public string Inventory { get; set; }

        public int Priority { get; set; }
        public bool Exist { get; set; }
        public bool Microsoft { get; set; }
        public string Solution { get; set; }
        public bool Solved { get; set; }

        public Guid UserCreation { get; set; }
        public DateTime DateCreation { get; set; }
        public Guid UserLastModification { get; set; }
        public DateTime DateLastModification { get; set; }
    }
}
