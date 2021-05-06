using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affidavit.Models.AdminMDS
{
    public class CompanySearchViewModel
    {
        public int CompanyID { get; set; }

        public int CampaignID { get; set; }

        public string CampaignName { get; set; }

        public string CompanyName { get; set; }

        public string Industry { get; set; }
        //public string IndustryID2 { get; set; }

        public string CompanyType { get; set; }

        public string Country { get; set; }

        public Guid LeadID { get; set; }

    }
}