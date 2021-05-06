using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface IChooseAnActionService
    {
        string Message { get; }

        string GetUrlRedirectMeasureMyPlataform(LeadInformationDTO user);

        string GetUrlRedirectMeasureMyPlataformSAM360(LeadInformationDTO user, CRMDataDTO crmUser);

        List<List<string>> GetSAM360Reports(int pOrganisationId, string pReportId);

        /// <summary>
        /// Get the general progress of MDS and Assessment for showing info in the Choose an action progress bar
        /// </summary>
        /// <param name="pId">Leadinformation table Id</param>
        /// <param name="pLeadId">Lead Id</param>
        /// <param name="pCampaignId">Campaign Id</param>
        /// <returns></returns>
        ChooseAnActionSummaryDTO GetLeadGeneralProgress( Guid pCompanyId, Guid pCampaignId);

        /// <summary>
        /// Get if a MDS summary is finished or not
        /// </summary>
        /// <param name="pCompanyId"></param>
        /// <returns></returns>
        bool GetLeadMDSStatus(Guid pCompanyId);

        /// <summary>
        /// Get percentage of Company
        /// </summary>
        /// <param name="pCompanyId"></param>
        /// <returns></returns>
        double GetLeadMDSPercentage(Guid pLeadId, short pCampaignID);
        string GetUrlManagmentPoint_SAM360(LeadInformationDTO leadDTO, CRMDataDTO data);
    }
}
