using SOMSightModels.DTOs;
using System;
using System.Collections.Generic;

namespace ISOMSightServices
{
    public interface IAssessmentService
    {

        IEnumerable<AssessmentSummaryDTO> GetAssessmentsSummary(Guid pUserId);
        AssessmentSummaryDTO GetAssessmentsSummaryById(int pAssessmentSummaryId);
    }
}
