using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface IProductCompanyRepository : IRepository<NS_tblProductCompany>
    {
        /// <summary>
        ///     Obtiene la lista de productos para una compañia y una campaña especificas
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia</param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <returns>Lista de productos que posee la compañia en una campaña</returns>
        IEnumerable<NS_tblProductCompany> GetByIDAndCampaign(int pCompanyID, short pCampaignID);

        /// <summary>
        ///     Obtiene una product company de una compañia por su clave compuesta.
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia </param>
        /// /// <param name="pProductID">Id del producto </param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <returns></returns>
        NS_tblProductCompany GetByFullCompositeKey(int pCompanyID, short pProductID, short pCampaignID);
    }
}
