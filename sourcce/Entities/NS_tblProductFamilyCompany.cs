namespace Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class NS_tblProductFamilyCompany
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CompanyID { get; set; }

        [Key]
        [Column(Order = 1)]
        public byte ProductFamilyID { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short CampaignID { get; set; }

        [StringLength(500)]
        public string AdditionalComments { get; set; }

        public virtual NS_tblCampaign NS_tblCampaign { get; set; }

        public virtual NS_tblCompany NS_tblCompany { get; set; }

        public virtual NS_tblProductFamily NS_tblProductFamily { get; set; }
    }
}
