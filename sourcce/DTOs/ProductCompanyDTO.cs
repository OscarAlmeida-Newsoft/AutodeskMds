using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class ProductCompanyDTO
    {
        public int CompanyID { get; set; }
        
        public short ProductID { get; set; }
        
        public short CampaignID { get; set; }

        public short InstalledLicenses { get; set; }

        public short VLS { get; set; }

        public short FPP { get; set; }

        public short OEM { get; set; }

        public short WEB { get; set; }

        public string Agreement { get; set; }
    }
}
