using Entities.NS_tblEmailQueue;
using IRepositories.EmailQueue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EmailQueue
{
    public class EmailQueueRepository : Repository<NS_tblEmailQueue>, IEmailQueueRepository
    {
        public EmailQueueRepository(AffidavitContext context) : base(context)
        {

        }
    }
}
