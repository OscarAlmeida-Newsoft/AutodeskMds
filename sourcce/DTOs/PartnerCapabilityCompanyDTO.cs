using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class PartnerCapabilityCompanyDTO
    {
        public int CompanyID { get; set; }
        
        public short PartnerCapabilityID { get; set; }
        
        public short CampaignID { get; set; }
        
        public string Category { get; set; }
        
        public string IDPartner { get; set; }
        
        public DateTime? RenovationDate { get; set; }
    }
}
