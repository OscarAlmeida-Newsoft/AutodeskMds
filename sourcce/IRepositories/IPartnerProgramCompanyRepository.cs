using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface IPartnerProgramCompanyRepository : IRepository<NS_tblPartnerProgramCompany>
    {
        /// <summary>
        ///     Obtiene los programas de una compañia que es partner o developer partner de  una campaña por su clave compuesta.
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia </param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <returns></returns>
        IEnumerable<NS_tblPartnerProgramCompany> GetByCompositeKey(int pCompanyID, short pCampaignID);


        /// <summary>
        ///     Obtiene una Program de una compañia que es partner o developer partner de  una campaña por su clave compuesta.
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia </param>
        /// /// <param name="pPartnerProgramID">Id del Partner Capability </param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <returns></returns>
        NS_tblPartnerProgramCompany GetByFullCompositeKey(int pCompanyID, short pPartnerProgramID, short pCampaignID);
    }
}
