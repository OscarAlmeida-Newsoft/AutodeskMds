using Affidavit.Helpers;
using Affidavit.Models;
using Affidavit.Models.AdminMDS;
using Affidavit.Models.Question;
using Affidavit.Utils;
using AutoMapper;
using DTOs;
using DTOs.SurveyQuestionResponse;
using DTOs.Utils;
using Entities.DbAffidavit;
using Ionic.Zip;
using IServices;
using IServices.Files;
using IServices.SurveyQuestion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Xceed.Words.NET;

namespace Affidavit.Controllers
{
#if !DEBUG
    [RequireHttps] //apply to all actions in controller
#endif
    public class AdminMDSController : BaseController
    {
        readonly ISurveyQuestionResponseService surveyQuestionResponseService;
        readonly ICompanyInfoService companyInfoService;
        readonly IProductCompanyService productCompanyService;
        readonly ICRMService CRMService;
        readonly IMDSService MDSService;
        readonly ICompanyService companyService;
        readonly IProductService productService;
        readonly IQuestionService questionService;
        readonly IAnswerCompanyService answerCompany;
        readonly IQuestionxLanguageService questionxLanguageService;
        readonly IResponseDataTypeService responseDataTypeService;
        readonly ILanguageService languageService;
        readonly IQuestionxProductFamilyService questionxProductFamilyService;
        readonly IProductFamilyService productFamilyService;
        readonly IProductCompanyFileService productCompanyFileService;




        private IAssessmentQuestionService assessmentQuestionService;
        private IAssessmentService assessmentService;
        private ITranslatorUtility translatorUtility;
        readonly ICampaignService campaignService;
        private IChooseAnActionService chooseAnActionService;
        private IAssessmentService assessmentSummaryService;

        private ITranslatorService translatorService;

        private IFileManagerService fileManagerService;

        public AdminMDSController(ISurveyQuestionResponseService pSurveyQuestionResponseService, ICRMService pCRMService, ICompanyInfoService pCompanyInfoService,
            IProductCompanyService pProductCompanyService, IMDSService pMDSService, ICompanyService pCompanyService, IProductService pProductService, IQuestionService pQuestionService,
            IAnswerCompanyService pAnswerCompany, IQuestionxLanguageService pQuestionxLanguageService, IResponseDataTypeService pResponseDataTypeService,
            ILanguageService pLanguageService, IQuestionxProductFamilyService pQuestionxProductFamilyService, IProductFamilyService pProductFamilyService,
            IProductCompanyFileService pProductCompanyFileService,

            IAssessmentQuestionService pAssessmentQuestionService,
            IAssessmentService pAssessmentService,
            ITranslatorUtility pTranslatorUtility,
            ICampaignService pCampaignService,
            IChooseAnActionService pChooseAnActionService,
            IAssessmentService pAssessmentSummaryService,
            ITranslatorService pTranslatorService,
            IFileManagerService pFileManagerService)
        {
            surveyQuestionResponseService = pSurveyQuestionResponseService;
            companyInfoService = pCompanyInfoService;
            productCompanyService = pProductCompanyService;
            CRMService = pCRMService;
            MDSService = pMDSService;
            companyService = pCompanyService;
            productService = pProductService;
            questionService = pQuestionService;
            answerCompany = pAnswerCompany;
            questionxLanguageService = pQuestionxLanguageService;
            responseDataTypeService = pResponseDataTypeService;
            languageService = pLanguageService;
            questionxProductFamilyService = pQuestionxProductFamilyService;
            productCompanyFileService = pProductCompanyFileService;


            assessmentQuestionService = pAssessmentQuestionService;
            assessmentService = pAssessmentService;
            translatorUtility = pTranslatorUtility;
            campaignService = pCampaignService;
            chooseAnActionService = pChooseAnActionService;
            assessmentSummaryService = pAssessmentSummaryService;

            translatorService = pTranslatorService;

            fileManagerService = pFileManagerService;
        }

        // GET: AdminMDS
        public ActionResult IndexAdmin()
        {
            if (Session["userLogin"] == null)
            {
                return RedirectToAction("../Home/LoginAdmin");
            }

            return View();
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
                    List<CompanySearchViewModel> _companySearch = new List<CompanySearchViewModel>();
                    //CompanyInfoViewModel _companyInfo = new CompanyInfoViewModel();
                    List<CompanyInfoViewModel> _companyInfoList = new List<CompanyInfoViewModel>();
                    List<CampaignViewModel> _campaign = new List<CampaignViewModel>();
                    List<IndustryViewModel> _industry = new List<IndustryViewModel>();
                    List<CountryViewModel> _country = new List<CountryViewModel>();

                    var _companyList = MDSCompany();


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
                                             CompanyID = companies.CompanyID,
                                             CompanyName = companies.CompanyName,
                                             IndustryID = companies.IndustryID,
                                             CountryID = companies.CountryID,
                                         };
                        }
                        else
                        {
                            _companies = from companies in _companyList
                                         join countries in _country
                                         on companies.CountryID equals countries.CountryID
                                         where countries.RegionID == _regionID
                                         where companies.CompanyName.ToLower().Contains(pCompanyName.ToLower())
                                         select new
                                         {
                                             CompanyID = companies.CompanyID,
                                             CompanyName = companies.CompanyName,
                                             IndustryID = companies.IndustryID,
                                             CountryID = countries.CountryID,
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
                        _companies = _companyList.Where(c => c.CompanyName.ToLower().Contains(pCompanyName.ToLower()));
                    }

                    foreach (var item in _companies)
                    {
                        int _companyId;
                        if (Session["vTitle"].ToString() == "License Consultant")
                        {
                            //_companyInfo = MDSCompanyInfo().Where(n => n.LeadID == item.LeadID.ToString()).FirstOrDefault();
                            _companyInfoList = MDSCompanyInfo().Where(n => n.LeadID == item.LeadID.ToString()).ToList();
                        }
                        else
                        {
                            //_companyInfo = MDSCompanyInfo().Where(n => n.CompanyID == item.CompanyID).FirstOrDefault();
                            _companyInfoList = MDSCompanyInfo().Where(n => n.CompanyID == item.CompanyID).ToList();
                        }

                        if (_companyInfoList != null)
                        {
                            foreach (var _companyInfo in _companyInfoList)
                            {
                                _companyId = _companyInfo.CompanyID;
                                if (Session["postofficebox"].ToString() == "System Administrator" || Session["postofficebox"].ToString() == "Operations Leader" || Session["vTitle"].ToString() == "SAM Consultant")
                                {
                                    _companySearch.Add(new CompanySearchViewModel
                                    {
                                        CompanyID = _companyId,
                                        CampaignName = _campaign.Where(s => s.CampaignID == _companyInfo.CampaignID).FirstOrDefault().CampaignName,
                                        CampaignID = _companyInfo.CampaignID,
                                        CompanyName = item.CompanyName,
                                        Industry = _industry.Where(s => s.IndustryID == item.IndustryID).FirstOrDefault().IndustryName,
                                        Country = _country.Where(s => s.CountryID == item.CountryID).FirstOrDefault().CountryName
                                    });
                                }
                                else
                                {
                                    _companySearch.Add(new CompanySearchViewModel
                                    {
                                        CompanyID = _companyId,
                                        CampaignName = _campaign.Where(s => s.CampaignID == _companyInfo.CampaignID).FirstOrDefault().CampaignName,
                                        CampaignID = _companyInfo.CampaignID,
                                        CompanyName = item.CompanyName,
                                        //Industry = _industry.Where(s => s.IndustryID == item.IndustryID).FirstOrDefault().IndustryName,
                                        //Country = _country.Where(s => s.CountryID == item.CountryID).FirstOrDefault().CountryName
                                    });
                                }
                            }
                        }
                    }

                    return PartialView("_CompanySearchViewPartial", _companySearch);
                }

