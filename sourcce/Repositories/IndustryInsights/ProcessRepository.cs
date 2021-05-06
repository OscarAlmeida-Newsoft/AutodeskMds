using Entities;
using IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ProcessRepository : IProcessRepository
    {
        private AffidavitContext dbContext;

        public ProcessRepository(AffidavitContext context)
        {
            dbContext = context;
        }

        public void DeleteProcess(int IdProcess)
        {
            IEnumerable<NS_TblProcessVersions> versions = dbContext.NS_tblProcessVersions.Where(d => d.ProcessId == IdProcess);
            dbContext.NS_tblProcessVersions.RemoveRange(versions);

            NS_TblProcess process = dbContext.NS_tblProcess.SingleOrDefault(d => d.Id == IdProcess);
            dbContext.NS_tblProcess.Remove(process);
            dbContext.SaveChanges();
        }

        public NS_TblProcess GetProcessById(int pId)
        {
            return dbContext.NS_tblProcess.Find(pId);
        }

        public IEnumerable<NS_TblProcess> GetProcessesByActivity(int pActivityId, int pAssessmentSummaryId)
        {
            IEnumerable<NS_TblProcess> _data = null;

            var results = dbContext.NS_tblProcess.Where(x => x.ActivityId == pActivityId && x.AssessmentSummaryId == pAssessmentSummaryId);

            if (results.Count() > 0)
            {
                _data = results;
            }

            return _data;
        }

        public IEnumerable<NS_TblProcess> GetProcessesByAssesmentSummaryId(int pAssessmentSummaryId)
        {
            IEnumerable<NS_TblProcess> _data = null;

            var results = dbContext.NS_tblProcess.Where(x => x.AssessmentSummaryId == pAssessmentSummaryId);

            if (results.Count() > 0)
            {
                _data = results;
            }

            return _data;
        }

        public int InsertProcess(NS_TblProcess newProcess)
        {
            dbContext.NS_tblProcess.Add(newProcess);
            dbContext.SaveChanges();
            int IdProcess = newProcess.Id;

            NS_TblProcessVersions version = new NS_TblProcessVersions();
            version.ProcessId = IdProcess;
            version.AssessmentSummaryId = newProcess.AssessmentSummaryId;
            version.ActivityId = newProcess.ActivityId;
            version.Name = newProcess.Name;
            version.Description = newProcess.Description;
            version.User = newProcess.UserCreation;
            version.Date = newProcess.DateCreation;

            dbContext.NS_tblProcessVersions.Add(version);

            dbContext.SaveChanges();

            return IdProcess;
        }

        public void UpdateProcess(NS_TblProcess process)
        {

            dbContext.NS_tblProcess.Attach(process);
            dbContext.Entry(process).State = System.Data.Entity.EntityState.Modified;

            NS_TblProcessVersions version = new NS_TblProcessVersions();
            version.ProcessId = process.Id;
            version.AssessmentSummaryId = process.AssessmentSummaryId;
            version.ActivityId = process.ActivityId;
            version.Name = process.Name;
            version.Description = process.Description;
            version.User = process.UserLastModification;
            version.Date = process.DateLastModification;

            dbContext.NS_tblProcessVersions.Add(version);

            dbContext.SaveChanges();
        }
    }
}
