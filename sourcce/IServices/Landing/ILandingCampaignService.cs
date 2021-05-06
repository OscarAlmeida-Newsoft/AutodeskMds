using DTOs.Landing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices.Landing
{
    public interface ILandingCampaignService
    {
        /// <summary>
        /// Get all landing campaing items
        /// </summary>
        /// <returns>List of landing objects</returns>
        IEnumerable<LandingCampaignDTO> GetAll(LandingCampaignFilterModel filterOptions);

        /// <summary>
        /// Get landing by Id
        /// </summary>
        /// <param name="pLandingId">Landing Id to query</param>
        /// <returns>Landing object with information</returns>
        LandingCampaignDTO GetLandingById(Guid pLandingId);

        /// <summary>
        /// Update a landing campaign
        /// </summary>
        /// <param name="pLanding">Landing object</param>
        void UpdateLanding(LandingCampaignDTO pLanding);

        /// <summary>
        /// Save a landing page
        /// </summary>
        /// <param name="pLanding">Landing object</param>
        bool CreateLanding(LandingCampaignDTO pLanding);

        /// <summary>
        /// Get landing by Campaign
        /// </summary>
        /// <param name="pCampaign">Campaign to query</param>
        /// <param name="pCountryId">Id country</param>
        /// <returns>Landing object with information</returns>
        LandingCampaignDTO GetLandingByCampaign(string pCampaign, int pCountryId);

        /// <summary>
        /// Get all landing campaing items per country without the from landing campaign
        /// </summary>
        /// <param name="PCountryId">Country Id for search the landings</param>
        /// <param name="pLandingFrom">Landing Id from to do de change</param>
        /// <returns>List of landing objects</returns>
        IEnumerable<LandingCampaignDTO> GetAllPerCountry(int pCountryId, Guid pLandingFrom);



        /// <summary>
        /// Assign the content of the landing to other(s) landing campaign
        /// </summary>
        /// <param name="pReplaceLandingContent">Object with the data to replace</param>
        void AssignLandingContent(ReplaceMultipleLandingDTO pReplaceLandingContent);

        /// <summary>
        /// Validate the landing campaign name, unique per country
        /// </summary>
        /// <param name="pCountryId">CountryId</param>
        /// <param name="pLandingCampaign">CampaignName</param>
        /// <param name="pLandingId">Landing Id for update</param>
        /// <returns>True/False</returns>
        bool ValidateLandingCampaignName(int pCountryId, string pLandingCampaign, Guid? pLandingId);


        /// <summary>
        /// Removes a landing by Id
        /// </summary>
        /// <param name="pLandingId">Landing Id to remove</param>
        /// <returns>True/False</returns>
        bool DeleteLanding(Guid pLandingId);
    }
}
