using DTOs;
using System.Collections.Generic;

namespace IServices
{
    
    public interface IVariableProductFamilyService : IBaseService<VariableProductFamilyDTO>
    {
        /// <summary>
        ///     Obtiene la lista de familias para una variable dada
        /// </summary>
        /// <param name="pVariableID">ID de la variable</param>
        /// <returns>Lista de familias para una variable dada</returns>
        IEnumerable<VariableProductFamilyDTO> GetByVariableID(short pVariableID);
    }
}
