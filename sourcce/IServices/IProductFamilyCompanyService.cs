using DTOs;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface IProductFamilyCompanyService : IBaseService<ProductFamilyCompanyDTO>
    {
        /// <summary>
        ///     Obtiene la lista de comentarios adicionales para una familia de productos para una compañia y una campaña especificas
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia</param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <returns>Lista de de comentarios de productos que posee la compañia en una campaña</returns>
        IEnumerable<ProductFamilyCompanyDTO> GetByIDAndCampaign(int pCompanyID, short pCampaignID);

        /// <summary>
        ///     Obtiene una product family company de una compañia por su clave compuesta.
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia </param>
        /// /// <param name="pProductFamilyID">Id de la familia de productos </param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <returns></returns>
        ProductFamilyCompanyDTO GetByCompositeKey(int pCompanyID, short pProductFamilyID, short pCampaignID);

        /// <summary>
        ///     Actualiza un campo de la tabla NS_tblProductFamilyCompany
        /// </summary>
        /// <param name="pProductFamilyCompany">ViewModel con informacion a actualizar</param>
        void UpdateProductFamilycompany(ProductFamilyCompanyDTO pProductFamilyCompany);
    }
}
