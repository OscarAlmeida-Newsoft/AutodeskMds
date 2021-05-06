namespace Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class NS_tblPartnerProgram
    {
        public NS_tblPartnerProgram()
        {
            NS_tblPartnerProgramCompany = new HashSet<NS_tblPartnerProgramCompany>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short PartnerProgramID { get; set; }

        [Required]
        [StringLength(50)]
        public string PartnerProgramName { get; set; }

        public virtual ICollection<NS_tblPartnerProgramCompany> NS_tblPartnerProgramCompany { get; set; }
    }
}
