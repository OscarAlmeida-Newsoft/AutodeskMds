using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface ICompanyService : IBaseService<CompanyDTO>
    {
        /// <summary>
        /// Obtiene una compañia por el account number
        /// </summary>
        /// <param name="pAccountNumberID">account number id</param>
        /// <returns></returns>
        CompanyDTO GetCompanyByAccountNumber(string pAccountNumberID);

        /// <summary>
        /// Obtiene una compañia por el Id
        /// </summary>
        /// <param name="pCompanyID">company id</param>
        /// <returns></returns>
        CompanyDTO GetCompanyByID(int pCompanyID);

        /// <summary>
        /// Obtiene una compañia por el nombre
        /// </summary>
        /// <param name="pAccountNumberID">Nombre de la compañia</param>
        /// <returns></returns>
        CompanyDTO GetCompanyByName(string pCompanyName);

        /// <summary>
        /// Actualiza la información de un compañia en la tabla NS_tblCompany.
        /// </summary>
        /// <param name="pCompany">Viewmodel con la información a actualizar</param>
        void UpdateCompany(CompanyDTO pCompanyDTO);
    }
}
