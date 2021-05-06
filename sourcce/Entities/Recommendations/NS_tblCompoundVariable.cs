using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Recommendations
{
    public partial class NS_tblCompoundVariable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short CompoundVariableExpressionID { get; set; }

        
        public short? VariableID { get; set; }
        public short? Order { get; set; }
        public string MathOperator { get; set; }
        public double? StaticValue { get; set; }
        public bool InitialBrackets { get; set; }
        public bool FinalBrackets { get; set; }

        
        public short? AassociateVariableID { get; set; }

        [ForeignKey("VariableID")]
        public virtual NS_tblVariable Variable { get; set; }
        [ForeignKey("AassociateVariableID")]
        public virtual NS_tblVariable AassociateVariable { get; set; }
    }
}
