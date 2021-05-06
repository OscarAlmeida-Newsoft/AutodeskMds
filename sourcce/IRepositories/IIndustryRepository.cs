using Entities;

namespace IRepositories
{
    public interface IIndustryRepository : IRepository<NS_tblIndustry>
    {
        /// <summary>
        ///     Obtiene un registro de la tabla industry por su nombre
        /// </summary>
        /// <param name="pName">Nombre de la industria a consultar.</param>
        /// <returns></returns>
        NS_tblIndustry GetByName(string pName);
    }
}
