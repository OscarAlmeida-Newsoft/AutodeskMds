using Entities;
using Entities.Recommendations;
using System.Collections.Generic;

namespace IRepositories
{
    public interface IVariableRepository : IRepository<NS_tblVariable>
    {
        /// <summary>
        ///     Obtiene la lista de variables por su tipo
        /// </summary>
        /// <param name="pType">Tipo de la variable</param>
        /// <returns>Lista de variables del tipo que se pide</returns>
        IEnumerable<NS_tblVariable> GetByType(short pType);
    }
}
