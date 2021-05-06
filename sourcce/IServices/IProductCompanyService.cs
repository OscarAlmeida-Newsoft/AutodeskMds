using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface IProductCompanyService : IBaseService<ProductCompanyDTO>
    {
        /// <summary>
        ///     Obtiene la lista de productos para una compañia y una campaña especificas
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia</param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <returns>Lista de productos que posee la compañia en una campaña</returns>
        IEnumerable<ProductCompanyDTO> GetByIDAndCampaign(int pCompanyID, short pCampaignID);

        /// <summary>
        ///     Obtiene una product company de una compañia por su clave compuesta.
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia </param>
        /// /// <param name="pProductID">Id del producto </param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <returns></returns>
        ProductCompanyDTO GetByFullCompositeKey(int pCompanyID, short pProductID, short pCampaignID);

        /// <summary>
        ///     Actualiza un campo de la tabla NS_tblProductCompany
        /// </summary>
        /// <param name="pProductCompany">ViewModel con informacion a actualizar</param>
        void UpdateProductcompany(ProductCompanyDTO pProductCompany);

        //void UpdateLicenses(List<ProductCompanyDTO> model);
    }
}
