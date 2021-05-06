using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface ICompanyInfoService : IBaseService<CompanyInfoDTO>
    {
        /// <summary>
        /// Actualiza la información de un compañia.
        /// </summary>
        /// <param name="pCompanyInfo">Viewmodel con la información a actualizar</param>
        void UpdateCompanyInfo(CompanyInfoDTO pCompanyInfo);

        /// <summary>
        /// Cambia el estado de un affidavit (MDS)
        /// </summary>
        /// <param name="pIsFinalVersion">estado del affidavit (MDS)</param>
        /// <param name="pCompanyId">id de la compañia</param>
        /// <param name="pCampaignID">id de la campaña</param>
        void UpdateState(bool pIsFinalVersion, int pCompanyID, short pCampaignID);

        /// <summary>
        /// obtiene info de una compañia por su id y id de la campaña.
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia</param>
        /// <param name="pCampaignID">Id de la campaña</param>
        CompanyInfoDTO GetCompanyInfoByIDandCampaign(int pCompanyID, short pCampaignID);

        /// <summary>
        /// Obitene el id una compañía de la tabla companyInfo por la campaña y el lead
        /// </summary>
        /// <param name="pLeadId">Id del lead</param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <returns></returns>
        int GetCompanyIDByLeadAndCampaign(Guid pLeadId, short pCampaignID);

    }
}
