using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class NS_TblDigitalTransformationVersions
    {
        public int Id { get; set; }
        public int DigitalTransformationId { get; set; }
        public int ProcessId { get; set; }
        public string Pillar { get; set; }
        public string Technology { get; set; }
        public string Brand { get; set; }
        public string Comment { get; set; }
        public string Solution { get; set; }

        public int Priority { get; set; }
        public bool Exist { get; set; }
        public bool Solved { get; set; }

        public Guid User { get; set; }
        public DateTime Date { get; set; }

        public virtual NS_TblDigitalTransformation DigitalTransformation { get; set; }
        public virtual NS_TblProcess Process { get; set; }
    }
}
