using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affidavit.Models.Question
{
    public class QuestionListViewModel
    {
        public int QuestionID { get; set; }
        public string QuestionText { get; set; }
        //public int LanguageID { get; set; }
        public int ResponseDataTypeID { get; set; }
        public string ResponseDataTypeDescription { get; set; }
        public int? ReletedQuestionID { get; set; }
        public string RelatedQuestionIDResponse { get; set; }
        public int ProductFamilyID { get; set; }
        public string ProductFamilyName { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
    }
}