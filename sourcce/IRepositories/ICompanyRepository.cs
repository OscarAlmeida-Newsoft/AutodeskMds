using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface ICompanyRepository : IRepository<NS_tblCompany>
    {
        /// <summary>
        /// Obtiene una compañia por el account number
        /// </summary>
        /// <param name="pAccountNumberID">account number id</param>
        /// <returns>Info de una compañia</returns>
        NS_tblCompany GetCompanyByAccountNumber(string pAccountNumberID);

        /// <summary>
        /// Obtiene una compañia por el Id
        /// </summary>
        /// <param name="pCompanyID">company id</param>
        /// <returns></returns>
        NS_tblCompany GetCompanyByID(int pCompanyID);

        /// <summary>
        /// Obtiene una compañia por el nombre
        /// </summary>
        /// <param name="pAccountNumberID">Nombre de la compañia</param>
        /// <returns></returns>
        NS_tblCompany GetCompanyByName(string pCompanyName);
    }
}
