using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface IProductCompanyFileService : IBaseService<ProductCompanyFileDTO>
    {
        /// <summary>
        ///     Obtiene la lista de imagenes de productos productos para una compañia y una campaña especificas
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia</param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <returns>Lista de productos productos que posee la compañia en una campaña</returns>
        IEnumerable<ProductCompanyFileDTO> GetByIDAndCampaign(int pCompanyID, short pCampaignID);


        /// <summary>
        ///     Obtiene la lista de imagenes de productos productos para una compañia, campaña y productos especificos
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia</param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <param name="pProductID">Id del producto</param>
        /// <returns>Lista de productos productos que posee la compañia en una campaña por producto</returns>
        IEnumerable<ProductCompanyFileDTO> GetByIDAndCampaignAndProduct(int pCompanyID, short pCampaignID, short pProductID);


        /// <summary>
        ///     Obtiene una product company family de una compañia por su clave compuesta.
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia </param>
        /// /// <param name="pProductID">Id del producto </param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// /// <param name="pFileNumber">numero del archivo </param>
        /// <returns></returns>
        ProductCompanyFileDTO GetByFullCompositeKey(int pCompanyID, short pProductID, short pCampaignID, short pFileNumber);


        /// <summary>
        ///     Borra la lista de archivos de productos para una compañia, campaña y productos especificos
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia</param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <param name="pProductID">Id del producto</param>
        /// <returns>Lista de productos productos que posee la compañia en una campaña por producto</returns>
        void RemoveByIDAndCampaignAndProduct(int pCompanyID, short pCampaignID, short pProductID);
    }
}
