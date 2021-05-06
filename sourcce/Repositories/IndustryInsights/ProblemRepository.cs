using Entities;
using IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ProblemRepository : IProblemRepository
    {
        private AffidavitContext dbContext;

        public ProblemRepository(AffidavitContext context)
        {
            dbContext = context;
        }

        public void DeleteProblem(int IdProblem)
        {
            IEnumerable<NS_TblProblemVersions> versions = dbContext.NS_tblProblemVersions.Where(d => d.ProblemId == IdProblem);
            dbContext.NS_tblProblemVersions.RemoveRange(versions);

            NS_TblProblem problem = dbContext.NS_tblProblem.SingleOrDefault(d => d.Id == IdProblem);
            dbContext.NS_tblProblem.Remove(problem);
            dbContext.SaveChanges();


        }

        public void DeleteProblemsByIdProcess(int IdProcess)
        {
            IEnumerable<NS_TblProblem> problems = GetAllProblemByProcess(IdProcess);

            foreach (NS_TblProblem pr in problems)
            {
                IEnumerable<NS_TblProblemVersions> versions = dbContext.NS_tblProblemVersions.Where(d => d.ProblemId == pr.Id);
                dbContext.NS_tblProblemVersions.RemoveRange(versions);
            }

            dbContext.NS_tblProblem.RemoveRange(problems);
            dbContext.SaveChanges();
        }

        public IEnumerable<NS_TblProblem> GetAllProblemByProcess(int ProcessId)
        {
            IEnumerable<NS_TblProblem> data = dbContext.NS_tblProblem.Where(d => d.ProcessId == ProcessId);

            return data;
        }

        public NS_TblProblem GetProblemById(int IdProblem)
        {
            return dbContext.NS_tblProblem.SingleOrDefault(d => d.Id == IdProblem);
        }

        public void InsertProblem(NS_TblProblem newProblem)
        {
            dbContext.NS_tblProblem.Add(newProblem);
            dbContext.SaveChanges();

            NS_TblProblemVersions version = new NS_TblProblemVersions();
            version.ProblemId = newProblem.Id;
            version.ProcessId = newProblem.ProcessId;
            version.ProblemDescription = newProblem.ProblemDescription;
            version.Opportunity = newProblem.Opportunity;
            version.Technology = newProblem.Technology;
            version.Inventory = newProblem.Inventory;
            version.Priority = newProblem.Priority;
            version.Exist = newProblem.Exist;
            version.Microsoft = newProblem.Microsoft;
            version.User = newProblem.UserCreation;
            version.Date = newProblem.DateCreation;
            version.Solution = newProblem.Solution;
            version.Solved = newProblem.Solved;

            dbContext.NS_tblProblemVersions.Add(version);
            dbContext.SaveChanges();


        }

        public void UpdateProblem(NS_TblProblem problem)
        {
            dbContext.NS_tblProblem.Attach(problem);
            dbContext.Entry(problem).State = System.Data.Entity.EntityState.Modified;
            dbContext.SaveChanges();

            NS_TblProblemVersions version = new NS_TblProblemVersions();
            version.ProblemId = problem.Id;
            version.ProcessId = problem.ProcessId;
            version.ProblemDescription = problem.ProblemDescription;
            version.Opportunity = problem.Opportunity;
            version.Technology = problem.Technology;
            version.Inventory = problem.Inventory;
            version.Priority = problem.Priority;
            version.Exist = problem.Exist;
            version.Microsoft = problem.Microsoft;
            version.User = problem.UserLastModification;
            version.Date = problem.DateLastModification;
            version.Solution = problem.Solution;
            version.Solved = problem.Solved;

            dbContext.NS_tblProblemVersions.Add(version);
            dbContext.SaveChanges();
        }
    }
}
