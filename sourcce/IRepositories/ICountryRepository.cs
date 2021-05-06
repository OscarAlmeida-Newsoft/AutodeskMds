using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface ICountryRepository : IRepository<NS_tblCountry>
    {
        /// <summary>
        ///     Obtiene un registro de la tabla country por su nombre
        /// </summary>
        /// <param name="pName">Nombre de la industria a consultar.</param>
        /// <returns></returns>
        NS_tblCountry GetByName(string pName);

        /// <summary>
        /// Get all countries
        /// </summary>
        /// <returns>List of countries</returns>
        IEnumerable<NS_tblCountry> GetAllCountries();
    }
}
