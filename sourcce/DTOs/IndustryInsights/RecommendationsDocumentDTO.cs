using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class RecommendationsDocumentDTO
    {
        public int Year { get; set; }
        public string Month { get; set; }
        public string CompanyName { get; set; }
        public string CompanyDescription { get; set; }
        public string NextSteps { get; set; }
        public IEnumerable<ActivityDTO> Activities { get; set; }
        public IEnumerable<ProcessDTO> Processes { get; set; }
    }
}
