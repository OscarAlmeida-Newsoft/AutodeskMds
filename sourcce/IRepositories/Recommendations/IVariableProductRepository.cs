using Entities;
using Entities.Recommendations;
using System.Collections.Generic;

namespace IRepositories
{
    
    public interface IVariableProductRepository : IRepository<NS_tblVariableProduct>
    {
        /// <summary>
        ///     Obtiene la lista de productos para una variable dada
        /// </summary>
        /// <param name="pVariableID">ID de la variable</param>
        /// <returns>Lista de productos para una variable dada</returns>
        IEnumerable<NS_tblVariableProduct> GetByVariableID(short pVariableID);

        void AddVariableProduct(NS_tblVariableProduct pVariableID);
    }
}
