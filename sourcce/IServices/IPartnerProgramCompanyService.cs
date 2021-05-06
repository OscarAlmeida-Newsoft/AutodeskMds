using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface IPartnerProgramCompanyService : IBaseService<PartnerProgramCompanyDTO>
    {
        /// <summary>
        ///     Obtiene los programas de una compañia que es partner o developer partner de  una campaña por su clave compuesta.
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia </param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <returns></returns>
        IEnumerable<PartnerProgramCompanyDTO> GetByCompositeKey(int pCompanyID, short pCampaignID);

        /// <summary>
        ///     Actualiza un campo de la tabla NS_tblPartnerProgramCompany
        /// </summary>
        /// <param name="pPartnerProgramCompany">ViewModel con informacion a actualizar</param>
        void UpdatePartnerProgramCompany(PartnerProgramCompanyDTO pPartnerProgramCompany);

        /// <summary>
        ///     Obtiene una Program de una compañia que es partner o developer partner de  una campaña por su clave compuesta.
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia </param>
        /// /// <param name="pPartnerProgramID">Id del Partner Capability </param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <returns></returns>
        PartnerProgramCompanyDTO GetByFullCompositeKey(int pCompanyID, short pPartnerProgramID, short pCampaignID);
    }
}
