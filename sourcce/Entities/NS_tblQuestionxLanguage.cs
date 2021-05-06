using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class NS_tblQuestionxLanguage
    {
        [Key, Column(Order = 0)]
        public int QuestionID { get; set; }

        [Key, Column(Order = 1)]
        public int LanguageID { get; set; }

        [StringLength(500)]
        public string QuestionText { get; set; }       

        public virtual NS_tblLanguage Language { get; set; }
        public virtual NS_tblQuestion Question { get; set; }
    }
}
