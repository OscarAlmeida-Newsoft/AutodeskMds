using Entities;
using IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ProcessPreLoadedRepository : IProcessPreLoadedRepository
    {
        private AffidavitContext dbContext;

        public ProcessPreLoadedRepository(AffidavitContext context)
        {
            dbContext = context;
        }

        public IEnumerable<NS_TblProcessPreLoaded> GetAllProccessPreLoadedByIndustry(int IdIndustry)
        {
            IEnumerable<NS_TblProcessPreLoaded> data = dbContext.NS_tblProcessPreloaded.Where(d => d.IndustryId == IdIndustry);

            return data;
        }
    }
}
