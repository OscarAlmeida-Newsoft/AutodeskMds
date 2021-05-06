namespace Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class NS_tblCampaign
    {
        public NS_tblCampaign()
        {
            NS_tblCompanyContacts = new HashSet<NS_tblCompanyContacts>();
            NS_tblCompanyInfo = new HashSet<NS_tblCompanyInfo>();
            NS_tblPartnerCapabilityCompany = new HashSet<NS_tblPartnerCapabilityCompany>();
            NS_tblPartnerProgramCompany = new HashSet<NS_tblPartnerProgramCompany>();
            NS_tblProductCompany = new HashSet<NS_tblProductCompany>();
            NS_tblProductFamilyCompany = new HashSet<NS_tblProductFamilyCompany>();
        }

        [Key]
        public short CampaignID { get; set; }

        [Required]
        [StringLength(100)]
        public string CampaignName { get; set; }

        [StringLength(50)]
        public string CRMCampaignID { get; set; }


        public virtual ICollection<NS_tblCompanyContacts> NS_tblCompanyContacts { get; set; }

        public virtual ICollection<NS_tblCompanyInfo> NS_tblCompanyInfo { get; set; }

        public virtual ICollection<NS_tblPartnerCapabilityCompany> NS_tblPartnerCapabilityCompany { get; set; }

        public virtual ICollection<NS_tblPartnerProgramCompany> NS_tblPartnerProgramCompany { get; set; }

        public virtual ICollection<NS_tblProductCompany> NS_tblProductCompany { get; set; }

        public virtual ICollection<NS_tblProductFamilyCompany> NS_tblProductFamilyCompany { get; set; }
    }
}
