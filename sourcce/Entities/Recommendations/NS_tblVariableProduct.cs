namespace Entities.Recommendations
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class NS_tblVariableProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short VariableProductID { get; set; }
        public short VariableID { get; set; }
        public short ProductID { get; set; }

        public virtual NS_tblVariable Variable { get; set; }
        public virtual NS_tblProduct Product { get; set; }
    }
}
