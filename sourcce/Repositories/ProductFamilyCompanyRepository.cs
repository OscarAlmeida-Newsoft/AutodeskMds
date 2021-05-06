using Entities;
using IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ProductFamilyCompanyRepository : Repository<NS_tblProductFamilyCompany>, IProductFamilyCompanyRepository
    {
        public ProductFamilyCompanyRepository(AffidavitContext context) : base(context)
        {
        }

        /// <summary>
        ///     Obtiene una product family company de una compañia por su clave compuesta.
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia </param>
        /// /// <param name="pProductFamilyID">Id de la familia de productos </param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <returns></returns>
        public NS_tblProductFamilyCompany GetByCompositeKey(int pCompanyID, short pProductFamilyID, short pCampaignID)
        {
            return base.Context.Set<NS_tblProductFamilyCompany>().Where(x => x.CompanyID == pCompanyID && x.CampaignID == pCampaignID
            && x.ProductFamilyID == pProductFamilyID).FirstOrDefault();
        }

        /// <summary>
        ///     Obtiene la lista de comentarios adicionales para una familia de productos para una compañia y una campaña especificas
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia</param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <returns>Lista de de comentarios de productos que posee la compañia en una campaña</returns>
        public IEnumerable<NS_tblProductFamilyCompany> GetByIDAndCampaign(int pCompanyID, short pCampaignID)
        {
            return base.Context.Set<NS_tblProductFamilyCompany>().Where(pfc => pfc.CompanyID == pCompanyID && pfc.CampaignID == pCampaignID);
        }
    }
}
