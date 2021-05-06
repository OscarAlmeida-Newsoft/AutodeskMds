using DTOs;
using Entities;
using IRepositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class AssessmentSummaryRepository : Repository<NS_TblAssessmentSummary>,IAssessmentSummaryRepository
    {
        private AffidavitContext dbContext;

        public AssessmentSummaryRepository(AffidavitContext context) : base(context)
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

        public bool CheckAssessmentsSummary(Guid pLeadID, Guid pCampaignID, IEnumerable<NS_TblAssessmentType> pAssessmentTypes)
        {
            try
            {
                //List of AssessmentsType
                IEnumerable<NS_TblAssessmentType> _assessmentsType = pAssessmentTypes; //base.Context.Set<NS_TblAssessmentType>().Where(x => x.Status == true);




                //List of lead and campaing assessments
                IEnumerable<NS_TblAssessmentSummary> _assessmentsLead = base.Context.Set<NS_TblAssessmentSummary>()
                    .Where(x => x.CompanyId == pLeadID && x.CampaignId == pCampaignID);


                var assessmentsToAdd = _assessmentsType.Where(x => !_assessmentsLead.Select(y => y.AssessmentTypeId).Contains(x.Id)).Select(z=>z.Id);


                foreach (int assessment in assessmentsToAdd)
                {
                    base.Context.Set<NS_TblAssessmentSummary>().Add(
                        new NS_TblAssessmentSummary {
                            CompanyId = pLeadID,
                            CampaignId = pCampaignID,
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

        public IEnumerable<NS_TblAssessmentSummary> GetAssessmentsSummary(Guid pLeadID, Guid pCampaignID)
        {

            IEnumerable<NS_TblAssessmentSummary> _data =base.Context.Set<NS_TblAssessmentSummary>()
                .Where(x => x.CompanyId == pLeadID && x.CampaignId == pCampaignID).AsEnumerable();

            return _data;
            
            
            
        }

        public NS_TblAssessmentSummary GetAssessmentsSummaryById(int pAssessmentSummaryId)
        {
            return base.Context.Set<NS_TblAssessmentSummary>().Find(pAssessmentSummaryId);
        }

        public void MarkAssessmentSummaryAsFinished(Guid pLeadId, Guid pCampaignId, int pAssessmentSummaryId, string pGlobalMaturityLevel)
        {
            DateTime _currentDate = DateTime.UtcNow;

            NS_TblAssessmentSummary _summary = base.Context.Set<NS_TblAssessmentSummary>().Find(pAssessmentSummaryId);

            _summary.ResponseEndDate = _currentDate;
            _summary.GlobalMaturityLevelId = pGlobalMaturityLevel;
        }

        public void MarkAssessmentSummaryAsDraft(Guid pLeadId, Guid pCampaignId, int pAssessmentSummaryId)
        {
            DateTime _currentDate = DateTime.UtcNow;

            base.Context.Set<NS_TblAssessmentSummary>().Find(pAssessmentSummaryId).ResponseStartDate = _currentDate;
        }

        public string GetAssessmentGlobalMaturityLevel(int pAssessmentTypeId, Guid pCompanyId, Guid pCampaignId)
        {
            var _parameters = new SqlParameter[] {
                new SqlParameter { ParameterName = "@AssessmentTypeId", Value = pAssessmentTypeId, Direction = ParameterDirection.Input, DbType = DbType.Int32 },
                new SqlParameter { ParameterName = "@CampaignId", Value = pCampaignId, Direction = ParameterDirection.Input, DbType = DbType.Guid },
                new SqlParameter { ParameterName = "@CompanyId", Value = pCompanyId, Direction = ParameterDirection.Input, DbType = DbType.Guid }
            };

            string _results = dbContext.Database.SqlQuery<string>("[dbo].[NS_spCalculateAssesstmenGlobalMaturityLevel] @AssessmentTypeId, @CampaignId, @CompanyId", _parameters).FirstOrDefault();

            return _results;
        }

        public int GetFinishedAssessmentCount(Guid pCompanyId, Guid pCampaignId)
        {
            int _count = dbContext.Set<NS_TblAssessmentSummary>().Count(x => x.ResponseEndDate != null && x.CompanyId == pCompanyId && x.CampaignId == pCampaignId);

            return _count;

        }

        public void SaveNextSteps(int pAssessmentSummaryId, string pNextSteps)
        {
            var _assessment = dbContext.NS_AssessmentSummary.Find(pAssessmentSummaryId);

            _assessment.NextSteps = pNextSteps;
        }

        public void UnlockAssessment(int pAssessmentSummaryId)
        {
            var _assessment = dbContext.NS_AssessmentSummary.Find(pAssessmentSummaryId);

            _assessment.ResponseEndDate = null;
        }
    }
}
