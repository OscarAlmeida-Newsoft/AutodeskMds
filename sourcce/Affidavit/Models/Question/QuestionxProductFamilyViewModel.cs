using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affidavit.Models.Question
{
    public class QuestionxProductFamilyViewModel
    {
        public byte ProductFamilyID { get; set; }

        public int QuestionID { get; set; }

        public int DisplayOrder { get; set; }

        public int IsActive { get; set; }
    }
}