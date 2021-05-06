using Entities;
using IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class PartnerCapabilityCompanyRepository : Repository<NS_tblPartnerCapabilityCompany>, IPartnerCapabilityCompanyRepository
    {
        public PartnerCapabilityCompanyRepository(AffidavitContext context) : base(context)
        {

        }

        /// <summary>
        ///     Obtiene los competencias de una compañia que es partner o developer partner de  una campaña por su clave compuesta.
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia </param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <returns></returns>
        public IEnumerable<NS_tblPartnerCapabilityCompany> GetByCompositeKey(int pCompanyID, short pCampaignID)
        {
            return base.Context.Set<NS_tblPartnerCapabilityCompany>().Where(pc => pc.CompanyID == pCompanyID && pc.CampaignID == pCampaignID);
        }

        /// <summary>
        ///     Obtiene una capability de una compañia que es partner o developer partner de  una campaña por su clave compuesta.
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia </param>
        /// /// <param name="pPartnerCapabilityID">Id del Partner Capability </param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <returns></returns>
        public NS_tblPartnerCapabilityCompany GetByFullCompositeKey(int pCompanyID, short pPartnerCapabilityID, short pCampaignID)
        {
            return base.Context.Set<NS_tblPartnerCapabilityCompany>().Where(x => x.CompanyID == pCompanyID && x.CampaignID == pCampaignID 
            && x.PartnerCapabilityID == pPartnerCapabilityID).FirstOrDefault();
        }
    }
}
