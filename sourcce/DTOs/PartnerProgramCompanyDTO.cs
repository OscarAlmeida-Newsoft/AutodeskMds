using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class PartnerProgramCompanyDTO
    {
        public int CompanyID { get; set; }
        
        public short PartnerProgramID { get; set; }
        
        public short CampaignID { get; set; }
        
        public string IDPartner { get; set; }
        
        public DateTime? ExpirationDate { get; set; }
    }
}
