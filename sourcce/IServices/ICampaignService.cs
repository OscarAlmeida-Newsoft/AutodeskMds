using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface ICampaignService : IBaseService<CampaignDTO>
    {
        /// <summary>
        ///     Obtiene la información de una campaña por su CRMCampaignId
        /// </summary>
        /// <param name="pCRMcampaignID">Id de la campaña en el CRM</param>
        /// <returns></returns>
        CampaignDTO GetByCRMCampaignID(string pCRMcampaignID);
    }
}
