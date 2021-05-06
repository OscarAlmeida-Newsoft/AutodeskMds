using Affidavit.Attribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Affidavit.Models.AdminMDS
{
    public class CompanyInfoSearchViewModel
    {
        public int CompanyID { get; set; }

        public string AccountNumber { get; set; }

        [TranslatorDisplay("Old_LabelNombreEmpresa")]
        public string CompanyName { get; set; }

        [TranslatorDisplay("Old_LabelIndustria")]
        public short IndustryID { get; set; }
        //public string IndustryID2 { get; set; }

        public byte? CompanyTypeID { get; set; }

        public int CountryID { get; set; }

        public bool? UpdatedFromCRMFlag { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public int? PurchaseAndInvoicingMode { get; set; }
    }

    public class LeadSearchViewModel
    {
        public Guid LeadId { get; set; }

        
        public string CompanyName { get; set; }

        
        public string CampaignName { get; set; }
        

        public int CountryID { get; set; }
        public string Country { get; set; } 
        public bool SAM360Info { get; set; }

    }
}