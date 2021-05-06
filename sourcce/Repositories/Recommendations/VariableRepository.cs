using Entities;
using IRepositories;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System;
using System.Data.Entity;
using Entities.Recommendations;

namespace Repositories
{
    public class VariableRepository : Repository<NS_tblVariable>, IVariableRepository
    {
        public VariableRepository(AffidavitContext context) : base(context)
        {

        }

        public IEnumerable<NS_tblVariable> GetByType(short pType)
        {
            return base.Context.Set<NS_tblVariable>().Where(x =>x.Type == pType);
        }
    }
}
