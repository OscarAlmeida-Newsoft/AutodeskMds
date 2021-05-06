namespace Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class NS_tblCompanyContacts
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CompanyID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short CampaignID { get; set; }

        [StringLength(50)]
        public string ContactName { get; set; }

        [StringLength(50)]
        public string CompanyEmail { get; set; }

        [StringLength(50)]
        public string CompanyPhone { get; set; }

        public virtual NS_tblCampaign NS_tblCampaign { get; set; }

        public virtual NS_tblCompany NS_tblCompany { get; set; }
    }
}
