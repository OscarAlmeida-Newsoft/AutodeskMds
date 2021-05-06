using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class CompanyDTO
    {
        public int CompanyID { get; set; }

        public string AccountNumber { get; set; }

        [Display(Name = "LabelNombreEmpresa", ResourceType = typeof(Resources.Resources))]
        public string CompanyName { get; set; }

        [Display(Name = "LabelIndustria", ResourceType = typeof(Resources.Resources))]
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
}
