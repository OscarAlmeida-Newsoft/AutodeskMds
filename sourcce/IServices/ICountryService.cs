using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface ICountryService : IBaseService<CountryDTO>
    {
        /// <summary>
        ///     Obtiene un registro de la tabla country por su nombre
        /// </summary>
        /// <param name="pName">Nombre de la industria a consultar.</param>
        /// <returns></returns>
        CountryDTO GetByName(string pName);

        /// <summary>
        /// Get all countries
        /// </summary>
        /// <returns>List of countries</returns>
        IEnumerable<CountryDTO> GetAllCountries();
    }
}
