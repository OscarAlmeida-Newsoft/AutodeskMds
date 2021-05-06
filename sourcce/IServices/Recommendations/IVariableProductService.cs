using DTOs;
using System.Collections.Generic;

namespace IServices
{
    
    public interface IVariableProductService : IBaseService<VariableProductDTO>
    {
        /// <summary>
        ///     Obtiene la lista de productos para una variable dada
        /// </summary>
        /// <param name="pVariableID">ID de la variable</param>
        /// <returns>Lista de productos para una variable dada</returns>
        IEnumerable<VariableProductDTO> GetByVariableID(short pVariableID);
    }
}
