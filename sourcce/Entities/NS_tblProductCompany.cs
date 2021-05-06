namespace Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class NS_tblProductCompany
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CompanyID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short ProductID { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short CampaignID { get; set; }

        public short? InstalledLicenses { get; set; }

        public short? VLS { get; set; }

        public short? FPP { get; set; }

        public short? OEM { get; set; }

        public short? WEB { get; set; }

        [StringLength(15)]
        public string Agreement { get; set; }

        public virtual NS_tblCampaign NS_tblCampaign { get; set; }

        public virtual NS_tblCompany NS_tblCompany { get; set; }

        public virtual NS_tblProduct NS_tblProduct { get; set; }
    }
}
