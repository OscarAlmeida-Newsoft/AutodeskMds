using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affidavit.Models.AdminMDS
{
    public class LicensesTypesViewModel
    {
        public short ProductId { get; set; }
        public short InstalledLicenses { get; set; }
        public short VLS { get; set; }
        public short OEM { get; set; }
        public short FPP { get; set; }
        public short WEB { get; set; }
        public int CompanyID { get; set; }
        public short CampaignID { get; set; }
    }
}