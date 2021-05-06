using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class NS_tblQuestion
    {
        public NS_tblQuestion()
        {
            this.QuestionsxLanguages = new HashSet<NS_tblQuestionxLanguage>();
            this.NS_tblQuestionxProductFamily = new HashSet<NS_tblQuestionxProductFamily>();
            this.NS_tblAnswerCompany = new HashSet<NS_tblAnswerCompany>();
        }

        [Key]
        public int QuestionID { get; set; }

        [Required]
        public int ResponseDataTypeID { get; set; }
        
        public int? ReletedQuestionID { get; set; }

        [StringLength(50)]
        public string RelatedQuestionIDResponse { get; set; }

        public virtual NS_tblResponseDataType ResponseDataType { get; set; }
        public virtual ICollection<NS_tblAnswerCompany> NS_tblAnswerCompany { get; set; }
        public virtual ICollection<NS_tblQuestionxLanguage> QuestionsxLanguages { get; set; }
        public virtual ICollection<NS_tblQuestionxProductFamily> NS_tblQuestionxProductFamily { get; set; }
    }
}
