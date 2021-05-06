using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface ICampaignRepository : IRepository<NS_tblCampaign>
    {
        /// <summary>
        ///     Obtiene la información de una campaña por su CRMCampaignId
        /// </summary>
        /// <param name="pCRMcampaignID">Id de la campaña en el CRM</param>
        /// <returns></returns>
        NS_tblCampaign GetByCRMCampaignID(string pCRMcampaignID);
    }
}
