using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class NS_tblLanguage
    {
        public NS_tblLanguage()
        {
            this.QuestionsxLanguages = new HashSet<NS_tblQuestionxLanguage>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LanguageID { get; set; }

        [Required]
        [StringLength(50)]
        public string LanguageName { get; set; }

        public virtual ICollection<NS_tblQuestionxLanguage> QuestionsxLanguages { get; set; }
    }
}
