using Entities;
using IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CompanyRepository : Repository<NS_tblCompany>, ICompanyRepository
    {
        public CompanyRepository(AffidavitContext context) : base(context)
        {

        }

        /// <summary>
        /// Obtiene una compañia por el account number
        /// </summary>
        /// <param name="pAccountNumberID">account number id</param>
        /// <returns></returns>
        public NS_tblCompany GetCompanyByAccountNumber(string pAccountNumberID)
        {
            string account = pAccountNumberID;
            return base.Context.Set<NS_tblCompany>().Where(x => x.AccountNumber == account).FirstOrDefault();
        }

        /// <summary>
        /// Obtiene una compañia por el Id
        /// </summary>
        /// <param name="pCompanyID">company id</param>
        /// <returns></returns>
        public NS_tblCompany GetCompanyByID(int pCompanyID)
        {
            return base.Context.Set<NS_tblCompany>().Where(x => x.CompanyID == pCompanyID).FirstOrDefault();
        }

        /// <summary>
        /// Obtiene una compañia por el nombre
        /// </summary>
        /// <param name="pAccountNumberID">Nombre de la compañia</param>
        /// <returns></returns>
        public NS_tblCompany GetCompanyByName(string pCompanyName)
        {
            return base.Context.Set<NS_tblCompany>().Where(x => x.CompanyName == pCompanyName).FirstOrDefault();
        }
    }
}
