using AutoMapper;
using DTOs.Landing;
using Entities.DbAffidavit;
using Entities.Landing;
using IRepositories.Landing;
using IServices.Landing;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Landing
{
    public class LandingCampaignService : ILandingCampaignService
    {
        protected IUnitOfWork.IUnitOfWork unitOfWork;
        readonly ILandingCampaignRepository landingRepository;

        public LandingCampaignService(IUnitOfWork.IUnitOfWork pUnitOfWork, ILandingCampaignRepository pLandingRepository)
        {
            unitOfWork = pUnitOfWork;
            landingRepository = pLandingRepository;
        }

        /// <summary>
        /// Get all landing campaing items
        /// </summary>
        /// <returns>List of landing objects</returns>
        public IEnumerable<LandingCampaignDTO> GetAll(LandingCampaignFilterModel filterOptions)
        {
            var _data = landingRepository.GetAll().Where(x => x.Status == true);

            if (filterOptions != null)
            {
                //Filter for campaign column
                if (!string.IsNullOrEmpty(filterOptions.Campaing))
                    _data = _data.Where(x => x.Campaing.ToLower().Contains(filterOptions.Campaing.ToLower()));

                //Filter for country column
                if (!string.IsNullOrEmpty(filterOptions.Country))
                    _data = _data.Where(x => x.Country.CountryName.ToLower().Contains(filterOptions.Country.ToLower()));

                //Filter for created by column
                if (!string.IsNullOrEmpty(filterOptions.CreatedBy))
                    _data = _data.Where(x => x.CreatedBy.UserName.ToLower().Contains(filterOptions.CreatedBy.ToLower()));

                //Filter for created column
                if (!string.IsNullOrEmpty(filterOptions.CreatedDate))
                    _data = _data.Where(x => x.CreatedDate.ToString().ToLower().Contains(filterOptions.CreatedDate.ToLower()));

            }

            _data = _data.OrderByDescending(x => x.CreatedDate);


            return Mapper.Map<IEnumerable<LandingCampaign>, IEnumerable<LandingCampaignDTO>>(_data);
        }


        /// <summary>
        /// Get landing by Id
        /// </summary>
        /// <param name="pLandingId">Landing Id to query</param>
        /// <returns>Landing object with information</returns>
        public LandingCampaignDTO GetLandingById(Guid pLandingId)
        {
            var _data = landingRepository.GetLandingById(pLandingId);
            return Mapper.Map<LandingCampaign, LandingCampaignDTO>(_data);
        }


        /// <summary>
        /// Update a landing campaign
        /// </summary>
        /// <param name="pLanding">Landing object</param>
        public void UpdateLanding(LandingCampaignDTO pLanding)
        {
            var _landing = landingRepository.GetLandingById(pLanding.Id);

            _landing.Campaing = pLanding.Campaing;
            _landing.LandingText = pLanding.LandingText ?? "";
            _landing.CountryId = pLanding.CountryId;

            unitOfWork.Complete();
        }


        /// <summary>
        /// Save a landing page
        /// </summary>
        /// <param name="pLanding">Landing object</param>
        public bool CreateLanding(LandingCampaignDTO pLanding)
        {
            bool _canSave = false;
            if (pLanding.CountryId == -1 || pLanding.CountryId == -2)
            {
                using (AffidavitModelsConnection db = new AffidavitModelsConnection())
                {
                    using (var _transac = db.Database.BeginTransaction())
                    {                        
                        try
                        {                            
                            ObjectParameter _response = new ObjectParameter("notSave", "string");
                            pLanding.LandingText = pLanding.LandingText ?? "";
                            db.NS_spCreateLetterLatam(pLanding.CreatedById, pLanding.LandingText, pLanding.CountryId, pLanding.Campaing, _response);

                            _canSave = (_response.Value.ToString() == "true") ? true : false;

                            db.SaveChanges();
                            _transac.Commit();
                            return _canSave;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.StackTrace);
                            _transac.Rollback();
                            return _canSave;
                        }
                    }
                }
            } else
            {
                pLanding.Id = Guid.NewGuid();
                pLanding.CreatedDate = DateTime.Now;
                pLanding.EditedDate = DateTime.Now;
                pLanding.CreatedById = pLanding.CreatedById;
                pLanding.Status = true;
                pLanding.LandingText = pLanding.LandingText ?? "";

                landingRepository.CreateLanding(Mapper.Map<LandingCampaignDTO, LandingCampaign>(pLanding));
                unitOfWork.Complete();
            }

            return _canSave;
        }


        /// <summary>
        /// Get landing by Campaign
        /// </summary>
        /// <param name="pCampaign">Campaign to query</param>
        /// <param name="pCountryId">Id country</param>
        /// <returns>Landing object with information</returns>
        public LandingCampaignDTO GetLandingByCampaign(string pCampaign, int pCountryId)
        {
            var _data = landingRepository.GetLandingByCampaign(pCampaign, pCountryId);
            return Mapper.Map<LandingCampaign, LandingCampaignDTO>(_data);
        }


        /// <summary>
        /// Get all landing campaing items per country without the from landing campaign
        /// </summary>
        /// <param name="PCountryId">Country Id for search the landings</param>
        /// <param name="pLandingFrom">Landing Id from to do de change</param>
        /// <returns>List of landing objects</returns>
        public IEnumerable<LandingCampaignDTO> GetAllPerCountry(int pCountryId, Guid pLandingFrom)
        {
            var _data = landingRepository
                .GetAll()
                .Where(x => x.CountryId == pCountryId && x.Status == true && !x.Id.ToString().Contains(pLandingFrom.ToString()))
                .OrderBy(x => x.Campaing);

            return Mapper.Map<IEnumerable<LandingCampaign>, IEnumerable<LandingCampaignDTO>>(_data);
        }


        /// <summary>
        /// Assign the content of the landing to other(s) landing campaign
        /// </summary>
        /// <param name="pReplaceLandingContent">Object with the data to replace</param>
        public void AssignLandingContent(ReplaceMultipleLandingDTO pReplaceLandingContent)
        {
            var _landingData = GetLandingById(pReplaceLandingContent.landingFrom);

            //replace the text
            var _listOfLandingToReplace = pReplaceLandingContent.landingTo.ToList();

            var _landings = landingRepository.GetAll().Where(x => _listOfLandingToReplace.Contains(x.Id));

            foreach (var item in _landings)
            {
                item.LandingText = _landingData.LandingText;
            }

            unitOfWork.Complete();
        }


        /// <summary>
        /// Validate the landing campaign name, unique per country
        /// </summary>
        /// <param name="pCountryId">CountryId</param>
        /// <param name="pLandingCampaign">CampaignName</param>
        /// <param name="pLandingId">Landing Id for update</param>
        /// <returns>True/False</returns>
        public bool ValidateLandingCampaignName(int pCountryId, string pLandingCampaign, Guid? pLandingId)
        {
            return landingRepository.GetAll().Any(x => x.Status == true && x.CountryId == pCountryId && x.Campaing == pLandingCampaign && x.Id != pLandingId);
        }


        /// <summary>
        /// Removes a landing by Id
        /// </summary>
        /// <param name="pLandingId">Landing Id to remove</param>
        /// <returns>True/False</returns>
        public bool DeleteLanding(Guid pLandingId)
        {
            var _landing = landingRepository.GetLandingById(pLandingId);

            landingRepository.DeleteLanding(_landing);
            return unitOfWork.Complete() > 0;
        }
    }
}
