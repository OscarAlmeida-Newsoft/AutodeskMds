using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affidavit.Models.AdminMDS
{
    public class ProductFamilyCompanyViewModel
    {
        public int CompanyID { get; set; }

        public byte ProductFamilyID { get; set; }

        public short CampaignID { get; set; }

        public string AdditionalComments { get; set; }
    }
}