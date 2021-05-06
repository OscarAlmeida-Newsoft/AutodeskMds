using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affidavit.Models
{
    public class ProgramasViewModel
    {
        public short PartnerProgramID { get; set; }
        public string PartnerProgramName { get; set; }
        public string PartnerID { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}