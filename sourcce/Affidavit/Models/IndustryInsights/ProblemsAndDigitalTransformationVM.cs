using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affidavit.Models
{
    public class ProblemsAndDigitalTransformationVM
    {
        public int IdProcess { get; set; }
        public bool IsAssessmentFinished { get; set; }
        public bool IsAdminUser { get; set; }
        public IEnumerable<ProblemDTO> Problems { get; set; }
        public IEnumerable<DigitalTransformationDTO> DigitalTransformations { get; set; }
    }
}