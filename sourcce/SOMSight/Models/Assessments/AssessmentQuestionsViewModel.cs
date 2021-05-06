using System;
using System.Collections.Generic;

namespace SOMSight.Models
{
    public class AssessmentQuestionsViewModel
    {
        public int Id { get; set; }
        public int TranslatorAdministratorId { get; set; }
        public int DisplayOrder { get; set; }
        public string SelectedMaturityLevelId { get; set; }
        public Guid UserId { get; set; }

        public IEnumerable<AssessmentQuestionsOptionsViewModel> Options { get; set; }
    }
}