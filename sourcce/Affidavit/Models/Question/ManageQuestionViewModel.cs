using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affidavit.Models.Question
{
    public class ManageQuestionViewModel
    {
        public CreateNewQuestionViewModel CreateNewQuestion { get; set; }
        public List<QuestionListViewModel> QuestionList { get; set; }
    }
}