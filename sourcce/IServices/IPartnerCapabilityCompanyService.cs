using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface IPartnerCapabilityCompanyService : IBaseService<PartnerCapabilityCompanyDTO>
    {
        /// <summary>
        ///     Obtiene los competencias de una compañia que es partner o developer partner de  una campaña por su clave compuesta.
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia </param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <returns></returns>
        IEnumerable<PartnerCapabilityCompanyDTO> GetByCompositeKey(int pCompanyID, short pCampaignID);

        /// <summary>
        ///     Actualiza un campo de la tabla NS_tblPartnerCapabilityCompany
        /// </summary>
        /// <param name="pPartnerCapabilityCompany">ViewModel con informacion a actualizar</param>
        void UpdatePartnerCapabilitycompany(PartnerCapabilityCompanyDTO pPartnerCapabilityCompany);

        /// <summary>
        ///     Obtiene una capability de una compañia que es partner o developer partner de  una campaña por su clave compuesta.
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia </param>
        /// /// <param name="pPartnerCapabilityID">Id del Partner Capability </param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <returns></returns>
        PartnerCapabilityCompanyDTO GetByFullCompositeKey(int pCompanyID, short pPartnerCapabilityID, short pCampaignID);
    }
}
