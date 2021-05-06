using Entities;
using IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ProductCompanyFileRepository : Repository<NS_tblProductCompanyFile>, IProductCompanyFileRepository
    {
        public ProductCompanyFileRepository(AffidavitContext context) : base(context)
        {
            
        }

        /// <summary>
        ///     Obtiene una product company family de una compañia por su clave compuesta.
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia </param>
        /// /// <param name="pProductID">Id del producto </param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// /// <param name="pFileNumber">numero del archivo </param>
        /// <returns></returns>
        public NS_tblProductCompanyFile GetByFullCompositeKey(int pCompanyID, short pProductID, short pCampaignID, short pFileNumber)
        {
            return base.Context.Set<NS_tblProductCompanyFile>().Where(x => x.CompanyID == pCompanyID && x.CampaignID == pCampaignID &&  x.ProductID == pProductID && x.FileNumber == pFileNumber).FirstOrDefault();
        }

        /// <summary>
        ///     Obtiene la lista de imagenes de productos productos para una compañia y una campaña especificas
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia</param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <returns>Lista de productos productos que posee la compañia en una campaña</returns>
        public IEnumerable<NS_tblProductCompanyFile> GetByIDAndCampaign(int pCompanyID, short pCampaignID)
        {
            return base.Context.Set<NS_tblProductCompanyFile>().Where(x => x.CompanyID == pCompanyID && x.CampaignID == pCampaignID );
        }

        /// <summary>
        ///     Obtiene la lista de imagenes de productos productos para una compañia, campaña y productos especificos
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia</param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <param name="pProductID">Id del producto</param>
        /// <returns>Lista de productos productos que posee la compañia en una campaña por producto</returns>
        public IEnumerable<NS_tblProductCompanyFile> GetByIDAndCampaignAndProduct(int pCompanyID, short pCampaignID, short pProductID)
        {
            return base.Context.Set<NS_tblProductCompanyFile>().Where(x => x.CompanyID == pCompanyID && x.CampaignID == pCampaignID && x.ProductID == pProductID);
        }
    }
}
