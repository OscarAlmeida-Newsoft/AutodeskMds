using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class ProductCompanyFileDTO
    {
        public int CompanyID { get; set; }
        
        public short ProductID { get; set; }
        
        public short CampaignID { get; set; }

        public short FileNumber { get; set; }

        public string AlternativeName { get; set; }

        public string Extension { get; set; }

    }
}
