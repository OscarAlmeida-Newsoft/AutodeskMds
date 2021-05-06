using Entities;
using Entities.Recommendations;
using System.Collections.Generic;

namespace IRepositories
{
    
    public interface IVariableProductFamilyRepository : IRepository<NS_tblVariableProductFamily>
    {
        /// <summary>
        ///     Obtiene la lista de familias para una variable dada
        /// </summary>
        /// <param name="pVariableID">ID de la variable</param>
        /// <returns>Lista de familias para una variable dada</returns>
        IEnumerable<NS_tblVariableProductFamily> GetByVariableID(short pVariableID);

        void AddVariableProductFamily(NS_tblVariableProductFamily pVariableID);
    }
}
