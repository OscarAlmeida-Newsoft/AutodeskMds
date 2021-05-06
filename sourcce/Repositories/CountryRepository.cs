using Entities;
using IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CountryRepository : Repository<NS_tblCountry>, ICountryRepository
    {
        public CountryRepository(AffidavitContext context) : base(context)
        {

        }

        /// <summary>
        ///     Obtiene un registro de la tabla country por su nombre
        /// </summary>
        /// <param name="pName">Nombre de la industria a consultar.</param>
        /// <returns></returns>
        public NS_tblCountry GetByName(string pName)
        {
            return base.Context.Set<NS_tblCountry>().Where(x => x.CountryName == pName).FirstOrDefault();
        }

        /// <summary>
        /// Get all countries
        /// </summary>
        /// <returns>List of countries</returns>
        public IEnumerable<NS_tblCountry> GetAllCountries()
        {
            return base.GetAll();
        }
    }
}
