using ISharedRepositories;
using SharedEntities;
using System.Collections;
using System.Collections.Generic;
using System;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Linq;

namespace SharedRepositories
{
    public class AssessmentMaturityLevelRepository : IAssessmentMaturityLevelRepository
    {
        public IEnumerable<NS_TblMaturityLevel> GetAssessmentsMaturityLevels()
        {
            DatabaseProviderFactory _factory = new DatabaseProviderFactory();
            IRowMapper<NS_TblMaturityLevel> _rowMapper = MapBuilder<NS_TblMaturityLevel>.BuildAllProperties();

            List<NS_TblMaturityLevel> _maturityLevels = null;


            try
            {
                Database _db = _factory.Create("SharedContext");
                _maturityLevels = _db.ExecuteSprocAccessor<NS_TblMaturityLevel>("NS_spGetAssessmentsMaturityLevels", _rowMapper).ToList();

            }
            catch (Exception)
            {

                throw;
            }

            return _maturityLevels;
        }
    }
}
