using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class NS_TblProblemVersions
    {
        public int Id { get; set; }
        public int ProblemId { get; set; }
        public int ProcessId { get; set; }

        public string ProblemDescription { get; set; }
        public string Opportunity { get; set; }
        public string Technology { get; set; }
        public string Inventory { get; set; }
        public string Solution { get; set; }

        public int Priority { get; set; }
        public bool Exist { get; set; }
        public bool Microsoft { get; set; }

        public bool Solved { get; set; }

        public Guid User { get; set; }
        public DateTime Date { get; set; }

        public virtual NS_TblProcess Process { get; set; }
        public virtual NS_TblProblem Problem { get; set; }
    }
}
