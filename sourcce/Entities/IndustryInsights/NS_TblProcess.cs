using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class NS_TblProcess
    {
        public int Id { get; set; }
        public int AssessmentSummaryId { get; set; }
        public int ActivityId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public Guid UserCreation { get; set; }
        public DateTime DateCreation { get; set; }
        public Guid UserLastModification { get; set; }
        public DateTime DateLastModification { get; set; }

        public virtual NS_TblAssessmentSummary AssessmentSummary { get; set; }
        public virtual NS_TblActivity Activity { get; set; }
    }
}
