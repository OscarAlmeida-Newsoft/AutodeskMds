using Entities;
using IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class PartnerProgramCompanyRepository : Repository<NS_tblPartnerProgramCompany>, IPartnerProgramCompanyRepository
    {
        public PartnerProgramCompanyRepository(AffidavitContext context) : base(context)
        {

        }

        /// <summary>
        ///     Obtiene los programas de una compañia que es partner o developer partner de  una campaña por su clave compuesta.
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia </param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <returns></returns>
        public IEnumerable<NS_tblPartnerProgramCompany> GetByCompositeKey(int pCompanyID, short pCampaignID)
        {
            return base.Context.Set<NS_tblPartnerProgramCompany>().Where(pc => pc.CompanyID == pCompanyID && pc.CampaignID == pCampaignID);
        }

        /// <summary>
        ///     Obtiene una Program de una compañia que es partner o developer partner de  una campaña por su clave compuesta.
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia </param>
        /// /// <param name="pPartnerProgramID">Id del Partner Capability </param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <returns></returns>
        public NS_tblPartnerProgramCompany GetByFullCompositeKey(int pCompanyID, short pPartnerProgramID, short pCampaignID)
        {
            return base.Context.Set<NS_tblPartnerProgramCompany>().Where(x => x.CompanyID == pCompanyID && x.CampaignID == pCampaignID
            && x.PartnerProgramID == pPartnerProgramID).FirstOrDefault();
        }
    }
}
