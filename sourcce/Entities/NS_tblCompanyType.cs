namespace Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class NS_tblCompanyType
    {
        public NS_tblCompanyType()
        {
            NS_tblCompany = new HashSet<NS_tblCompany>();
        }

        [Key]
        public byte CompanyTypeID { get; set; }

        [Required]
        [StringLength(50)]
        public string CompanyTypeName { get; set; }

        public virtual ICollection<NS_tblCompany> NS_tblCompany { get; set; }
    }
}
