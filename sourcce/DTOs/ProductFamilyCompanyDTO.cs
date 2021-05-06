using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class ProductFamilyCompanyDTO
    {
        public int CompanyID { get; set; }

        public byte ProductFamilyID { get; set; }
        
        public short CampaignID { get; set; }
        
        public string AdditionalComments { get; set; }
    }
}
