using Entities;
using IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private AffidavitContext dbContext;

        public ActivityRepository(AffidavitContext pDbContext)
        {
            dbContext = pDbContext;
        }

        public NS_TblActivity GetActivityById(int pActivityId)
        {
            return dbContext.NS_tblTActivity.Find(pActivityId);
        }

        public IEnumerable<NS_TblActivity> GetAllActivities()
        {
            return dbContext.NS_tblTActivity.OrderBy(x => x.Order);
        }
    }
}
