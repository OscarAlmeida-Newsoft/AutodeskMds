using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affidavit.Models
{
    public class AssessmentQuestionsHeaderViewModel
    {
        public int LanguageId { get; set; }
        public IEnumerable<AssessmentQuestionsViewModel> AssessmentQuestions { get; set; }
    }
}