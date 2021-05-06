using Entities;
using IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
   public class PartnerCapabilityRepository : Repository<NS_tblPartnerCapability>, IPartnerCapabilityRepository
    {
        public PartnerCapabilityRepository(AffidavitContext context) : base(context)
        {
        }
    }
}
