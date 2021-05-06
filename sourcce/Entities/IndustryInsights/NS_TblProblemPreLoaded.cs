using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class NS_TblProblemPreLoaded
    {
        public int Id { get; set; }
        public int ProcessPreLoadedId { get; set; }
        public int TranslatorAdministratorProblemId { get; set; }
        public int TranslatorAdministratorOpportunityId { get; set; }
        public int TranslatorAdministratorTechnologyId { get; set; }
        public int TranslatorAdministratorInventoryId { get; set; }
        public int? TranslatorAdministratorSolutionId { get; set; }

        public int Priority { get; set; }
        public bool Exist { get; set; }
        public bool Microsoft { get; set; }

        public virtual NS_TblProcessPreLoaded ProcessPreLoaded { get; set; }
    }
}
