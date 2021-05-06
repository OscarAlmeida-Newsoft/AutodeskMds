using Entities.Landing;
using IRepositories.Landing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Landing
{
    public class LandingCampaignRepository : Repository<LandingCampaign>, ILandingCampaignRepository
    {
        public LandingCampaignRepository(AffidavitContext context) : base(context)
        {
        }


        /// <summary>
        /// Get all landing campaing items
        /// </summary>
        /// <returns>List of landing objects</returns>
        public override IEnumerable<LandingCampaign> GetAll()
        {
            return Context.Set<LandingCampaign>().AsEnumerable();

        }


        /// <summary>
        /// Get landing by Id
        /// </summary>
        /// <param name="pLandingId">Landing Id to query</param>
        /// <returns>Landing object with information</returns>
        public LandingCampaign GetLandingById(Guid pLandingId)
        {
            LandingCampaign lan = new LandingCampaign();
            lan = base.Context.Set<LandingCampaign>().Where(x => x.Id == pLandingId).FirstOrDefault();
            return lan;
        }


        /// <summary>
        /// Save a landing page
        /// </summary>
        /// <param name="pLanding">Landing object</param>
        public void CreateLanding(LandingCampaign pLanding)
        {
            base.Add(pLanding);
        }


        /// <summary>
        /// Get landing by Campaign
        /// </summary>
        /// <param name="pCampaign">Campaign to query</param>
        /// <param name="pCountryId">Id country</param>
        /// <returns>Landing object with information</returns>
        public LandingCampaign GetLandingByCampaign(string pCampaign, int pCountryId)
        {
            return base.Find(x => x.Campaing == pCampaign).Where(x => x.Status == true && x.CountryId == pCountryId).FirstOrDefault();
        }

        /// <summary>
        /// Removes a landing by Id
        /// </summary>
        /// <param name="pLanding">Landing to remove</param>
        /// <returns>True/False</returns>
        public void DeleteLanding(LandingCampaign pLanding)
        {
            pLanding.Status = false;
        }
    }
}
