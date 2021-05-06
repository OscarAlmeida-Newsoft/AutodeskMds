using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.SurveyQuestionResponse
{
    public class SurveyQuestionResponse
    {
        [Key]
        public Guid LeadId { get; set; }

        [Required(ErrorMessage = "Falta respuesta")]
        public Int16 SurveyQuestion1Response { get; set; }

        [StringLength(500)]
        public String SurveyQuestion1OtherResponse { get; set; }

        public Int16 SurveyQuestion2Response { get; set; }

        [StringLength(500)]
        public String SurveyQuestion2OtherResponse { get; set; }

        public Int16 SurveyQuestion3Response { get; set; }

        [StringLength(500)]
        public String SurveyQuestion3OtherResponse { get; set; }

        public Int16 SurveyQuestion4Option1Value { get; set; }
        public Int16 SurveyQuestion4Option2Value { get; set; }
        public Int16 SurveyQuestion4Option3Value { get; set; }
        public Int16 SurveyQuestion4Option4Value { get; set; }

        [StringLength(500)]
        public String SurveyQuestion5Response { get; set; }

        public bool IsNotInterested { get; set; }
    }
}
