using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DTOs;
namespace Affidavit
{
    public class AssessmentRecommendationViewModel
    {
        public int AssessmentTypeTranslationAdministrationId { get; set; }
        public string IconName { get; set; }
        public int? GlobalMaturityLevelTranslatorId { get; set; }
        public int GlobalMaturityLevelId { get; set; }
        public AssessmentRecommendationsDTO Recomendations { get; set; }
    }
}