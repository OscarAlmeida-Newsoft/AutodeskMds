using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface ILeadService
    {
        /// <summary>
        /// Save the lead information
        /// </summary>
        /// <param name="pLeadInformation">Lead information to save</param>
        void SaveLeadInformation(LeadInformationDTO pLeadInformation);

        /// <summary>
        /// Save the disagree reason
        /// </summary>
        /// <param name="disagreeReasonDTO">Disagree reason object with data</param>
        void SaveDisagreeReason(DisagreeReasonDTO disagreeReasonDTO);

        /// <summary>
        /// Get lead information by ID
        /// </summary>
        /// <param name="pLeadId">Lead Id</param>
        /// <returns>Object with the lead information</returns>
        LeadInformationDTO GetByLeadID(Guid pLeadID);

        /// <summary>
        /// Update the information of a row
        /// </summary>
        /// <param name="pLeadID">Lead ID of the row to update</param>
        void UpdateLeadInformation(Guid pLeadID, Guid? pLandingID, string pLandingText, string dateUpdate);

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
        IEnumerable<LeadInformationDTO> GetLeadByEmail(string pEmailUser);

        //TODO:Remove this method after create all company user names
        void CreateAllCompanyUserNames();

        void UpdateLeadInformationIndustryIndInsId(Guid pId, int IndustryIndInsId);

        IEnumerable<LeadInformationDTO> GetByUserName(string pCompanyName);
    }
}
