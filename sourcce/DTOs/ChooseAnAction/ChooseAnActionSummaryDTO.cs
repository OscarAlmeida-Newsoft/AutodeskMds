using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class ChooseAnActionSummaryDTO
    {
        public int MDSProgressValue { get; set; }
        public int AssessmentsProgress { get; set; }
        public int AvailableAssessments { get; set; }
        public int FinishedAssessments { get; set; }
    }
}
