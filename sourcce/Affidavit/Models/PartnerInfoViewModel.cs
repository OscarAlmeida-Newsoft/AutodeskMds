using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affidavit.Models
{
    public class PartnerInfoViewModel
    {
        public IEnumerable<CompetenciasViewModel> Competencias { get; set; }
        public IEnumerable<ProgramasViewModel> Programas { get; set; }
    }
}