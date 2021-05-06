using SharedEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISharedRepositories
{
    public interface IAssessmentRecommendations
    {
        IEnumerable<NS_TblRecommendation> GetAssessmentRecommendations();
    }
}
