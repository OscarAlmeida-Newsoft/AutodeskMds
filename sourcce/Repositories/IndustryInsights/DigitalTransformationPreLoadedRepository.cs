using Entities;
using IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class DigitalTransformationPreLoadedRepository : IDigitalTransformationPreLoadedRepository
    {
        private AffidavitContext dbContext;

        public DigitalTransformationPreLoadedRepository(AffidavitContext context)
        {
            dbContext = context;
        }

        public IEnumerable<NS_TblDigitalTransformationPreLoaded> GetAllDigitalTransformationPreLoadedByProcess(int ProcessPreLoadedId)
        {
            IEnumerable<NS_TblDigitalTransformationPreLoaded> data = dbContext.NS_tblDigitalTransformationPreloaded.Where(d => d.ProcessPreLoadedId == ProcessPreLoadedId);

            return data;
        }
    }
}
