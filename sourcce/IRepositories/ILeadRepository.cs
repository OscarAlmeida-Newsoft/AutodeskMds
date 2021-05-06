using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface ILeadRepository
    {
        /// <summary>
        /// Save the lead information
        /// </summary>
        /// <param name="pLeadInformation">Lead information to save</param>
        void SaveLeadInformation(LeadInformation pLeadInformation);

        /// <summary>
        /// Get lead information by ID
        /// </summary>
        /// <param name="pLeadId">Lead Id</param>
        /// <returns>Object with the lead information</returns>
        LeadInformation GetByLeadId(Guid pLeadId);

        /// <summary>
        /// Get Lead by Landing campaign id
        /// </summary>
        /// <param name="pLandingId">Landing campaign id</param>
        /// <returns></returns>
        bool GetLeadByLandingCampaignId(Guid pLandingId);

        /// <summary>
        /// Get a Lead by LeadUsser 
        /// </summary>
        /// <param name="pEmailUser">User by the lead</param>
        /// <returns></returns>
        IEnumerable<LeadInformation> GetLeadByEmail(string pEmailUser);

        /// <summary>
        /// Get all lead information objects
        /// </summary>
        /// <returns></returns>
        IEnumerable<LeadInformation> GetAll();

        //TODO: Remove this method after masive lead information update for the username creation
        void UpdateLeadInformationUserName(Guid pId, string pUserNameCode);

        void UpdateLeadInformationIndustryIndInsId(Guid pId, int IndustryIndInsId);

        /// <summary>
        /// Get the progress of the lead in MDS
        /// </summary>
        /// <param name="pCampaignId">Campaign Id</param>
        /// <param name="pCompanyId">Xompany Id</param>
        /// <returns>0 -> Without progress, 50 -> Only has accepted the letter, 100 -> has finished the MDS form</returns>
        int GetLeadInformationProgress(Guid pCompanyId);

        /// <summary>
        /// Get a boolean that means if a lead exist or not
        /// </summary>
        /// <param name="pCompanyUserNameCode">CompanyUserNameCode of Company</param>
        /// <returns>bool that means if a lead exist or not</returns>
        bool ExistLeadByCompanyUserNameCode(string pCompanyUserNameCode);

        IEnumerable<LeadInformation> GetByUserName(string pCompanyName);
    }
}
