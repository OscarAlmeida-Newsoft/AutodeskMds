using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class QuestionxProductFamilyDTO
    {
        public byte ProductFamilyID { get; set; }
        
        public int QuestionID { get; set; }
        
        public int DisplayOrder { get; set; }

        public int IsActive { get; set; }

        //public QuestionDTO Question { get; set; }
        //public ProductFamilyDTO ProductFamily { get; set; }
    }
}
