using DTOs;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface ICRMService
    {
        string GetCRMEntityValue();

        /// <summary>
        ///     Service para consultar un lead por su id.
        /// </summary>
        /// <param name="pLeadID">Id del lead a consultar</param>
        /// <returns></returns>
        CRMDataDTO GetLeadByID(Guid pLeadID);

        /// <summary>
        /// Get lead information for survey question consultant page
        /// </summary>
        /// <param name="pCompanyName">Name of the company</param>
        /// <returns></returns>
        List<CRMDataDTO> GetLeadInfoByCompanyName(string pCompanyName);

        /// <summary>
        /// Get lead information for survey question consultant page
        /// </summary>
        /// <param name="pCompanyName">Name of the company</param>
        /// <param name="pConsultant">Name of the consultant</param>
        /// <returns></returns>
        List<CRMDataDTO> GetLeadInfoByCompanyNameAndConsultant(string pCompanyName, string pConsultant);

        /// <summary>
        /// check if exist a campaign in the CRM
        /// </summary>
        /// <param name="pCampaignName">Campaign Name to verified</param>
        /// <returns></returns>
        bool CheckCampaign(string pCampaignName);

        /// <summary>
        /// Get the consultant job title 
        /// </summary>
        /// <param name="pConsultantName">consultant name</param>
        /// <returns></returns>
        string GetConsultantTitle(Guid pLeadID);

        /// <summary>
        /// Get the consultant authorize region by email
        /// </summary>
        /// <param name="pEmailconsultant">Email consultant</param>
        /// <returns></returns>
        string GetUserRegionByEmail(string pEmailconsultant);
    }
}
