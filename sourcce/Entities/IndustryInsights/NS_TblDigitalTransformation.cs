using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class NS_TblDigitalTransformation
    {
        public int Id { get; set; }
        public int ProcessId { get; set; }
        public string Pillar { get; set; }
        public string Technology { get; set; }
        public string Brand { get; set; }
        public string Comment { get; set; }
        public string Solution { get; set; }

        public int Priority { get; set; }
        public bool Exist { get; set; }
        public bool Solved { get; set; }

        public Guid UserCreation { get; set; }
        public DateTime DateCreation { get; set; }
        public Guid UserLastModification { get; set; }
        public DateTime DateLastModification { get; set; }

        public virtual NS_TblProcess Process { get; set; }
    }
}
