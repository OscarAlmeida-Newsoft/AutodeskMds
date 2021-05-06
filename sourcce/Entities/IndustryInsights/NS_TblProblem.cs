using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class NS_TblProblem
    {
        public int Id { get; set; }
        public int ProcessId { get; set; }

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

        public virtual NS_TblProcess Process { get; set; }
    }
}
