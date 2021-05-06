using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affidavit.Models.Question
{
    public class QuestionViewModel
    {
        public int QuestionID { get; set; }

        public int ResponseDataTypeID { get; set; }

        public int? ReletedQuestionID { get; set; }

        public string RelatedQuestionIDResponse { get; set; }
    }
}