namespace Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class NS_tblPartnerCapability
    {
        public NS_tblPartnerCapability()
        {
            NS_tblPartnerCapabilityCompany = new HashSet<NS_tblPartnerCapabilityCompany>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short PartnerCapabilityID { get; set; }

        [Required]
        [StringLength(50)]
        public string PartnerCapabilityName { get; set; }

        public virtual ICollection<NS_tblPartnerCapabilityCompany> NS_tblPartnerCapabilityCompany { get; set; }
    }
}