                return RedirectToAction("../Home/LoginAdmin");

            }
            catch (Exception ex)
            {

                Response.StatusCode = 403;
                return RedirectToAction("Index", "Error", new { error = Response.StatusCode, message = ex.Message });
            }
        }


        private static IEnumerable<object[]> Read(DbDataReader reader)
        {

            var values = new List<object>();
            for (int i = 1; i < reader.FieldCount; i++)
            {
                string columnName = reader.GetSchemaTable().Rows[i][9] as string;
                values.Add(columnName);
            }
            yield return values.ToArray();
            while (reader.Read())
            {
                values = new List<object>();
                for (int i = 1; i < reader.FieldCount; i++)
                {
                    //string columnName = reader.GetSchemaTable().Rows[i][9] as string;
                    values.Add(reader.GetValue(i));
                }
                yield return values.ToArray();
            }
        }



        /// <summary>
        /// Obtiene la lista de respuestas a las preguntas adicionales.
        /// </summary>
        /// <param name="pCompanyID"></param>
        /// <param name="pCampaignID"></param>
        /// <returns></returns>
        public PartialViewResult GetAnswerCompany(int pCompanyID, short pCampaignID, int pFamilyId)
        {
            var _family = (byte)pFamilyId;
            var _lan = 1;
            using (var ctx = new AffidavitModelsConnection())
            using (var cmd = ctx.Database.Connection.CreateCommand())
            {
                ctx.Database.Connection.Open();
                cmd.CommandText = "EXEC NS_spAnswerCompany_QuestionResponses " + pCompanyID + ", " + pCampaignID + ", " + _lan + ", " + _family;
                cmd.CommandTimeout = 0;
                using (var reader = cmd.ExecuteReader())
                {
                    var model = Read(reader).ToList();
                    if (_family == 3)
                    {
                        return PartialView("_AnswerQuestionWSPartial", model);
                    }
                    else
                    {
                        return PartialView("_AnswerQuestionSQLPartial", model);
                    }
                }
            }
        }


        /// <summary>
        /// Search the MDS info by Company Id and campaign Id 
        /// </summary>
        /// <param name="pCompanyID">Company id </param>
        /// <param name="pCampaignID">Campaign id </param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SearchMDSInfo(int pCompanyID, short pCampaignID)
        {
            List<PartnerProgramListViewModel> _partnerProgramList = new List<PartnerProgramListViewModel>();
            List<PartnerCapabilityListViewModel> _partnerCapabilityList = new List<PartnerCapabilityListViewModel>();
            List<ProductListaViewModel> _productList = new List<ProductListaViewModel>();
            List<AdditonalCommentsViewModel> _additonalComments = new List<AdditonalCommentsViewModel>();
            List<ProductFamilyCompanyViewModel> _productFamilyCompany = new List<ProductFamilyCompanyViewModel>();
            CompanyInfoSearchViewModel _company = new CompanyInfoSearchViewModel();
            CompanyInfoViewModel _companyInfo = new CompanyInfoViewModel();
            CompanyContactsViewModel _companyContact = new CompanyContactsViewModel();
            List<IndustryViewModel> _industry = new List<IndustryViewModel>();


            CompanyInfoCRMViewModel _companyInfoCRM = new CompanyInfoCRMViewModel();
            CRMDataDTO _crmDataInfo = new CRMDataDTO();


            using (AffidavitModelsConnection db = new AffidavitModelsConnection())
            {
                _partnerProgramList = (from pp in db.NS_spPartnerProgramInfo_Select(pCompanyID, pCampaignID)
                                       select new PartnerProgramListViewModel
                                       {
                                           PartenerProgramName = pp.PartnerProgramName,
                                           ExpirationDate = pp.ExpirationDate,
                                           IDPartner = pp.IDPartner
                                       }).ToList();

                _partnerCapabilityList = (from pc in db.NS_spPartnerCapabilityInfo_Select(pCompanyID, pCampaignID)
                                          select new PartnerCapabilityListViewModel
                                          {
                                              PartenerCapabilityName = pc.PartnerCapabilityName,
                                              IDPartner = pc.IDPartner,
                                              Category = pc.Category,
                                              RenovationDate = pc.RenovationDate
                                          }).ToList();

                _productList = (from p in db.NS_spProductInfo_Select(pCompanyID, pCampaignID)
                                select new ProductListaViewModel
                                {
                                    ProductID = p.ProductID,
                                    ProductName = p.ProductNameDisplay,
                                    InstalledLicenses = p.InstalledLicenses,
                                    ProductVersion = p.ProductVersionGroup,
                                    ProductFamily = p.ProductFamilyName,
                                    VLS = p.VLS.Value,
                                    OEM = p.OEM.Value,
                                    FPP = p.FPP.Value,
                                    WEB = p.WEB.Value
                                }).ToList();

                _additonalComments = (from ad in db.NS_spAdditionalComments_Select(pCompanyID, pCampaignID)
                                      select new AdditonalCommentsViewModel
                                      {
                                          ProductFamilyName = ad.ProductFamilyName,
                                          AdditionalComments = ad.AdditionalComments
                                      }).ToList();

                _productFamilyCompany = (from pfc in db.NS_spProductFamilyCompany_Select(pCompanyID, pCampaignID)
                                         select new ProductFamilyCompanyViewModel
                                         {
                                             CampaignID = pfc.CampaignID,
                                             CompanyID = pfc.CompanyID,
                                             AdditionalComments = pfc.AdditionalComments,
                                             ProductFamilyID = pfc.ProductFamilyID
                                         }).ToList();

                _companyContact = (from cc in db.NS_spCompanyContact_Select(pCompanyID, pCampaignID)
                                   select new CompanyContactsViewModel
                                   {
                                       CampaignID = cc.CampaignID,
                                       CompanyID = cc.CompanyID,
                                       CompanyEmail = cc.CompanyEmail,
                                       CompanyPhone = cc.CompanyPhone,
                                       ContactName = cc.ContactName
                                   }).FirstOrDefault();

                _industry = (from i in db.NS_spIndustry_Select()
                             select new IndustryViewModel
                             {
                                 IndustryID = i.IndustryID,
                                 IndustryName = i.IndustryName,
                                 IndustrySpanishName = i.IndustrySpanishName
                             }).ToList();
            }

            _company = MDSCompany().Where(c => c.CompanyID == pCompanyID).FirstOrDefault();
            _companyInfo = MDSCompanyInfo().Where(c => c.CompanyID == pCompanyID && c.CampaignID == pCampaignID).FirstOrDefault();


            try
            {
                Guid leadGuid = new Guid(_companyInfo.LeadID);

                _crmDataInfo = CRMService.GetLeadByID(leadGuid);

                _companyInfoCRM = new CompanyInfoCRMViewModel()
                {
                    NumberOfEmployees = _crmDataInfo.NumberOfEmployees,
                    NumberOfPCs = _crmDataInfo.NumberOfPCs
                };
            }
            catch (Exception ex)
            {

                Response.StatusCode = 403;
                return RedirectToAction("Index", "Error", new { error = Response.StatusCode, message = ex.Message });
            }




            MDSInfoViewModel _infoMDS = new MDSInfoViewModel();

            _infoMDS.PartnerProgramVMList = _partnerProgramList;
            _infoMDS.PartnerCapabilityVMList = _partnerCapabilityList;
            _infoMDS.ProductsVMList = _productList.Where(p => p.InstalledLicenses != 0).ToList();

            var _productFiles = productCompanyFileService.GetByIDAndCampaign(pCompanyID, pCampaignID);
            var _productsID = _productFiles.Select(x => x.ProductID).Distinct();

            foreach (var aux in _productsID)
            {
                try
                {
                    _infoMDS.ProductsVMList.Where(x => x.ProductID == aux).FirstOrDefault().HaveFiles = true;
                    if (!_infoMDS.HaveFiles)
                    {
                        _infoMDS.HaveFiles = true;
                    }
                }
                catch (Exception e)
                {
                    using (var tx = MDSService.BeginTx())
                    {

                        try
                        {
                            //Si entra aqui es porque hay archivos para ese aux, pero el producto se elimino, esto quiere decir que se deben eliminar dichos archivos del sharepoint y de la base de datos
                            productCompanyFileService.RemoveByIDAndCampaignAndProduct(pCompanyID, pCampaignID, aux);
                            MDSService.CommitChangesAffidavit();
                            fileManagerService.DeleteAllFilesByProduct(new Guid(_companyInfo.LeadID), (short)pCompanyID, pCampaignID, aux);
                            MDSService.CommitTx(tx);
                        }
                        catch (Exception exception)
                        {
                            MDSService.RollbackTx(tx);
                        }
                    }

                }
            }


            _infoMDS.AdditionalCommentsVMList = _additonalComments;

            _infoMDS.Company = _company;
            _infoMDS.CompanyInfo = _companyInfo;
            _infoMDS.CompanyContact = _companyContact;

            //Modificado
            _infoMDS.CompanyInfoCRM = _companyInfoCRM;

            _infoMDS.ProductFamilyCompanyList = _productFamilyCompany.ToList();
            _infoMDS.IndustryName = _industry.Where(s => s.IndustryID == _company.IndustryID).FirstOrDefault().IndustryName;
            _infoMDS.IsFinalVersion = _companyInfo.IsFinalVersion;

            _infoMDS.CompanyInfo.CustomOrBasicsApplications = (_companyInfo.CustomOrBasicsApplications == "C") ? "Custom" : "Basics";
            //_infoMDS.CustomOrBasicsApplications = (_companyInfo.CustomOrBasicsApplications == "C") ? "Custom" : "Basics";
            _infoMDS.CompanyInfo.DevelopersPartnersApplicationsType = (_companyInfo.DevelopersPartnersApplicationsType != null) ? _companyInfo.DevelopersPartnersApplicationsType : "Not available";
            _infoMDS.CompanyInfo.DevelopersPartnersMicrosofTools = (_companyInfo.DevelopersPartnersMicrosofTools != null) ? _companyInfo.DevelopersPartnersMicrosofTools : "Not available";
            _infoMDS.CompanyInfo.DevelopersPartnersProjectsTypes = (_companyInfo.DevelopersPartnersProjectsTypes != null) ? _companyInfo.DevelopersPartnersProjectsTypes : "Not available";

            _infoMDS.CompanyInfo.AcademicQttyAdmEmpFullTime = (_companyInfo.AcademicQttyAdmEmpFullTime != null) ? _companyInfo.AcademicQttyAdmEmpFullTime : 0;
            _infoMDS.CompanyInfo.AcademicQttyAdmEmpPartialTime = (_companyInfo.AcademicQttyAdmEmpPartialTime != null) ? _companyInfo.AcademicQttyAdmEmpPartialTime : 0;
            _infoMDS.CompanyInfo.AcademicQttyTeachersFullTime = (_companyInfo.AcademicQttyTeachersFullTime != null) ? _companyInfo.AcademicQttyTeachersFullTime : 0;
            _infoMDS.CompanyInfo.AcademicQttyTeachersPartialTime = (_companyInfo.AcademicQttyTeachersPartialTime != null) ? _companyInfo.AcademicQttyTeachersPartialTime : 0;


            switch (_company.CompanyTypeID)
            {
                case (byte)TipoCliente.Comercial:
                    _infoMDS.TipoCliente = TipoCliente.Comercial.ToString();
                    break;
                case (byte)TipoCliente.Partner:
                    _infoMDS.TipoCliente = TipoCliente.Partner.ToString();
                    break;
                case (byte)TipoCliente.Academic:
                    _infoMDS.TipoCliente = TipoCliente.Academic.ToString();
                    break;
                case (byte)TipoCliente.DeveloperPartner:
                    _infoMDS.TipoCliente = TipoCliente.DeveloperPartner.ToString();
                    break;
                case (byte)TipoCliente.PublicSector:
                    _infoMDS.TipoCliente = TipoCliente.PublicSector.ToString();
                    break;
            }
            SharePointProvider _sharePointProvider = new SharePointProvider();

            _infoMDS.SharepointURL = _sharePointProvider.GetSharePointUrl() + "Shared Documents"+ "/" +_sharePointProvider.GetSharePointFolder() + "/"+ _infoMDS.CompanyInfo.LeadID;

            return PartialView("_MDSInfoViewPartial", _infoMDS);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<CompanyInfoSearchViewModel> MDSCompany()
        {
            List<CompanyInfoSearchViewModel> _companyList = new List<CompanyInfoSearchViewModel>();

            using (AffidavitModelsConnection db = new AffidavitModelsConnection())
            {
                _companyList = (from c in db.NS_spCompany_Select()
                                select new CompanyInfoSearchViewModel
                                {
                                    AccountNumber = c.AccountNumber,
                                    CompanyID = c.CompanyID,
                                    CompanyName = c.CompanyName,
                                    CompanyTypeID = c.CompanyTypeID,
                                    CountryID = c.CountryID,
                                    CreatedBy = c.CreatedBy,
                                    CreatedOn = c.CreatedOn,
                                    IndustryID = c.IndustryID,
                                    ModifiedBy = c.ModifiedBy,
                                    ModifiedOn = c.ModifiedOn,
                                    PurchaseAndInvoicingMode = c.PurchaseAndInvoicingMode,
                                    UpdatedFromCRMFlag = c.UpdatedFromCRMFlag
                                }).ToList();

            }

            return _companyList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<CompanyInfoViewModel> MDSCompanyInfo()
        {
            List<CompanyInfoViewModel> _companyInfo = new List<CompanyInfoViewModel>();

            using (AffidavitModelsConnection db = new AffidavitModelsConnection())
            {
                _companyInfo = (from ci in db.NS_spCompanyInfo_Select()
                                select new CompanyInfoViewModel
                                {
                                    AcademicQttyAdmEmpFullTime = ci.AcademicQttyAdmEmpFullTime,
                                    AcademicQttyAdmEmpPartialTime = ci.AcademicQttyAdmEmpPartialTime,
                                    AcademicQttyTeachersFullTime = ci.AcademicQttyTeachersFullTime,
                                    AcademicQttyTeachersPartialTime = ci.AcademicQttyTeachersPartialTime,
                                    AuthorizedChannel = ci.AuthorizedChannel,
                                    AuthorizedMicrosoftChannelFlag = ci.AuthorizedMicrosoftChannelFlag,
                                    CampaignID = ci.CampaignID,
                                    CompanyID = ci.CompanyID,
                                    CustomOrBasicsApplications = ci.CustomOrBasicsApplications,
                                    DevelopersPartnersApplicationsType = ci.DevelopersPartnersApplicationsType,
                                    DevelopersPartnersMicrosofTools = ci.DevelopersPartnersMicrosofTools,
                                    DevelopersPartnersProjectsTypes = ci.DevelopersPartnersProjectsTypes,
                                    ExactNameInInvoicing = ci.ExactNameInInvoicing,
                                    FolderCreationDate = ci.FolderCreationDate,
                                    IsFinalVersion = ci.IsFinalVersion,
                                    LeadID = ci.LeadID,
                                    LicenseStatusResponseID = ci.LicenseStatusResponseID,
                                    LogFileID = ci.LogFileID,
                                    MicrosoftPartnerContactName = ci.MicrosoftPartnerContactName,
                                    MicrosoftPartnerEmail = ci.MicrosoftPartnerEmail,
                                    MicrosoftPartnerPhoneNumber = ci.MicrosoftPartnerPhoneNumber,
                                    PlansToPurchaseNewLicensesComments = ci.PlansToPurchaseNewLicensesComments,
                                    PlansToPurchaseNewLicensesFlag = ci.PlansToPurchaseNewLicensesFlag,
                                    PlansToUpgradeCurrentlyOwnedLicensesComments = ci.PlansToUpgradeCurrentlyOwnedLicensesComments,
                                    PlansToUpgradeCurrentlyOwnedLicensesFlag = ci.PlansToUpgradeCurrentlyOwnedLicensesFlag,
                                    SoftwareAssuranceFlag = ci.SoftwareAssuranceFlag,
                                    StatusRenewalAndContratDetails = ci.StatusRenewalAndContratDetails,
                                    TotalQuantityOfDesktops = ci.TotalQuantityOfDesktops,
                                    TotalQuantityOfEmployees = ci.TotalQuantityOfEmployees,
                                    TotalQuantityOfLaptops = ci.TotalQuantityOfLaptops,
                                    TotalQuantityOfPhysicalServers = ci.TotalQuantityOfPhysicalServers,
                                    TotalQuantityOfVirtualServers = ci.TotalQuantityOfVirtualServers,
                                    TotalQuantityPCWithOtherOS = ci.TotalQuantityPCWithOtherOS,
                                    VolumeLicenceNumber1 = ci.VolumeLicenceNumber1,
                                    VolumeLicenceNumber2 = ci.VolumeLicenceNumber2,
                                    VolumeLicenceNumber3 = ci.VolumeLicenceNumber3,
                                    VolumeLicenceNumber4 = ci.VolumeLicenceNumber4,
                                    VolumeLicenceNumber5 = ci.VolumeLicenceNumber5,
                                    VolumeLicenceNumber6 = ci.VolumeLicenceNumber6,
                                    VolumeLicenceNumber7 = ci.VolumeLicenceNumber7,
                                    VolumeLicenceNumber8 = ci.VolumeLicenceNumber8,
                                    VolumeLicenceNumber9 = ci.VolumeLicenceNumber9,
                                    AzureFlag = ci.AzureFlag
                                }).ToList();

                return _companyInfo;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult SurverQuestionResponse()
        {
            if (Session["userLogin"] == null)
            {
                return RedirectToAction("../Home/LoginAdmin");
            }

            return View();
        }

        /// <summary>
        /// busca la lista de compañias que coincidan con el nombre dado en el campo de búsqueda
        /// </summary>
        /// <param name="pCompanyName">Nombre de compañia a buscar</param>
        /// <returns></returns>
        public ActionResult searchCompanySurvey(string pCompanyName)
        {
            List<CRMDataDTO> _list = new List<CRMDataDTO>();

            if (Session["postofficebox"].ToString() == "System Administrator" || Session["postofficebox"].ToString() == "Operations Leader" || Session["vTitle"].ToString() == "SAM Consultant")
            {
                _list = CRMService.GetLeadInfoByCompanyName(pCompanyName);

            }
            else if (Session["vTitle"].ToString() == "License Consultant")
            {
                _list = CRMService.GetLeadInfoByCompanyNameAndConsultant(pCompanyName, Session["vEmail"].ToString());

            }

            List<CompanySearchViewModel> _companySearch = Mapper.Map<List<CRMDataDTO>, List<CompanySearchViewModel>>(_list);

            return PartialView("_SurveyCompanyListViewPartial", _companySearch);
        }

        /// <summary>
        /// Busca la información referente a la encuesta de "Not interested"
        /// </summary>
        /// <param name="pLeadID"></param>
        /// <returns></returns>
        public ActionResult SearchSurveyQuestionInfo(Guid pLeadID)
        {
            SurveyQuestionResponseDTO _surveyQR = surveyQuestionResponseService.GetSurveyQuestionByLeadId(pLeadID);

            SurveyQuestionResponseVM _surveyVM = Mapper.Map<SurveyQuestionResponseDTO, SurveyQuestionResponseVM>(_surveyQR);

            return PartialView("_SurveyQuestionInfoPartial", _surveyVM);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pIsFinal"></param>
        [HttpPost]
        public void ChangeState(int pIsFinal, int pCompanyID, short pCampaignID, string pCompanyName)
        {
            var _isFinalVersion = (pIsFinal == 1) ? true : false;
            if (pCompanyName != null && pCompanyName.Length >= 3)
            {
                CompanyDTO _company = companyService.GetCompanyByID(pCompanyID);
                _company.CompanyName = pCompanyName;
                companyService.UpdateCompany(_company);
            }


            companyInfoService.UpdateState(_isFinalVersion, pCompanyID, pCampaignID);
        }

        /// <summary>
        /// activa compañia para acceder al MDS
        /// </summary>
        /// <param name="pIsNotInterested"></param>
        /// <param name="pLeadID">lead</param>
        [HttpPost]
        public void ActiveToMDS(int pIsNotInterested, Guid pLeadID)
        {
            surveyQuestionResponseService.UpdateIsNotInterested(pIsNotInterested, pLeadID);
        }

        /// <summary>
        ///  Guarda el número de licencias de cada tipo
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        public JsonResult SaveLicenses(List<LicensesTypesViewModel> model)
        {
            List<ProductCompanyDTO> _productsCompany = Mapper.Map<List<LicensesTypesViewModel>, List<ProductCompanyDTO>>(model);

            foreach (var item in _productsCompany)
            {
                var _licenses = item.VLS + item.OEM + item.FPP + item.WEB;
                if (_licenses > item.InstalledLicenses)
                {
                    return Json(new { _EqualLicensesNumber = false });
                }
                productCompanyService.UpdateProductcompany(item);
            }

            MDSService.CommitChangesAffidavit();

            return Json(new { _EqualLicensesNumber = true });
        }

        /// <summary>
        /// Permite descargar las preguntas adicionales a un archivo .csv
        /// </summary>
        /// <param name="pCompanyID">Id dela compañía</param>
        /// <param name="pCampaignID">Id de la campaña</param>
        public void ExportAnswersToCSV(int pCompanyID, short pCampaignID)
        {
            int _WSFamily = 3;
            int _SQLSFamily = 4;
            using (var ctx = new AffidavitModelsConnection())
            using (var cmd = ctx.Database.Connection.CreateCommand())
            {
                ctx.Database.Connection.Open();
                cmd.CommandText = "EXEC NS_spAnswerCompany_QuestionResponses " + pCompanyID + ", " + pCampaignID + ", " + 1 + ", " + _WSFamily;
                cmd.CommandTimeout = 0;
                var sb = new StringBuilder();
                using (var reader = cmd.ExecuteReader())
                {
                    var WSModel = Read(reader).ToList();
                    //Type type = typeof(productosxVersionViewModel);
                    //PropertyInfo[] properties = type.GetProperties();

                    sb.Append("Windows Server").AppendLine();
                    sb.AppendLine();
                    foreach (var item in WSModel)
                    {
                        foreach (var column in item)
                        {
                            sb.Append(column).Append(";");
                        }

                        foreach (var column in item)
                        {
                            sb.Append(" ").Append(";");
                        }

                        sb.Length--;
                        sb.AppendLine();
                    }
                }

                cmd.CommandText = "EXEC NS_spAnswerCompany_QuestionResponses " + pCompanyID + ", " + pCampaignID + ", " + 1 + ", " + _SQLSFamily;
                cmd.CommandTimeout = 0;
                using (var reader = cmd.ExecuteReader())
                {
                    var SQLModel = Read(reader).ToList();
                    sb.AppendLine();
                    sb.Append("SQL Server").AppendLine();
                    sb.AppendLine();

                    foreach (var item in SQLModel)
                    {
                        foreach (var column in item)
                        {
                            sb.Append(column).Append(";");
                        }

                        sb.Length--;
                        sb.AppendLine();
                    }
                }

                Response.ContentType = "text/csv";
                string currentDate = DateTime.Now.ToString("yyyyMMdd");
                Response.AddHeader("Content-Disposition", "attachment;filename=AnswersToAdditionalQuestios" + ".csv");
                Response.Write(sb.ToString());
                Response.End();
            }


        }

        /// <summary>
        ///     Exporta a archivo CSV 
        /// </summary>
        /// <param name="pCompanyID"></param>
        /// <param name="pCampaignID"></param>
        [HttpPost]
        public void ExportToCSV(int pCompanyID, short pCampaignID)
        {
            int _WSFamily = 3;
            int _SQLSFamily = 4;



            List<ProductListaViewModel> _productList = new List<ProductListaViewModel>();
            List<ProductsToExportViewModel> _productsToExport = new List<ProductsToExportViewModel>();
            var _companyName = companyService.Get(pCompanyID).CompanyName;
            dynamic _products = null;


            List<ProductDTO> _productsList = productService.GetAll().ToList();
            List<ProductCompanyDTO> _productCompanyList = productCompanyService.GetByIDAndCampaign(pCompanyID, pCampaignID).ToList();
            CompanyInfoDTO _commpanyInfo = companyInfoService.GetCompanyInfoByIDandCampaign(pCompanyID, pCampaignID);

            _products = from product in _productsList
                        join productCompany in _productCompanyList
                        on product.ProductID equals productCompany.ProductID
                        where productCompany.CompanyID == pCompanyID
                        where productCompany.CampaignID == pCampaignID
                        select new
                        {
                            ProductName = product.ProductName,
                            ProductVersion = product.ProductVersion,
                            InstalledLicenses = productCompany.InstalledLicenses
                        };

            foreach (var item in _products)
            {
                _productsToExport.Add(new ProductsToExportViewModel
                {
                    ProductName = item.ProductName,
                    ProductVersion = item.ProductVersion,
                    InstalledLicenses = item.InstalledLicenses,
                    //CompanyName = _companyName
                });
            }

            _productsToExport.First().CompanyName = _companyName;
            _productsToExport.First().TotalQuantityOfDesktops = (_commpanyInfo.TotalQuantityOfDesktops != null) ? _commpanyInfo.TotalQuantityOfDesktops.Value : (short)0;
            _productsToExport.First().TotalQuantityPCWithOtherOS = (_commpanyInfo.TotalQuantityPCWithOtherOS != null) ? _commpanyInfo.TotalQuantityPCWithOtherOS.Value : (short)0;
            _productsToExport.First().TotalQuantityOfPhysicalServers = (_commpanyInfo.TotalQuantityOfPhysicalServers != null) ? _commpanyInfo.TotalQuantityOfPhysicalServers.Value : (short)0;
            _productsToExport.First().TotalQuantityOfVirtualServers = (_commpanyInfo.TotalQuantityOfVirtualServers != null) ? _commpanyInfo.TotalQuantityOfVirtualServers.Value : (short)0;

            //pFiltervalues = (pFiltervalues ?? new valuesFilterSales());
            //List<EmployeeViewModel> employeesList = this.GetEmployeeList().ToList();
            Type type = typeof(ProductsToExportViewModel);
            PropertyInfo[] properties = type.GetProperties();
            var sb = new StringBuilder();

            sb.Append("PRODUCT LIST INFORMATION").AppendLine().AppendLine();

            //-------------------------------------------ENCABEZADOS---------------------------------------------
            foreach (PropertyInfo prp in properties)
            {
                if (prp.CanRead)
                {
                    sb.Append((prp.Name)).Append(';');
                }
            }

            sb.Length--;
            sb.AppendLine();
            //-------------------------------------------PRODUCTOS---------------------------------------------
            foreach (ProductsToExportViewModel _produtsList in _productsToExport)
            {
                foreach (PropertyInfo prp in properties)
                {
                    if (prp.CanRead)
                    {
                        sb.Append(prp.GetValue(_produtsList, null)).Append(';');
                    }
                }
                sb.Length--;
                sb.AppendLine();
            }

            sb.AppendLine().AppendLine();



            //-------------------------------------------PREGUNTAS DE INFRAESTRUCTURA---------------------------------------------
            using (var ctx = new AffidavitModelsConnection())
            using (var cmd = ctx.Database.Connection.CreateCommand())
            {
                ctx.Database.Connection.Open();
                cmd.CommandText = "EXEC NS_spAnswerCompany_QuestionResponses " + pCompanyID + ", " + pCampaignID + ", " + 1 + ", " + _WSFamily;
                cmd.CommandTimeout = 0;
                //var sb = new StringBuilder();
                using (var reader = cmd.ExecuteReader())
                {
                    var WSModel = Read(reader).ToList();
                    //Type type = typeof(productosxVersionViewModel);
                    //PropertyInfo[] properties = type.GetProperties();

                    sb.Append("WINDOWS SERVER").AppendLine();
                    sb.AppendLine();
                    foreach (var item in WSModel)
                    {
                        if (item.Count() > 1)
                        {
                            foreach (var column in item)
                            {
                                if (column.Equals("QuestionID"))
                                {
                                    continue;
                                }
                                else
                                {
                                    string stringAppend = column.ToString();
                                    stringAppend = stringAppend.Replace("\r", " ").Replace("\n", " ");

                                    sb.Append(stringAppend).Append(";");
                                }
                            }

                            sb.Length--;
                            sb.AppendLine();
                        }
                        else
                        {
                            sb.Append(TranslatorHelper.TranslateTextByIdentifier("Old_LabelNotAvailable",2)).AppendLine();
                            break;
                        }

                    }
                }

                cmd.CommandText = "EXEC NS_spAnswerCompany_QuestionResponses " + pCompanyID + ", " + pCampaignID + ", " + 1 + ", " + _SQLSFamily;
                cmd.CommandTimeout = 0;
                using (var reader = cmd.ExecuteReader())
                {
                    var SQLModel = Read(reader).ToList();
                    sb.AppendLine();
                    sb.Append("SQL SERVER").AppendLine();
                    sb.AppendLine();

                    foreach (var item in SQLModel)
                    {
                        if (item.Count() > 1)
                        {
                            foreach (var column in item)
                            {
                                if (column.Equals("QuestionID"))
                                {
                                    continue;
                                }
                                else
                                {
                                    string stringAppend = column.ToString();
                                    stringAppend = stringAppend.Replace("\r", " ").Replace("\n", " ");

                                    sb.Append(stringAppend).Append(";");
                                }

                            }

                            sb.Length--;
                            sb.AppendLine();
                        }
                        else
                        {
                            sb.Append(TranslatorHelper.TranslateTextByIdentifier("Old_LabelNotAvailable",2));
                            break;
                        }

                    }
                }


                //-------------------------------------------CLOUD COMPUTING---------------------------------------------

                List<AdditonalCommentsViewModel> _additonalComments;

                using (AffidavitModelsConnection db = new AffidavitModelsConnection())
                {
                    _additonalComments = (from ad in db.NS_spAdditionalComments_Select(pCompanyID, pCampaignID)
                                          select new AdditonalCommentsViewModel
                                          {
                                              ProductFamilyName = ad.ProductFamilyName,
                                              AdditionalComments = ad.AdditionalComments
                                          }).ToList();
                }

                sb.AppendLine().AppendLine();


                if (_additonalComments != null && _additonalComments.Count() > 0)
                {
                    var comment = _additonalComments.Where(x => x.ProductFamilyName.Contains("AZURE")).FirstOrDefault();


                    if (_commpanyInfo.AzureFlag != null && comment != null)
                    {
                        sb.Append("Cloud information").Append(";").
                           Append(TranslatorHelper.TranslateTextByIdentifier("Old_NewAzure1Question")).Append(";").
                           Append(TranslatorHelper.TranslateTextByIdentifier("Old_NewAzure2Question")).Append(";").
                           AppendLine();
                        sb.Append("AZURE").Append(";").
                           Append(_commpanyInfo.AzureFlag == true ? "Yes" : "No").Append(";").
                           Append(comment.AdditionalComments.ToString()).Append(";").
                           AppendLine();

                        sb.AppendLine().AppendLine();
                    }
                }

                Response.ContentType = "text/csv";
                string currentDate = DateTime.Now.ToString("yyyyMMdd");
                Response.AddHeader("Content-Disposition", "attachment;filename=MicrosoftDeploymentSummary" + ".csv");
                Response.Write(sb.ToString());
                Response.End();
            }
        }

        [HttpGet]
        public FileResult DownloadSavingInDisk()
        {
            using (ZipFile zip = new ZipFile())
            {
                zip.AddDirectory(Server.MapPath("~/Images/"));
                //zip.Save(Server.MapPath("~/Directories/hello/sample.zip"));
                //return File(Server.MapPath("~/Directories/hello/sample.zip"),
                //                           "application/zip", "sample.zip");
                zip.Save(Server.MapPath("~/Images/sample.zip"));
            }

            // Read bytes from disk
            byte[] fileBytes = System.IO.File.ReadAllBytes(
                Server.MapPath("~/Images/sample.zip"));
            string fileName = "sample.zip";

            // Return bytes as stream for download
            return File(fileBytes, "application/zip", fileName);
        }

        [HttpGet]
        public FileResult DownloadWithoutSaving(Guid pLeadID, short pCompanyID, short pCampaignID, short pProductID)
        {
            string name = "DESCARDO.zip";
            MemoryStream memStream = new MemoryStream();

            var company = companyService.GetCompanyByID(pCompanyID);


            //Se descargara grupo
            if (pProductID == -1)
            {
                name = company.CompanyName.ToUpper() + ".zip";

                var _productFiles = productCompanyFileService.GetByIDAndCampaign(pCompanyID, pCampaignID);
                var _productsID = _productFiles.Select(x => x.ProductID).Distinct();

                using (ZipFile zip = new ZipFile())
                {
                    memStream.Seek(0, SeekOrigin.Begin);

                    foreach (var productID in _productsID)
                    {
                        var _product = productService.Get(productID);
                        string productFolderName = _product.ProductName + " " + _product.ProductVersionGroup;

                        foreach (var aux in _productFiles.Where(x => x.ProductID == productID))
                        {
                            string fileName = aux.CompanyID + "_" + aux.CampaignID + "_" + aux.ProductID + "_" + aux.FileNumber + "." + aux.Extension;

                            zip.AddFile(Server.MapPath("~/Images/" + pLeadID.ToString().ToLower() + "/" + fileName), @"\" + productFolderName);
                        }

                    }



                    zip.Save(memStream);
                    memStream.Position = 0;
                }
            }
            //Se descarga para un producto especifico
            else
            {
                var _productFiles = productCompanyFileService.GetByIDAndCampaignAndProduct(pCompanyID, pCampaignID, pProductID);
                var _product = productService.Get(pProductID);

                name = company.CompanyName.ToUpper() + " - " + _product.ProductName.ToUpper() + " " + _product.ProductVersionGroup.ToUpper() + ".zip";

                using (ZipFile zip = new ZipFile())
                {
                    memStream.Seek(0, SeekOrigin.Begin);

                    foreach (var aux in _productFiles)
                    {
                        string fileName = aux.CompanyID + "_" + aux.CampaignID + "_" + aux.ProductID + "_" + aux.FileNumber + "." + aux.Extension;
                        zip.AddFile(Server.MapPath("~/Images/" + pLeadID.ToString().ToLower() + "/" + fileName), @"\");
                    }
                    zip.Save(memStream);
                    memStream.Position = 0;
                }
            }


            return File(memStream, "application/zip", name);
        }


        public ActionResult IndexAssestment()
        {
            if (Session["userLogin"] == null)
            {
                return RedirectToAction("../Home/LoginAdmin");
            }

            return View();
        }
        /// <summary>
        /// Search the MDS info by Company Id and campaign Id 
        /// </summary>
        /// <param name="pCompanyID">Company id </param>
        /// <param name="pCampaignID">Campaign id </param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SearchMDSAssestmentInfo(int pCompanyID, short pCampaignID, int pLanguageId)
        {
            //AssessmentViewModel _model = new AssessmentViewModel();

            //AssessmentSummaryDTO _summary = assessmentService.GetAssessmentsSummaryById(1);

            //var _data = Mapper.Map<IEnumerable<AssessmentQuestionDTO>, IEnumerable<AssessmentQuestionsViewModel>>(assessmentQuestionService.GetAssessmentQuestions(_summary));
            var _company = companyInfoService.GetCompanyInfoByIDandCampaign(pCompanyID, pCampaignID);
            List<AssessmentViewModel> _assessmentsList = new List<AssessmentViewModel>();
            AssessmentAdminViewModel _model = new AssessmentAdminViewModel();
            _model.LanguageID = pLanguageId;
            TranslatorHelper.SetCulture(pLanguageId);

            if (_company != null)
            {
                Guid _leadId = new Guid(_company.LeadID);
                Guid _crmCampaingID = new Guid(campaignService.Get(_company.CampaignID).CRMCampaignID);

                ChooseAnActionSummaryDTO _info = chooseAnActionService.GetLeadGeneralProgress(_leadId, _crmCampaingID);


                AssessmentGridViewModel _actualAssestments = new AssessmentGridViewModel();

                _actualAssestments.AssessmentDetails = Mapper.Map<IEnumerable<AssessmentSummaryDTO>, IEnumerable<AssessmentGridViewModelDetail>>(assessmentSummaryService.GetAssessmentsSummary(_leadId, _crmCampaingID)).ToList();


                foreach (AssessmentGridViewModelDetail aux in _actualAssestments.AssessmentDetails)
                {
                    if (aux.IsInProgress)
                    {
                        AssessmentViewModel _assestment = new AssessmentViewModel();

                        AssessmentSummaryDTO _summary = assessmentService.GetAssessmentsSummaryById(aux.AssessmentSummaryId);

                        var _data = Mapper.Map<IEnumerable<AssessmentQuestionDTO>, IEnumerable<AssessmentQuestionsViewModel>>(assessmentQuestionService.GetAssessmentQuestions(_summary));

                        _assestment.AssessmentQuestions = _data;
                        _assestment.AssessmentTypeTranslatorId = _summary.AssessmentTypeTraslatorId;
                        _assestment.CompanyId = _summary.CompanyId;
                        _assestment.CampaignId = _summary.CampaignId;
                        _assestment.AssessmentSummaryId = _summary.Id;
                        _assestment.IconName = _summary.IconName;
                        _assestment.AssessmentTypeId = _summary.AssessmentTypeId;
                        _assestment.TotalQuestion = _data.Count();
                        _assestment.AnsweredQuestions = _data.Count(x => x.SelectedMaturityLevelId != null);
                        _assestment.AssessmentMaturityLevelTranslatorId = _summary.GlobalMaturityLevelTranslatorId;
                        _assestment.CompanyId = _leadId;
                        _assestment.CampaignId = _crmCampaingID;
                        _assestment.IsFinal = aux.DownloadPDF;
                        _assestment.IsIndustryInsights = _summary.IsIndustryInsights;
                        

                        _assessmentsList.Add(_assestment);
                    }
                }

                _model.AssessmentList = _assessmentsList;
                _model.CompanyID = _leadId;
                _model.CampaignID = _crmCampaingID;
                _model.IdCompany = pCompanyID;
                _model.IdCampaign = pCampaignID;
            }

            return PartialView("_MDSAssestmentInfoViewPartial", _model);
        }

        public JsonResult UnlockUserAssessment(int pAssessmentSummaryId)
        {
            assessmentService.UnlockAssessment(pAssessmentSummaryId);
            return Json(new { error = "false" });
        }

        public void GenerateWordOldTemplate(Guid pCompanyID, Guid pCampaignID, short pAssessmentSummaryId, int pLanguageId)
        {
            DocX _template = null;

            AssessmentSummaryDTO _summary = assessmentService.GetAssessmentsSummaryById(pAssessmentSummaryId);
            if (_summary != null)
            {
                if (pCompanyID == _summary.CompanyId && pCampaignID == _summary.CampaignId)
                {
                    var _assessmentQA = Mapper.Map<IEnumerable<AssessmentQuestionDTO>, IEnumerable<AssessmentQuestionsViewModel>>(assessmentQuestionService.GetAssessmentQuestions(_summary));

                    if (_assessmentQA != null)
                    {
                        AssessmentRecommendationsDTO _recommendationsInfo;
                        DateTime _currentDate;
                        string _fileName;
                        string _serverFileDestination;
                        string _templateLocation;

                        //Para las recomendaciones
                        _recommendationsInfo = assessmentQuestionService.AssessmentScoringModel(_summary.AssessmentTypeId, pCompanyID, pCampaignID);

                        if(_recommendationsInfo != null)
                        {
                            var introductionText = "Vestibulum odio odio, ornare eu placerat in, aliquam at ligula. Quisque in condimentum magna. Aenean facilisis enim ultricies scelerisque finibus. Etiam faucibus diam sit amet ipsum interdum suscipit. Sed orci nunc, tempor nec ultrices sed, elementum vel nulla. Nulla quis tellus gravida, molestie dui eget, eleifend sapien. Nullam augue nisi, blandit non dignissim ut, cursus eu sem. Nam eget ex tincidunt, gravida metus sit amet, consectetur quam. Donec quam lorem, blandit eget vestibulum vitae, pharetra in quam. Morbi venenatis libero vehicula urna gravida vehicula. Proin in pharetra lorem, quis tempus lorem. Fusce ullamcorper libero lacinia dolor scelerisque, eu pharetra purus pharetra. Phasellus nec velit tempus, interdum purus sit amet, ultricies diam. Sed sit amet tempor est. Cras tincidunt eu nunc sit amet accumsan.";
                            var finalText = "Nam nibh urna, volutpat eu ligula at, gravida iaculis lacus. Nam mattis quis dolor ullamcorper vulputate. Phasellus imperdiet id ex id fringilla. Sed luctus varius nunc, eget feugiat tortor. Suspendisse hendrerit urna felis, ac lacinia tellus placerat vitae. Vivamus malesuada turpis blandit, tincidunt velit sed, dignissim felis. Morbi id finibus tellus. Aliquam ultrices vitae metus ac congue. Mauris mattis tellus id vestibulum porttitor. Donec semper, neque vel auctor feugiat, nulla neque semper leo, sed faucibus dui justo quis magna. Vestibulum pretium vitae eros sed ultrices. Sed ac tellus tellus. Etiam ullamcorper metus eu interdum vehicula. Curabitur ornare id sapien ut interdum. In vel nibh a lorem hendrerit vestibulum eget eget dui.";
                            string consultantName;
                            string consultantTitle;

                            CRMDataDTO _leadCRM = CRMService.GetLeadByID(pCompanyID);
                            consultantName = _leadCRM.ConsultantName;
                            consultantTitle = CRMService.GetConsultantTitle(pCompanyID);

                            //Para generar el nombre del archivo y crearlo
                            _currentDate = DateTime.Now;
                            _fileName = TranslatorHelper.TranslateTextById(_summary.AssessmentTypeTraslatorId, pLanguageId) + "_" + _currentDate.Month + "_" + _currentDate.Day + "_" + _currentDate.Year + ".docx".Replace(" ", string.Empty);
                            _serverFileDestination = Server.MapPath("~/WordFiles/" + _fileName);
                            _templateLocation = Server.MapPath("~/App_Data/template.docx");
                            _template = DocX.Load(_templateLocation);

                            
                            
                            //int widhtAssessmentIcon = _template.Tables[1].Rows[1].Cells[0].Paragraphs[0].Pictures[0].Width;
                            //int heightAssessmentIcon = _template.Tables[1].Rows[1].Cells[0].Paragraphs[0].Pictures[0].Height;

                            //int widhtLevelIcon = _template.Tables[1].Rows[2].Cells[0].Paragraphs[0].Pictures[0].Width;
                            //int heightLevelIcon = _template.Tables[1].Rows[2].Cells[0].Paragraphs[0].Pictures[0].Height;

                            ////Se elimina la imagen de prueba
                            //_template.Tables[1].Rows[1].Cells[0].Paragraphs[0].Pictures[0].Remove();
                            //_template.Tables[1].Rows[2].Cells[0].Paragraphs[0].Pictures[0].Remove();

                            //if (_summary.IconName.Equals("icono_cybersecurity.png"))
                            //{
                            //    widhtAssessmentIcon = widhtAssessmentIcon - 7;
                            //}
                            //var assessmentIcon = _template.AddImage(Server.MapPath("~/Content/Images/"+ _summary.IconName ));      
                            //var imageLevel = _template.AddImage(Server.MapPath("~/Content/Images/" + string.Format("level-{0}.jpg", _summary.GlobalMaturityLevelId)));

                            //var pictureAssessmentIcon = assessmentIcon.CreatePicture(heightAssessmentIcon, widhtAssessmentIcon);
                            //var pictureLevel = imageLevel.CreatePicture(heightLevelIcon, widhtLevelIcon);


                            ////Aqui se inserta el icono del assessment
                            // _template.Tables[1].Rows[1].Cells[0].Paragraphs[0].InsertPicture(pictureAssessmentIcon);
                            
                            ////Aqui se inserta y centra la imagen del nivel en que quedo la compañia
                            //_template.Tables[1].Rows[2].Cells[0].Paragraphs[0].AppendPicture(pictureLevel);
                            //_template.Tables[1].Rows[2].Cells[0].Paragraphs[0].Alignment = Alignment.center;


                            _template.AddCustomProperty(new CustomProperty("assessment_name", TranslatorHelper.TranslateTextById(_summary.AssessmentTypeTraslatorId, pLanguageId) + " Assessment"));
                            _template.AddCustomProperty(new CustomProperty("assessment_level", TranslatorHelper.TranslateTextById(_summary.GlobalMaturityLevelTranslatorId ?? 0, pLanguageId)));
                            _template.AddCustomProperty(new CustomProperty("introduction_text", introductionText));


                            var recommendations = _template.Paragraphs.Where(x => x.Text.Equals("{Here comes the assessment recommendation}")).FirstOrDefault();
                            var inicial = recommendations;
                             
                            int i = 1;

                            foreach(var currentQA in _assessmentQA)
                            {
                                var question = inicial.InsertParagraphAfterSelf(i + ". " + TranslatorHelper.TranslateTextById(currentQA.TranslatorAdministratorId, pLanguageId)).FontSize(16d).Font("Arial").Color(ColorTranslator.FromHtml("#3BAFDE"));/*.Color(System.Drawing.Color.DeepSkyBlue);*/ //3bafde
                                var answer = question.InsertParagraphAfterSelf(currentQA.SelectedMaturityLevelId +
                                    ": " + TranslatorHelper.TranslateTextById(currentQA.Options.Where(x => x.MaturityLevelId == currentQA.SelectedMaturityLevelId).FirstOrDefault().TranslatorAdministratorId, pLanguageId)).FontSize(14d).Color(ColorTranslator.FromHtml("#7C7C81")).Font("Arial");
                                answer.SpacingAfter(3f);
                                answer.SpacingBefore(3f);
                                var _recommendationTextTranslations = _recommendationsInfo.TextTranslationsIds.Where(x => x.MaturityLevelId == currentQA.SelectedMaturityLevelId && x.AssessmentQuestionId == currentQA.Id).FirstOrDefault();

                                if (_recommendationTextTranslations != null)
                                {
                                    var recommendation = answer.InsertParagraphAfterSelf(TranslatorHelper.TranslateTextById(_recommendationTextTranslations.TextTranslationsId, pLanguageId)).FontSize(12d).Font("Arial").Color(ColorTranslator.FromHtml("#B9B8BC")).InsertParagraphAfterSelf("").InsertParagraphAfterSelf("");
                                    inicial = recommendation;
                                }
                                else
                                {
                                    inicial = answer;
                                }
                                i++;
                            }

                            _template.AddCustomProperty(new CustomProperty("assessment_recommendation", ""));
                            _template.AddCustomProperty(new CustomProperty("final_text", finalText));
                            _template.AddCustomProperty(new CustomProperty("consultant_name", consultantName));
                            _template.AddCustomProperty(new CustomProperty("consultant_title", consultantTitle));
                            

                            MemoryStream ms = new MemoryStream();
                            _template.SaveAs(ms);
                            byte[] byteArray = ms.ToArray();
                            ms.Flush();
                            ms.Close();
                            ms.Dispose();

                            //Delete File
                            if (System.IO.File.Exists(_serverFileDestination))
                            {
                                System.IO.File.Delete(_serverFileDestination);
                            }


                            Response.Clear();
                            Response.AddHeader("Content-Disposition", "attachment; filename=" + _fileName);
                            Response.AddHeader("Content-Length", byteArray.Length.ToString());
                            Response.BinaryWrite(byteArray);
                            Response.End();
                        }

                    }
                }

            }
        }

        public void GenerateWordNewTemplate(Guid pCompanyID, Guid pCampaignID, short pAssessmentSummaryId, int pLanguageId)
        {
            DocX _template = null;

            AssessmentSummaryDTO _summary = assessmentService.GetAssessmentsSummaryById(pAssessmentSummaryId);
            if (_summary != null)
            {
                if (pCompanyID == _summary.CompanyId && pCampaignID == _summary.CampaignId)
                {
                    var _assessmentQA = Mapper.Map<IEnumerable<AssessmentQuestionDTO>, IEnumerable<AssessmentQuestionsViewModel>>(assessmentQuestionService.GetAssessmentQuestions(_summary));

                    if (_assessmentQA != null)
                    {
                        AssessmentRecommendationsDTO _recommendationsInfo;
                        DateTime _currentDate;
                        string _fileName;
                        string _serverFileDestination;
                        string _templateLocation;

                        //Para las recomendaciones
                        _recommendationsInfo = assessmentQuestionService.AssessmentScoringModel(_summary.AssessmentTypeId, pCompanyID, pCampaignID);

                        if (_recommendationsInfo != null)
                        {
                            var introductionText = "Vestibulum odio odio, ornare eu placerat in, aliquam at ligula. Quisque in condimentum magna. Aenean facilisis enim ultricies scelerisque finibus. Etiam faucibus diam sit amet ipsum interdum suscipit. Sed orci nunc, tempor nec ultrices sed, elementum vel nulla. Nulla quis tellus gravida, molestie dui eget, eleifend sapien. Nullam augue nisi, blandit non dignissim ut, cursus eu sem. Nam eget ex tincidunt, gravida metus sit amet, consectetur quam. Donec quam lorem, blandit eget vestibulum vitae, pharetra in quam. Morbi venenatis libero vehicula urna gravida vehicula. Proin in pharetra lorem, quis tempus lorem. Fusce ullamcorper libero lacinia dolor scelerisque, eu pharetra purus pharetra. Phasellus nec velit tempus, interdum purus sit amet, ultricies diam. Sed sit amet tempor est. Cras tincidunt eu nunc sit amet accumsan.";
                            var finalText = "Nam nibh urna, volutpat eu ligula at, gravida iaculis lacus. Nam mattis quis dolor ullamcorper vulputate. Phasellus imperdiet id ex id fringilla. Sed luctus varius nunc, eget feugiat tortor. Suspendisse hendrerit urna felis, ac lacinia tellus placerat vitae. Vivamus malesuada turpis blandit, tincidunt velit sed, dignissim felis. Morbi id finibus tellus. Aliquam ultrices vitae metus ac congue. Mauris mattis tellus id vestibulum porttitor. Donec semper, neque vel auctor feugiat, nulla neque semper leo, sed faucibus dui justo quis magna. Vestibulum pretium vitae eros sed ultrices. Sed ac tellus tellus. Etiam ullamcorper metus eu interdum vehicula. Curabitur ornare id sapien ut interdum. In vel nibh a lorem hendrerit vestibulum eget eget dui.";
                            string consultantName;
                            string consultantTitle;

                            CRMDataDTO _leadCRM = CRMService.GetLeadByID(pCompanyID);
                            consultantName = _leadCRM.ConsultantName;
                            consultantTitle = CRMService.GetConsultantTitle(pCompanyID);

                            //Para generar el nombre del archivo y crearlo
                            _currentDate = DateTime.Now;
                            _fileName = TranslatorHelper.TranslateTextById(_summary.AssessmentTypeTraslatorId, pLanguageId) + "_" + _currentDate.Month + "_" + _currentDate.Day + "_" + _currentDate.Year + ".docx".Replace(" ", string.Empty);
                            _serverFileDestination = Server.MapPath("~/WordFiles/" + _fileName);


                            var assessmentName = "";

                            var font = "Segoe UI";
                            var questionColor = "";
                            var answerColor = "#808080";
                            var recommendationColor = "#A6A6A6";

                            switch (_summary.AssessmentTypeId)
                            {
                                case ConstantsStringKeys.CYBERSECURITY_ID_KEY:
                                    _templateLocation = Server.MapPath("~/App_Data/template/template_cybersecurity.docx");
                                    questionColor = "#FFC000";
                                    assessmentName = TranslatorHelper.TranslateTextByIdentifier("Cybersecurity_Asssessment_Template", pLanguageId);
                                    break;
                                case ConstantsStringKeys.CLOUD_ID_KEY:
                                    _templateLocation = Server.MapPath("~/App_Data/template/template_cloud_readiness.docx");
                                    questionColor = "#F94107";
                                    assessmentName = TranslatorHelper.TranslateTextByIdentifier("Cloud_Readiness_Asssessment_Template", pLanguageId);
                                    break;
                                case ConstantsStringKeys.PRODUCTIVITY_ID_KEY:
                                    _templateLocation = Server.MapPath("~/App_Data/template/template_productivity.docx");
                                    assessmentName = TranslatorHelper.TranslateTextByIdentifier("Productivity_Assessment_Template", pLanguageId);
                                    questionColor = "#0EABF2";
                                    break;
                                case ConstantsStringKeys.INDUSTRY_ID_KEY:
                                    _templateLocation = Server.MapPath("~/App_Data/template/template_industry_insights.docx");
                                    questionColor = "#2595D3";
                                    assessmentName = TranslatorHelper.TranslateTextByIdentifier("Industry_Insight_Asssessment_Template", pLanguageId);
                                    break;
                                default:
                                    _templateLocation = "";
                                    questionColor= "#FFC000";
                                    break;
                            }
                            
                            _template = DocX.Load(_templateLocation);

                            string _titleText = TranslatorHelper.TranslateTextByIdentifier("Assessment_Sumary_Template", pLanguageId).Replace("<>", "-");
                            string[] _titleParts = _titleText.Split('-');

                            _template.AddCustomProperty(new CustomProperty("assessment_word", _titleParts[0]));
                            _template.AddCustomProperty(new CustomProperty("summary_word", _titleParts[1]));

                            _template.AddCustomProperty(new CustomProperty("assessment_name", assessmentName));
                            //_template.AddCustomProperty(new CustomProperty("assessment_level", TranslatorHelper.TranslateTextById(_summary.GlobalMaturityLevelTranslatorId ?? 0, pLanguageId)));
                            //_template.AddCustomProperty(new CustomProperty("introduction_text", introductionText));


                            var recommendations = _template.Paragraphs.Where(x => x.Text.Equals("{Question}")).FirstOrDefault();
                            var inicial = recommendations;




                            foreach (var currentQA in _assessmentQA)
                            {
                                var question = inicial.InsertParagraphAfterSelf(TranslatorHelper.TranslateTextById(currentQA.TranslatorAdministratorId, pLanguageId)).FontSize(16d).Font(font).Color(ColorTranslator.FromHtml(questionColor));
                                var answer = question.InsertParagraphAfterSelf(TranslatorHelper.TranslateTextById(currentQA.Options.Where(x => x.MaturityLevelId == currentQA.SelectedMaturityLevelId).FirstOrDefault().TranslatorAdministratorId, pLanguageId)).FontSize(16d).Font(font).Color(ColorTranslator.FromHtml(answerColor));
                                answer.SpacingAfter(3f);
                                answer.SpacingBefore(3f);
                                var _recommendationTextTranslations = _recommendationsInfo.TextTranslationsIds.Where(x => x.MaturityLevelId == currentQA.SelectedMaturityLevelId && x.AssessmentQuestionId == currentQA.Id).FirstOrDefault();

                                if (_recommendationTextTranslations != null)
                                {
                                    var recommendation = answer.InsertParagraphAfterSelf(TranslatorHelper.TranslateTextById(_recommendationTextTranslations.TextTranslationsId, pLanguageId)).FontSize(11d).Font(font).Color(ColorTranslator.FromHtml(recommendationColor)).InsertParagraphAfterSelf("");
                                    inicial = recommendation;
                                }
                                else
                                {
                                    inicial = answer;
                                }
                            }
                            recommendations.Remove(false);


                            //_template.AddCustomProperty(new CustomProperty("assessment_recommendation",""));
                            //_template.AddCustomProperty(new CustomProperty("final_text", finalText));
                            _template.AddCustomProperty(new CustomProperty("salutation_text", TranslatorHelper.TranslateTextByIdentifier("Sincerely_Template", pLanguageId)));
                            _template.AddCustomProperty(new CustomProperty("consultant_name", consultantName));
                            _template.AddCustomProperty(new CustomProperty("consultant_title", consultantTitle));
                            


                            MemoryStream ms = new MemoryStream();
                            _template.SaveAs(ms);
                            byte[] byteArray = ms.ToArray();
                            ms.Flush();
                            ms.Close();
                            ms.Dispose();

                            //Delete File
                            if (System.IO.File.Exists(_serverFileDestination))
                            {
                                System.IO.File.Delete(_serverFileDestination);
                            }


                            Response.Clear();
                            Response.AddHeader("Content-Disposition", "attachment; filename=" + _fileName);
                            Response.AddHeader("Content-Length", byteArray.Length.ToString());
                            Response.BinaryWrite(byteArray);
                            Response.End();
                        }

                    }
                }

            }
        }
    }
}