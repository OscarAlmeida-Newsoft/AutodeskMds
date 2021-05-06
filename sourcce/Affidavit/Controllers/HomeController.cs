namespace Affidavit.Controllers
{
    using System.Web.Mvc;
    using Models;
    using System.Collections.Generic;
    using System;
    using Utils;
    using Models.Home;
    using IServices;
    using DTOs;
    using IServices.Landing;
    using DTOs.SurveyQuestionResponse;
    using IServices.SurveyQuestion;
    using System.DirectoryServices.AccountManagement;
    using System.DirectoryServices;
    using Helpers;
    using System.Web;
    using IServices.Users;
    using HiQPdf;
    using Services;
    using global::AutoMapper;
    using System.Linq;
    using System.Configuration;
    using Ionic.Zip;
    using System.IO;
    using DTOs.Utils;
    using Microsoft.CookieCompliance;
    using Microsoft.CookieCompliance.NetStd.IP2Geo;
    using Microsoft.CookieCompliance.IPAddressResolver;

#if !DEBUG
    [RequireHttps] //apply to all actions in controller
#endif
    public class HomeController : BaseController
    {
        readonly ICRMService CRMService;
        readonly ILeadService leadService;
        readonly ILandingCampaignService landingCampaignService;
        readonly ISurveyQuestionResponseService surveyQuestionResponseService;
        readonly IUserService userService;
        readonly ISMTPService SMTPService;
        readonly ICountryService countryService;
        private ISessionState sessionState;
        readonly IMDSService MDSService;
        private readonly ICookieConsentClient _cookieConsentClient;

        public HomeController(ICRMService pCRMService, ILeadService pLeadService, ILandingCampaignService pLandingCampaignService,
            ISurveyQuestionResponseService pSurveyQuestionResponseService, IUserService pUserService, ISMTPService pSMTPService, ICountryService pCountryService, IMDSService pMDSService
            , ISessionState pSessionState)
        {
            CRMService = pCRMService;
            leadService = pLeadService;
            landingCampaignService = pLandingCampaignService;
            surveyQuestionResponseService = pSurveyQuestionResponseService;
            userService = pUserService;
            SMTPService = pSMTPService;
            countryService = pCountryService;
            sessionState = pSessionState;
            MDSService = pMDSService;
            _cookieConsentClient = CookieConsentClientFactory.Create("store");
        }

        public ActionResult Index(Guid? pLeadID)
        {

            var _leadID = pLeadID ?? Guid.Empty;

            if (_leadID == Guid.Empty)
            {
                //Session["MessageError"] = TranslatorHelper.TranslateTextByIdentifier("Old_WithoutLeadId");
                //return RedirectToAction("NotFound", "Error");
                return View("Home");
                //throw new System.Exception(TranslatorHelper.TranslateTextByIdentifier("Old_WithoutLeadId"));
            }
            return View("Login", new LoginVM { leadID = _leadID });
        }

        /// <summary>
        ///     Action for present the user login view
        /// </summary>
        /// <param name="pOpportunityId"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Login(Guid? pLeadID)
        {


            Session.Abandon();
            var _leadID = pLeadID ?? Guid.Empty;

            //20170206 no redirecciona a vista home, tiene mismo contenido de login            
            //if (_leadID == Guid.Empty)
            //{
            //    //Response.StatusCode = 404;
            //    //return RedirectToAction("Error/Index");
            //    //throw new System.Exception("No se ha proporcionado el LEAD ID");
            //    return View("Home");
            //}

            LoginVM loginModel = new LoginVM() { leadID = _leadID };

            //20180205 - Manejo de las cookies
            bool TestCookies = ConfigurationManager.AppSettings["TestCookies"] == "true" ? true: false;
            string SiteDomain = ConfigurationManager.AppSettings["DomainCookieComplaince"];

            string IpAddress = Request.UserHostAddress;
            IPAddressResolver ipResolver = IPAddressResolverFactory.Create(SiteDomain);

            string countryCode;

            if (TestCookies)
            {
                countryCode = "es";                
            }
            else
            {
                countryCode = ipResolver.GetCountryCode(IpAddress);
            }
            //string countryCode = ipResolver.GetCountryCode("52.16.176.118"); //Irlanda

            if (_cookieConsentClient.IsConsentRequiredForRegion(SiteDomain, countryCode, this.HttpContext) == ConsentStatus.Required)
            {
                loginModel.RenderConsentBanner = true;
                loginModel.ConsentMarkup = _cookieConsentClient.GetConsentMarkup(countryCode);
            }
            
            return View(loginModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Guid? pLeadID, LoginVM pLogin)
        {
            //Trim
            if(pLogin.Usuario != null)
            {
                pLogin.Usuario = pLogin.Usuario.Trim();
            }

            if (pLogin.Password != null)
            {

                pLogin.Password = pLogin.Password.Trim();
            }


            if (ModelState.IsValid)
            {
                SurveyQuestionResponseDTO _surverQuestionDTO;
                CRMDataDTO _info;
                List<LeadInformationDTO> _leadInfoDTO;
                Guid _leadID;

                if (pLogin.leadID == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    //TODO: Revisar si se puede presentar que un LeadUser esté en dos campañas de manera simultánea.
                    _leadInfoDTO = leadService.GetLeadByEmail(pLogin.Usuario).Where(a => a.AcceptLanding == true).OrderByDescending(f => f.CreateDate).ToList();

                    if (_leadInfoDTO.Count() == 0)
                    {
                        ModelState.AddModelError("", TranslatorHelper.TranslateTextByIdentifier("Old_WrongUserAccess"));
                        return View("Home");
                    }

                    _leadID = _leadInfoDTO.First().LeadId;
                } else
                {
                    _leadID = pLogin.leadID;
                }

                _surverQuestionDTO = surveyQuestionResponseService.GetSurveyQuestionByLeadId(_leadID);

                bool _surveyResponse = (_surverQuestionDTO != null) ? _surverQuestionDTO.IsNotInterested : false;

                string _leadStr = _leadID.ToString().Replace("-", "").ToUpper();

                //var _passwordTemplate = _leadStr.Substring(0, 4) + "" + _leadStr.Substring(_leadStr.Length - 4, 4);
                _info = CRMService.GetLeadByID(_leadID);
                string _NumberOfEmp = _info.NumberOfEmployees.ToString();
                string _NumberOfPCs = _info.NumberOfPCs.ToString();
                var _passwordTemplate = "#" + _NumberOfEmp + "$" + _NumberOfPCs + "*";

                Session["_leadCRM"] = _info;
                //User and password validation
                if (_info.Email != pLogin.Usuario || pLogin.Password != _passwordTemplate)
                {
                    ModelState.AddModelError("", TranslatorHelper.TranslateTextByIdentifier("Old_WrongUserPassword"));
                    return View("Login", pLogin);
                }
                var countryId = countryService.GetByName(_info.CountryName).CountryID;

                //Save lead data if neccesary
                leadService.SaveLeadInformation(
                    new LeadInformationDTO {
                        LeadId = _leadID,
                        LeadUser = pLogin.Usuario,

                        //Campos adicionales
                        CompanyName = _info.CompanyName,
                        MicrosoftConsultantEmail = _info.MicrosoftConsultantEmail,
                        CountryID = countryId,
                        CampaignName = _info.CampaignName
                    });


                //get landing campaing information
                var _dat = landingCampaignService.GetLandingByCampaign(_info.CampaignName, countryId);

                LandingPageVM _landingVM = new LandingPageVM();
                _landingVM.LandingText = _dat.LandingText;
                _landingVM.CompanyName = _info.CompanyName;
                _landingVM.ConsultantName = _info.ConsultantName;
                _landingVM.ConsultantPhoneNumber = _info.MicrosoftConsultantPhoneNumber;
                _landingVM.ContactName = _info.ContactName;
                _landingVM.MicrosoftConsultantEmail = _info.MicrosoftConsultantEmail;
                _landingVM.Tittle = _info.Tittle;
                _landingVM.LeadId = _leadID;
                _landingVM.LanguageID = _dat.LanguageID;
                _landingVM.LastName = _info.LastName;

                LeadInformationDTO _leadInfo = leadService.GetByLeadID(_info.LeadID);

                //AffidavitProvider AffidavitProvider = new AffidavitProvider();

                string LanguajeText;

                if (_landingVM.LanguageID == 2)
                {
                    LanguajeText = "es";
                }
                else
                {
                    LanguajeText = "en-US";
                }

                Session["Landing"] = _landingVM;
                Session["user"] = pLogin.Usuario;
                Session["lead"] = _leadID;
                Session["IndustryId"] = _leadInfo.IndustryIndInsId;
                sessionState.Store(ConstantsStringKeys.CURRENT_LEADID__KEY, _leadID.ToString());

                if (_leadInfo.AcceptLanding)
                {
                    if (!ModularityHelper.CanISeeV3(_info.LeadID))
                    {
                        return RedirectToAction("Index", "MDS");
                    }
                    else
                    {
                        return RedirectToAction("Index", "ChooseAnAction");
                    }
                    //return RedirectToAction("Index", "ChooseAnAction");
                    //return RedirectToAction("Index", "MDS", new { pLeadID = _info.LeadID, pLanguageID = LanguajeText });
                }

                //TODO: If the user already has accepted the landing redirect to affidavit web
                return RedirectToAction("Letter", new { pLeadId = _leadID, pSurveyResponse = _surveyResponse });
                //return View("Landing", _landingVM);
            }
            return RedirectToAction("Login", new { pLeadId = pLogin.leadID });
        }

        /// <summary>
        ///     Action for present the user login view
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult LoginAdmin()
        {
            //Session["userLogin"] = null;

            SetCultureAdmin("en-US", "LoginAdmin");
            return View(new LoginVM { });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LoginAdmin(LoginVM pLogin)
        {
            if (ModelState.IsValid)
            {
                Boolean EsValido;
                string userLogin = "";
                var vDominio = "complarnet.local";

                PrincipalContext pc = new PrincipalContext(ContextType.Domain, vDominio);


                EsValido = pc.ValidateCredentials(pLogin.Usuario, pLogin.Password);

                if (EsValido)
                {
                    userLogin = pLogin.Usuario;
                    Session["userLogin"] = userLogin;
                    Session["pass"] = pLogin.Password;

                    DirectorySearcher search = null;
                    search = new DirectorySearcher(new DirectoryEntry("LDAP://" + vDominio, Session["userLogin"].ToString(), Session["pass"].ToString()));

                    if (pLogin.Usuario.ToLower() == "ns_jorge.gomez")
                    {
                        search.Filter = "(&((&(objectCategory=Person)(objectClass=User)))(samaccountname=julian.giraldo))";
                    }
                    else {
                        search.Filter = "(&((&(objectCategory=Person)(objectClass=User)))(samaccountname=" + Session["userLogin"].ToString() + "))";
                    }
                                        
                    SearchResult result = search.FindOne();

                    if (result != null)
                    {
                        var _samAccountName = "";
                        if (result.GetDirectoryEntry().Properties["samaccountname"].Value != null)
                        {
                            _samAccountName = result.Properties["samaccountname"][0].ToString();
                        }
                        var usuario = userService.GetByUserName(_samAccountName);
                        if (usuario == null)
                        {
                            var _id = Guid.NewGuid();
                            userService.Create(_id, _samAccountName.ToString());
                            Session["userId"] = _id;
                        }
                        else
                        {
                            Session["userId"] = usuario.Id;
                        }
                        // MostrarMensaje(1, "El usuario " + txtCodigoUsuario.Text + " no existe en el diretorio activo. Por favor verifique", "");
                    }

                    // Se recupera el cargo
                    if (result.GetDirectoryEntry().Properties["title"].Value != null)
                    {
                        Session["vTitle"] = result.Properties["title"][0];
                    }
                    else
                    {
                        Session["vTitle"] = "";
                    }

                    if (result.GetDirectoryEntry().Properties["co"].Value != null)
                    {
                        Session["vCountryName"] = result.Properties["co"][0];
                    }
                    else
                    {
                        Session["vCountryName"] = "";
                    }

                    if (result.GetDirectoryEntry().Properties["name"].Value != null)
                    {
                        Session["vName"] = result.Properties["name"][0];
                    }
                    else
                    {
                        Session["vName"] = "";
                    }

                    if (result.GetDirectoryEntry().Properties["mail"].Value != null)
                    {
                        Session["vEmail"] = result.Properties["mail"][0];
                    }
                    else
                    {
                        Session["vEmail"] = "";
                    }

                    if (result.GetDirectoryEntry().Properties["postofficebox"].Value != null)
                    {
                        Session["postofficebox"] = result.Properties["postofficebox"][0];
                    }
                    else
                    {
                        Session["postofficebox"] = "";
                    }

                    // Se recupera el código del país

                    if (result.GetDirectoryEntry().Properties["countrycode"].Value != null)
                    {
                        Session["vCountry"] = result.Properties["countrycode"][0];
                    }

                    if (Session["vTitle"].ToString() == "SAM Consultant" || Session["vTitle"].ToString() == "License Consultant" ||
                        Session["postofficebox"].ToString() == "Operations Leader" || Session["postofficebox"].ToString() == "System Administrator")
                    {
                        return RedirectToAction("../ManagePlatform/Index");
                    } else
                    {
                        //ModelState.AddModelError("", "You are not authorized to log in this application ");

                        Response.StatusCode = 403;
                        return RedirectToAction("Index", "Error", new { error = Response.StatusCode });
                    }

                }
                else
                {
                    ModelState.AddModelError("", TranslatorHelper.TranslateTextByIdentifier("Old_WrongUserPassword"));
                    return View("LoginAdmin", pLogin);
                }

            }

            //return EsValido;
            return View("LoginAdmin", pLogin);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pLeadId"></param>
        /// <returns></returns>
        public ActionResult Letter(Guid pLeadId, bool pSurveyResponse)
        {
            if (Session["lead"] == null || (Guid)Session["lead"] != pLeadId)
            {
                return RedirectToAction("../Home/Login", new { pLeadId = pLeadId });
            }

            var _info = CRMService.GetLeadByID(pLeadId);

            var _countryId = countryService.GetByName(_info.CountryName).CountryID;
            var _dat = landingCampaignService.GetLandingByCampaign(_info.CampaignName, _countryId);


            LandingPageVM _landingVM = new LandingPageVM();
            _landingVM.LandingText = _dat.LandingText;
            _landingVM.CompanyName = _info.CompanyName;
            _landingVM.ConsultantName = _info.ConsultantName;
            _landingVM.ConsultantPhoneNumber = _info.MicrosoftConsultantPhoneNumber;
            _landingVM.ContactName = _info.ContactName;
            _landingVM.MicrosoftConsultantEmail = _info.MicrosoftConsultantEmail;
            _landingVM.Tittle = _info.Tittle;
            _landingVM.LeadId = pLeadId;
            _landingVM.LanguageID = _dat.LanguageID;
            _landingVM.LastName = _info.LastName;
            _landingVM.SurveyResponse = pSurveyResponse;
            _landingVM.LandingId = _dat.Id;

            return View("Landing", _landingVM);
        }

        /// <summary>
        /// Show the not interested form in a different view
        /// </summary>
        /// <param name="pLeadId">Lead Id </param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult NotInterested(Guid pLeadId)
        {
            if (Session["lead"] == null || (Guid)Session["lead"] != pLeadId)
            {
                return RedirectToAction("../Home/Login", new { pLeadId = pLeadId });
            }

            return View("NotInterestedForm", new SurveyQuestionResponseVM { LeadId = pLeadId });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pLeadID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult NotInterestedCheck(Guid pLeadID)
        {
            SurveyQuestionResponseDTO _surverQuestionDTO = surveyQuestionResponseService.GetSurveyQuestionByLeadId(pLeadID);

            if (_surverQuestionDTO != null)
            {
                if (_surverQuestionDTO.IsNotInterested)
                {
                    return Json(new { Request = true });
                }
                else
                {
                    return Json(new { Request = false });
                }
            }

            return Json(new { Request = false });
        }

        /// <summary>
        /// obtiene la información del consultor para el popup de contacto
        /// </summary>
        /// <param name="pLeadID">Lead ID con el cual se consulta información del CRM</param>
        /// <returns>Vista parcial con la información del consultor</returns>
        public ActionResult ContacUs(Guid pLeadID)
        {
            Models.ContactUsViewModel _contactus = new Models.ContactUsViewModel();
            CRMDataDTO _leadCRM = CRMService.GetLeadByID(pLeadID);
            // CRMDataDTO _leadCRM = CRMService.GetLeadByID(pLeadID);

            _contactus.ConsultantName = _leadCRM.ConsultantName;
            _contactus.EmailAddress = _leadCRM.MicrosoftConsultantEmail;
            _contactus.PhoneNumber = (_leadCRM.MicrosoftConsultantPhoneNumber != "") ? _leadCRM.MicrosoftConsultantPhoneNumber : TranslatorHelper.TranslateTextByIdentifier("Old_LabelNotAvailable");

            return PartialView("_ContactUsViewPartial", _contactus);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult LogOut()
        {
            Guid _leadId = Guid.Parse(sessionState.Get<string>(ConstantsStringKeys.CURRENT_LEADID__KEY));

            Session.Abandon();


            return RedirectToAction("Login", "Home", new { pLeadID = _leadId });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOutAdmin()
        {
            Session.Abandon();
            return RedirectToAction("LoginAdmin");
        }

        //public ActionResult SetCultureAdmin(string culture, string returnUrl)
        //{
        //    // Validate input
        //    culture = CultureHelper.GetImplementedCulture(culture);

        //    // Save culture in a cookie
        //    HttpCookie cookie = Request.Cookies["_culture"];
        //    if (cookie != null)
        //        cookie.Value = culture;   // update cookie value
        //    else
        //    {

        //        cookie = new HttpCookie("_culture");
        //        cookie.Value = culture;
        //        cookie.Expires = DateTime.Now.AddYears(1);
        //    }
        //    Response.Cookies.Add(cookie);

        //    return Redirect(returnUrl);
        //    //return RedirectToAction("Index", new { pLeadID = leadID, pLanguageID = culture });
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pLeadID"></param>
        /// <returns></returns>
        public ActionResult SavePDF(Guid pLeadID)
        {
            FileResult fileResult = null;
            String FileName = "Invitation_Letter.pdf";
            String HtmlText;

            var _info = CRMService.GetLeadByID(pLeadID);

            var _countryId = countryService.GetByName(_info.CountryName).CountryID;
            //get landing campaing information
            var _dat = landingCampaignService.GetLandingByCampaign(_info.CampaignName, _countryId);

            LandingPageVM _landingVM = new LandingPageVM();
            _landingVM.LandingText = _dat.LandingText;
            _landingVM.CompanyName = _info.CompanyName;
            _landingVM.ConsultantName = _info.ConsultantName;
            _landingVM.ConsultantPhoneNumber = _info.MicrosoftConsultantPhoneNumber;
            _landingVM.ContactName = _info.ContactName;
            _landingVM.MicrosoftConsultantEmail = _info.MicrosoftConsultantEmail;
            _landingVM.Tittle = _info.Tittle;
            //_landingVM.LeadId = pLeadID;

            HtmlText = _dat.LandingText
                            .Replace("{{tittle}}", _info.Tittle)
                            .Replace("{{contactName}}", _info.ContactName)
                            .Replace("{{microsoftConsultantEmail}}", _info.MicrosoftConsultantEmail)
                            .Replace("{{consultantPhoneNumber}}", _info.MicrosoftConsultantPhoneNumber)
                            .Replace("{{companyName}}", _info.CompanyName)
                            .Replace("{{consultantName}}", _info.ConsultantName)
                            .Replace("{{CurrentDate}}", DateTime.Now.ToString("dd MMMM yyy"))
                            .Replace("style=\"", "style=\"font-size: 26px !important;font-family: Arial !important;"); 
            

            // Código con la librería NReco.PdfGenerator: tiene problemas con unicode
            //var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();
            //htmlToPdf.CustomWkHtmlArgs = "--encoding UTF-8";
            //var pdfBytes = htmlToPdf.GeneratePdf(HtmlText);

            // create the HTML to PDF converter
            HtmlToPdf htmlToPdfConverter = new HtmlToPdf();

            htmlToPdfConverter.BrowserWidth = 1200;

            // set HTML Load timeout
            htmlToPdfConverter.HtmlLoadedTimeout = 120;

            // set PDF page size and orientation
            htmlToPdfConverter.Document.PageSize = PdfPageSize.Letter;
            htmlToPdfConverter.Document.PageOrientation = PdfPageOrientation.Portrait;

            // set PDF page margins
            htmlToPdfConverter.Document.Margins = new PdfMargins(60);

            // set a wait time before starting the conversion
            htmlToPdfConverter.WaitBeforeConvert = 2;

            // convert HTML to PDF
            byte[] pdfBytes = null;

            pdfBytes = htmlToPdfConverter.ConvertHtmlToMemory(HtmlText, "");

            HttpContext.Response.ContentType = "application/pdf";
            HttpContext.Response.ContentEncoding = System.Text.Encoding.UTF8;
            HttpContext.Response.AppendHeader("content-disposition", string.Format("attachment; filename={0}", FileName));

            fileResult = new FileContentResult(pdfBytes, "application/pdf");
            fileResult.FileDownloadName = FileName;

            return fileResult;
        }



        /// <summary>
        /// Save the desagree reason
        /// </summary>
        /// <param name="pDesagreeReason">Desagree reason object</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveSurveyQuestionResponse(SurveyQuestionResponseVM pSurveyQuestionResponseVM)
        {
            if (ModelState.IsValid)
            {
                Guid _leadID = pSurveyQuestionResponseVM.LeadId;

                SurveyQuestionResponseDTO _surveyResponse = surveyQuestionResponseService.GetSurveyQuestionByLeadId(_leadID);

                if (_surveyResponse != null)
                {
                    SurveyQuestionResponseDTO _surveyResponseDTO = Mapper.Map<SurveyQuestionResponseVM, SurveyQuestionResponseDTO>(pSurveyQuestionResponseVM);
                    _surveyResponseDTO.IsNotInterested = true;
                    surveyQuestionResponseService.UpdateSurveyQuestionResponse(_surveyResponseDTO);
                } else
                {
                    surveyQuestionResponseService.CreateSurveyQuestionResponse(Mapper.Map<SurveyQuestionResponseVM, SurveyQuestionResponseDTO>(pSurveyQuestionResponseVM));
                }
                //pSurveyQuestionResponseVM.LeadId =  new Guid(Request.Form["HiddenLeadID"]);

                //var _passwordTemplate = _leadStr.Substring(0, 4) + "" + _leadStr.Substring(_leadStr.Length - 4, 4);

                var _survey = GetSurverQuestionResponseString(_leadID);

                var _info = CRMService.GetLeadByID(_leadID);

                string MicrosoftConsultantEmail = _info.MicrosoftConsultantEmail;

                string EmailSubject = TranslatorHelper.TranslateTextByIdentifier("Old_SubjectTextForNonParticipation");

                string EmailBody = TranslatorHelper.TranslateTextByIdentifier("Old_BodyTextForNonParticipation") + "<br /> <h1>Survey Question Response</h1>" + _survey;

                EmailSubject = EmailSubject.Replace("{CompanyName}", _info.CompanyName).Replace("{ConsultantName}", _info.ConsultantName);

                EmailBody = EmailBody.Replace("{CompanyName}", _info.CompanyName).Replace("{ConsultantName}", _info.ConsultantName);

                SMTPService.EnviarCorreo(MicrosoftConsultantEmail, EmailSubject, EmailBody);

                leadService.UpdateLeadInformation(_leadID, null, null, "noParticipateDateUpdate");
                MDSService.CommitChangesAffidavit();



                //pSurveyQuestionResponseVM.

                //if (pDesagreeReason.LeadId != Guid.Empty)
                //{

                //return Json(TranslatorHelper.TranslateTextByIdentifier("Old_SaveSurveyQuestionResponseSuccessMessage"));
                return Json(new { pSuccess = true, pMessage = TranslatorHelper.TranslateTextByIdentifier("Old_SaveSurveyQuestionResponseSuccessMessage") });
            }
            //}
            //return RedirectToAction("Index", new { pLeadID = pSurveyQuestionResponseVM.LeadId});
            //return RedirectTo(new { pSuccess = false, pMessage = TranslatorHelper.TranslateTextByIdentifier("Old_SaveSurveyQuestionResponseSuccessMessage") });

            return Json(new { pSuccess = false, pMessage = TranslatorHelper.TranslateTextByIdentifier("Old_SaveSurveyQuestionResponseSuccessMessage") });
            //Response.StatusCode = (int)HttpStatusCode.NotFound;
            //return Json(new { error = "err", errorMessage = TranslatorHelper.TranslateTextByIdentifier("Old_RecordNotFoundAdmin") });
        }

        public string GetSurverQuestionResponseString(Guid pLeadID)
        {
            string _SurveyResponse = "";
            var _surveyQuestion1Response = "";
            var _surveyQuestion2Response = "";
            var _surveyQuestion3Response = "";
            var _surveyQuestion5Response = "";

            SurveyQuestionResponseDTO _surveyQR = surveyQuestionResponseService.GetSurveyQuestionByLeadId(pLeadID);

            switch (_surveyQR.SurveyQuestion1Response)
            {
                case 1:
                    _surveyQuestion1Response = "<label>" + TranslatorHelper.TranslateTextByIdentifier("Old_Option1ForSurverQuestion1") + "</label>";
                    break;
                case 2:
                    _surveyQuestion1Response = "<label>" + TranslatorHelper.TranslateTextByIdentifier("Old_Option2ForSurverQuestion1") + "</label>";
                    break;
                case 3:
                    _surveyQuestion1Response = "<label>" + TranslatorHelper.TranslateTextByIdentifier("Old_Option3ForSurverQuestion1") + "</label>";
                    break;
                case 4:
                    _surveyQuestion1Response = "<label>" + TranslatorHelper.TranslateTextByIdentifier("Old_Option4ForSurverQuestion1") + "</label>";
                    break;
                case 5:
                    _surveyQuestion1Response = "<label>" + TranslatorHelper.TranslateTextByIdentifier("Old_Option5ForSurverQuestion1") + "</label>";
                    break;
                case 6:
                    _surveyQuestion1Response = "<label>" + TranslatorHelper.TranslateTextByIdentifier("Old_Option6ForSurverQuestion1") + ":</label><br />" +
                        "<label>" + _surveyQR.SurveyQuestion1OtherResponse + "</label><br />";
                    break;
            }

            switch (_surveyQR.SurveyQuestion2Response)
            {
                case 1:
                    _surveyQuestion2Response = "<label>" + TranslatorHelper.TranslateTextByIdentifier("Old_Option1ForSurverQuestion2") + "</label>";
                    break;
                case 2:
                    _surveyQuestion2Response = "<label>" + TranslatorHelper.TranslateTextByIdentifier("Old_Option2ForSurverQuestion2") + "</label>";
                    break;
                case 3:
                    _surveyQuestion2Response = "<label>" + TranslatorHelper.TranslateTextByIdentifier("Old_Option3ForSurverQuestion2") + "</label>";
                    break;
                case 4:
                    _surveyQuestion2Response = "<label>" + TranslatorHelper.TranslateTextByIdentifier("Old_Option4ForSurverQuestion2") + "</label>";
                    break;
                case 5:
                    _surveyQuestion2Response = "<label>" + TranslatorHelper.TranslateTextByIdentifier("Old_Option5ForSurverQuestion2") + "</label>";
                    break;
                case 6:
                    _surveyQuestion2Response = "<label>" + TranslatorHelper.TranslateTextByIdentifier("Old_Option6ForSurverQuestion2") + "</label>";
                    break;
                case 7:
                    _surveyQuestion2Response = "<label>" + TranslatorHelper.TranslateTextByIdentifier("Old_Option7ForSurverQuestion2") + "</label>";
                    break;
                case 8:
                    _surveyQuestion2Response = "<label>" + TranslatorHelper.TranslateTextByIdentifier("Old_Option8ForSurverQuestion2") + ":</label><br />" +
                        "<label>" + _surveyQR.SurveyQuestion2OtherResponse + "</label><br />";
                    break;
            }

            switch (_surveyQR.SurveyQuestion3Response)
            {
                case 1:
                    _surveyQuestion3Response = "<label>" + TranslatorHelper.TranslateTextByIdentifier("Old_Option1ForSurverQuestion3") + "</label>";
                    break;
                case 2:
                    _surveyQuestion3Response = "<label>" + TranslatorHelper.TranslateTextByIdentifier("Old_Option2ForSurverQuestion3") + "</label>";
                    break;
                case 3:
                    _surveyQuestion3Response = "<label>" + TranslatorHelper.TranslateTextByIdentifier("Old_Option3ForSurverQuestion3") + "</label>";
                    break;
                case 4:
                    _surveyQuestion3Response = "<label>" + TranslatorHelper.TranslateTextByIdentifier("Old_Option4ForSurverQuestion3") + "</label>";
                    break;
                case 5:
                    _surveyQuestion3Response = "<label>" + TranslatorHelper.TranslateTextByIdentifier("Old_Option5ForSurverQuestion3") + "</label>";
                    break;
                case 6:
                    _surveyQuestion3Response = "<label>" + TranslatorHelper.TranslateTextByIdentifier("Old_Option6ForSurverQuestion3") + "</label>";
                    break;
                case 7:
                    _surveyQuestion3Response = "<label>" + TranslatorHelper.TranslateTextByIdentifier("Old_Option7ForSurverQuestion3") + "</label>";
                    break;
                case 8:
                    _surveyQuestion3Response = "<label>" + TranslatorHelper.TranslateTextByIdentifier("Old_Option8ForSurverQuestion3") + "</label>";
                    break;
                case 9:
                    _surveyQuestion3Response = "<label>" + TranslatorHelper.TranslateTextByIdentifier("Old_Option9ForSurverQuestion3") + ":</label><br />" +
                        "<label>" + _surveyQR.SurveyQuestion3OtherResponse + "</label><br />";
                    break;
            }

            _surveyQuestion5Response = (_surveyQR.SurveyQuestion5Response == null || _surveyQR.SurveyQuestion5Response == "") ? "Not response" : _surveyQR.SurveyQuestion5Response;

            _SurveyResponse = "<h2>" + TranslatorHelper.TranslateTextByIdentifier("Old_SurveyQuestion1") + "</h2>" + _surveyQuestion1Response +
                "<h2>" + TranslatorHelper.TranslateTextByIdentifier("Old_SurveyQuestion2") + "</h2>" + _surveyQuestion2Response +
                "<h2>" + TranslatorHelper.TranslateTextByIdentifier("Old_SurveyQuestion3") + "</h2>" + _surveyQuestion3Response +
                "<h2>" + TranslatorHelper.TranslateTextByIdentifier("Old_SurveyQuestion4") + "</h2>" +
                "<label>" + TranslatorHelper.TranslateTextByIdentifier("Old_Option1ForSurverQuestion4") + ": " + _surveyQR.SurveyQuestion4Option1Value + "</label><br />" +
                "<label>" + TranslatorHelper.TranslateTextByIdentifier("Old_Option2ForSurverQuestion4") + ": " + _surveyQR.SurveyQuestion4Option2Value + "</label><br />" +
                "<label>" + TranslatorHelper.TranslateTextByIdentifier("Old_Option3ForSurverQuestion4") + ": " + _surveyQR.SurveyQuestion4Option3Value + "</label><br />" +
                "<label>" + TranslatorHelper.TranslateTextByIdentifier("Old_Option4ForSurverQuestion4") + ": " + _surveyQR.SurveyQuestion4Option4Value + "</label><br />" +
                "<h2>" + TranslatorHelper.TranslateTextByIdentifier("Old_SurveyQuestion5") + "</h2>" +
                _surveyQuestion5Response;

            return _SurveyResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ForgetPassword()
        {
            ForgetPassword _login = new ForgetPassword();
            return View("ForgetPassword", _login);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ForgetPassword(ForgetPassword pForgetPassword)
        {
            List<LeadInformationDTO> _leadInfoDTO = leadService.GetLeadByEmail(pForgetPassword.Usuario).OrderByDescending(f => f.CreateDate).ToList();

            if (_leadInfoDTO.Count() == 0)
            {
                ModelState.AddModelError("", TranslatorHelper.TranslateTextByIdentifier("Old_ForgetPassValidationMessage"));
                return View("ForgetPassword");
            }

            var _leadId = _leadInfoDTO.First().LeadId;

            CRMDataDTO _leadCRM = CRMService.GetLeadByID(_leadId);

            var _title = CRMService.GetConsultantTitle(_leadId);

            string MicrosoftConsultantEmail = _leadCRM.MicrosoftConsultantEmail;

            string EmailSubject = TranslatorHelper.TranslateTextByIdentifier("Old_ForgetPassSubject");

            string EmailBody = TranslatorHelper.TranslateTextByIdentifier("Old_ForgetPassBodyText");

            //EmailSubject = EmailSubject.Replace("{CompanyName}", _leadCRM.CompanyName);
            EmailBody = EmailBody.Replace("{User Name}", _leadCRM.ContactName);

            EmailBody = EmailBody.Replace("{Company Name}", _leadCRM.CompanyName);

            EmailBody = EmailBody.Replace("{Customer Link}", ConfigurationManager.AppSettings["MDSPath"].ToString() + _leadCRM.LeadID);

            EmailBody = EmailBody.Replace("{No. of Employees(Lead)}", _leadCRM.NumberOfEmployees.ToString());

            EmailBody = EmailBody.Replace("{Number of PCs(Lead)}", _leadCRM.NumberOfPCs.ToString());

            EmailBody = EmailBody.Replace("{Consultant Email}", _leadCRM.NumberOfPCs.ToString());

            EmailBody = EmailBody.Replace("{Licensing Consultant New(Lead)}", _leadCRM.ConsultantName);

            EmailBody = EmailBody.Replace("{ Title(Licensing Consultant New(User))}", _title);

            EmailBody = EmailBody.Replace("{Email 2(Licensing Consultant New (User))}", _leadCRM.MicrosoftConsultantEmail);

            EmailBody = EmailBody.Replace("{Other Phone(Licensing Consultant New (User))}", _leadCRM.MicrosoftConsultantPhoneNumber);

            EmailBody = EmailBody.Replace("{logo}", "<img src=" + ConfigurationManager.AppSettings["MicrosoftLogo"].ToString() + " alt='Microsoft Corporation' />");

            SMTPService.EnviarCorreo(_leadCRM.MicrosoftConsultantEmail, EmailSubject, EmailBody);

            MDSService.CommitChangesAffidavit();


            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public PartialViewResult ContactAConsultant()
        {
            ContactAConsultanViewModel _model = new ContactAConsultanViewModel();
            CRMDataDTO data = (CRMDataDTO)Session["_leadCRM"];

            _model.CampaignId = Guid.Parse(data.CRMCampaignID);
            _model.LeadId = data.LeadID;
            _model.CompanyContact = data.ContactName;
            _model.CompanyName = data.CompanyName;
            _model.ConstultantEmail = data.MicrosoftConsultantEmail;
            _model.ConsultantName = data.ConsultantName;
            _model.LeadEmail = data.Email;


            return PartialView("_ContactAConsultant", _model);

        }

        [HttpPost]
        public void ContactAConsultant(ContactAConsultanViewModel pModel)
        {
            if (ModelState.IsValid)
            {
                //TODO: Convert into a resource type
                string emailBody =string.Format("Hello {0} <br/>"+
                    "The company <strong> {1} </strong> has contacted you. <br/> <br/> " +
                    "Company contact: {2} <br/>"+
                    "Company email: {3} <br/> <br/>" +
                    "Message: <br/><br/>" +
                    "{4}", pModel.ConsultantName, pModel.CompanyName, pModel.CompanyContact, pModel.LeadEmail, pModel.MessageText);

                //TODO: Change to the consultand email
                //SMTPService.EnviarCorreo("jagomez@newsoft.com.co"/*pModel.ConstultantEmail*/, $"Contact a consultant action: {pModel.CompanyName} ", emailBody);
                SMTPService.EnviarCorreo(pModel.ConstultantEmail/*pModel.ConstultantEmail*/, $"Contact a consultant action: {pModel.CompanyName} ", emailBody);



            }

            return;
        }

        //TODO:Remove this method after create all company user names
        public void UpdateLeadUserName()
        {
            leadService.CreateAllCompanyUserNames();
        }
    }
}