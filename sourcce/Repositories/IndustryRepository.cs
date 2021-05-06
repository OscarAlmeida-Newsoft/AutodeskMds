using Entities;
using IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class IndustryRepository : Repository<NS_tblIndustry>, IIndustryRepository
    {
        public IndustryRepository(AffidavitContext context) : base(context)
        {

        }

        /// <summary>
        ///     Obtiene un registro de la tabla industry por su nombre
        /// </summary>
        /// <param name="pName">Nombre de la industria a consultar.</param>
        /// <returns></returns>
        public NS_tblIndustry GetByName(string pName)
        {
            return base.Context.Set<NS_tblIndustry>().Where(x => x.IndustryName == pName).FirstOrDefault();
        }
    }
}
