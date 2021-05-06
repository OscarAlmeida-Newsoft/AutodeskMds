namespace Entities.Recommendations
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class NS_tblVariable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short VariableID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public short Type { get; set; }

        public bool? SelectAllFamilies { get; set; }

        public bool? SelectAllProducts { get; set; }

        [StringLength(50)]
        public string Selector { get; set; }

        [StringLength(50)]
        public string Field { get; set; }

        public bool? IsCorporate { get; set; }

        public bool? IsCommercial { get; set; }

        public bool? IsSupported { get; set; }

        public bool? IsMathExpression{ get; set; }

        public string MathExpression { get; set; }

        [StringLength(50)]
        public string CustomerVariable { get; set; }
    }
}
