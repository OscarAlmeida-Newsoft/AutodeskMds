using Affidavit.Attribute;
using DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Affidavit.Models.Question
{
    public enum IsActiveFlag
    {
        False = 0,
        True = 1
    }

    public class CreateNewQuestionViewModel
    {
        public List<string> LanguagesQuestionsText { get; set; }

        [TranslatorDisplay("Old_LabelIdioma")]
        public int LanguageID { get; set; }
        public List<LanguageViewModel> Languages { get; set; }
        public List<QuestionxLanguageViewModel> QuestionsxLanguages { get; set; }

        public List<int> LanguagesID { get; set; }
        
        [TranslatorDisplay("Old_LabelResponseDataType")]
        public int ResponseDataTypeID { get; set; }
        public SelectList ResponseDataTypeList { get; set; }

        public List<SelectListItem> ResponseDataTypeListItem { get; set; }

        [TranslatorDisplay("Old_LabelRelatedQuestion")]
        public int? RelatedQuestionID { get; set; }

        [TranslatorDisplay("Old_LabelRelatedQuestion")]
        public int QuestionID { get; set; }

        public int CurrentQuestionID { get; set; }
        public SelectList RelatedQuestionList { get; set; }

        public int RelatedQuestionResponseID { get; set; }
        public SelectList RelatedQuestionResponseList { get; set; }

        [Display(Name ="Related Question Response")]
        public string RelatedQuestionIDResponse { get; set; }
        
        [TranslatorDisplay("Old_LabelProductFamily")]
        public int ProductFamilyID { get; set; }
        public SelectList ProductFamilyList { get; set; }
        
        [TranslatorDisplay("Old_LabelOrder")]
        public int Order { get; set; }

        public int LanguagaID { get; set; }

        //[Display(Name = "Active")]
        //public int IsActive { get; set; }
        [Display(Name = "Active")]
        public IsActiveFlag ActiveFlag { get; set; }

        public bool ExisteID { get; set; }
    }
}