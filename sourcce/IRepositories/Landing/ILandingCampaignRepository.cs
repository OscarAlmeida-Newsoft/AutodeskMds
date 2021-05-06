using Entities.Landing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories.Landing
{
    public interface ILandingCampaignRepository
    {
        /// <summary>
        /// Get all landing campaing items
        /// </summary>
        /// <returns>List of landing objects</returns>
        IEnumerable<LandingCampaign> GetAll();

        /// <summary>
        /// Get landing by Id
        /// </summary>
        /// <param name="pLandingId">Landing Id to query</param>
        /// <returns>Landing object with information</returns>
        LandingCampaign GetLandingById(Guid pLandingId);


        /// <summary>
        /// Save a landing page
        /// </summary>
        /// <param name="pLanding">Landing object</param>
        void CreateLanding(LandingCampaign pLanding);


        /// <summary>
        /// Get landing by Campaign
        /// </summary>
        /// <param name="pCampaign">Campaign to query</param>
        /// <param name="pCountryId">Id country</param>
        /// <returns>Landing object with information</returns>
        LandingCampaign GetLandingByCampaign(string pCampaign, int pCountryId);

        /// <summary>
        /// Removes a landing by Id
        /// </summary>
        /// <param name="pLanding">Landing to remove</param>
        /// <returns>True/False</returns>
        void DeleteLanding(LandingCampaign pLanding);
    }
}
