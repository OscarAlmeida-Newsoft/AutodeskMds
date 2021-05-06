namespace Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class NS_tblLicenseStatusResponse
    {
        public NS_tblLicenseStatusResponse()
        {
            NS_tblCompanyInfo = new HashSet<NS_tblCompanyInfo>();
        }

        [Key]
        public byte LicenseStatusResponseID { get; set; }

        [Required]
        [StringLength(150)]
        public string LicenseStatusResponseDescription { get; set; }

        public virtual ICollection<NS_tblCompanyInfo> NS_tblCompanyInfo { get; set; }
    }
}
