using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface ICompanyInfoRepository : IRepository<NS_tblCompanyInfo>
    {
        /// <summary>
        /// obtiene info de una compañia por su id y id de la campaña.
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia</param>
        /// <param name="pCampaignID">Id de la campaña</param>
        NS_tblCompanyInfo GetCompanyInfoByIDandCampaign(int pCompanyID, short pCampaignID);

        /// <summary>
        /// Obitene el id una compañía de la tabla companyInfo por la campaña y el lead
        /// </summary>
        /// <param name="pLeadId">Id del lead</param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <returns></returns>
        NS_tblCompanyInfo GetCompanyIDByLeadAndCampaign(Guid pLeadId, short pCampaignID);
    }
}
