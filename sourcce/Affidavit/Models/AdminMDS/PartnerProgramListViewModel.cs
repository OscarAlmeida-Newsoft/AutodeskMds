using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affidavit.Models.AdminMDS
{
    public class PartnerProgramListViewModel
    {
        public string PartenerProgramName { get; set; }
        public string IDPartner { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }

    public class PartnerCapabilityListViewModel
    {
        public string PartenerCapabilityName { get; set; }
        public string Category { get; set; }
        public string IDPartner { get; set; }
        public DateTime? RenovationDate { get; set; }
    }

    public class ProductListaViewModel
    {
        public short ProductID { get; set; }
        public string ProductName { get; set; }
        public short? InstalledLicenses { get; set; }
        public string ProductVersion { get; set; }
        public string ProductFamily { get; set; }
        public short VLS { get; set; }
        public short OEM { get; set; }
        public short FPP { get; set; }
        public short WEB { get; set; }
        public bool HaveFiles { get; set; }
    }

    public class AdditonalCommentsViewModel
    {
        public string ProductFamilyName { get; set; }
        public string AdditionalComments { get; set; }
    }
}