using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface IIndustryService : IBaseService<IndustryDTO>
    {
        /// <summary>
        ///     Obtiene un registro de la tabla industry por su nombre
        /// </summary>
        /// <param name="pName">Nombre de la industria a consultar.</param>
        /// <returns></returns>
        IndustryDTO GetByName(string pName);
    }
}
