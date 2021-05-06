using Affidavit.Attribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Affidavit.Models.Question
{
    public class AssignProductToQuestionViewModel
    {
        [TranslatorDisplay("Old_LabelProductFamily")]
        public byte ProductFamilyID { get; set; }
        public int QuestionID { get; set; }
        public int DisplayOrder { get; set; }

        public SelectList ProductFamilyList { get; set; }
    }
}