using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class NS_tblQuestionxProductFamily
    {
        [Key, Column(Order = 0)]
        public byte ProductFamilyID { get; set; }

        [Key, Column(Order = 1)]
        public int QuestionID { get; set; }

        [Required]
        public int DisplayOrder { get; set; }

        public int IsActive { get; set; }

        public virtual NS_tblQuestion Question { get; set; }
        public virtual NS_tblProductFamily ProductFamily { get; set; }
    }
}
