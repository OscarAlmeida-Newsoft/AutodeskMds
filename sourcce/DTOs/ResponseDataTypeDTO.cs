using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class ResponseDataTypeDTO
    {
        public int ResponseDataTypeID { get; set; }
        
        public string ResponseDataTypeDescription { get; set; }

        public int? ResponseDataTypeLength { get; set; }

        //public IEnumerable<QuestionDTO> NS_tblQuestion { get; set; }
    }
}
