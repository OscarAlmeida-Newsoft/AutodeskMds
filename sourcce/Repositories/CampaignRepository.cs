

namespace Repositories
{
    using Entities;
    using IRepositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class CampaignRepository : Repository<NS_tblCampaign>, ICampaignRepository
    {
        public CampaignRepository(AffidavitContext context) : base(context)
        {
        }

        /// <summary>
        ///     Obtiene la información de una campaña por su CRMCampaignId
        /// </summary>
        /// <param name="pCRMcampaignID">Id de la campaña en el CRM</param>
        /// <returns>Información de una campaña</returns>
        public NS_tblCampaign GetByCRMCampaignID(string pCRMcampaignID)
        {
            return base.Context.Set<NS_tblCampaign>().Where(x => x.CRMCampaignID == pCRMcampaignID.ToString()).FirstOrDefault();
        }
    }
}
