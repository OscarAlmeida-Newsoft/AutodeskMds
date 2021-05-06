using ISharedRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedEntities;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace SharedRepositories
{
    public class AssessmentRecommendations : IAssessmentRecommendations
    {
        public IEnumerable<NS_TblRecommendation> GetAssessmentRecommendations()
        {
            DatabaseProviderFactory _factory = new DatabaseProviderFactory();
            IRowMapper<NS_TblRecommendation> _rowMapper = MapBuilder<NS_TblRecommendation>.BuildAllProperties();
            IEnumerable<NS_TblRecommendation> _data = null;

            try
            {
                Database _db = _factory.Create("SharedContext");
                _data = _db.ExecuteSprocAccessor<NS_TblRecommendation>("NS_spGetRecommendations", _rowMapper);
            }
            catch (Exception)
            {

                throw;
            }

            return _data;
        }
    }
}
