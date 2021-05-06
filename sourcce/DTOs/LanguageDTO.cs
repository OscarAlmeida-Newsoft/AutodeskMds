using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class LanguageDTO
    {
        
        public int LanguageID { get; set; }
               
        public string LanguageName { get; set; }

        public virtual ICollection<QuestionxLanguageDTO> QuestionsxLanguages { get; set; }
    }
}
