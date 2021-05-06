using Entities;
using IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CompanyInfoRepository : Repository<NS_tblCompanyInfo>, ICompanyInfoRepository
    {
        public CompanyInfoRepository(AffidavitContext context) : base(context)
        {

        }

        /// <summary>
        /// obtiene info de una compañia por su id y id de la campaña.
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia</param>
        /// <param name="pCampaignID">Id de la campaña</param>
        public NS_tblCompanyInfo GetCompanyInfoByIDandCampaign(int pCompanyID, short pCampaignID)
        {
            return base.Context.Set<NS_tblCompanyInfo>().Where(x => x.CompanyID == pCompanyID && x.CampaignID == pCampaignID).FirstOrDefault();
        }

        /// <summary>
        /// Obitene el id una compañía de la tabla companyInfo por la campaña y el lead
        /// </summary>
        /// <param name="pLeadId">Id del lead</param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <returns></returns>
        public NS_tblCompanyInfo GetCompanyIDByLeadAndCampaign(Guid pLeadId, short pCampaignID)
        {
            return base.Context.Set<NS_tblCompanyInfo>().Where(x => x.LeadID == pLeadId.ToString() && x.CampaignID == pCampaignID).FirstOrDefault();
        }
    }
}
