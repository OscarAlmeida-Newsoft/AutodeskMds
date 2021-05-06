using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affidavit.Models
{
    public class CompetenciasViewModel
    {
        public short PartnerCapabilityID { get; set; }
        public string PartnerCapabilityName { get; set; }
        public string Level { get; set; }
        public string PartnerID { get; set; }
        public DateTime RenovationDate { get; set; }
    }
}