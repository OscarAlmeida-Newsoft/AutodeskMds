
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedEntities
{
    public class NS_TblRecommendation
    {
        public int Id { get; set; }
        public int AssessmentTypeId { get; set; }
        public int AssessmentTypeTranslationAdministrationId { get; set; }
        public string MaturityLevelId { get; set; }
        public int TranslatorAdministratorNameId { get; set; }
        public int TranslatorAdministratorTextId { get; set; }
        public int AssessmentQuestionId { get; set; }

    }
}
