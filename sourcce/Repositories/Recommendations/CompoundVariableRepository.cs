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
    public class CompoundVariableRepository : Repository<NS_tblCompoundVariable>, ICompoundVariableRepository
    {
        public CompoundVariableRepository(AffidavitContext context) : base(context)
        {

        }

        public IEnumerable<NS_tblCompoundVariable> GetByVariableID(int pVariableID)
        {
            return Context.Set<NS_tblCompoundVariable>().Where(x => x.VariableID == pVariableID);
        }

        public NS_tblCompoundVariable GetByVariableIDAndAssociatedVariable(int pVariableID, int pAssociatedVariableID)
        {
            return Context.Set<NS_tblCompoundVariable>().Where(x => x.VariableID == pVariableID && x.AassociateVariableID == pAssociatedVariableID).FirstOrDefault() ;
        }
    }
}
