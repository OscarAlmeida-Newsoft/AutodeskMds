using ISharedRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedEntities;

namespace SharedRepositories
{
    public class EmailQueueRepository : IEmailQueueRepository
    {
        public void EnqueueEmail(EmailQueue pModel)
        {
            throw new NotImplementedException();
        }
    }
}
