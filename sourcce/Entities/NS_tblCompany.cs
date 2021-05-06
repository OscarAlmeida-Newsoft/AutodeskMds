namespace Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class NS_tblCompany
    {
        public NS_tblCompany()
        {
            NS_tblCompanyContacts = new HashSet<NS_tblCompanyContacts>();
            NS_tblCompanyInfo = new HashSet<NS_tblCompanyInfo>();
            NS_tblPartnerCapabilityCompany = new HashSet<NS_tblPartnerCapabilityCompany>();
            NS_tblPartnerProgramCompany = new HashSet<NS_tblPartnerProgramCompany>();
            NS_tblProductCompany = new HashSet<NS_tblProductCompany>();
            NS_tblProductFamilyCompany = new HashSet<NS_tblProductFamilyCompany>();
        }

        
        [Key]
        public int CompanyID { get; set; }

        [StringLength(50)]
        public string AccountNumber { get; set; }

        [Display(Name = "LabelNombreEmpresa", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
                  ErrorMessageResourceName = "MensajeErrorNombreEmpresaRequerido")]
        [StringLength(120, ErrorMessageResourceType = typeof(Resources.Resources),
                          ErrorMessageResourceName = "MensajeErrorNombreEmpresaLargo")]
        public string CompanyName { get; set; }

        public short IndustryID { get; set; }

        public byte? CompanyTypeID { get; set; }

        public int CountryID { get; set; }

        public bool? UpdatedFromCRMFlag { get; set; }

        [StringLength(15, ErrorMessageResourceType = typeof(Resources.Resources),
                          ErrorMessageResourceName = "MensajeErrorNombreEmpresaLargo")]
        public string CreatedBy { get; set; }
        
        public DateTime CreatedOn { get; set; }

        [StringLength(15, ErrorMessageResourceType = typeof(Resources.Resources),
                          ErrorMessageResourceName = "MensajeErrorNombreEmpresaLargo")]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public int? PurchaseAndInvoicingMode { get; set; }

        public virtual NS_tblCompanyType NS_tblCompanyType { get; set; }

        public virtual NS_tblIndustry NS_tblIndustry { get; set; }

        public virtual ICollection<NS_tblCompanyContacts> NS_tblCompanyContacts { get; set; }

        public virtual ICollection<NS_tblCompanyInfo> NS_tblCompanyInfo { get; set; }

        public virtual ICollection<NS_tblPartnerCapabilityCompany> NS_tblPartnerCapabilityCompany { get; set; }

        public virtual ICollection<NS_tblPartnerProgramCompany> NS_tblPartnerProgramCompany { get; set; }

        public virtual ICollection<NS_tblProductCompany> NS_tblProductCompany { get; set; }

        public virtual ICollection<NS_tblProductFamilyCompany> NS_tblProductFamilyCompany { get; set; }
    }
}
