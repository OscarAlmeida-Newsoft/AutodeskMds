using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOMSightModels.DTOs
{
    public class AssessmentRecommendationsDTO
    {
        public int TitleTranslationId { get; set; }
        public List<int> TextTranslationsIds { get; set; }

    }
}
