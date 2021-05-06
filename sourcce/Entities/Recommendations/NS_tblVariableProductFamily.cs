namespace Entities.Recommendations
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class NS_tblVariableProductFamily
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short VariableProductFamilyID { get; set; }
        public short VariableID { get; set; }
        public byte ProductFamilyID { get; set; }

        public virtual NS_tblVariable Variable { get; set; }
        public virtual NS_tblProductFamily ProductFamily { get; set; }
    }
}
