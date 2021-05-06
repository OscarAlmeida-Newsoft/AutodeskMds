using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class QuestionDTO
    {
        public int QuestionID { get; set; }

        public int ResponseDataTypeID { get; set; }

        public int? ReletedQuestionID { get; set; }
        
        public string RelatedQuestionIDResponse { get; set; }

        public virtual ResponseDataTypeDTO ResponseDataType { get; set; }
        public virtual IEnumerable<QuestionxLanguageDTO> QuestionsxLanguages { get; set; }
        public virtual IEnumerable<QuestionxProductFamilyDTO> NS_tblQuestionxProductFamily { get; set; }
    }
}
