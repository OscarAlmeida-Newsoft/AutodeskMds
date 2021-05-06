using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Affidavit.Models.Question
{
    public class QuestionxLanguageViewModel
    {
        public int QuestionID { get; set; }

        public int LanguageID { get; set; }
        [Required]
        public string QuestionText { get; set; }
    }
}