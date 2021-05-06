using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affidavit.Models
{
    public class SurveyQuestionResponseVM
    {
        public Guid LeadId { get; set; }

        public Int16 SurveyQuestion1Response { get; set; }
        public String SurveyQuestion1OtherResponse { get; set; }

        public Int16 SurveyQuestion2Response { get; set; }
        public String SurveyQuestion2OtherResponse { get; set; }

        public Int16 SurveyQuestion3Response { get; set; }
        public String SurveyQuestion3OtherResponse { get; set; }

        public Int16 SurveyQuestion4Option1Value { get; set; }
        public Int16 SurveyQuestion4Option2Value { get; set; }
        public Int16 SurveyQuestion4Option3Value { get; set; }
        public Int16 SurveyQuestion4Option4Value { get; set; }

        public String SurveyQuestion5Response { get; set; }

        public bool IsNotInterested { get; set; }
    }
}