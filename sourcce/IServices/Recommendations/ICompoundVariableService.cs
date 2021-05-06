using DTOs;
using System.Collections.Generic;

namespace IServices
{    
    public interface ICompoundVariableService : IBaseService<CompoundVariableDTO>
    {
        ///// <summary>
        ///// Actualiza una variable de la tabla de variables
        ///// </summary>
        ///// <param name="pVariableDTO">Id de la variable que se actualizará</param>
        //int AddAndReturnID(VariableDTO pVariableDTO);

        ///// <summary>
        ///// Actualiza una variable de la tabla de variables
        ///// </summary>
        ///// <param name="pVariableDTO">Id de la variable que se actualizará</param>
        //void UpdateVariable(VariableDTO pVariableDTO);
        IEnumerable<CompoundVariableDTO> GetAllByVariableID(int pVariableID);

        CompoundVariableDTO GetByVariableIDAndAssociatedVariable(int pVariableID, int AssociatedVariableID);
    }
}
