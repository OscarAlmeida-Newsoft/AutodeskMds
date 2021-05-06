using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface IPartnerCapabilityCompanyRepository : IRepository<NS_tblPartnerCapabilityCompany>
    {
        /// <summary>
        ///     Obtiene los competencias de una compañia que es partner o developer partner de  una campaña por su clave compuesta.
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia </param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <returns></returns>
        IEnumerable<NS_tblPartnerCapabilityCompany> GetByCompositeKey(int pCompanyID, short pCampaignID);

        /// <summary>
        ///     Obtiene una capability de una compañia que es partner o developer partner de  una campaña por su clave compuesta.
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia </param>
        /// /// <param name="pPartnerCapabilityID">Id del Partner Capability </param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <returns></returns>
        NS_tblPartnerCapabilityCompany GetByFullCompositeKey(int pCompanyID, short pPartnerCapabilityID, short pCampaignID);
    }
}
