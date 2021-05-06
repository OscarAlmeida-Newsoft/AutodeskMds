using Entities;
using Entities.Recommendations;
using System.Collections.Generic;

namespace IRepositories
{
    public interface ICompoundVariableRepository : IRepository<NS_tblCompoundVariable>
    {
        NS_tblCompoundVariable GetByVariableIDAndAssociatedVariable(int pVariableID, int AssociatedVariableID);
        IEnumerable<NS_tblCompoundVariable> GetByVariableID(int pVariableID);
    }
}
