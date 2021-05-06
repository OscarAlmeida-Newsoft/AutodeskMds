using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class NS_tblResponseDataType
    {
        public NS_tblResponseDataType()
        {
            this.NS_tblQuestion = new HashSet<NS_tblQuestion>();
        }

        [Key]
        public int ResponseDataTypeID { get; set; }

        [Required]
        [StringLength(50)]
        public string ResponseDataTypeDescription { get; set; }

        public int? ResponseDataTypeLength { get; set; }

        public virtual ICollection<NS_tblQuestion> NS_tblQuestion { get; set; }
    }
}
