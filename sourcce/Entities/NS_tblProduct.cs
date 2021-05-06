namespace Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class NS_tblProduct
    {
        public NS_tblProduct()
        {
            NS_tblProductCompany = new HashSet<NS_tblProductCompany>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short ProductID { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductName { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductNameDisplay { get; set; }

        [Required]
        [StringLength(15)]
        public string ProductVersion { get; set; }

        [Required]
        [StringLength(15)]
        public string ProductVersionGroup { get; set; }

        public short? OrderVersion { get; set; }

        public bool OEMFlag { get; set; }

        public bool IsActive { get; set; }

        public byte ProductFamilyID { get; set; }

        public virtual ICollection<NS_tblProductCompany> NS_tblProductCompany { get; set; }

        public virtual NS_tblProductFamily ProductFamily { get; set; }

        public bool IsCal { get; set; }

        public bool IsCorporate { get; set; }

        public bool IsCommercial { get; set; }

        public bool IsSupported { get; set; }

        public short? DisplayOrder { get; set; }
    }
}
