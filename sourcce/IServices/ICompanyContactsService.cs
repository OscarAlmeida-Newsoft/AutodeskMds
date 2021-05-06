using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface ICompanyContactsService : IBaseService<CompanyContactsDTO>
    {
        /// <summary>
        ///     Obtiene la información general de una compañia
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia</param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <returns></returns>
        CompanyContactsDTO GetByIDAndCampaign(int pCompanyID, short pCampaignID);

        /// <summary>
        /// Actualiza la información general de un compañia.
        /// </summary>
        /// <param name="pCompanyContacts">Viewmodel con la información a actualizar</param>
        void UpdateCompanyContacts(CompanyContactsDTO pCompanyContacts);
    }
}
