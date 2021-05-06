using Affidavit.Models.ManagePlatform;
using AutoMapper;
using DTOs.Landing;
using IServices;
using IServices.Landing;
using IServices.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Affidavit.Utils;

namespace Affidavit.Controllers
{
    #if !DEBUG
    [RequireHttps] //apply to all actions in controller
    #endif
    public class ManagePlatformController : BaseController
    {
        readonly ILandingCampaignService landingService;
        readonly IUserService userService;
        readonly ICountryService countryService;
        readonly ICRMService CRMService;
        readonly ILeadService leadService;

        public ManagePlatformController(ILandingCampaignService pLandingCampaignService, IUserService pUserService, ICountryService pCountryService,
            ICRMService pCRMService, ILeadService pLeadService)
        {
            landingService = pLandingCampaignService;
            userService = pUserService;
            countryService = pCountryService;
            CRMService = pCRMService;
            leadService = pLeadService;
        }

        // GET: ManagePlatform
        public ActionResult Index()
        {
            if (Session["userLogin"] == null)
            {
                return RedirectToAction("../Home/LoginAdmin");
            }

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult ManageLanding()
        {
            if (Session["userLogin"] == null)
            {
                return RedirectToAction("../Home/LoginAdmin");
            }

            var _countries = countryService.GetAllCountries();
            ManageLandingNewVM _model = new ManageLandingNewVM();
            List<SelectListItem> _countriesList = new List<SelectListItem>();

            _countries = _countries.OrderBy(p => p.CountryName);

            foreach (var item in _countries)
            {
                _countriesList.Add(new SelectListItem
                {
                    Text = item.CountryName,
                    Value = item.CountryID.ToString()
                });
            }

            _countriesList.Insert(0, new SelectListItem {
                Text = "Latam New Market(Español)",
                Value = "-1"
            });

            _countriesList.Insert(0, new SelectListItem
            {
                Text = "Latam New Market(English)",
                Value = "-2"
            });
            _model.Countries = _countriesList;

            return View("ManageLanding", _model);
        }

        /// <summary>
        /// Returns the grid view with the Manage Landing List
        /// </summary>
        /// <returns></returns>
        public PartialViewResult ManageLandingListCallBack(int? pPage, int? pPageSize, LandingCampaignFilterModel pFilter)
        {
            var _pageNumber = pPage ?? 1;
            var _pageSize = pPageSize ?? 2;

            var _dataModel = landingService.GetAll(pFilter);

            IEnumerable<LandingCampaignDTO> _landingData =
                Mapper.Map<IEnumerable<LandingCampaignDTO>, IEnumerable<LandingCampaignDTO>>(
                    _dataModel.Skip((_pageNumber - 1) * _pageSize).Take(_pageSize));



            ManageLandingVM _model = new ManageLandingVM();
            _model.landingCampaignPagination = _landingData;
            _model.pageSettings = new Helpers.NSPageSettings();
            _model.pageSettings.dataItems = _dataModel.Count();
            _model.pageSettings.page = _pageNumber;
            _model.pageSettings.size = _pageSize;

            _model.filters = pFilter;

            return PartialView("_ManageLandingList", _model);
        }

        /// <summary>
        /// Resturn the landing text
        /// </summary>
        /// <param name="pLandingId">Landing Id</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetLandingText(Guid pLandingId)
        {
            var _dato = landingService.GetLandingById(pLandingId);

            if (_dato != null)
            {
                return Json(_dato.LandingText, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Json(new { error = "err", errorMessage = TranslatorHelper.TranslateTextByIdentifier("Old_RecordNotFoundAdmin") }, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// Save the landing page
        /// </summary>
        /// <param name="pModel">Model with landing data</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveLanding(SaveLandingVM pModel)
        {
            if (!ModelState.IsValid)
            {
                //Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { Success = false, Message = "" });
            }

            //Validate the landing campaign name, is unique per country
            if (landingService.ValidateLandingCampaignName(pModel.CountryId, pModel.LandingCampaign, pModel.LandingId))
            {
                //Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { Success = false, Message = TranslatorHelper.TranslateTextByIdentifier("Old_CampaignAlreadyExistsAdmin") });
            }

            //If the landingId isn´t empty update this landing
            if (pModel.LandingId != Guid.Empty && pModel.LandingId != null)
            {
                if (pModel.CountryId != -1 && pModel.CountryId != -2)
                {
                    landingService.UpdateLanding(Mapper.Map<SaveLandingVM, LandingCampaignDTO>(pModel));

                    return Json(new { Success = true, Message = TranslatorHelper.TranslateTextByIdentifier("Old_SaveRecordSuccessAdmin") });
                } else
                {
                    //Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return Json(new { Success = false, Message = "Can not update for the Latam group of countries" });
                }
                    
            }
            else
            {
                var id = Session["userId"];
                pModel.CreatedById = Guid.Parse(id.ToString());
                var _save = landingService.CreateLanding(Mapper.Map<SaveLandingVM, LandingCampaignDTO>(pModel));

                if (pModel.CountryId == -1 || pModel.CountryId == -2)
                {
                    if (_save)
                    {
                        return Json(new { Success = true, Message = TranslatorHelper.TranslateTextByIdentifier("Old_SaveRecordSuccessAdmin") });
                    } else
                    {
                        //Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        return Json(new { Success = false, Message = TranslatorHelper.TranslateTextByIdentifier("Old_CampaignAlreadyExistsAdmin") });
                    }
                } else
                {
                    return Json(new { Success = true, Message = TranslatorHelper.TranslateTextByIdentifier("Old_SaveRecordSuccessAdmin") });
                }                
            }
        }


        /// <summary>
        /// Removes a landing by Id
        /// </summary>
        /// <param name="pLandingId">Landing Id to remove</param>
        /// <returns>True/False</returns>
        [HttpPost]
        public JsonResult DeleteLanding(Guid pLandingId)
        {
            var _isExiste = leadService.GetLeadByLandingCampaignId(pLandingId);

            if (_isExiste)
            {
                //Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Json(new { IsExist = true, Message = TranslatorHelper.TranslateTextByIdentifier("Old_DeleteCampaignErrorAdmin") });
            }

            if (landingService.DeleteLanding(pLandingId))
            {
                return Json(new { IsExist = false, Message = TranslatorHelper.TranslateTextByIdentifier("Old_DeleteCampaignSuccessAdmin") }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Json(new { error = "err", errorMessage = TranslatorHelper.TranslateTextByIdentifier("Old_DeleteCampaignErrorAdmin") }, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// List of campaigns enables to replace the landing text for another, without the from landing campaign
        /// </summary>
        /// <param name="PCountryId">Country Id for search the landings</param>
        /// <param name="pLandingFrom">Landing Id from to do de change</param>
        /// <returns>Partial View with the campaings per country</returns>
        public PartialViewResult CampaignsToReplaceLanding(int pCountryId, Guid pLandingFrom)
        {
            var _model = Mapper.Map<IEnumerable<LandingCampaignDTO>, IEnumerable<LandingsPerCountryVM>>(landingService.GetAllPerCountry(pCountryId, pLandingFrom));

            return PartialView("_LandingsPerCountry", _model);
        }


        /// <summary>
        /// Replace
        /// </summary>
        /// <param name="pModel"></param>
        /// <returns></returns>
        public JsonResult ReplaceLandingToMultipleCampaing(ReplaceMultipleLandingVM pModel)
        {
            try
            {
                landingService.AssignLandingContent(Mapper.Map<ReplaceMultipleLandingVM, ReplaceMultipleLandingDTO>(pModel));
            }
            catch (Exception ex)
            {

                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { err = "" + ex.Message });
            }

            return Json(new { Ok = "Ok" });
        }

        /// <summary>
        /// check if exist a campaign in the CRM
        /// </summary>
        /// <param name="pCampaignName">Campaign Name to verified</param>
        /// <returns></returns>
        public JsonResult CheckCampaign(string pCampaignName)
        {
            var _isExistCampaign = CRMService.CheckCampaign(pCampaignName);

            if (_isExistCampaign)
            {
                return Json(new { Exist = true}, JsonRequestBehavior.AllowGet);
            } else
            {
                return Json(new { Exist = false }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}