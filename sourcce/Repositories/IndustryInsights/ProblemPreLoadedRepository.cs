using Entities;
using IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ProblemPreLoadedRepository : IProblemPreLoadedRepository
    {
        private AffidavitContext dbContext;

        public ProblemPreLoadedRepository(AffidavitContext context)
        {
            dbContext = context;
        }

        public IEnumerable<NS_TblProblemPreLoaded> GetAllProblemPreLoadedByProcess(int ProcessPreLoadedId)
        {
            IEnumerable<NS_TblProblemPreLoaded> data = dbContext.NS_tblProblemPreloaded.Where(d => d.ProcessPreLoadedId == ProcessPreLoadedId);

            return data;
        }
    }
}
