using ISharedRepositories;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SharedEntities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedRepositories
{
    public class AssessmentTypeRepository : IAssessmentTypeRepository
    {

        public IEnumerable<NS_TblAssessmentType> GetAssessmentsType()
        {
            DatabaseProviderFactory _factory = new DatabaseProviderFactory();
            IRowMapper<NS_TblAssessmentType> _rowMapper = MapBuilder<NS_TblAssessmentType>.BuildAllProperties();
            IEnumerable<NS_TblAssessmentType> _data = null;

            try
            {
                Database _db = _factory.Create("SharedContext");
                _data = _db.ExecuteSprocAccessor<NS_TblAssessmentType>("NS_spGetAssessmentType", _rowMapper);
            }
            catch (Exception)
            {

                throw;
            }
           
            return _data;
        }

        public int GetActiveAssessmentsCount()
        {
            DatabaseProviderFactory _factory = new DatabaseProviderFactory();
            int _activeAssessments = 0;

            try
            {
                Database _db = _factory.Create("SharedContext");
                DbCommand _command = _db.GetStoredProcCommand("dbo.NS_spGetActiveAssessmentsCount");

                _activeAssessments = Convert.ToInt32(_db.ExecuteScalar(_command));
                
            }
            catch (Exception)
            {

                throw;
            }

            return _activeAssessments;
        }
    }
}
