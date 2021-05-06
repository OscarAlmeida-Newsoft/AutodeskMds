using ISOMSightRepositories;
using SOMSightModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOMSightRepositories
{
    public class AssessmentSummaryRepository : IAssessmentSummaryRepository
    {
        private SOMSightContext dbContext;

        public AssessmentSummaryRepository(SOMSightContext context)
        {
            dbContext = context;
        }

        private string _message;
        public string Message
        {
            get
            {
                return _message;
            }
        }

        public bool CheckAssessmentsSummary(Guid pUSerId, IEnumerable<NS_TblAssessmentType> pAssessmentTypes)
        {
            try
            {
                //List of AssessmentsType
                IEnumerable<NS_TblAssessmentType> _assessmentsType = pAssessmentTypes; //base.Context.Set<NS_TblAssessmentType>().Where(x => x.Status == true);




                //List of lead and campaing assessments
                IEnumerable<NS_TblAssessmentSummary> _assessmentsLead =   dbContext.Set<NS_TblAssessmentSummary>()
                    .Where(x => x.UserId == pUSerId);


                var assessmentsToAdd = _assessmentsType.Where(x => !_assessmentsLead.Select(y => y.AssessmentTypeId).Contains(x.Id)).Select(z=>z.Id);


                foreach (int assessment in assessmentsToAdd)
                {
                    dbContext.Set<NS_TblAssessmentSummary>().Add(
                        new NS_TblAssessmentSummary {
                            UserId = pUSerId,
                            AssessmentTypeId = assessment,
                            CreatedDate = DateTime.UtcNow
                        });
                }

                return true;
            }
            catch (Exception ex)
            {
                _message = ex.Message;
                return false;
            }
           
        }

        public IEnumerable<NS_TblAssessmentSummary> GetAssessmentsSummary(Guid pUserId)
        {

            IEnumerable<NS_TblAssessmentSummary> _data = dbContext.Set<NS_TblAssessmentSummary>()
                .Where(x => x.UserId == pUserId).AsEnumerable();

            return _data;
            
            
            
        }

        public NS_TblAssessmentSummary GetAssessmentsSummaryById(int pAssessmentSummaryId)
        {
            return dbContext.Set<NS_TblAssessmentSummary>().Find(pAssessmentSummaryId);
        }

        public void MarkAssessmentSummaryAsFinished(Guid pUserId, int pAssessmentSummaryId, string pGlobalMaturityLevel)
        {
            DateTime _currentDate = DateTime.UtcNow;

            NS_TblAssessmentSummary _summary = dbContext.Set<NS_TblAssessmentSummary>().Find(pAssessmentSummaryId);

            _summary.ResponseEndDate = _currentDate;
            _summary.GlobalMaturityLevelId = pGlobalMaturityLevel;
        }

        public void MarkAssessmentSummaryAsDraft(Guid pUserId, int pAssessmentSummaryId)
        {
            DateTime _currentDate = DateTime.UtcNow;

            dbContext.Set<NS_TblAssessmentSummary>().Find(pAssessmentSummaryId).ResponseStartDate = _currentDate;
        }

        public string GetAssessmentGlobalMaturityLevel(int pAssessmentTypeId, Guid pUserId)
        {
            var _parameters = new SqlParameter[] {
                new SqlParameter { ParameterName = "@AssessmentTypeId", Value = pAssessmentTypeId, Direction = ParameterDirection.Input, DbType = DbType.Int32 },
                new SqlParameter { ParameterName = "@UserId", Value = pUserId, Direction = ParameterDirection.Input, DbType = DbType.Guid },
            };

            string _results = dbContext.Database.SqlQuery<string>("[dbo].[NS_spCalculateAssesstmenGlobalMaturityLevel] @AssessmentTypeId, @UserId", _parameters).FirstOrDefault();

            return _results;
        }
    }
}
