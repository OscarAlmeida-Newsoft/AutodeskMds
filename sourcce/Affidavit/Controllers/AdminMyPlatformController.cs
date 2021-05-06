using Affidavit.Models.AdminMDS;
using DTOs.Utils;
using Entities.DbAffidavit;
using IServices;
using IServices.Files;
using IServices.SurveyQuestion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Affidavit.Controllers
{
    public class AdminMyPlatformController : BaseController
    {
        readonly ILeadService leadService;
        readonly ICompanyInfoService companyInfoService;
        readonly IChooseAnActionService chooseAnActionService;
        readonly ICRMService CRMService;

        public AdminMyPlatformController(ILeadService pLeadService, ICompanyInfoService pCompanyInfoService, IChooseAnActionService pChooseAnActionService, ICRMService pCRMService)
        {
            leadService = pLeadService;
            companyInfoService = pCompanyInfoService;
            chooseAnActionService = pChooseAnActionService;
            CRMService = pCRMService;
        }

        // GET: AdminMyPlatform
        public ActionResult Index()
        {
            if (Session["userLogin"] == null)
            {
                return RedirectToAction("../Home/LoginAdmin");
            }

            return View("IndexMyPlatform");
        }



        /// <summary>
        /// Search the MDS info by Company Id and campaign Id 
        /// </summary>
        /// <param name="pCompanyID">Company id </param>
        /// <param name="pCampaignID">Campaign id </param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SearchMDSMyPlatformInfo(Guid pLeadID)
        {
            List<List<string>> lista = new List<List<string>>();
            
            var _lead = leadService.GetByLeadID(pLeadID);

            if (_lead != null && _lead.SAM360OrganisationId.HasValue)
            {
                lista = chooseAnActionService.GetSAM360Reports(_lead.SAM360OrganisationId.Value, "HardwareAllActiveInScopeComputersGeneralDetails");
                //lista = chooseAnActionService.GetSAM360Reports(7126, 131.ToString());
            }
            List<string> reportList = new List<string>();
            reportList.Add("HardwareAllActiveInScopeComputersGeneralDetails");
            reportList.Add("HardwareAllWindowsDomainsGeneralDetails");
            reportList.Add("UsersAllActiveUsersGeneralDetails");
            reportList.Add("OrganisationsAllActiveClientOrganisationsGeneralDetails");

            ViewBag.ReportList = reportList;
            ViewBag.LeadID = pLeadID;
            //Obtener reportes para ese usuario
            return PartialView("_MDSMyPlatformInfoViewPartial", lista);
        }

        [HttpPost]
        public ActionResult GetSelectedReportInfo(Guid pLeadID, string pReportName)
        {

            List<List<string>> lista = new List<List<string>>();

            var _lead = leadService.GetByLeadID(pLeadID);


            if (_lead != null && _lead.SAM360OrganisationId.HasValue)
            {
                lista = chooseAnActionService.GetSAM360Reports(_lead.SAM360OrganisationId.Value, pReportName);
                //lista = chooseAnActionService.GetSAM360Reports(7126, 131.ToString());
            }
            return PartialView("_MDSReportTable", lista);
        }






        /// <summary>
        /// realiza la busqueda de la compañia a la que se quiere consultar el formulario MDS
        /// </summary>
        /// <param name="pCompanyName">Nombre de la compañia</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SearchCompany(string pCompanyName)
        {
            try
            {
                if (Session["vCountryName"] != null)
                {
                    List<LeadSearchViewModel> _companySearch = new List<LeadSearchViewModel>();

                    List<CampaignViewModel> _campaign = new List<CampaignViewModel>();
                    List<IndustryViewModel> _industry = new List<IndustryViewModel>();
                    List<CountryViewModel> _country = new List<CountryViewModel>();

                    var _companyList = MDSCompany(pCompanyName);


                    using (AffidavitModelsConnection db = new AffidavitModelsConnection())
                    {
                        _campaign = (from c in db.NS_spCampaign_Select()
                                     select new CampaignViewModel
                                     {
                                         CampaignID = c.CampaignID,
                                         CampaignName = c.CampaignName,
                                         CRMCampaignID = c.CRMCampaignID
                                     }).ToList();

                        _country = (from c in db.NS_spCountry_Select()
                                    select new CountryViewModel
                                    {
                                        CountryID = c.CountryID,
                                        CountryName = c.CountryName,
                                        RegionID = c.RegionID
                                    }).ToList();

                        _industry = (from i in db.NS_spIndustry_Select()
                                     select new IndustryViewModel
                                     {
                                         IndustryID = i.IndustryID,
                                         IndustryName = i.IndustryName,
                                         IndustrySpanishName = i.IndustrySpanishName
                                     }).ToList();
                    }



                    //var _countryUser = _country.Where(c => c.CountryName == Session["vCountryName"].ToString()).FirstOrDefault();
                    var _regionUser = CRMService.GetUserRegionByEmail(Session["vEmail"].ToString());
                    var _companyName = "/" + pCompanyName + "/";
                    short _regionID = 0;

                    switch (_regionUser)
                    {
                        case "America":
                            _regionID = 1;
                            break;
                        case "Europa":
                            _regionID = 3;
                            break;
                        case "Asia":
                            _regionID = 2;
                            break;
                    }
                    //_companyList = _companyList.Where(c => c.CompanyName.Contains(pCompanyName) && c.CountryID == _countryUser.CountryID).ToList();
                    dynamic _companies = null;

                    if (Session["postofficebox"].ToString() == "Operations Leader" || Session["vTitle"].ToString() == "SAM Consultant")
                    {
                        if (_regionUser.Equals("Echez Inc."))
                        {
                            _companies = from companies in _companyList
                                         where companies.CompanyName.ToLower().Contains(pCompanyName.ToLower())
                                         select new
                                         {
                                             LeadID = companies.LeadId,
                                             CompanyName = companies.CompanyName,
                                             CampaignName = companies.CampaignName,
                                             CountryID = companies.CountryID,
                                             SAM360Info= companies.SAM360Info
                                         };
                        }
                        else
                        {
                            _companies = from companies in _companyList
                                         join countries in _country
                                         on companies.CountryID equals countries.CountryID
                                         where countries.RegionID == _regionID
                                         //where companies.CompanyName.ToLower().Contains(pCompanyName.ToLower())
                                         select new
                                         {
                                             LeadID = companies.LeadId,
                                             CompanyName = companies.CompanyName,
                                             CampaignName = companies.CampaignName,
                                             CountryID = companies.CountryID,
                                             SAM360Info = companies.SAM360Info
                                         };
                        }
                    }
                    else if (Session["vTitle"].ToString() == "License Consultant")
                    {
                        _companies = CRMService.GetLeadInfoByCompanyNameAndConsultant(pCompanyName, Session["vEmail"].ToString());
                        //_companies = _CRMDataList.Where(c => c.MicrosoftConsultantEmail == Session["vEmail"].ToString()).ToList();
                    }
                    else if (Session["postofficebox"].ToString() == "System Administrator")
                    {
                        _companies = _companyList;
                    }


                    foreach (var item in _companies)
                    {
                        if (Session["postofficebox"].ToString() == "System Administrator" || Session["postofficebox"].ToString() == "Operations Leader" || Session["vTitle"].ToString() == "SAM Consultant")
                        {
                            _companySearch.Add(new LeadSearchViewModel
                            {
                                LeadId = item.LeadId,
                                CampaignName = item.CampaignName,
                                CompanyName = item.CompanyName,
                                Country = _country.Where(s => s.CountryID == item.CountryID).FirstOrDefault().CountryName,
                                SAM360Info = item.SAM360Info
                            });
                        }
                        else
                        {
                            _companySearch.Add(new LeadSearchViewModel
                            {
                                LeadId = item.LeadId,
                                CampaignName = item.CampaignName,
                                CompanyName = item.CompanyName,
                                SAM360Info = item.SAM360Info
                                //Industry = _industry.Where(s => s.IndustryID == item.IndustryID).FirstOrDefault().IndustryName,
                                //Country = _country.Where(s => s.CountryID == item.CountryID).FirstOrDefault().CountryName
                            });
                        }


                    }
                    _companySearch = _companySearch.OrderBy(x=>x.CompanyName).OrderByDescending(x => x.SAM360Info).ToList();

                    return PartialView("_LeadSearchViewPartial", _companySearch);
                }

                return RedirectToAction("../Home/LoginAdmin");

            }
            catch (Exception ex)
            {

                Response.StatusCode = 403;
                return RedirectToAction("Index", "Error", new { error = Response.StatusCode, message = ex.Message });
            }
        }



        public List<LeadSearchViewModel> MDSCompany(string pCompanyName)
        {
            List<LeadSearchViewModel> _companyList = new List<LeadSearchViewModel>();

            _companyList = (from c in leadService.GetByUserName(pCompanyName)
                            select new LeadSearchViewModel
                            {
                                CompanyName = c.CompanyName,
                                CampaignName = c.CompanyName,
                                CountryID = c.CountryID,
                                LeadId = c.LeadId,
                                SAM360Info = c.SAM360UserId != null && c.SAM360CompanyPassword != null ? true : false

                            }).ToList();

            //using (AffidavitModelsConnection db = new AffidavitModelsConnection())
            //{
            //    _companyList = (from c in db.NS_spCompany_Select()
            //                    select new LeadSearchViewModel
            //                    {
            //                        AccountNumber = c.AccountNumber,
            //                        CompanyID = c.CompanyID,
            //                        CompanyName = c.CompanyName,
            //                        CompanyTypeID = c.CompanyTypeID,
            //                        CountryID = c.CountryID,
            //                        CreatedBy = c.CreatedBy,
            //                        CreatedOn = c.CreatedOn,
            //                        IndustryID = c.IndustryID,
            //                        ModifiedBy = c.ModifiedBy,
            //                        ModifiedOn = c.ModifiedOn,
            //                        PurchaseAndInvoicingMode = c.PurchaseAndInvoicingMode,
            //                        UpdatedFromCRMFlag = c.UpdatedFromCRMFlag
            //                    }).ToList();

            //}

            return _companyList;
        }

    }
}