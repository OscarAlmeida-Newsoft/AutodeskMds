namespace Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class NS_tblPartnerCapabilityCompany
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CompanyID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short PartnerCapabilityID { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short CampaignID { get; set; }

        [StringLength(10)]
        public string Category { get; set; }

        [StringLength(10)]
        public string IDPartner { get; set; }

        [Column(TypeName = "date")]
        public DateTime? RenovationDate { get; set; }

        public virtual NS_tblCampaign NS_tblCampaign { get; set; }

        public virtual NS_tblCompany NS_tblCompany { get; set; }

        public virtual NS_tblPartnerCapability NS_tblPartnerCapability { get; set; }
    }
}
