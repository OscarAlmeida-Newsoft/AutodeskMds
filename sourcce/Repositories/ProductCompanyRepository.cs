using Entities;
using IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ProductCompanyRepository : Repository<NS_tblProductCompany>, IProductCompanyRepository
    {
        public ProductCompanyRepository(AffidavitContext context) : base(context)
        {
            
        }

        /// <summary>
        ///     Obtiene una product company de una compañia por su clave compuesta.
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia </param>
        /// /// <param name="pProductID">Id del producto </param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <returns></returns>
        public NS_tblProductCompany GetByFullCompositeKey(int pCompanyID, short pProductID, short pCampaignID)
        {
            return base.Context.Set<NS_tblProductCompany>().Where(x => x.CompanyID == pCompanyID && x.CampaignID == pCampaignID
            && x.ProductID == pProductID).FirstOrDefault();
        }

        /// <summary>
        ///     Obtiene la lista de productos para una compañia y una campaña especificas
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia</param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <returns>Lista de productos que posee la compañia en una campaña</returns>
        public IEnumerable<NS_tblProductCompany> GetByIDAndCampaign(int pCompanyID, short pCampaignID)
        {
            return Context.Set<NS_tblProductCompany>().Where(s => s.CompanyID == pCompanyID && s.CampaignID == pCampaignID).OrderBy(s => s.CompanyID).ThenByDescending(n => n.CampaignID);
        }
    }
}
