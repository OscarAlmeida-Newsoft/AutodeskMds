namespace Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class NS_tblIndustry
    {
        public NS_tblIndustry()
        {
            NS_tblCompany = new HashSet<NS_tblCompany>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short IndustryID { get; set; }

        [StringLength(100)]
        public string IndustryName { get; set; }

        [Required]
        [StringLength(100)]
        public string IndustrySpanishName { get; set; }

        public virtual ICollection<NS_tblCompany> NS_tblCompany { get; set; }
    }
}
