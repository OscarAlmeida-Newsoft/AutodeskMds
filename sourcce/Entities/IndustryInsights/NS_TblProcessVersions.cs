using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class NS_TblProcessVersions
    {
        public int Id { get; set; }
        public int ProcessId { get; set; }
        public int AssessmentSummaryId { get; set; }
        public int ActivityId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public Guid User { get; set; }
        public DateTime Date { get; set; }

        public virtual NS_TblProcess Process { get; set; }
        public virtual NS_TblAssessmentSummary AssessmentSummary { get; set; }
        public virtual NS_TblActivity Activity { get; set; }
    }
}
