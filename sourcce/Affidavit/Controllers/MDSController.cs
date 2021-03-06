namespace Affidavit.Controllers
{

    using System;
    using System.Web;
    using System.Web.Mvc;
    using Helpers;
    using System.Collections.Generic;
    using Models;
    using IServices;
    using DTOs;
    using System.Linq;
    using global::AutoMapper;
    using IServices.Landing;
    using Models.Home;
    using IServices.EmailQueue;
    using DTOs.EmailQueueDTO;
    using DTOs.Utils;
    using System.IO;
    using System.Data.Entity;
    using Utils;
    using System.Net;
    using Microsoft.SharePoint.Client;
    using System.Security;
    using IServices.Files;
    using System.Threading;

#if !DEBUG
    [RequireHttps] //apply to all actions in controller
#endif
    [SessionExpire]
    public class MDSController : BaseController
    {

        //private ICampaignRepository campaignRepository;
        private IProductService productService;
        private IProductFamilyService productFamilyService;
        private IIndustryService industryService;
        private IPartnerCapabilityService partnerCapabilityService;
        private IPartnerProgramService partnerProgramSercive;
        private ICRMService CRMService;
        private ICompanyInfoService companyInfoService;
        private IProductCompanyService productCompanyService;
        private IQuestionxLanguageService questionxLanguageService;
        private IQuestionService questionService;
        private IResponseDataTypeService responseDataTypeService;
        private ILanguageService languageService;
        private IQuestionxProductFamilyService questionxProductFamilyService;
        private ICompanyService companyService;
        private ICompanyContactsService companyContactsService;
        private IPartnerCapabilityCompanyService partnerCapabilityCompanyService;
        private IPartnerProgramCompanyService partnerProgramCompanyService;
        private IProductFamilyCompanyService productFamilyCompanyService;
        private ICampaignService campaignService;
        private ICountryService countryService;
        private IAnswerCompanyService answerCompanyService;
        private IMDSService MDSService;
        private ILeadService leadService;
        private ILandingCampaignService landingCampaignService;
        private ISessionState sessionState;
        private IProductCompanyFileService productCompanyFileService;

        private IFileManagerService fileManagerService;

        //private ISMTPService

        readonly ISMTPService SMTPService;

        public MDSController(IProductService pProductService, IProductFamilyService pProductFamilyService, IIndustryService pIndustryService,
            IPartnerCapabilityService pPartnerCapabilityService, IPartnerProgramService pPartnerProgramService, ICRMService pCRMService, ICompanyInfoService pCompanyInfoService,
            IProductCompanyService pProductCompanyService, IQuestionxLanguageService pQuestionxLanguageService, IQuestionService pQuestionService,
            IResponseDataTypeService pResponseDataTypeService, ILanguageService pLanguageService, IQuestionxProductFamilyService pQuestionxProductFamilyService,
            ICompanyService pCompanyService, ICompanyContactsService pCompanyContactsService, IPartnerCapabilityCompanyService pPartnerCapabilityCompanyService,
            IPartnerProgramCompanyService pPartnerProgramCompanyService, IProductFamilyCompanyService pProductFamilyCompanyService, ICampaignService pCampaignService,
            ICountryService pCountryService, IAnswerCompanyService pAnswerCompanyService, ISMTPService pSMTPService, IMDSService pMDSService,
            ILeadService pLeadService, ILandingCampaignService pLandingCampaignService, IProductCompanyFileService pProductCompanyFileService, ISessionState pSessionState,
            IFileManagerService pFileManagerService)
        {
            productService = pProductService;
            productFamilyService = pProductFamilyService;
            industryService = pIndustryService;
            partnerCapabilityService = pPartnerCapabilityService;
            partnerProgramSercive = pPartnerProgramService;
            CRMService = pCRMService;
            companyInfoService = pCompanyInfoService;
            productCompanyService = pProductCompanyService;
            questionxLanguageService = pQuestionxLanguageService;
            questionService = pQuestionService;
            responseDataTypeService = pResponseDataTypeService;
            languageService = pLanguageService;
            questionxProductFamilyService = pQuestionxProductFamilyService;
            companyService = pCompanyService;
            companyContactsService = pCompanyContactsService;
            partnerCapabilityCompanyService = pPartnerCapabilityCompanyService;
            partnerProgramCompanyService = pPartnerProgramCompanyService;
            productFamilyCompanyService = pProductFamilyCompanyService;
            campaignService = pCampaignService;
            countryService = pCountryService;
            answerCompanyService = pAnswerCompanyService;
            SMTPService = pSMTPService;
            MDSService = pMDSService;
            leadService = pLeadService;
            landingCampaignService = pLandingCampaignService;
            sessionState = pSessionState;
            productCompanyFileService = pProductCompanyFileService;

            fileManagerService = pFileManagerService;
        }


        // GET: Affidavit
        /// <summary>
        ///     Index de formulario Affidavit
        /// </summary>
        /// <returns>Vista con tab's para llenar info requerida de affidavit</returns>
        public ActionResult Index(bool pIsFinal = false, int pTabNumber = 1)
        {
            bool _firstTime = false;
            //SetCulture(pLanguageID, pLeadID, null);
            SoftwareUpdateViewModel _softwareUpdateModel = new SoftwareUpdateViewModel();
            _softwareUpdateModel.TabNumber = pTabNumber == 0? 1: pTabNumber;
            Guid _leadId = Guid.Parse(sessionState.Get<string>(ConstantsStringKeys.CURRENT_LEADID__KEY));

            CRMDataDTO _leadCRM;
            CompanyDTO _companyDTO;
            CampaignDTO _campaignDTO;

            //CompanyViewModel _companyModel = new CompanyViewModel();
            //TODO: cambiar el valor quemado del _leadID por el valor que venga de SomSigth
            //Guid _leadID = new Guid("369C4290-BEF6-E511-80F0-3863BB2E6228");

            //string _currentCulture = CultureHelper.GetCurrentCulture();
            //int _lenguageID = _currentCulture == "es" ? 2 : 1;

            if (Session["lead"] != null && Session["lead"].ToString() == _leadId.ToString())
            {

                //if (pAccept == true)
                //{
                //    CRMDataDTO _leadCRMAccept = CRMService.GetLeadByID(pLeadID);

                //    string MicrosoftConsultantEmail = _leadCRMAccept.MicrosoftConsultantEmail;

                //    string EmailSubject = TranslatorHelper.TranslateTextByIdentifier("Old_SubjectTextAffidavitParticipation");

                //    string EmailBody = TranslatorHelper.TranslateTextByIdentifier("Old_BodyTextAffidavitParticipation");

                //    EmailSubject = EmailSubject.Replace("{ConsultantName}", _leadCRMAccept.ConsultantName).Replace("{CompanyName}", _leadCRMAccept.CompanyName);

                //    EmailBody = EmailBody.Replace("{CompanyName}", _leadCRMAccept.CompanyName);

                //    EmailBody = EmailBody.Replace("{ConsultantName}", _leadCRMAccept.ConsultantName).Replace("{LeadId}", "'" + _leadCRMAccept.LeadID.ToString() + "'");

                //    EmailBody = EmailBody.Replace("{SourceCampaign}", _leadCRMAccept.CampaignName).Replace("{Hora}", DateTime.Now.ToString("hh:mm tt")).Replace("{Fecha}", DateTime.Now.ToString("dd-MM-yyyy"));

                //    SMTPService.EnviarCorreo(_leadCRMAccept.MicrosoftConsultantEmail, EmailSubject, EmailBody);

                //    leadService.UpdateLeadInformation(pLeadID, null, null, "acceptLandingDateUpdate");

                //}

                _leadCRM = (CRMDataDTO)Session["_leadCRM"];
                _companyDTO = (CompanyDTO)Session["_companyDTO"];
                _campaignDTO = (CampaignDTO)Session["_campaignDTO"];

                //Entra aqui si ya habia entrado al index (Si recargo pagina, por ejemplo)
                if (_companyDTO != null && _campaignDTO != null)
                {
                    _softwareUpdateModel.CompanyId = _companyDTO.CompanyID;
                    _softwareUpdateModel.CampaignID = _campaignDTO.CampaignID;
                    _firstTime = false;

                }
                //Entra aqui si no habia entrado al index (si es la primera vez en la sesion que entra).
                //Aqui, por lo tanto, se comprobara si la compañia ya habia entrado 
                else
                {
                    _firstTime = true;
                    string _accountNumber = _leadCRM.AccountNumber;
                    Guid _leadID = _leadCRM.LeadID;
                    //short _campaignID = Convert.ToInt16(_leadCRM.CRMCampaignID);
                    CampaignDTO _CampDTO = campaignService.GetByCRMCampaignID(_leadCRM.CRMCampaignID);
                    int _companyIDFromCI = 0;
                    bool _foundedCompanyByName = false;

                    if (_CampDTO != null)
                    {
                        _companyIDFromCI = companyInfoService.GetCompanyIDByLeadAndCampaign(_leadID, _CampDTO.CampaignID);
                    }

                    if (_companyIDFromCI != 0)
                    {
                        _companyDTO = companyService.GetCompanyByID(_companyIDFromCI);
                    }
                    //SI ENTRA A ESTE ELSE Y ENCUENTRA ALGO QUIERE DECIR QUE LA COMPAÑIA YA ENTRO AL MDS EN OTRO MOMENTO Y SE GENERARON DATOS PARA ELLA
                    else
                    {
                        _companyDTO = companyService.GetCompanyByName(_leadCRM.CompanyName);
                        //Para saber si la compañía ya existia y se encontro por nombre
                        _foundedCompanyByName = _companyDTO != null ? true : false;

                    }


                    CompanyDTO _companyAGuardarDTO = new CompanyDTO();
                    CompanyContactsDTO _companyContactsDTO;
                    CompanyInfoDTO _companyInfoDTO;

                    _campaignDTO = campaignService.GetByCRMCampaignID(_leadCRM.CRMCampaignID);

                    //Si existe la campaña la asigna
                    if (_campaignDTO != null)
                    {
                        _softwareUpdateModel.CampaignID = _campaignDTO.CampaignID;
                    }
                    //Si no existe la crea
                    else
                    {
                        _campaignDTO = new CampaignDTO();
                        _campaignDTO.CampaignName = _leadCRM.CampaignName;
                        _campaignDTO.CRMCampaignID = _leadCRM.CRMCampaignID;

                        campaignService.Add(_campaignDTO);
                        MDSService.CommitChangesAffidavit();

                        _softwareUpdateModel.CampaignID = campaignService.GetByCRMCampaignID(_leadCRM.CRMCampaignID).CampaignID;
                    }

                    ////////////////////////////////////////////////////
                    // Save in session variable
                    Session["_campaignDTO"] = _campaignDTO;

                    //Si se encontro información sobre la compañía, pero no la encontro por nombre 
                    if (_companyDTO != null && !_foundedCompanyByName)
                    {
                        _softwareUpdateModel.CompanyId = _companyDTO.CompanyID;

                        ////////////////////////////////////////////////////
                        // Save in session variable
                        Session["_companyDTO"] = _companyDTO;
                        _firstTime = false;
                    }
                    else
                    {
                        bool _companyWasAddOrUpdate = false;
                        bool _catch = false;
                        try
                        {
                            using (var tx = MDSService.BeginTx())
                            {
                                try
                                {
                                    //Si entra aqui quiere decir que encontro información de lá compañía
                                    //Se debe actualizar la información de la compañía
                                    if (_foundedCompanyByName)
                                    {

                                        //En LeadInformation queda la información para ese LEAD
                                        //Se debe agregar toda la informacion para la compañia con la nueva campaña entonces: 
                                        //¿Se actualiza la información de la compañía en Company o se crea un nuevo registro?
                                        //¿Se debe agregar CompanyContacts y CompanyInfo(tabla que contiene el leadId nuevo) para esta compañia, donde varía la campaña?
                                        //Es decir, en estas tablas la compañía quedará 2 veces, con distinta compañía.
                                        _companyDTO.AccountNumber = (_accountNumber == null) ? null : _accountNumber.ToString();
                                        _companyDTO.CompanyName = _leadCRM.CompanyName;
                                        _companyDTO.IndustryID = industryService.GetByName(_leadCRM.IndustryName).IndustryID;
                                        _companyDTO.CountryID = countryService.GetByName(_leadCRM.CountryName).CountryID;


                                        companyService.UpdateCompany(_companyDTO);
                                    }
                                    //Si es nueva la compañía
                                    else
                                    {
                                        _companyAGuardarDTO = new CompanyDTO();
                                        _companyAGuardarDTO.AccountNumber = (_accountNumber == null) ? null : _accountNumber.ToString();
                                        _companyAGuardarDTO.CompanyName = _leadCRM.CompanyName;
                                        _companyAGuardarDTO.IndustryID = industryService.GetByName(_leadCRM.IndustryName).IndustryID;
                                        _companyAGuardarDTO.CountryID = countryService.GetByName(_leadCRM.CountryName).CountryID;
                                        _companyAGuardarDTO.CreatedOn = DateTime.UtcNow;
                                        companyService.Add(_companyAGuardarDTO);
                                    }
                                    MDSService.CommitChangesAffidavit();
                                    MDSService.CommitTx(tx);
                                    _companyWasAddOrUpdate = true;
                                }
                                catch (Exception e)
                                {
                                    MDSService.RollbackTx(tx); //Required according to MSDN article 
                                    throw new Exception();
                                }
                            }



                            int _companyID;

                            //Si la compañía es nueva
                            //ver si puedo obtener el companyid directamente
                            if (!_foundedCompanyByName && _companyDTO == null)
                            {
                                if (_accountNumber != null)
                                {
                                    _companyID = companyService.GetCompanyByAccountNumber(_accountNumber).CompanyID;
                                }
                                else
                                {
                                    _companyID = companyService.GetCompanyByName(_leadCRM.CompanyName).CompanyID;
                                }

                                _companyAGuardarDTO.CompanyID = _companyID;

                                ////////////////////////////////////////////////////
                                // Save in session variable
                                Session["_companyDTO"] = _companyAGuardarDTO;

                            }
                            //Si encontro la compañía por nombre
                            else
                            {
                                _companyID = _companyDTO.CompanyID;
                                // Save in session variable
                                Session["_companyDTO"] = _companyDTO;
                            }

                            using (var txContactsInfo = MDSService.BeginTx())
                            {

                                try
                                {
                                    _companyContactsDTO = new CompanyContactsDTO();
                                    _companyContactsDTO.CompanyID = _companyID;
                                    _companyContactsDTO.CampaignID = campaignService.GetByCRMCampaignID(_leadCRM.CRMCampaignID).CampaignID;
                                    _companyContactsDTO.ContactName = _leadCRM.ContactName;
                                    _companyContactsDTO.CompanyEmail = _leadCRM.Email;
                                    _companyContactsDTO.CompanyPhone = _leadCRM.PhoneNumber;

                                    companyContactsService.Add(_companyContactsDTO);

                                    _companyInfoDTO = new CompanyInfoDTO();
                                    _companyInfoDTO.CompanyID = _companyID;
                                    _companyInfoDTO.CampaignID = campaignService.GetByCRMCampaignID(_leadCRM.CRMCampaignID).CampaignID;
                                    _companyInfoDTO.LeadID = _leadId.ToString();
                                    _companyInfoDTO.IsFinalVersion = false;
                                    _companyInfoDTO.FolderCreationDate = DateTime.UtcNow;

                                    companyInfoService.Add(_companyInfoDTO);

                                    _softwareUpdateModel.CompanyId = _companyID;

                                    MDSService.CommitChangesAffidavit();
                                    MDSService.CommitTx(txContactsInfo);
                                }
                                catch (Exception e)
                                {
                                    MDSService.RollbackTx(txContactsInfo); //Required according to MSDN article 
                                    throw new Exception();
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            _catch = true;
                        }
                        //Comprueba si estuvo en el catch
                        if (_catch)
                        {
                            if (_companyWasAddOrUpdate)
                            {

                                using (var txRemoveCompany = MDSService.BeginTx())
                                {
                                    try
                                    {
                                        companyService.Remove(_companyAGuardarDTO);

                                        MDSService.CommitChangesAffidavit();
                                        MDSService.CommitTx(txRemoveCompany);
                                    }
                                    catch (Exception e)
                                    {
                                        MDSService.RollbackTx(txRemoveCompany);

                                    }
                                }


                            }
                            Response.StatusCode = 403;
                            return RedirectToAction("Index", "Error", new { error = Response.StatusCode, message = "" });
                        }

                    }

                }

            }
            else
            {
                return RedirectToAction("LogOut", "Home", new { pLeadID = _leadId });
            }

            CompanyInfoDTO _compInfo = companyInfoService.GetCompanyInfoByIDandCampaign(_softwareUpdateModel.CompanyId, _softwareUpdateModel.CampaignID);
            var _isFinal = (_compInfo == null) ? false : _compInfo.IsFinalVersion;
            _softwareUpdateModel.LeadID = _leadId.ToString();
            _softwareUpdateModel.IsFinalVersion = _isFinal;

            ViewBag.isFinalVersion = pIsFinal;
            ViewBag.canISeeV3 = ModularityHelper.CanISeeV3(Guid.Parse(_compInfo.LeadID));
            ViewBag.firsTime = _firstTime;

            var _culture = Thread.CurrentThread.CurrentUICulture;
            if(_culture!=null || _culture.Name != "th")
            {
                ViewBag.Lenguaje = _culture.Name;
            }
            else
            {
                ViewBag.Lenguaje = "en";
            }
              
            return View(_softwareUpdateModel);
        }

        /// <summary>
        ///     Guarda la información referente al formulario Affidavit
        /// </summary>
        /// <param name="pCompanyInfo">ViewModel con la información general de la compañia</param>
        /// <param name="pFamilyModel">ViewModel con la informaión de los productos de la compañia</param>
        /// <param name="pAdditionalInfo">ViewModel con la información adicional de la compañia</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GuardarGeneralAffidavit(CompanyViewModel pCompanyInfo, FamilyViewModel pFamilyModel, AdditionalInfoViewModel pAdditionalInfo, AnswerQuestion pAnswers)
        {
            string _currentCulture = CultureHelper.GetCurrentCulture();
            int _lenguageID = _currentCulture == "es" ? 2 : 1;
            bool _isFinal = false;

            using (var tx = MDSService.BeginTx())
            {
                try
                {
                    //Se toma el campo azure del view model de las familias
                    pCompanyInfo.AzureFlag = pFamilyModel.AzureFlag;

                    if (pCompanyInfo.TipoCliente != TipoCliente.DeveloperPartner)
                    {
                        pAdditionalInfo.CustomOrBasicsApplications = null;
                        pAdditionalInfo.DevelopersPartnersApplicationsType = null;
                        pAdditionalInfo.DevelopersPartnersMicrosofTools = null;
                        pAdditionalInfo.DevelopersPartnersProjectsTypes = null;
                    }

                    if (pCompanyInfo.TipoCliente != TipoCliente.Academic)
                    {
                        pCompanyInfo.AcademicQttyAdmEmpFullTime = null;
                        pCompanyInfo.AcademicQttyAdmEmpPartialTime = null;
                        pCompanyInfo.AcademicQttyTeachersFullTime = null;
                        pCompanyInfo.AcademicQttyTeachersPartialTime = null;
                    }

                    //Guarda los comentarios para cada familia de productos
                    GuardarProductFamilyCompany(pCompanyInfo, pFamilyModel);

                    //Guarda los productos que se hayan registrado con licencias distintas de 0 en el MDS
                    GuardarInfoProductos(pCompanyInfo, pFamilyModel);

                    GuardarPartnerProgramCompanyData(pCompanyInfo);

                    GuardarPartnerCapabilityCompanyData(pCompanyInfo);

                    GuardarCompanyInfo(pCompanyInfo, pAdditionalInfo);

                    GuardarCompany(pCompanyInfo);

                    GuardarCompanyContacts(pCompanyInfo);

                    if (pAnswers.AnswerQuestions != null)
                    {
                        GuardarAnswerCompany(pAnswers, pCompanyInfo.CompanyID, pCompanyInfo.CampaignID);
                    }

                    //MDSService.CommitChangesAffidavit();
                    //Aqui se envia el correo si se hizo el commit correctamente
                    var _isFinalVersion = (pCompanyInfo.IsFinal == true) ? true : false;

                    if (_isFinalVersion)
                    {
                        bool requestSupport = (int)pAdditionalInfo.EstLicenciamiento == 3 ? true : false;
                        Guid _leadID = pCompanyInfo.LeadID;
                        EnviarCorreoMDSFinal(_leadID, requestSupport);
                        leadService.UpdateLeadInformation(_leadID, null, null, "submittedDateUpdate");
                        _isFinal = true;
                    }

                    if (pCompanyInfo.IsLogOut)
                    {
                        return RedirectToAction("LogOut", "Home", new { pLeadID = pCompanyInfo.LeadID });
                    }

                    MDSService.CommitChangesAffidavit();
                    MDSService.CommitTx(tx);
                }
                catch (Exception ex)
                {
                    MDSService.RollbackTx(tx); //Required according to MSDN article 
                    Response.StatusCode = 403;
                    return RedirectToAction("Index", "Error", new { error = Response.StatusCode, message = "" });
                    //throw; //Not in MSDN article, but recommended so the exception still bubbles up
                }
            }
            
            return RedirectToAction("Index", new { pIsFinal = _isFinal, pTabNumber = pCompanyInfo.CurrentTab });

        }

        /// <summary>
        ///     Envia un correo al consultor cuando la compañia hace submmit a su MDS.
        /// </summary>
        /// <param name="leadId">LeadID de la compañia sobre la cual se enviara el correo al consultor.</param>
        /// <returns></returns>
        private void EnviarCorreoMDSFinal(Guid leadId, bool requestSupport)
        {
            string requestSupportString = "";
            CRMDataDTO _leadCRM = CRMService.GetLeadByID(leadId);

            string MicrosoftConsultantEmail = _leadCRM.MicrosoftConsultantEmail;

            string EmailSubject = @TranslatorHelper.TranslateTextByIdentifier("Old_SubjectTextAffidavitCompletion");

            string EmailBody = @TranslatorHelper.TranslateTextByIdentifier("Old_BodyTextAffidavitCompletion");

            EmailSubject = EmailSubject.Replace("{CompanyName}", _leadCRM.CompanyName);

            EmailBody = EmailBody.Replace("{CompanyName}", _leadCRM.CompanyName);

            EmailBody = EmailBody.Replace("{ConsultantName}", _leadCRM.ConsultantName);


            if (requestSupport)
            {
                requestSupportString = @TranslatorHelper.TranslateTextByIdentifier("Old_RequestSupport");
            }

            EmailBody = EmailBody.Replace("{RequestSupport}", requestSupportString);

            SMTPService.EnviarCorreo(MicrosoftConsultantEmail, EmailSubject, EmailBody);

        }

        private string ComprobarAgreement(List<short> pAgreementID, List<string> pAgreement, short item)
        {
            string retorno = "";
            for (var i = 0; i < pAgreementID.Count(); i++)
            {
                if (pAgreementID[i] == item)
                {
                    retorno = pAgreement[i];
                    break;
                }
            }
            return retorno;
        }

        private short ComprobarOEM(List<short> pOEMID, List<short> pOEM, short item)
        {
            short retorno = 0;
            for (var i = 0; i < pOEMID.Count(); i++)
            {
                if (pOEMID[i] == item)
                {
                    retorno = pOEM[i];
                    break;
                }
            }
            return retorno;
        }


        private short ComprobarWEB(List<short> pWEBID, List<short> pWEB, short item)
        {
            short retorno = 0;
            if (pWEB != null)
            {
                for (var i = 0; i < pWEBID.Count(); i++)
                {
                    if (pWEBID[i] == item)
                    {
                        retorno = pWEB[i];
                        break;
                    }
                }
            }
            return retorno;
        }

        public class ServerProductId
        {
            public int ServerId { get; set; }
            public bool IsVirtual { get; set; }
            public string NewProductId { get; set; }
            public string OldProductId { get; set; }
            public bool IsRealProductId { get; set; }

        }

        public void GuardarAnswerCompany(AnswerQuestion pAnswers, int pCompanyID, short pCampaignID)
        {
            bool flagWs = false; //Esta flag da aviso de que ya se cambio el id asociado a los servidores de windows para que no se repita el proceso.
            bool flagWsPhysic = false; //Esta flag da aviso de que ya se cambio el inside asociado a los servidores virtuales de windows para que no se repita el proceso.
            bool flagSQL = false; //Esta flag da aviso de que ya se cambio el id asociado a los servidores de SQL para que no se repita el proceso.

            List<ServerProductId> listaWS = null;//Esta lista tiene los servidores de windows que cambiaron su id desde la ultima vez que se guardaron
            List<ServerProductId> listaSQL = null;//Esta lista tiene los servidores de SQL que cambiaron su id desde la ultima vez que se guardaron

            //Aqui se recorren todas las respuestas para las preguntas registradas. Cabe aclarar que de las 2 cantidades de preguntas (para Win y SQL) se tomará la mayor
            //es decir, puede que en esta estructura no hayan respuestas para algunas preguntas de los servidores, por lo que al principio se pregunta si para esa pregunta
            //hay componentes de Windows o SQL.
            //Si se recorre dicho componente se tiene que el contenido es la respuesta a dicha pregunta para cada uno de los servidores, discriminando por WIN o SQL
            foreach (var item in pAnswers.AnswerQuestions)
            {
                if (item.ServersWindows != null)
                {
                    //Ingresa aqui si no ha hecho el cambio de los id de los seervidores
                    if (!flagWs)
                    {
                        //################################################# PROCESO CUANDO SE CAMBIA EL ID DE UN SERVIDOR #####################################
                        var aux = item.ServersWindows.Where(x => x.QuestionID == 99998);

                        if (aux != null)
                        {
                            try
                            {
                                listaWS = new List<ServerProductId>();
                                foreach (var server in aux)
                                {
                                    if ((server.Answer != null && server.ProductID == short.Parse(server.Answer) && !server.IsRealProductID) ||
                                        !server.ProductID.ToString().Equals(server.Answer))
                                    {
                                        listaWS.Add(new ServerProductId
                                        {
                                            OldProductId = server.ProductID.ToString(),
                                            NewProductId = server.Answer ?? "84",
                                            ServerId = server.Posicion,
                                            IsVirtual = server.IsVirtual,
                                            IsRealProductId = server.Answer == null ? false : true

                                        });
                                    }
                                }

                                foreach (var server in listaWS)
                                {
                                    //Se cambian los productId si se cambio el id del producto
                                    answerCompanyService.UpdateProductIDAnswerCompany(pCompanyID, pCampaignID, server.ServerId, short.Parse(server.OldProductId), short.Parse(server.NewProductId), server.IsVirtual, server.IsRealProductId);
                                }

                            }
                            catch (Exception e)
                            {
                                listaWS = null;
                                //throw new Exception();
                            }



                            //Se cambia la bandera para que no se verifique de nuevo el cambio en el id de los servidores
                            flagWs = true;

                        }

                    }

                    //Ingresa aqui si no ha hecho el cambio de los inside de los seervidores virtuales de windows
                    if (!flagWsPhysic)
                    {
                        //################################################# PROCESO CUANDO SE CAMBIA EL INSIDE DE UN SERVIDOR VIRTUAL #####################################

                        var aux2 = item.ServersWindows.Where(x => x.QuestionID == 99999);

                        if (aux2 != null)
                        {
                            foreach (var server in aux2)
                            {
                                if (server.IsVirtual)
                                {
                                    //Se cambia el IsInside si se requiere
                                    answerCompanyService.UpdateIsInsideServer(pCompanyID, pCampaignID, server.Posicion, server.Answer ?? "-1");
                                    flagWsPhysic = true;
                                }
                            }

                        }
                    }

                    //Aqui se mira la respuesta a la pregunta actual para cada servidor
                    foreach (var acDTO in item.ServersWindows)
                    {
                        //Entra aqui si no se trata de preguntas para cambiar el producto o para el inside de los virtuales
                        if (acDTO.QuestionID != 99998 && acDTO.QuestionID != 99999)
                        {
                            //En esta variable se guardara si dicho servidor tiene un nuevo productID
                            string newProductID = null;

                            if (listaWS != null && listaWS.Count() > 0)
                            {
                                try
                                {
                                    newProductID = listaWS.Where(x => x.ServerId == acDTO.Posicion && x.IsVirtual == acDTO.IsVirtual).FirstOrDefault().NewProductId;
                                }
                                catch
                                {
                                    newProductID = null;
                                }

                            }

                            AnswerCompanyDTO _answerQuestionWindows = new AnswerCompanyDTO();


                            _answerQuestionWindows.CompanyID = acDTO.CompanyID;
                            _answerQuestionWindows.QuestionID = acDTO.QuestionID;
                            _answerQuestionWindows.CampaignID = acDTO.CampaignID;
                            _answerQuestionWindows.Answer = acDTO.Answer;
                            //_answerQuestionWindows.ProductID = newProductId != null? short.Parse(newProductId): acDTO.ProductID;
                            _answerQuestionWindows.ProductID =
                                (listaWS != null && newProductID != null) ? short.Parse(newProductID) : acDTO.ProductID;

                            _answerQuestionWindows.LicenseID = acDTO.Posicion;
                            _answerQuestionWindows.IsVLS = (acDTO.IsVLS == 1) ? true : false;
                            _answerQuestionWindows.IsOEM = (acDTO.IsOEM == 1) ? true : false;
                            _answerQuestionWindows.IsWEB = (acDTO.IsWEB == 1) ? true : false;
                            _answerQuestionWindows.IsFPP = (acDTO.IsFPP == 1) ? true : false;
                            _answerQuestionWindows.IsInstalledLicenses = (acDTO.IsInstalledLicenses == 1) ? true : false;
                            //TODO Verificar si es Virtual o fisico
                            _answerQuestionWindows.IsVirtual = acDTO.IsVirtual;
                            _answerQuestionWindows.IsNew = true;

                            var _answer = answerCompanyService.GetByCompositeKey(acDTO.CompanyID,
                                                                                    acDTO.QuestionID,
                                                                                    acDTO.CampaignID,
                                                                                    acDTO.Posicion,
                                                                                    (acDTO.ProductID != _answerQuestionWindows.ProductID) ? _answerQuestionWindows.ProductID : acDTO.ProductID,
                                                                                    acDTO.IsVirtual);

                            if (_answer == null)
                            {
                                answerCompanyService.Add(_answerQuestionWindows);
                            }
                            else
                            {
                                answerCompanyService.UpdateAnswerCompany(_answerQuestionWindows);
                            }
                        }

                    }
                }

                if (item.ServersSQL != null)
                {
                    //Ingresa aqui si no ha hecho el cambio de los id de los seervidores SQL
                    if (!flagSQL)
                    {
                        //################################################# PROCESO CUANDO SE CAMBIA EL ID DE UN SERVIDOR #####################################
                        var aux = item.ServersSQL.Where(x => x.QuestionID == 99998);

                        if (aux != null)
                        {
                            try
                            {
                                listaSQL = new List<ServerProductId>();
                                foreach (var server in aux)
                                {
                                    if ((server.Answer != null && server.ProductID == short.Parse(server.Answer) && !server.IsRealProductID) ||
                                        !server.ProductID.ToString().Equals(server.Answer))
                                    {
                                        listaSQL.Add(new ServerProductId
                                        {
                                            OldProductId = server.ProductID.ToString(),
                                            NewProductId = server.Answer ?? "139",
                                            ServerId = server.Posicion,
                                            IsVirtual = server.IsVirtual,
                                            IsRealProductId = server.Answer == null ? false : true

                                        });
                                    }
                                }

                                foreach (var server in listaSQL)
                                {
                                    //Se cambian los productId si se cambio el id del producto
                                    answerCompanyService.UpdateProductIDAnswerCompany(pCompanyID, pCampaignID, server.ServerId, short.Parse(server.OldProductId), short.Parse(server.NewProductId), server.IsVirtual, server.IsRealProductId);
                                }

                            }
                            catch (Exception e)
                            {
                                listaSQL = null;
                            }

                            flagSQL = true;

                        }

                    }


                    foreach (var acDTO in item.ServersSQL)
                    {
                        if (acDTO.QuestionID != 99998 && acDTO.QuestionID != 99999)
                        {
                            string newProductID = null;

                            if (listaSQL != null && listaSQL.Count() > 0)
                            {
                                try
                                {
                                    newProductID = listaSQL.Where(x => x.ServerId == acDTO.Posicion && x.IsVirtual == acDTO.IsVirtual).FirstOrDefault().NewProductId;
                                }
                                catch
                                {
                                    newProductID = null;
                                }

                            }

                            AnswerCompanyDTO _answerQuestionSQL = new AnswerCompanyDTO();

                            _answerQuestionSQL.CompanyID = acDTO.CompanyID;
                            _answerQuestionSQL.QuestionID = acDTO.QuestionID;
                            _answerQuestionSQL.CampaignID = acDTO.CampaignID;
                            _answerQuestionSQL.Answer = acDTO.Answer;
                            //_answerQuestionWindows.ProductID = newProductId != null? short.Parse(newProductId): acDTO.ProductID;
                            _answerQuestionSQL.ProductID =
                                (listaSQL != null && newProductID != null) ? short.Parse(newProductID) : acDTO.ProductID;

                            _answerQuestionSQL.LicenseID = acDTO.Posicion;
                            _answerQuestionSQL.IsVLS = (acDTO.IsVLS == 1) ? true : false;
                            _answerQuestionSQL.IsOEM = (acDTO.IsOEM == 1) ? true : false;
                            _answerQuestionSQL.IsWEB = (acDTO.IsWEB == 1) ? true : false;
                            _answerQuestionSQL.IsFPP = (acDTO.IsFPP == 1) ? true : false;
                            _answerQuestionSQL.IsInstalledLicenses = (acDTO.IsInstalledLicenses == 1) ? true : false;
                            //TODO Verificar si es Virtual o fisico
                            _answerQuestionSQL.IsVirtual = acDTO.IsVirtual;
                            _answerQuestionSQL.IsNew = true;

                            var _answer = answerCompanyService.GetByCompositeKey(acDTO.CompanyID,
                                                                                    acDTO.QuestionID,
                                                                                    acDTO.CampaignID,
                                                                                    acDTO.Posicion,
                                                                                    (acDTO.ProductID != _answerQuestionSQL.ProductID) ? _answerQuestionSQL.ProductID : acDTO.ProductID,
                                                                                    acDTO.IsVirtual);

                            if (_answer == null)
                            {
                                answerCompanyService.Add(_answerQuestionSQL);
                            }
                            else
                            {
                                answerCompanyService.UpdateAnswerCompany(_answerQuestionSQL);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        ///     Guarda la información de la tabla ProductFamilyCompany
        /// </summary>
        /// <param name="pCompanyInfo"></param>
        /// <param name="pFamilyModel"></param>
        public void GuardarProductFamilyCompany(CompanyViewModel pCompanyInfo, FamilyViewModel pFamilyModel)
        {
            List<ProductFamilyCompanyDTO> _productFamilyCompanyDTO = productFamilyCompanyService.GetByIDAndCampaign(pCompanyInfo.CompanyID, pCompanyInfo.CampaignID).ToList();
            List<ProductFamilyCompanyDTO> _productFamilyCompanyDTOList = new List<ProductFamilyCompanyDTO>();

            var j = 0;
            foreach (var item in pFamilyModel.ProductosComentariosAdicionales)
            {
                _productFamilyCompanyDTOList.Add(new ProductFamilyCompanyDTO
                {
                    CompanyID = pCompanyInfo.CompanyID,
                    CampaignID = pCompanyInfo.CampaignID,
                    ProductFamilyID = pFamilyModel.IDFamilyList[j],
                    AdditionalComments = pFamilyModel.ProductosComentariosAdicionales[j]
                });
                j++;
            }
            var _commentList = productFamilyCompanyService.GetByIDAndCampaign(pCompanyInfo.CompanyID, pCompanyInfo.CampaignID);

            foreach (var pfcDTO in _productFamilyCompanyDTOList)
            {
                var _commentItem = _commentList.Where(x => x.ProductFamilyID == pfcDTO.ProductFamilyID).FirstOrDefault();

                if (pfcDTO.AdditionalComments != "")
                {
                    if (_productFamilyCompanyDTO.Exists(x => x.ProductFamilyID == pfcDTO.ProductFamilyID))
                    {
                        productFamilyCompanyService.UpdateProductFamilycompany(pfcDTO);
                    }
                    else
                    {
                        productFamilyCompanyService.Add(pfcDTO);
                    }
                }
                else if (_commentItem != null)
                {
                    productFamilyCompanyService.Remove(_commentItem);
                }
            }
        }

        /// <summary>
        ///     Guarda la información referente al número y el tipo de licencias de cada producto
        /// </summary>
        /// <param name="pCompanyInfo"></param>
        /// <param name="pFamilyModel"></param>
        public void GuardarInfoProductos(CompanyViewModel pCompanyInfo, FamilyViewModel pFamilyModel)
        {

            List<ProductCompanyDTO> _productCompanyDTO = productCompanyService.GetByIDAndCampaign(pCompanyInfo.CompanyID, pCompanyInfo.CampaignID).ToList();
            List<ProductCompanyDTO> _productosCompanyDTOList = new List<ProductCompanyDTO>();
            var k = 0;

            foreach (var item in pFamilyModel.IDProductos)
            {
                _productosCompanyDTOList.Add(new ProductCompanyDTO
                {
                    CompanyID = pCompanyInfo.CompanyID,
                    ProductID = pFamilyModel.IDProductos[k],
                    CampaignID = pCompanyInfo.CampaignID,
                    Agreement = null/*ComprobarAgreement(pFamilyModel.IDContratos, pFamilyModel.Contratos, item)*/,
                    InstalledLicenses = pFamilyModel.ProductosInstalledLicenses[k],
                    VLS = pFamilyModel.ProductosVLS[k],
                    FPP = pFamilyModel.ProductosFPP[k],
                    WEB = ComprobarWEB(pFamilyModel.IDProductosWEB, pFamilyModel.ProductosWEB, item),
                    OEM = ComprobarOEM(pFamilyModel.IDProductosOEM, pFamilyModel.ProductosOEM, item)
                });
                k++;
            }

            int productAnsersNumer = _productCompanyDTO.Count;

            //Foreach para productos en mds 
            //_productCompanyDTO productos existentes en base de datos
            foreach (var item in _productosCompanyDTOList)
            {
                //Si existe el producto en la base de datos
                ProductCompanyDTO _currentProduct = null;

                //Si no hay productos guardados en la bd no busca
                if (productAnsersNumer > 0)
                {
                    _currentProduct = _productCompanyDTO.Find(x => x.ProductID == item.ProductID);
                }

                //Si se encuentra un producto entra
                if (_currentProduct != null)
                {
                    productAnsersNumer--;
                    if (item.InstalledLicenses == 0)
                    {
                        //Borre el producto  de la bd
                        productCompanyService.Remove(item);
                    }
                    else if (item.InstalledLicenses != _currentProduct.InstalledLicenses ||
                        item.OEM != _currentProduct.OEM ||
                        item.VLS != _currentProduct.VLS ||
                        item.FPP != _currentProduct.FPP)
                    {
                        //Actualicelo si cambio el numero de licencias
                        productCompanyService.UpdateProductcompany(item);
                    }

                }
                else if (item.VLS != 0 || item.FPP != 0 || item.OEM != 0 || item.WEB != 0 || item.InstalledLicenses != 0)
                {
                    productCompanyService.Add(item);
                }
            }
        }

        /// <summary>
        ///     Guarda y actualiza la información referente al Partner Program de una compañia
        /// </summary>
        /// <param name="pCompanyInfo"></param>
        public void GuardarPartnerProgramCompanyData(CompanyViewModel pCompanyInfo)
        {

            IEnumerable<PartnerProgramDTO> _partnerProgramDTO = partnerProgramSercive.GetAll();
            IEnumerable<PartnerProgramCompanyDTO> _programCompany = partnerProgramCompanyService.GetByCompositeKey(pCompanyInfo.CompanyID, pCompanyInfo.CampaignID);

            var j = 0;
            List<PartnerProgramCompanyDTO> _partnerProgramCompanyDTOList = new List<PartnerProgramCompanyDTO>();

            foreach (var item in _partnerProgramDTO)
            {
                _partnerProgramCompanyDTOList.Add(new PartnerProgramCompanyDTO
                {
                    CompanyID = pCompanyInfo.CompanyID,
                    CampaignID = pCompanyInfo.CampaignID,
                    PartnerProgramID = item.PartnerProgramID,
                    IDPartner = pCompanyInfo.ProgramasPartnerID[j],
                    ExpirationDate = pCompanyInfo.ProgramasDate[j]
                });
                j++;
            }

            List<PartnerProgramCompanyDTO> _programCompanyList = _programCompany.ToList();

            foreach (var ppDTO in _partnerProgramCompanyDTOList)
            {
                if (!ppDTO.IDPartner.Equals(""))
                {
                    if (_programCompanyList.Exists(x => x.PartnerProgramID == ppDTO.PartnerProgramID))
                    {
                        partnerProgramCompanyService.UpdatePartnerProgramCompany(ppDTO);
                    }
                    else
                    {
                        partnerProgramCompanyService.Add(ppDTO);
                    }
                }
            }

        }

        /// <summary>
        ///     Guarda y actualiza la información referente al Partner Capability de una compañia
        /// </summary>
        /// <param name="pCompanyInfo"></param>
        public void GuardarPartnerCapabilityCompanyData(CompanyViewModel pCompanyInfo)
        {

            IEnumerable<PartnerCapabilityDTO> _partnerCapabilityDTO = partnerCapabilityService.GetAll();
            IEnumerable<PartnerCapabilityCompanyDTO> _capabilityCompany = partnerCapabilityCompanyService.GetByCompositeKey(pCompanyInfo.CompanyID, pCompanyInfo.CampaignID);

            List<PartnerCapabilityCompanyDTO> _partnerCapabilityCompanyDTOList = new List<PartnerCapabilityCompanyDTO>();
            var i = 0;
            foreach (var item in _partnerCapabilityDTO)
            {
                _partnerCapabilityCompanyDTOList.Add(new PartnerCapabilityCompanyDTO
                {
                    CompanyID = pCompanyInfo.CompanyID,
                    CampaignID = pCompanyInfo.CampaignID,
                    PartnerCapabilityID = item.PartnerCapabilityID,
                    Category = pCompanyInfo.CompetenciasLevel[i],
                    IDPartner = pCompanyInfo.CompetenciasPartnerID[i],
                    RenovationDate = pCompanyInfo.CompetenciasDate[i]
                });
                i++;
            }

            List<PartnerCapabilityCompanyDTO> _capabilityCompanyList = _capabilityCompany.ToList();

            foreach (var pcDTO in _partnerCapabilityCompanyDTOList)
            {
                if (!pcDTO.Category.Equals(""))
                {
                    if (_capabilityCompanyList.Exists(x => x.PartnerCapabilityID == pcDTO.PartnerCapabilityID))
                    {
                        partnerCapabilityCompanyService.UpdatePartnerCapabilitycompany(pcDTO);
                    }
                    else
                    {
                        partnerCapabilityCompanyService.Add(pcDTO);
                    }
                }
            }

        }

        /// <summary>
        ///     Guarda la información relevante del affidavit
        /// </summary>
        /// <param name="pCompanyInfo"></param>
        /// <param name="pAdditionalInfo"></param>
        public void GuardarCompanyInfo(CompanyViewModel pCompanyInfo, AdditionalInfoViewModel pAdditionalInfo)
        {
            CompanyInfoDTO _companyInfoDTO = new CompanyInfoDTO();

            _companyInfoDTO.CustomOrBasicsApplications = (pAdditionalInfo.CustomOrBasicsApplications != null) ? ((pAdditionalInfo.CustomOrBasicsApplications.Value) ? "C" : "B") : null;
            _companyInfoDTO.DevelopersPartnersApplicationsType = pAdditionalInfo.DevelopersPartnersApplicationsType;
            _companyInfoDTO.DevelopersPartnersMicrosofTools = pAdditionalInfo.DevelopersPartnersMicrosofTools;
            _companyInfoDTO.DevelopersPartnersProjectsTypes = pAdditionalInfo.DevelopersPartnersProjectsTypes;
            _companyInfoDTO.CompanyID = pCompanyInfo.CompanyID;
            _companyInfoDTO.CampaignID = pCompanyInfo.CampaignID;
            //_companyInfoDTO.LeadID = pCompanyInfo.LeadID;
            _companyInfoDTO.TotalQuantityOfEmployees = pCompanyInfo.NumeroTotalEmpleados;
            _companyInfoDTO.TotalQuantityOfLaptops = pCompanyInfo.NumeroPortatiles;
            _companyInfoDTO.TotalQuantityOfDesktops = pCompanyInfo.NumeroPCsEscritorio;
            _companyInfoDTO.TotalQuantityOfPhysicalServers = pCompanyInfo.NumeroServidoresFisicos;
            _companyInfoDTO.TotalQuantityOfVirtualServers = pCompanyInfo.NumeroServidoresVirtuales;
            _companyInfoDTO.TotalQuantityOfSQLServers = pCompanyInfo.NumeroServidoresSQL;
            _companyInfoDTO.TotalQuantityPCWithOtherOS = pCompanyInfo.NumeroPCsOtrosSO;
            _companyInfoDTO.ExactNameInInvoicing = pCompanyInfo.NombreExactoFacturacion;
            _companyInfoDTO.FolderCreationDate = pCompanyInfo.Fecha;
            _companyInfoDTO.SoftwareAssuranceFlag = pAdditionalInfo.Assurance;
            _companyInfoDTO.StatusRenewalAndContratDetails = pAdditionalInfo.EstadoContrato;
            _companyInfoDTO.PlansToPurchaseNewLicensesFlag = pAdditionalInfo.NewLinceses;
            _companyInfoDTO.PlansToPurchaseNewLicensesComments = pAdditionalInfo.ExpliqueNewLinc;
            _companyInfoDTO.PlansToUpgradeCurrentlyOwnedLicensesFlag = pAdditionalInfo.UpgrateLicenses;
            _companyInfoDTO.PlansToUpgradeCurrentlyOwnedLicensesComments = pAdditionalInfo.ExpliqueUpgrateLinc;
            _companyInfoDTO.AuthorizedMicrosoftChannelFlag = pAdditionalInfo.AutMicChannel;
            _companyInfoDTO.AuthorizedChannel = pAdditionalInfo.AutChannel;
            _companyInfoDTO.MicrosoftPartnerContactName = pAdditionalInfo.ContactNameCanalAutorizado;
            _companyInfoDTO.MicrosoftPartnerPhoneNumber = pAdditionalInfo.TelefonoCanalAutorizado;
            _companyInfoDTO.MicrosoftPartnerEmail = pAdditionalInfo.EmailCanalAutorizado;
            _companyInfoDTO.AcademicQttyAdmEmpFullTime = pCompanyInfo.AcademicQttyAdmEmpFullTime;
            _companyInfoDTO.AcademicQttyAdmEmpPartialTime = pCompanyInfo.AcademicQttyAdmEmpPartialTime;
            _companyInfoDTO.AcademicQttyTeachersFullTime = pCompanyInfo.AcademicQttyTeachersFullTime;
            _companyInfoDTO.AcademicQttyTeachersPartialTime = pCompanyInfo.AcademicQttyTeachersPartialTime;
            //New
            _companyInfoDTO.AzureFlag = pCompanyInfo.AzureFlag;

            switch (pAdditionalInfo.EstLicenciamiento)
            {
                case EstadoLicenciamiento.Correcto:
                    _companyInfoDTO.LicenseStatusResponseID = 1;
                    break;
                case EstadoLicenciamiento.Diferencias:
                    _companyInfoDTO.LicenseStatusResponseID = 2;
                    break;
                case EstadoLicenciamiento.Apoyo:
                    _companyInfoDTO.LicenseStatusResponseID = 3;
                    break;
            }

            _companyInfoDTO.IsFinalVersion = (pCompanyInfo.IsFinal == true) ? true : false;


            var i = 0;
            foreach (var item in pCompanyInfo.ContratosPorVolumen)
            {
                switch (i)
                {
                    case 0:
                        _companyInfoDTO.VolumeLicenceNumber1 = item;
                        break;
                    case 1:
                        _companyInfoDTO.VolumeLicenceNumber2 = item;
                        break;
                    case 2:
                        _companyInfoDTO.VolumeLicenceNumber3 = item;
                        break;
                    case 3:
                        _companyInfoDTO.VolumeLicenceNumber4 = item;
                        break;
                    case 4:
                        _companyInfoDTO.VolumeLicenceNumber5 = item;
                        break;
                    case 5:
                        _companyInfoDTO.VolumeLicenceNumber6 = item;
                        break;
                    case 6:
                        _companyInfoDTO.VolumeLicenceNumber7 = item;
                        break;
                    case 7:
                        _companyInfoDTO.VolumeLicenceNumber8 = item;
                        break;
                    case 8:
                        _companyInfoDTO.VolumeLicenceNumber9 = item;
                        break;
                }
                i++;
            }
            companyInfoService.UpdateCompanyInfo(_companyInfoDTO);

        }

        /// <summary>
        /// obtiene la información del consultor para el popup de contacto
        /// </summary>
        /// <param name="pLeadID">Lead ID con el cual se consulta información del CRM</param>
        /// <returns>Vista parcial con la información del consultor</returns>
        public ActionResult ContacUs()
        {

            Guid _leadId = Guid.Parse(sessionState.Get<string>(ConstantsStringKeys.CURRENT_LEADID__KEY));
            Models.ContactUsViewModel _contactus = new Models.ContactUsViewModel();
            CRMDataDTO _leadCRM = CRMService.GetLeadByID(_leadId);
            // CRMDataDTO _leadCRM = CRMService.GetLeadByID(pLeadID);

            _contactus.ConsultantName = _leadCRM.ConsultantName;
            _contactus.EmailAddress = (_leadCRM.MicrosoftConsultantEmail != "") ? _leadCRM.MicrosoftConsultantEmail : @TranslatorHelper.TranslateTextByIdentifier("Old_LabelNotAvailable");
            _contactus.PhoneNumber = (_leadCRM.MicrosoftConsultantPhoneNumber != "") ? _leadCRM.MicrosoftConsultantPhoneNumber : @TranslatorHelper.TranslateTextByIdentifier("Old_LabelNotAvailable");

            return PartialView("_ContactUsViewPartial", _contactus);
        }

        /// <summary>
        ///     Guarda la información de la compañia.
        /// </summary>
        /// <param name="pCompanyInfo"></param>
        public void GuardarCompany(CompanyViewModel pCompanyInfo)
        {
            CompanyDTO _companyDTO = new CompanyDTO();

            switch (pCompanyInfo.TipoCliente)
            {
                case TipoCliente.Comercial:
                    _companyDTO.CompanyTypeID = 1;
                    break;
                case TipoCliente.Partner:
                    _companyDTO.CompanyTypeID = 2;
                    break;
                case TipoCliente.Academic:
                    _companyDTO.CompanyTypeID = 3;
                    break;
                case TipoCliente.DeveloperPartner:
                    _companyDTO.CompanyTypeID = 4;
                    break;
                case TipoCliente.PublicSector:
                    _companyDTO.CompanyTypeID = 5;
                    break;
            }

            //switch (pCompanyInfo.ProcesoCompra)
            //{
            //    case ProcesoCompra.RegisteredName:
            //        _companyDTO.PurchaseAndInvoicingMode = 1;
            //        break;
            //    case ProcesoCompra.NaturalPerson:
            //        _companyDTO.PurchaseAndInvoicingMode = 2;
            //        break;
            //    case ProcesoCompra.TradeName:
            //        _companyDTO.PurchaseAndInvoicingMode = 3;
            //        break;
            //}
            _companyDTO.PurchaseAndInvoicingMode = (pCompanyInfo.ProcesoCompra == true) ? 1 : 0;
            _companyDTO.CompanyID = pCompanyInfo.CompanyID;
            _companyDTO.CompanyName = pCompanyInfo.CompanyName;
            _companyDTO.IndustryID = pCompanyInfo.IndustryID;
            _companyDTO.UpdatedFromCRMFlag = true;

            companyService.UpdateCompany(_companyDTO);

        }

        /// <summary>
        ///     Guarda la información de contacto del affidavit
        /// </summary>
        /// <param name="pCompanyInfo"></param>
        public void GuardarCompanyContacts(CompanyViewModel pCompanyInfo)
        {

            CompanyContactsDTO _companyContactDTO = new CompanyContactsDTO();

            _companyContactDTO.CompanyID = pCompanyInfo.CompanyID;
            _companyContactDTO.CampaignID = pCompanyInfo.CampaignID;
            _companyContactDTO.CompanyEmail = pCompanyInfo.Email;
            _companyContactDTO.CompanyPhone = pCompanyInfo.Telefono;
            _companyContactDTO.ContactName = pCompanyInfo.NombreContacto;

            companyContactsService.UpdateCompanyContacts(_companyContactDTO);

        }

        /// <summary>
        ///     Action que rendiriza la vista de productos Microsoft
        /// </summary>
        /// <returns>Vista parcial con los productos</returns>
        ///     
        [Route("")]
        [Route("Index")]
        public ActionResult MicrosoftProductsPartial(int pCompanyID, short pCampaignID, string pLead, bool pIsFinal)
        {
            ViewBag.IDFamily = 0;
            //TODO: estos dos valores deben desaparecer (sacar del lead)
            //int _companyID = 16853;
            //short _campaignID = 1792;
            //SoftwareUpdateViewModel _softwareUpdateModel = new SoftwareUpdateViewModel();
            FamilyViewModel _familyModel = new FamilyViewModel();

            IEnumerable<ProductFamilyDTO> _productFamilyList = productFamilyService.GetAll().OrderBy(x => x.OrderFamily);
            IEnumerable<ProductCompanyDTO> _productCompanyList = productCompanyService.GetByIDAndCampaign(pCompanyID, pCampaignID);
            List<ProductFamilyCompanyDTO> _productFamilyCompanyDTO = productFamilyCompanyService.GetByIDAndCampaign(pCompanyID, pCampaignID).ToList();
            CompanyInfoDTO _companyInfo = companyInfoService.GetCompanyInfoByIDandCampaign(pCompanyID, pCampaignID);

            List<productosxVersionViewModel> _prodF = new List<productosxVersionViewModel>();


            List<Productos> _productosList;
            List<Versiones> _versionesList;

            IEnumerable<ProductDTO> _productList = productService.GetAll().Where(p => p.IsActive == true);
            var k = 0;
            foreach (var item in _productFamilyList)
            {
                bool _familyHasCAL = false;
                //_versiones = new List<string>();
                var productos = _productList.Where(x => x.ProductFamilyID == item.ProductFamilyID);

                //var productosOrdenados = _productList.Where(x => x.ProductFamilyID == item.ProductFamilyID).GroupBy(x => x.ProductVersionGroup).Select(x => new { Id = x.Key, OEMFlag = x.Max(z => z.OEMFlag), OrderVersion = x.Min(z => z.OrderVersion) }).OrderBy(s => s.OrderVersion);

                var versionesDistinct = _productList.Where(x => x.ProductFamilyID == item.ProductFamilyID).GroupBy(x => x.ProductVersionGroup).Select(x => new { Id = x.Key, OEMFlag = x.Max(z => z.OEMFlag), OrderVersion = x.Min(z => z.OrderVersion) }).OrderBy(s => s.OrderVersion);

                _versionesList = new List<Versiones>();

                foreach (var _vd in versionesDistinct)
                {
                    _versionesList.Add(new Versiones
                    {
                        version = _vd.Id,
                        Flag = _vd.OEMFlag
                    });

                }

                _productosList = new List<Productos>();
                foreach (var _prod in productos)
                {
                    short _VLS = 0;
                    short _FPP = 0;
                    short _OEM = 0;
                    short _WEB = 0;
                    short _Installed = 0;
                    //se marca si la familia tiene por lo menos una cal
                    if (_prod.IsCal)
                    {
                        _familyHasCAL = true;
                    }

                    var _cantidadDeArchivos = 0;
                    foreach (var _prodCom in _productCompanyList)
                    {
                        if (_prod.ProductID == _prodCom.ProductID)
                        {
                            _VLS = _prodCom.VLS;
                            _FPP = _prodCom.FPP;
                            _OEM = _prodCom.OEM;
                            _WEB = _prodCom.WEB;
                            _Installed = _prodCom.InstalledLicenses;

                            var _productFiles = productCompanyFileService.GetByIDAndCampaignAndProduct(pCompanyID, pCampaignID, _prod.ProductID);
                            if (_productFiles != null)
                            {
                                _cantidadDeArchivos = _productFiles.Count();
                            }
                        }
                    }
                    
                    

                    _productosList.Add(new Productos
                    {
                        Id = _prod.ProductID,
                        Nombre = _prod.ProductName,
                        NombreDisplay = _prod.ProductNameDisplay,
                        Version = _prod.ProductVersionGroup,
                        ProductVersion = _prod.ProductVersion,
                        InstalledLicenses = _Installed,
                        FilesAmount = (short)_cantidadDeArchivos,
                        VLS = _VLS,
                        FPP = _FPP,
                        OEM = _OEM,
                        WEB = _WEB,
                        DisplayOrder = _prod.DisplayOrder,
                        IsCal = _prod.IsCal
                    });

                }

                _prodF.Add(new productosxVersionViewModel
                {
                    FamiliaID = item.ProductFamilyID,
                    Nombre = item.ProductFamilyName,
                    Image = item.ProductFamilyImage,
                    ImageLarge = item.ProductFamilyImageLarge,
                    ImageLargeWidth = item.ProductFamilyImageLargeWidth,
                    ImageLargeHeight = item.ProductFamilyImageLargeHeight,
                    Comentario = AgregarComentario(item.ProductFamilyID, _productFamilyCompanyDTO),
                    Versiones = _versionesList,
                    Productos = _productosList,
                    FamilyHasCAL = _familyHasCAL
                });
                k++;
            }

            _familyModel.Families = _prodF;
            _familyModel.CompanyID = pCompanyID;
            _familyModel.CampaignID = pCampaignID;
            _familyModel.LeadID = Guid.Parse(pLead);
            _familyModel.IsFinal = pIsFinal;
            _familyModel.AzureFlag = _companyInfo.AzureFlag;

            return PartialView("_MicrosoftProductsPartial", _familyModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pProductFamilyID"></param>
        /// <param name="pProductFamilyCompany"></param>
        /// <returns></returns>
        private string AgregarComentario(short pProductFamilyID, List<ProductFamilyCompanyDTO> pProductFamilyCompany)
        {
            if (pProductFamilyCompany.Exists(x => x.ProductFamilyID == pProductFamilyID))
            {
                return pProductFamilyCompany.Where(x => x.ProductFamilyID == pProductFamilyID).Select(x => x.AdditionalComments).FirstOrDefault();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Funcion para obtener el numero de servidores de un cliente en una categoria
        /// que puede ser fisico, virtual o sql
        /// </summary>
        /// <param name="category"></param>
        /// <param name="pCompanyID"></param>
        /// <param name="pCampaignId"></param>
        /// <returns></returns>
        public int getNroServersbyCategory(string category, int pCompanyID, short pCampaignId)
        {

            int pFamilyID = 0;
            bool isVirtual = false;

            if (category == "physicServer")
            {
                pFamilyID = 3;
            }
            else if (category == "virtualServer")
            {
                pFamilyID = 3;
                isVirtual = true;
            }
            else
            {
                pFamilyID = 4;
            }

            List<AnswerCompanyDTO> servers = answerCompanyService.GetRegisteredServers(pCompanyID, pCampaignId, pFamilyID, isVirtual).ToList();

            int numberServer = servers.GroupBy(d => d.LicenseID).Count();

            return numberServer;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAdditionalQuestions(QuestionsViewModels model, short? pCampaignId, int? pCompanyId, Guid pLeadID, bool pIsFinal = false)
        {
            //Familia Windows Server == 3
            AdditionalQuestionCompleteViewModel _model = new AdditionalQuestionCompleteViewModel();
            try
            {
                _model = ProcessAdditionalQuestions(model.windowsServerInformation, 3, pCampaignId, pCompanyId, pLeadID);
                _model.IsFinal = pIsFinal;
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Error", new { error = Response.StatusCode, message = "" });

            }

            return PartialView("_AdditionalQuestionsPartial", _model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAdditionalQuestionsSQL(QuestionsViewModels model, short? pCampaignId, int? pCompanyId, Guid pLeadID, bool pIsFinal = false)
        {
            //Familia SQL Server ==4
            AdditionalQuestionCompleteViewModel _model = new AdditionalQuestionCompleteViewModel();
            try
            {
                _model = ProcessAdditionalQuestions(model.sqlServerInformation, 4, pCampaignId, pCompanyId, pLeadID);
                _model.IsFinal = pIsFinal;
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Error", new { error = Response.StatusCode, message = "" });

            }
            return PartialView("_AdditionalQuestionsSQLPartial", _model);
        }

        public ActionResult RemoveColumn(int pCompanyId, short pCampaignId, short pProductId, string pLicenseType, Guid pLead)
        {
            string _currentCulture = CultureHelper.GetCurrentCulture();

            bool isVLS = false, isFPP = false, isOEM = false, isWeb = false, isInstalledLicenses = false;

            switch (pLicenseType)
            {
                case "VLS":
                    isVLS = true;
                    break;
                case "FPP":
                    isFPP = true;
                    break;
                case "OEM":
                    isOEM = true;
                    break;
                case "WEB":
                    isWeb = true;
                    break;
                case "InstalledLicenses":
                    isInstalledLicenses = true;
                    break;
            }

            answerCompanyService.RemoveAnswer(pCompanyId, pCampaignId, pProductId, isVLS, isOEM, isFPP, isWeb, isInstalledLicenses);

            return RedirectToAction("Index");
        }




        public ActionResult RemoveServer(int pCompanyId, short pCampaignId, bool pIsVirtual, int pLicenseId, int pFamilyId, Guid pLead)
        {
            string _currentCulture = CultureHelper.GetCurrentCulture();

            using (var tx = MDSService.BeginTx())
            {
                try
                {
                    //se borra el servidor
                    answerCompanyService.RemoveServerByIdAndTypeAndFamily(pCompanyId, pCampaignId, pIsVirtual, pLicenseId, pFamilyId, true);
                    //Se actualiza la cantidad
                    UpdateQuantityCompanyInfo(pCompanyId, (short)pCampaignId, pFamilyId, -1, pIsVirtual);


                    MDSService.CommitChangesAffidavit();
                    MDSService.CommitTx(tx);
                }
                catch (Exception)
                {
                    MDSService.RollbackTx(tx); //Required according to MSDN article 
                    throw; //Not in MSDN article, but recommended so the exception still bubbles up
                }

            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AnswerQuestions"></param>
        /// <returns></returns>
        [HttpPost]
        public PartialViewResult AdditionalQuestionPost(List<AdditionalQuestion> pAnswerQuestions)
        {
            //List<AnswerCompanyDTO> _answerCompany = answerCompanyService.GetByIDAndCampaign(pAnswerQuestions.FirstOrDefault().CompanyID, pAnswerQuestions.FirstOrDefault().CampaignID).ToList();
            ServersViewModel _servers = new ServersViewModel();

            //List<AdditionalQuestion> _additional = new List<Models.AdditionalQuestion>();

            //pAnswerQuestions = pAnswerQuestions.OrderBy(x => x.ProductID).ThenBy(x => x.IsVLS).ThenBy(x => x.IsOEM).ThenBy(x => x.IsFPP).ThenBy(x => x.IsWEB).ToList();


            //foreach (var item in pAnswerQuestions)
            //{
            //    var _idProducto = item.ProductID;
            //    for (int i = 0; i < item.ProductQuantity; i++)
            //    {
            //        _additional.Add(new AdditionalQuestion
            //        {
            //            ProductID = item.ProductID,
            //            ProductName = item.ProductName,
            //            FamilyID = item.FamilyID,
            //            IsVLS = item.IsVLS,
            //            IsOEM = item.IsOEM,
            //            IsFPP = item.IsFPP,
            //            IsWEB = item.IsWEB,
            //            Posicion = i,
            //            Answer = item.Answer,
            //            CompanyID = item.CompanyID,
            //            CampaignID = item.CampaignID,
            //            QuestionID = item.QuestionID,

            //        });
            //    }
            //}
            //string _currentCulture = CultureHelper.GetCurrentCulture();
            ////obtiene el id de la cultura actual 
            //int _lenguageID = _currentCulture == "es" ? 2 : 1;

            //IEnumerable<ResponseDataTypeDTO> _resDataType = responseDataTypeService.GetAll();
            //IEnumerable<QuestionxLanguageDTO> _preguntasxLanguage = questionxLanguageService.GetAll()/*.Where(p => p.LanguageID == _lenguageID)*/;
            //IEnumerable<QuestionDTO> _questions = questionService.GetAll();
            //IEnumerable<LanguageDTO> _languages = languageService.GetAll();
            //IEnumerable<QuestionxProductFamilyDTO> _questionxProductFamily = questionxProductFamilyService.GetAll();
            //IEnumerable<ProductFamilyDTO> _productFamily = productFamilyService.GetAll();

            //var JoinPreguntas = from ques in _questions
            //                    join res in _resDataType on ques.ResponseDataTypeID equals res.ResponseDataTypeID
            //                    join pxl in _preguntasxLanguage on ques.QuestionID equals pxl.QuestionID
            //                    join lan in _languages on pxl.LanguageID equals lan.LanguageID
            //                    join qprodFam in _questionxProductFamily on ques.QuestionID equals qprodFam.QuestionID
            //                    join prodFam in _productFamily on qprodFam.ProductFamilyID equals prodFam.ProductFamilyID
            //                    select new
            //                    {
            //                        QuestionID = ques.QuestionID,
            //                        QuestionText = pxl.QuestionText,
            //                        ResDataType = ques.ResponseDataTypeID,
            //                        Description = res.ResponseDataTypeDescription,
            //                        ResDataTypeLength = res.ResponseDataTypeLength,
            //                        LanguageID = pxl.LanguageID,
            //                        LanguageName = lan.LanguageName,
            //                        ProductFamailyName = prodFam.ProductFamilyName,
            //                        productFamilyID = prodFam.ProductFamilyID,
            //                        DisplayOrder = qprodFam.DisplayOrder
            //                    };

            //var JoinPreguntasFiltrado = JoinPreguntas.Where(s => s.LanguageID == _lenguageID && s.productFamilyID == 3).OrderBy(s => s.DisplayOrder).ToList();
            //var data = new List<AdditionalQuestionViewModel>();

            //foreach (var item in JoinPreguntasFiltrado)
            //{
            //    data.Add(new AdditionalQuestionViewModel
            //    {
            //        Description = item.Description,
            //        DisplayOrder = item.DisplayOrder,
            //        LanguageID = item.LanguageID,
            //        LanguageName = item.LanguageName,
            //        ResDataTypeLength = item.ResDataTypeLength,
            //        ProductFamilyName = item.ProductFamailyName,
            //        QuestionID = item.QuestionID,
            //        QuestionText = item.QuestionText,
            //        ResDataType = item.ResDataType,
            //        ProductFamilyID = item.productFamilyID
            //    });
            //}

            //_servers.AdditionalQuestion = data;
            //_servers.AnswerQuestions = _additional;
            //_servers.ServersNumber = pAnswerQuestions.Sum(s => s.ProductQuantity);

            return PartialView("_AdditionalQuestionsPartial", _servers);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AnswerQuestions"></param>
        /// <returns></returns>
        [HttpPost]
        public PartialViewResult AdditionalQuestionSQLPost(List<AdditionalQuestion> pAnswerQuestions)
        {
            //List<AnswerCompanyDTO> _answerCompany = answerCompanyService.GetByIDAndCampaign(pAnswerQuestions.FirstOrDefault().CompanyID, pAnswerQuestions.FirstOrDefault().CampaignID).ToList();
            ServersViewModel _servers = new ServersViewModel();

            //List<AdditionalQuestion> _additional = new List<Models.AdditionalQuestion>();

            //pAnswerQuestions = pAnswerQuestions.OrderBy(x => x.ProductID).ToList();

            //foreach (var item in pAnswerQuestions)
            //{
            //    for (int i = 0; i < item.ProductQuantity; i++)
            //    {
            //        _additional.Add(new AdditionalQuestion
            //        {
            //            ProductID = item.ProductID,
            //            ProductName = item.ProductName,
            //            FamilyID = item.FamilyID,
            //            IsVLS = item.IsVLS,
            //            IsOEM = item.IsOEM,
            //            IsFPP = item.IsFPP,
            //            IsWEB = item.IsWEB,
            //            Posicion = i,
            //            Answer = item.Answer,
            //            CompanyID = item.CompanyID,
            //            CampaignID = item.CampaignID,
            //            QuestionID = item.QuestionID,

            //        });
            //    }
            //}
            //string _currentCulture = CultureHelper.GetCurrentCulture();
            ////obtiene el id de la cultura actual 
            //int _lenguageID = _currentCulture == "es" ? 2 : 1;

            //IEnumerable<ResponseDataTypeDTO> _resDataType = responseDataTypeService.GetAll();
            //IEnumerable<QuestionxLanguageDTO> _preguntasxLanguage = questionxLanguageService.GetAll()/*.Where(p => p.LanguageID == _lenguageID)*/;
            //IEnumerable<QuestionDTO> _questions = questionService.GetAll();
            //IEnumerable<LanguageDTO> _languages = languageService.GetAll();
            //IEnumerable<QuestionxProductFamilyDTO> _questionxProductFamily = questionxProductFamilyService.GetAll();
            //IEnumerable<ProductFamilyDTO> _productFamily = productFamilyService.GetAll();

            //var JoinPreguntas = from ques in _questions
            //                    join res in _resDataType on ques.ResponseDataTypeID equals res.ResponseDataTypeID
            //                    join pxl in _preguntasxLanguage on ques.QuestionID equals pxl.QuestionID
            //                    join lan in _languages on pxl.LanguageID equals lan.LanguageID
            //                    join qprodFam in _questionxProductFamily on ques.QuestionID equals qprodFam.QuestionID
            //                    join prodFam in _productFamily on qprodFam.ProductFamilyID equals prodFam.ProductFamilyID
            //                    select new
            //                    {
            //                        QuestionID = ques.QuestionID,
            //                        QuestionText = pxl.QuestionText,
            //                        ResDataType = ques.ResponseDataTypeID,
            //                        Description = res.ResponseDataTypeDescription,
            //                        ResDataTypeLength = res.ResponseDataTypeLength,
            //                        LanguageID = pxl.LanguageID,
            //                        LanguageName = lan.LanguageName,
            //                        ProductFamailyName = prodFam.ProductFamilyName,
            //                        productFamilyID = prodFam.ProductFamilyID,
            //                        DisplayOrder = qprodFam.DisplayOrder
            //                    };

            //var JoinPreguntasFiltrado = JoinPreguntas.Where(s => s.LanguageID == _lenguageID && s.productFamilyID == 4).OrderBy(s => s.DisplayOrder).ToList();
            //var data = new List<AdditionalQuestionViewModel>();

            //foreach (var item in JoinPreguntasFiltrado)
            //{
            //    data.Add(new AdditionalQuestionViewModel
            //    {
            //        Description = item.Description,
            //        DisplayOrder = item.DisplayOrder,
            //        LanguageID = item.LanguageID,
            //        LanguageName = item.LanguageName,
            //        ResDataTypeLength = item.ResDataTypeLength,
            //        ProductFamilyName = item.ProductFamailyName,
            //        QuestionID = item.QuestionID,
            //        QuestionText = item.QuestionText,
            //        ResDataType = item.ResDataType,
            //        ProductFamilyID = item.productFamilyID
            //    });
            //}

            //_servers.AdditionalQuestion = data;
            //_servers.AnswerQuestions = _additional;
            //_servers.ServersNumber = pAnswerQuestions.Sum(s => s.ProductQuantity);

            return PartialView("_AdditionalQuestionsSQLPartial", _servers);
        }

        /// <summary>
        ///    Consulta las tablas de las preguntas para SQL Server y Windows Server.
        /// </summary>
        public ActionResult AdditionalQuestion(List<AdditionalQuestion> AnswerQuestions)
        {
            ServersViewModel _servers = new ServersViewModel();
            string _currentCulture = CultureHelper.GetCurrentCulture();
            //obtiene el id de la cultura actual 
            int _lenguageID = _currentCulture == "es" ? 2 : 1;

            IEnumerable<ResponseDataTypeDTO> _resDataType = responseDataTypeService.GetAll();
            IEnumerable<QuestionxLanguageDTO> _preguntasxLanguage = questionxLanguageService.GetAll()/*.Where(p => p.LanguageID == _lenguageID)*/;
            IEnumerable<QuestionDTO> _questions = questionService.GetAll();
            IEnumerable<LanguageDTO> _languages = languageService.GetAll();
            IEnumerable<QuestionxProductFamilyDTO> _questionxProductFamily = questionxProductFamilyService.GetAll();
            IEnumerable<ProductFamilyDTO> _productFamily = productFamilyService.GetAll();

            var JoinPreguntas = from ques in _questions
                                join res in _resDataType on ques.ResponseDataTypeID equals res.ResponseDataTypeID
                                join pxl in _preguntasxLanguage on ques.QuestionID equals pxl.QuestionID
                                join lan in _languages on pxl.LanguageID equals lan.LanguageID
                                join qprodFam in _questionxProductFamily on ques.QuestionID equals qprodFam.QuestionID
                                join prodFam in _productFamily on qprodFam.ProductFamilyID equals prodFam.ProductFamilyID
                                select new
                                {
                                    QuestionID = ques.QuestionID,
                                    QuestionText = pxl.QuestionText,
                                    ResDataType = ques.ResponseDataTypeID,
                                    Description = res.ResponseDataTypeDescription,
                                    ResDataTypeLength = res.ResponseDataTypeLength,
                                    LanguageID = pxl.LanguageID,
                                    LanguageName = lan.LanguageName,
                                    ProductFamailyName = prodFam.ProductFamilyName,
                                    productFamilyID = prodFam.ProductFamilyID,
                                    DisplayOrder = qprodFam.DisplayOrder,
                                    IsActive = qprodFam.IsActive
                                };

            var JoinPreguntasFiltrado = JoinPreguntas.Where(s => s.LanguageID == _lenguageID && s.productFamilyID == 3).OrderBy(s => s.DisplayOrder).ToList();
            var data = new List<AdditionalQuestionViewModel>();

            foreach (var item in JoinPreguntasFiltrado)
            {
                data.Add(new AdditionalQuestionViewModel
                {
                    Description = item.Description,
                    DisplayOrder = item.DisplayOrder,
                    LanguageID = item.LanguageID,
                    LanguageName = item.LanguageName,
                    ResDataTypeLength = item.ResDataTypeLength,
                    ProductFamilyName = item.ProductFamailyName,
                    QuestionID = item.QuestionID,
                    QuestionText = item.QuestionText,
                    ResDataType = item.ResDataType,
                    ProductFamilyID = item.productFamilyID,
                    IsActive = item.IsActive
                });
            }

            var _data = data.Where(m => m.IsActive != 0);

            _servers.AdditionalQuestion = _data.ToList();
            //ViewBag.NumberServer = (ServerNumber == null) ? 0 : ServerNumber;
            //ViewBag.IDFamily = (FamilyID == null) ? 0 : FamilyID;
            //var _full = _fullJoin.Join(_preguntas, a => a.qi, c => c.QuestionID, (p, b) => new)

            return PartialView("_AdditionalQuestionsPartial", _servers);
        }

        /// <summary>
        ///    Consulta las tablas de las preguntas para SQL Server y Windows Server.
        /// </summary>
        public ActionResult AdditionalQuestionSQL(List<AdditionalQuestion> AnswerQuestions)
        {
            ServersViewModel _servers = new ServersViewModel();
            string _currentCulture = CultureHelper.GetCurrentCulture();
            //obtiene el id de la cultura actual 
            int _lenguageID = _currentCulture == "es" ? 2 : 1;

            IEnumerable<ResponseDataTypeDTO> _resDataType = responseDataTypeService.GetAll();
            IEnumerable<QuestionxLanguageDTO> _preguntasxLanguage = questionxLanguageService.GetAll()/*.Where(p => p.LanguageID == _lenguageID)*/;
            IEnumerable<QuestionDTO> _questions = questionService.GetAll();
            IEnumerable<LanguageDTO> _languages = languageService.GetAll();
            IEnumerable<QuestionxProductFamilyDTO> _questionxProductFamily = questionxProductFamilyService.GetAll();
            IEnumerable<ProductFamilyDTO> _productFamily = productFamilyService.GetAll();

            var JoinPreguntas = from ques in _questions
                                join res in _resDataType on ques.ResponseDataTypeID equals res.ResponseDataTypeID
                                join pxl in _preguntasxLanguage on ques.QuestionID equals pxl.QuestionID
                                join lan in _languages on pxl.LanguageID equals lan.LanguageID
                                join qprodFam in _questionxProductFamily on ques.QuestionID equals qprodFam.QuestionID
                                join prodFam in _productFamily on qprodFam.ProductFamilyID equals prodFam.ProductFamilyID
                                select new
                                {
                                    QuestionID = ques.QuestionID,
                                    QuestionText = pxl.QuestionText,
                                    ResDataType = ques.ResponseDataTypeID,
                                    Description = res.ResponseDataTypeDescription,
                                    ResDataTypeLength = res.ResponseDataTypeLength,
                                    LanguageID = pxl.LanguageID,
                                    LanguageName = lan.LanguageName,
                                    ProductFamailyName = prodFam.ProductFamilyName,
                                    productFamilyID = prodFam.ProductFamilyID,
                                    DisplayOrder = qprodFam.DisplayOrder,
                                    IsActive = qprodFam.IsActive
                                };

            var JoinPreguntasFiltrado = JoinPreguntas.Where(s => s.LanguageID == _lenguageID && s.productFamilyID == 4).OrderBy(s => s.DisplayOrder).ToList();
            var data = new List<AdditionalQuestionViewModel>();

            foreach (var item in JoinPreguntasFiltrado)
            {
                data.Add(new AdditionalQuestionViewModel
                {
                    Description = item.Description,
                    DisplayOrder = item.DisplayOrder,
                    LanguageID = item.LanguageID,
                    LanguageName = item.LanguageName,
                    ResDataTypeLength = item.ResDataTypeLength,
                    ProductFamilyName = item.ProductFamailyName,
                    QuestionID = item.QuestionID,
                    QuestionText = item.QuestionText,
                    ResDataType = item.ResDataType,
                    ProductFamilyID = item.productFamilyID,
                    IsActive = item.IsActive
                });
            }

            _servers.AdditionalQuestion = data;

            return PartialView("_AdditionalQuestionsSQLPartial", _servers);
        }

        /// <summary>
        ///     Action que renderiza el contenido del tab de info adicional
        /// </summary>
        /// <returns></returns>
        public ActionResult InformacionAdicionalPartial(int pCompanyID, short pCampaignID, string pLead)
        {
            //int _companyID = 16853;
            //short _campaignID = 1792;

            AdditionalInfoViewModel _additionalInfoModel = new AdditionalInfoViewModel();

            CompanyInfoDTO _additionalCompanyInfo = companyInfoService.GetCompanyInfoByIDandCampaign(pCompanyID, pCampaignID);

            var _tipoCliente = companyService.Get(pCompanyID).CompanyTypeID;

            if (_additionalCompanyInfo != null)
            {
                _additionalInfoModel.CustomOrBasicsApplications = (_additionalCompanyInfo.CustomOrBasicsApplications != null) ? ((_additionalCompanyInfo.CustomOrBasicsApplications) == "C" ? true : false) : (bool?)null;
                _additionalInfoModel.DevelopersPartnersApplicationsType = _additionalCompanyInfo.DevelopersPartnersApplicationsType;
                _additionalInfoModel.DevelopersPartnersMicrosofTools = _additionalCompanyInfo.DevelopersPartnersMicrosofTools;
                _additionalInfoModel.DevelopersPartnersProjectsTypes = _additionalCompanyInfo.DevelopersPartnersProjectsTypes;
                _additionalInfoModel.Assurance = _additionalCompanyInfo.SoftwareAssuranceFlag;
                _additionalInfoModel.EstadoContrato = _additionalCompanyInfo.StatusRenewalAndContratDetails;
                _additionalInfoModel.NewLinceses = _additionalCompanyInfo.PlansToPurchaseNewLicensesFlag;
                _additionalInfoModel.ExpliqueNewLinc = _additionalCompanyInfo.PlansToPurchaseNewLicensesComments;
                _additionalInfoModel.UpgrateLicenses = _additionalCompanyInfo.PlansToUpgradeCurrentlyOwnedLicensesFlag;
                _additionalInfoModel.ExpliqueUpgrateLinc = _additionalCompanyInfo.PlansToUpgradeCurrentlyOwnedLicensesComments;
                _additionalInfoModel.AutMicChannel = _additionalCompanyInfo.AuthorizedMicrosoftChannelFlag;
                _additionalInfoModel.AutChannel = _additionalCompanyInfo.AuthorizedChannel;
                _additionalInfoModel.ContactNameCanalAutorizado = _additionalCompanyInfo.MicrosoftPartnerContactName;
                _additionalInfoModel.EmailCanalAutorizado = _additionalCompanyInfo.MicrosoftPartnerEmail;
                _additionalInfoModel.TelefonoCanalAutorizado = _additionalCompanyInfo.MicrosoftPartnerPhoneNumber;
                _additionalInfoModel.IsFinal = _additionalCompanyInfo.IsFinalVersion;

                switch (_additionalCompanyInfo.LicenseStatusResponseID)
                {
                    case (byte)EstadoLicenciamiento.Correcto:
                        _additionalInfoModel.EstLicenciamiento = EstadoLicenciamiento.Correcto;
                        break;
                    case (byte)EstadoLicenciamiento.Diferencias:
                        _additionalInfoModel.EstLicenciamiento = EstadoLicenciamiento.Diferencias;
                        break;
                    case (byte)EstadoLicenciamiento.Apoyo:
                        _additionalInfoModel.EstLicenciamiento = EstadoLicenciamiento.Apoyo;
                        break;
                }

                switch (_tipoCliente)
                {
                    case (byte)TipoCliente.Comercial:
                        _additionalInfoModel.TipoDeCliente = TipoCliente.Comercial;
                        break;
                    case (byte)TipoCliente.Partner:
                        _additionalInfoModel.TipoDeCliente = TipoCliente.Partner;
                        break;
                    case (byte)TipoCliente.Academic:
                        _additionalInfoModel.TipoDeCliente = TipoCliente.Academic;
                        break;
                    case (byte)TipoCliente.DeveloperPartner:
                        _additionalInfoModel.TipoDeCliente = TipoCliente.DeveloperPartner;
                        break;
                    case (byte)TipoCliente.PublicSector:
                        _additionalInfoModel.TipoDeCliente = TipoCliente.PublicSector;
                        break;
                }
            }

            return PartialView("_InformacionAdicionalPartial", _additionalInfoModel);
        }

        /// <summary>
        ///     Action que renderiza el contenido del tab de info de la empresa
        /// </summary>
        /// <returns>Vista parcial y con viewModel para la empresa</returns>
        public ActionResult DatosBasicosEmpresaPartial(int pCompanyID, short pCampaignID, string pLead)
        {
            //    int _companyID = 16853;
            //    short _campaignID = 1792;
            CompanyViewModel _companyModel = new CompanyViewModel();
            //    ////TODO: cambiar el valor quemado del _leadID por el valor que venga de SomSigth
            //    Guid _leadID = new Guid("1ea8d94d-c4bf-e511-80e9-3863bb34abf8");

            string _currentCulture = CultureHelper.GetCurrentCulture();
            int _lenguageID = _currentCulture == "es" ? 2 : 1;

            CRMDataDTO _leadCRM = CRMService.GetLeadByID(Guid.Parse(pLead));

            //Guid id = _leadCRM.AccountNumber;
            CompanyDTO _companyDTO = companyService.Get(pCompanyID); //GetCompanyByAccountNumber(_leadCRM.AccountNumber);

            IEnumerable<PartnerCapabilityDTO> _partnerCapabilityDTO = partnerCapabilityService.GetAll();
            IEnumerable<PartnerProgramDTO> _partnerProgramDTO = partnerProgramSercive.GetAll();

            IEnumerable<CompetenciasViewModel> _competencias = Mapper.Map<IEnumerable<PartnerCapabilityDTO>, IEnumerable<CompetenciasViewModel>>(_partnerCapabilityDTO);
            IEnumerable<ProgramasViewModel> _programas = Mapper.Map<IEnumerable<PartnerProgramDTO>, IEnumerable<ProgramasViewModel>>(_partnerProgramDTO);

            //obtiene la lista de capabilities y programs para una compañia y una campaña
            IEnumerable<PartnerCapabilityCompanyDTO> _capabilityCompany = partnerCapabilityCompanyService.GetByCompositeKey(pCompanyID, pCampaignID);
            IEnumerable<PartnerProgramCompanyDTO> _programCompany = partnerProgramCompanyService.GetByCompositeKey(pCompanyID, pCampaignID);

            CompanyContactsDTO _companyContact = companyContactsService.GetByIDAndCampaign(pCompanyID, pCampaignID);
            CompanyInfoDTO _companyInfoDTO = companyInfoService.GetCompanyInfoByIDandCampaign(pCompanyID, pCampaignID);

            _companyModel.CompanyName = _companyDTO.CompanyName;
            _companyModel.NombreContacto = _companyContact.ContactName;
            if (_lenguageID == 2)
            {
                _companyModel.Industries = new SelectList(industryService.GetAll(), "IndustryID", "IndustrySpanishName", _companyDTO.IndustryID);
            }
            else
            {
                _companyModel.Industries = new SelectList(industryService.GetAll(), "IndustryID", "IndustryName", _companyDTO.IndustryID);

            }

            _companyModel.IsFinal = _companyInfoDTO.IsFinalVersion;
            _companyModel.Email = _companyContact.CompanyEmail;
            _companyModel.Telefono = _companyContact.CompanyPhone;

            switch (_companyDTO.CompanyTypeID)
            {
                case (byte)TipoCliente.Comercial:
                    _companyModel.TipoCliente = TipoCliente.Comercial;
                    break;
                case (byte)TipoCliente.Partner:
                    _companyModel.TipoCliente = TipoCliente.Partner;
                    break;
                case (byte)TipoCliente.Academic:
                    _companyModel.TipoCliente = TipoCliente.Academic;
                    break;
                case (byte)TipoCliente.DeveloperPartner:
                    _companyModel.TipoCliente = TipoCliente.DeveloperPartner;
                    break;
                case (byte)TipoCliente.PublicSector:
                    _companyModel.TipoCliente = TipoCliente.PublicSector;
                    break;
            }

            //switch (_companyDTO.PurchaseAndInvoicingMode)
            //{
            //    case (int)ProcesoCompra.RegisteredName:
            //        _companyModel.ProcesoCompra = ProcesoCompra.RegisteredName;
            //        break;
            //    case (int)ProcesoCompra.NaturalPerson:
            //        _companyModel.ProcesoCompra = ProcesoCompra.NaturalPerson;
            //        break;
            //    case (int)ProcesoCompra.TradeName:
            //        _companyModel.ProcesoCompra = ProcesoCompra.TradeName;
            //        break;
            //}
            _companyModel.ProcesoCompra = (_companyDTO.PurchaseAndInvoicingMode == 1) ? true : false;
            _companyModel.NumeroPCsEscritorio = _companyInfoDTO.TotalQuantityOfDesktops;
            _companyModel.NumeroPortatiles = _companyInfoDTO.TotalQuantityOfLaptops;
            _companyModel.NumeroPCsOtrosSO = _companyInfoDTO.TotalQuantityPCWithOtherOS;
            _companyModel.NumeroServidoresFisicos = _companyInfoDTO.TotalQuantityOfPhysicalServers;
            _companyModel.NumeroServidoresVirtuales = _companyInfoDTO.TotalQuantityOfVirtualServers;
            _companyModel.NumeroServidoresSQL = _companyInfoDTO.TotalQuantityOfSQLServers;
            _companyModel.NumeroTotalEmpleados = _companyInfoDTO.TotalQuantityOfEmployees;
            _companyModel.NombreExactoFacturacion = _companyInfoDTO.ExactNameInInvoicing;
            _companyModel.Fecha = _companyInfoDTO.FolderCreationDate;
            _companyModel.CapabilityCompanyDTOList = _capabilityCompany.ToList();
            _companyModel.ProgramCompanyDTOList = _programCompany.ToList();
            _companyModel.AcademicQttyAdmEmpFullTime = _companyInfoDTO.AcademicQttyAdmEmpFullTime;
            _companyModel.AcademicQttyAdmEmpPartialTime = _companyInfoDTO.AcademicQttyAdmEmpPartialTime;
            _companyModel.AcademicQttyTeachersFullTime = _companyInfoDTO.AcademicQttyTeachersFullTime;
            _companyModel.AcademicQttyTeachersPartialTime = _companyInfoDTO.AcademicQttyTeachersPartialTime;
            _companyModel.IndustryID = _companyDTO.IndustryID;

            List<string> _contratosPorVolumen = new List<string>();


            for (int i = 0; i < 9; i++)
            {
                switch (i)
                {
                    case 0:
                        _contratosPorVolumen.Add(_companyInfoDTO.VolumeLicenceNumber1);
                        break;
                    case 1:
                        _contratosPorVolumen.Add(_companyInfoDTO.VolumeLicenceNumber2);
                        break;
                    case 2:
                        _contratosPorVolumen.Add(_companyInfoDTO.VolumeLicenceNumber3);
                        break;
                    case 3:
                        _contratosPorVolumen.Add(_companyInfoDTO.VolumeLicenceNumber4);
                        break;
                    case 4:
                        _contratosPorVolumen.Add(_companyInfoDTO.VolumeLicenceNumber5);
                        break;
                    case 5:
                        _contratosPorVolumen.Add(_companyInfoDTO.VolumeLicenceNumber6);
                        break;
                    case 6:
                        _contratosPorVolumen.Add(_companyInfoDTO.VolumeLicenceNumber7);
                        break;
                    case 7:
                        _contratosPorVolumen.Add(_companyInfoDTO.VolumeLicenceNumber8);
                        break;
                    case 8:
                        _contratosPorVolumen.Add(_companyInfoDTO.VolumeLicenceNumber9);
                        break;
                }
            }
            _companyModel.ContratosPorVolumen = _contratosPorVolumen;
            ViewBag.IsPartner = (_companyDTO.CompanyTypeID == 2 || _companyDTO.CompanyTypeID == 4) ? true : false;

            //}
            //else
            //{
            //    _companyModel.Industries = new SelectList(industryService.GetAll(), "IndustryID", "IndustryName");
            //    _companyModel.CompanyName = _leadCRM.CompanyName;
            //    _companyModel.NombreContacto = _leadCRM.ContactName;
            //    _companyModel.Email = _leadCRM.Email;
            //    _companyModel.Telefono = _leadCRM.PhoneNumber;
            //    _companyModel.NumeroTotalEmpleados = _leadCRM.NumberOfEmployees;

            //}

            _companyModel.Competencias = _competencias;
            _companyModel.Programas = _programas;
            _companyModel.LeadID = Guid.Parse(pLead);
            _companyModel.CompanyID = pCompanyID;
            _companyModel.CampaignID = pCampaignID;



            return PartialView("_DatosBasicosEmpresaPartial", _companyModel);
        }


        /// <summary>
        ///     action que cambia el idioma de la aplicación.
        /// </summary>
        /// <param name="culture">string que contiene la cultura en la que se va a mostrar la aplicación</param>
        /// <returns></returns>
        //public ActionResult SetCulture(string culture, Guid leadID, string returnUrl)
        //{
        //    // Validate input
        //    culture = CultureHelper.GetImplementedCulture(culture);

        // Save culture in a cookie
        //    HttpCookie cookie = Request.Cookies["_culture"];
        //        if (cookie != null)
        //            cookie.Value = culture;   // update cookie value
        //        else
        //        {

        //            cookie = new HttpCookie("_culture");
        //    cookie.Value = culture;
        //            cookie.Expires = DateTime.Now.AddYears(1);
        //        }
        //Response.Cookies.Add(cookie);

        //        return Redirect(returnUrl);
        //return RedirectToAction("Index", new { pLeadID = leadID, pLanguageID = culture });
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //public ActionResult CreateNewQuestion()
        //{

        //    string _currentCulture = CultureHelper.GetCurrentCulture();
        //    int _lenguageID = _currentCulture == "es" ? 2 : 1;

        //    IEnumerable<ResponseDataTypeDTO> _resDataType = responseDataTypeService.GetAll();
        //    IEnumerable<QuestionxLanguageDTO> _preguntasxLanguage = questionxLanguageService.GetAll().Where(p => p.LanguageID == _lenguageID);
        //    //IEnumerable<QuestionDTO> _questions = questionService.GetAll();
        //    IEnumerable<LanguageDTO> _languages = languageService.GetAll();
        //    //IEnumerable<QuestionxProductFamilyDTO> _questionxProductFamily = questionxProductFamilyService.GetAll();
        //    IEnumerable<ProductFamilyDTO> _productFamily = productFamilyService.GetAll();

        //    CreateNewQuestionViewModel _newQuestion = new CreateNewQuestionViewModel();

        //    _newQuestion.Languages = new SelectList(_languages, "LanguageID", "LanguageName");
        //    _newQuestion.ResponseDataTypeList = new SelectList(_resDataType, "ResponseDataTypeID", "ResponseDataTypeDescription");
        //    _newQuestion.ProductFamilyList = new SelectList(_productFamily, "ProductFamilyID", "ProductFamilyName");

        //    return PartialView("_CreateNewQuestionPartial", _newQuestion);
        //}



        private List<AnswerquestionsViewModel> ProcessQuestions(IOrderedEnumerable<GoupServerInformation> pGroup, short pCampaignId, int pCompanyId)
        {
            List<AnswerquestionsViewModel> _serverByQuestion = new List<AnswerquestionsViewModel>();

            pGroup = pGroup.OrderBy(x => x.productId).ThenBy(x => x.VLS).ThenBy(x => x.OEM).ThenBy(x => x.FPP).ThenBy(x => x.WEB);

            foreach (var item in pGroup)
            {
                for (int i = 0; i < item.count; i++)
                {
                    _serverByQuestion.Add(new AnswerquestionsViewModel
                    {
                        campaignId = pCampaignId,
                        companyId = pCompanyId,
                        productFamilyComplete = item.familyComplete,
                        productId = item.productId,
                        productIsFPP = item.FPP,
                        productIsOEM = item.OEM,
                        productIsVLS = item.VLS,
                        productIsWeb = item.WEB,
                        productInstalledLicenses = item.InstalledLicenses,
                        IsNew = true,
                        productName = item.productName
                    });
                }
            }

            return _serverByQuestion;
        }


        private List<AnswerquestionsViewModel> ProcessQuestionsNew(List<AnswerquestionsViewModel> _serverByQuestion, short pCampaignId, int pCompanyId, int pQty, bool pIsVirtual, int pFamilyId, int pServerCountPhysic, int pServerCountVirtual)
        {
            List<AdditionalQuestionViewModel> _questions = GetQuestions(pFamilyId).Where(m => m.IsActive != 0).ToList();
            int limit = 0;

            if (!pIsVirtual)
            {
                limit = pQty - pServerCountPhysic;
            }
            else
            {
                limit = pQty - pServerCountVirtual;
            }

            for (int i = 0; i < limit; i++)
            {
                foreach (var item in _questions)
                {

                    answerCompanyService.Add(new AnswerCompanyDTO
                    {
                        CompanyID = pCompanyId,
                        QuestionID = item.QuestionID,
                        CampaignID = pCampaignId,
                        Answer = item.ResDataType == 1 ? "false" :
                                    (item.ResDataType == 2 ? "0" :
                                        (item.ResDataType == 3 ? "0" :
                                            (item.ResDataType == 4 ? null : "0")
                                        )
                                    ),
                        LicenseID = pIsVirtual ? pServerCountVirtual + i : pServerCountPhysic + i,
                        ProductID = pFamilyId == 3 ? (short)84 : (short)139, //Valor por defecto WS SQLServer
                        IsFPP = false,
                        IsOEM = false,
                        IsVLS = false,
                        IsWEB = false,
                        IsInstalledLicenses = true,
                        IsVirtual = pIsVirtual,
                        IsInside = -1,
                        IsNew = true

                    });
                }

                _serverByQuestion.Add(new AnswerquestionsViewModel
                {
                    campaignId = pCampaignId,
                    companyId = pCompanyId,
                    productFamilyComplete = pFamilyId == 3 ? "Windows Server" : "SQL Server",
                    productId = pFamilyId == 3 ? 84 : 139, //Valor por defecto WS SQLServer
                    productIsFPP = false,
                    productIsOEM = false,
                    productIsVLS = false,
                    productIsWeb = false,
                    productInstalledLicenses = true,
                    productName = "",
                    //Nuevo modelo,
                    IsNew = true,
                    IsVirtual = pIsVirtual,
                    LicenseId = pIsVirtual ? pServerCountVirtual + i : pServerCountPhysic + i,
                    IsInside = -1
                });


                //answerCompanyService.AddAnswer()
            }

            //MDSService.CommitChangesAffidavit();

            //TODO: Ordenar servidores que quedaron
            _serverByQuestion = _serverByQuestion.OrderBy(x => x.LicenseId).OrderBy(x => x.IsVirtual).ToList();

            return _serverByQuestion;
        }

        private List<AdditionalQuestionViewModel> GetQuestions(int pProductFamily)
        {
            string _currentCulture = CultureHelper.GetCurrentCulture();
            //obtiene el id de la cultura actual 
            int _lenguageID = _currentCulture == "es" ? 2 : 1;

            IEnumerable<ResponseDataTypeDTO> _resDataType = responseDataTypeService.GetAll();
            IEnumerable<QuestionxLanguageDTO> _preguntasxLanguage = questionxLanguageService.GetAll()/*.Where(p => p.LanguageID == _lenguageID)*/;
            IEnumerable<QuestionDTO> _questions = questionService.GetAll();
            IEnumerable<LanguageDTO> _languages = languageService.GetAll();
            IEnumerable<QuestionxProductFamilyDTO> _questionxProductFamily = questionxProductFamilyService.GetAll();
            IEnumerable<ProductFamilyDTO> _productFamily = productFamilyService.GetAll();

            var JoinPreguntas = from ques in _questions
                                join res in _resDataType on ques.ResponseDataTypeID equals res.ResponseDataTypeID
                                join pxl in _preguntasxLanguage on ques.QuestionID equals pxl.QuestionID
                                join lan in _languages on pxl.LanguageID equals lan.LanguageID
                                join qprodFam in _questionxProductFamily on ques.QuestionID equals qprodFam.QuestionID
                                join prodFam in _productFamily on qprodFam.ProductFamilyID equals prodFam.ProductFamilyID
                                select new
                                {
                                    QuestionID = ques.QuestionID,
                                    QuestionText = pxl.QuestionText,
                                    ResDataType = ques.ResponseDataTypeID,
                                    Description = res.ResponseDataTypeDescription,
                                    ResDataTypeLength = res.ResponseDataTypeLength,
                                    LanguageID = pxl.LanguageID,
                                    LanguageName = lan.LanguageName,
                                    ProductFamailyName = prodFam.ProductFamilyName,
                                    productFamilyID = prodFam.ProductFamilyID,
                                    DisplayOrder = qprodFam.DisplayOrder,
                                    IsActive = qprodFam.IsActive
                                };

            var JoinPreguntasFiltrado = JoinPreguntas.Where(s => s.LanguageID == _lenguageID && s.productFamilyID == pProductFamily).OrderBy(s => s.DisplayOrder).ToList();
            var data = new List<AdditionalQuestionViewModel>();

            foreach (var item in JoinPreguntasFiltrado)
            {
                data.Add(new AdditionalQuestionViewModel
                {
                    Description = item.Description,
                    DisplayOrder = item.DisplayOrder,
                    LanguageID = item.LanguageID,
                    LanguageName = item.LanguageName,
                    ResDataTypeLength = item.ResDataTypeLength,
                    ProductFamilyName = item.ProductFamailyName,
                    QuestionID = item.QuestionID,
                    QuestionText = item.QuestionText,
                    ResDataType = item.ResDataType,
                    ProductFamilyID = item.productFamilyID,
                    IsActive = item.IsActive
                });
            }

            return data;
        }

        private AdditionalQuestionCompleteViewModel ProcessAdditionalQuestions(List<ServersInformation> model, int pFamilyId, short? pCampaignId, int? pCompanyId, Guid pLeadID)
        {
            //Se obtiene información de la campaña y compañía
            short _campaingId;
            int _companyId;

            if (model == null)
            {
                _campaingId = pCampaignId ?? 0;
                _companyId = pCompanyId ?? 0;
            }
            else
            {
                _campaingId = model.First().campaignId;
                _companyId = model.First().companyId;
            }


            //Se obtienen las respuestas guardadas para la campaña y compaía
            IEnumerable<AnswerCompanyDTO> _answers = answerCompanyService.GetByIDAndCampaign(_companyId, _campaingId, pFamilyId);

            //Objetos para el procesamiento de los datos
            AdditionalQuestionCompleteViewModel _model = new AdditionalQuestionCompleteViewModel();
            List<AnswerquestionsViewModel> _serverByQuestion = new List<AnswerquestionsViewModel>();
            List<AnswerquestionsViewModel> _currentServerByQuestion = new List<AnswerquestionsViewModel>();
            List<DetailAnswers> _currentDetailAnswers;
            List<TableHeaderAnswerQuestion> _headerByServerQuestion = new List<TableHeaderAnswerQuestion>();
            List<AdditionalQuestionViewModel> _questions = new List<AdditionalQuestionViewModel>();


            //Se crean los datos para los datos actuales, los que ya se encuentran guardados
            var _currentAnswersGroup = _answers
                .GroupBy(x => new
                {
                    campaign = x.CampaignID,
                    productId = x.ProductID,
                    licenseId = x.LicenseID,
                    VLS = x.IsVLS,
                    OEM = x.IsOEM,
                    FPP = x.IsFPP,
                    WEB = x.IsWEB,
                    InstalledLicenses = x.IsInstalledLicenses,
                    FamilyComplete = x.FamilyComplete,
                    productName = x.ProductName,
                    //Nuevo modelo
                    IsNew = x.IsNew,
                    IsVirtual = x.IsVirtual,
                    LicenceId = x.LicenseID,
                    IsInside = x.IsInside,
                    IsRealProductID = x.IsRealProductId
                });


            foreach (var item in _currentAnswersGroup)
            {
                var _answersByServer = item.Select(y => new { questionId = y.QuestionID, answer = y.Answer, licenseId = y.LicenseID });
                _currentDetailAnswers = new List<DetailAnswers>();
                foreach (var answer in _answersByServer)
                {
                    _currentDetailAnswers.Add(new DetailAnswers
                    {
                        answer = answer.answer,
                        licenceId = answer.licenseId,
                        questionId = answer.questionId
                    });
                }

                _currentServerByQuestion.Add(new AnswerquestionsViewModel
                {
                    campaignId = _campaingId,
                    companyId = _companyId,
                    detailQuestions = _currentDetailAnswers,
                    productFamilyComplete = item.Key.FamilyComplete,
                    productId = item.Key.productId,
                    productIsFPP = item.Key.FPP,
                    productIsOEM = item.Key.OEM,
                    productIsVLS = item.Key.VLS,
                    productIsWeb = item.Key.WEB,
                    productInstalledLicenses = item.Key.InstalledLicenses,
                    productName = item.Key.productName,
                    //Nuevo modelo
                    IsNew = item.Key.IsNew,
                    IsVirtual = item.Key.IsVirtual,
                    LicenseId = item.Key.LicenceId,
                    IsInside = (short)item.Key.IsInside.GetValueOrDefault(),
                    IsRealProductID = item.Key.IsRealProductID
                });
            }

            _serverByQuestion.AddRange(_currentServerByQuestion);




            //Se agrupan la información de los servidores
            List<GoupServerInformation> _group = new List<GoupServerInformation>();
            List<AnswerquestionsViewModel> _resultsProcess = new List<AnswerquestionsViewModel>();

            //Aquí entra cuando se van a crear servidores
            if (model != null && model.Count() > 0)
            {
                //##############################################        VIEJO MODELO        ##########################################################
                if (!model.First().IsNew)
                {
                    _group = model
                    .GroupBy(x => new { productId = x.productId, VLS = x.productIsVLS, OEM = x.productIsOEM, FPP = x.productIsFPP, WEB = x.productIsWeb, InstalledLicenses = x.productInstalledLicenses, productName = x.productName, familyComplete = x.productFamilyComplete })
                    .Select(x => new GoupServerInformation { productId = x.Key.productId, VLS = x.Key.VLS, OEM = x.Key.OEM, FPP = x.Key.FPP, WEB = x.Key.WEB, InstalledLicenses = x.Key.InstalledLicenses, productName = x.Key.productName, familyComplete = x.Key.familyComplete, count = x.Sum(y => y.Qty) })
                    .ToList();

                    var _tipoLicenciaXProducto = productCompanyService.GetByIDAndCampaign(_companyId, _campaingId);

                    //Se restan las cantidades que tiene asignadas actualmente
                    foreach (var item in _group)
                    {
                        //cantidad actual
                        int _currCount = item.count;
                        int vls = 0;
                        int oem = 0;
                        int fpp = 0;
                        int web = 0;
                        int installedLic = 0;
                        if (_tipoLicenciaXProducto.Where(x => x.ProductID == item.productId).Count() > 0)
                        {
                            vls = (_tipoLicenciaXProducto != null) ? _tipoLicenciaXProducto.Where(x => x.ProductID == item.productId).First().VLS : 0;
                            oem = (_tipoLicenciaXProducto != null) ? _tipoLicenciaXProducto.Where(x => x.ProductID == item.productId).First().OEM : 0;
                            fpp = (_tipoLicenciaXProducto != null) ? _tipoLicenciaXProducto.Where(x => x.ProductID == item.productId).First().FPP : 0;
                            web = (_tipoLicenciaXProducto != null) ? _tipoLicenciaXProducto.Where(x => x.ProductID == item.productId).First().WEB : 0;
                            installedLic = (_tipoLicenciaXProducto != null) ? _tipoLicenciaXProducto.Where(x => x.ProductID == item.productId).First().InstalledLicenses : 0;
                        }

                        if (item.VLS)
                        {
                            item.count = _currCount - vls;
                        }
                        else if (item.OEM)
                        {
                            item.count = _currCount - oem;
                        }
                        else if (item.FPP)
                        {
                            item.count = _currCount - fpp;
                        }
                        else if (item.WEB)
                        {
                            item.count = item.count - web;
                        }
                        else if (item.InstalledLicenses)
                        {
                            item.count = _currCount - installedLic;
                        }
                    }

                    //Se convierte la agrupación en líneas por cada uno de los productos y su cantidad
                    _resultsProcess = ProcessQuestions(_group.OrderBy(y => y.familyComplete).ThenBy(y => y.VLS), _campaingId, _companyId);
                }
                //##############################################        NUEVO MODELO        ##########################################################
                //Hay que comprobar, segun el modelo que le llega, la familia, y la virtualidad, si ya existe en bd y agregar los existentes
                else
                {
                    //Se ordenan los datos de fisicos a virtuales
                    _serverByQuestion = _serverByQuestion.OrderBy(x => x.LicenseId).OrderBy(x => x.IsVirtual).ToList();

                    //var _tipoLicenciaXProducto = productCompanyService.GetByIDAndCampaign(_companyId, _campaingId);

                    //Debe revisar que seervers tiene y si el numero que viene en Qty es igual, no se hace nada, si es mayor se crean los necesarios  (Qty - reales), 
                    // y sies menor se le restan a los reales (reales - ( reales-Qty))


                    //Si hay servers para esa familia entonces los organiza por virtuales, y numero de server.
                    int _serverCountPhysic = 0;
                    int _serverCountVirtual = 0;

                    if (_serverByQuestion != null)// && _serverByQuestion.Count() > 0)
                    {
                        //Se cuentan cuantos servidores de ese tipo hay actualmente
                        _serverCountPhysic = _serverByQuestion.Where(x => x.IsVirtual == false).Count(); //Si no son virtuales
                        _serverCountVirtual = _serverByQuestion.Where(x => x.IsVirtual == true).Count(); //Si son virtuales


                        //se debe crear los servidores que faltan o se borran los necesarios
                        //group tiene los datos de los servidores existentes

                        //Cantidad ingresada en el campo
                        int qty = model.Last().Qty;

                        //Tipo de servidor (virtual o fisico)
                        bool _isVirtual = model.First().IsVirtual;

                        using (var tx = MDSService.BeginTx())
                        {
                            try
                            {
                                //Si la cantidad entrada es menor que el numero de servidores fisicos actuales y se trata de fisicos
                                //(Se borra)
                                if ((_serverCountPhysic > qty) && !_isVirtual)
                                {
                                    int serversToDelete = _serverCountPhysic - qty;

                                    for (int i = 0; i < serversToDelete; i++)
                                    {
                                        int currentServerId = _serverCountPhysic - 1;
                                        //TODO: Delete last Server from BD 

                                        //DIFEENCIAR ENTRE VIRTUAL Y FISICO
                                        answerCompanyService.RemoveServerByIdAndTypeAndFamily(_companyId, _campaingId, _isVirtual, currentServerId, pFamilyId, false);

                                        _serverByQuestion.RemoveAt(currentServerId);//Borra el ultimo elemento de la lista
                                        _serverCountPhysic--;//Resta un elemento
                                    }

                                    UpdateQuantityCompanyInfo(_companyId, _campaingId, pFamilyId, qty, _isVirtual);

                                }
                                //Si la cantidad entrada es menor que el numero de servidores virtuales actuales y es un servidor virtual
                                //(Se borra)
                                else if ((_serverCountVirtual > qty) && _isVirtual)
                                {
                                    int serversToDelete = _serverCountVirtual - qty;

                                    for (int i = 0; i < serversToDelete; i++)
                                    {
                                        int currentServerId = _serverCountVirtual - 1;
                                        //TODO: Delete last Server from BD 

                                        //DIFEENCIAR ENTRE VIRTUAL Y FISICO
                                        answerCompanyService.RemoveServerByIdAndTypeAndFamily(_companyId, _campaingId, _isVirtual, currentServerId, pFamilyId, false);

                                        currentServerId = _serverCountPhysic + _serverCountVirtual - 1;
                                        _serverByQuestion.RemoveAt(currentServerId);//Borra el ultimo elemento de la lista

                                        _serverCountVirtual--;//Resta un elemento
                                    }
                                    UpdateQuantityCompanyInfo(_companyId, _campaingId, pFamilyId, qty, _isVirtual);
                                }
                                //(Se agrega)
                                else if (((_serverCountPhysic < qty) && !_isVirtual) || ((_serverCountVirtual < qty) && _isVirtual))
                                {
                                    _serverByQuestion = ProcessQuestionsNew(
                                        _serverByQuestion,
                                        _campaingId,
                                        _companyId,
                                        qty,
                                        _isVirtual,
                                        pFamilyId,
                                        _serverCountPhysic,
                                        _serverCountVirtual);
                                    UpdateQuantityCompanyInfo(_companyId, _campaingId, pFamilyId, qty, _isVirtual);
                                }

                                MDSService.CommitChangesAffidavit();
                                MDSService.CommitTx(tx);
                            }
                            catch (Exception)
                            {
                                MDSService.RollbackTx(tx); //Required according to MSDN article 
                                throw; //Not in MSDN article, but recommended so the exception still bubbles up
                            }

                        }

                    }
                }
            }




            bool IsNewModel = false;

            try
            {
                IsNewModel = _serverByQuestion.First().IsNew;
            }
            catch
            {
                IsNewModel = false;
            }


            if (!IsNewModel)
            {
                //Se agregan los items nuevos
                _serverByQuestion.AddRange(_resultsProcess);
                //Se ordenan los datos
                _serverByQuestion = _serverByQuestion.OrderBy(x => x.productFamilyComplete).ThenBy(x => x.productId).ThenByDescending(x => x.productIsVLS).ThenByDescending(x => x.productIsOEM)
                        .ThenByDescending(x => x.productIsFPP).ThenByDescending(x => x.productIsWeb).ThenByDescending(x => x.productInstalledLicenses).ToList();
            }
            else
            {
                //Se ordenan los datos de fisicos a virtuales
                _serverByQuestion = _serverByQuestion.OrderBy(x => x.LicenseId).OrderBy(x => x.IsVirtual).ToList();
            }

            //############################################    CABECERAS     ###########################################################

            //Se contruye la cabecera de la tabla si es info nueva
            if ((model != null && model.Count() > 0 && model.First().IsNew) ||
                (_serverByQuestion != null && _serverByQuestion.Count() > 0 && _serverByQuestion.First().IsNew))
            {
                foreach (var item in _serverByQuestion)
                {
                    if (item.IsNew)
                    {
                        string _serverName = "";
                        string _familyComplete = "";
                        if (pFamilyId == 3)
                        {
                            _familyComplete = "Windows Server";
                            if (item.IsVirtual)
                            {
                                _serverName = "Virtual Server " + (item.LicenseId + 1); //Se le agrega  para que no empiece desde 0                            
                            }
                            else
                            {
                                _serverName = "Physical Server " + (item.LicenseId + 1);
                            }
                        }
                        else if (pFamilyId == 4)
                        {
                            _familyComplete = "SQL Server";
                            _serverName = "SQL Server " + (item.LicenseId + 1);

                        }


                        _headerByServerQuestion.Add(new TableHeaderAnswerQuestion
                        {
                            familyComplete = _familyComplete,
                            productName = _serverName,
                            IsNew = item.IsNew,
                            IsVirtual = item.IsVirtual,
                            LicenseId = item.LicenseId,
                            IsInside = item.IsInside,
                            productId = item.productId,
                            companyId = item.companyId,
                            campaignId = item.campaignId,
                            licenseType = (item.productIsVLS) ? "VLS" : (item.productIsOEM) ? "OEM" : (item.productIsFPP) ? "FPP" : (item.productInstalledLicenses) ? "InstalledLicenses" : "WEB"
                        });
                    }
                }
            }
            //Se contruye la cabecera de la tabla si es info vieja
            else
            {
                foreach (var item in _serverByQuestion)
                {
                    _headerByServerQuestion.Add(new TableHeaderAnswerQuestion
                    {
                        familyComplete = item.productFamilyComplete,
                        productName = item.productName,
                        IsNew = item.IsNew,
                        productId = item.productId,
                        companyId = item.companyId,
                        campaignId = item.campaignId,
                        licenseType = (item.productIsVLS) ? "VLS" : (item.productIsOEM) ? "OEM" : (item.productIsFPP) ? "FPP" : (item.productInstalledLicenses) ? "InstalledLicenses" : "WEB"
                    });
                }
            }



            //############################################    PREGUNTAS NORMALES     ##################################################

            if (_serverByQuestion.Count > 0)
            {
                _questions = GetQuestions(pFamilyId).Where(m => m.IsActive != 0).ToList();
            }



            //################################    PREGUNTAS Y RESPUESTAS NUEVAS SOBRE SERVERS     ########################################

            if ((model != null && model.Count() > 0 && model.First().IsNew) ||
                (_serverByQuestion != null && _serverByQuestion.Count() > 0 && _serverByQuestion.First().IsNew))
            {
                bool isInsertedFirstQuestion = false;
                bool isInsertedSecondQuestion = false;
                bool isInsertedFirstSQLQuestion = false;
                foreach (var item in _serverByQuestion)
                {
                    //###########################################   Windows Server      #################################################
                    if (pFamilyId == 3)
                    {
                        //Se inserta la pregunta para saber que version de ws tiene
                        if (_questions != null && _questions.Count() > 0)
                        {
                            if (!isInsertedFirstQuestion)
                            {
                                AdditionalQuestionViewModel firstQuestion = new AdditionalQuestionViewModel();
                                firstQuestion.QuestionID = 99998;
                                firstQuestion.ProductFamilyID = (short)pFamilyId;
                                firstQuestion.LanguageName = "English";
                                firstQuestion.LanguageID = 1;
                                firstQuestion.IsActive = 1;
                                firstQuestion.ResDataType = 5;
                                _questions.Insert(0, firstQuestion);
                                isInsertedFirstQuestion = true;
                            }



                            //Si es virtual
                            if (item.IsVirtual && !isInsertedSecondQuestion)
                            {
                                AdditionalQuestionViewModel secondQuestion = new AdditionalQuestionViewModel();
                                secondQuestion.QuestionID = 99999;
                                secondQuestion.ProductFamilyID = (short)pFamilyId;
                                secondQuestion.LanguageName = "English";
                                secondQuestion.LanguageID = 1;
                                secondQuestion.IsActive = 1;
                                secondQuestion.ResDataType = 6;
                                _questions.Insert(1, secondQuestion);
                                isInsertedSecondQuestion = true;
                            }
                        }

                        if (item.detailQuestions != null)
                        {
                            item.detailQuestions.Insert(0, new DetailAnswers
                            {
                                answer = item.IsRealProductID ? item.productId.ToString() : "0",
                                licenceId = item.LicenseId,
                                questionId = 99998
                            });

                            //Si es virtual
                            if (item.IsVirtual)
                            {
                                item.detailQuestions.Insert(1, new DetailAnswers
                                {
                                    answer = item.IsInside.ToString(),
                                    licenceId = item.LicenseId,
                                    questionId = 99999
                                });
                            }

                        }
                    }
                    //###########################################   SQL Server      #################################################
                    else if (pFamilyId == 4)
                    {
                        //Se inserta la pregunta para saber que version de ws tiene
                        if (_questions != null && _questions.Count() > 0 && !isInsertedFirstSQLQuestion)
                        {
                            AdditionalQuestionViewModel firstQuestion = new AdditionalQuestionViewModel();
                            firstQuestion.QuestionID = 99998;
                            firstQuestion.ProductFamilyID = (short)pFamilyId;
                            firstQuestion.LanguageName = "English";
                            firstQuestion.LanguageID = 1;
                            firstQuestion.IsActive = 1;
                            firstQuestion.ResDataType = 5;
                            _questions.Insert(0, firstQuestion);
                            isInsertedFirstSQLQuestion = true;
                        }

                        if (item.detailQuestions != null)
                        {
                            item.detailQuestions.Insert(0, new DetailAnswers
                            {
                                answer = item.IsRealProductID ? item.productId.ToString() : "0",
                                licenceId = item.LicenseId,
                                questionId = 99998
                            });
                        }
                    }
                }
            }

            //Se asignan los diferentes objetos al modelo

            _model.questions = _questions;
            _model.headerServerByQuestions = _headerByServerQuestion;
            _model.serverByQuestions = _serverByQuestion;
            _model.LeadID = pLeadID;

            return _model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pLeadID"></param>
        /// <returns></returns>
        public ActionResult SeeLetter()
        {
            Guid _leadId = Guid.Parse(sessionState.Get<string>(ConstantsStringKeys.CURRENT_LEADID__KEY));
            if (Session["lead"] == null)
            {
                return RedirectToAction("../Home/Login", new { pLeadId = _leadId });
            }

            var _acceptedLandingText = leadService.GetByLeadID(_leadId).AcceptedLandingText;
            var _landignVM = (LandingPageVM)Session["Landing"];
            _landignVM.LandingText = _acceptedLandingText;

            return View("LetterCampaign", _landignVM);
        }

        public void UpdateQuantityCompanyInfo(int pComapanyID, int pCampaignID, int pFamilyID, int qty, bool IsVirtual)
        {
            CompanyInfoDTO _companyInfo = companyInfoService.GetCompanyInfoByIDandCampaign(pComapanyID, (short)pCampaignID);
            if (pFamilyID == 3)
            {
                if (!IsVirtual)
                {
                    if (qty == -1)
                    {
                        _companyInfo.TotalQuantityOfPhysicalServers = (short)(_companyInfo.TotalQuantityOfPhysicalServers - 1);
                    }
                    else
                    {
                        _companyInfo.TotalQuantityOfPhysicalServers = (short)qty;
                    }

                }
                else
                {
                    if (qty == -1)
                    {
                        _companyInfo.TotalQuantityOfVirtualServers = (short)(_companyInfo.TotalQuantityOfVirtualServers - 1);
                    }
                    else
                    {
                        _companyInfo.TotalQuantityOfVirtualServers = (short)qty;
                    }
                }

            }
            else if (pFamilyID == 4)
            {
                if (qty == -1)
                {
                    _companyInfo.TotalQuantityOfSQLServers = (short)(_companyInfo.TotalQuantityOfSQLServers - 1);
                }
                else
                {
                    _companyInfo.TotalQuantityOfSQLServers = (short)qty;
                }
            }

            companyInfoService.UpdateCompanyInfo(_companyInfo);

            //MDSService.CommitChangesAffidavit();
        }








        [HttpPost]
        public JsonResult SubmitFile(HttpPostedFileBase[] file, Guid pLeadID, int pCompanyID, short pCampaignID, short pProductID, short pQtyInstalled, short pQtyVLS, short pQtyOEM, short pQtyFPP)
        {
            //Sharepoint(file);
            var _fileList = productCompanyFileService.GetByIDAndCampaignAndProduct(pCompanyID, pCampaignID, pProductID).OrderBy(x => x.FileNumber);

            //Comprobacion de restricciones de archivos
            if (file != null && file.Length > 0)
            {
                if ((_fileList.Count() + file.Count()) > 100)
                {
                    //Se retorna porque se excedio la cantidad de archivos
                    return Json(new { exito = 4 }, JsonRequestBehavior.AllowGet);
                }

                var allowedExtensions = new[] { ".pdf", ".jpg", ".jpeg", ".png" };
                foreach (HttpPostedFileBase auxFile in file)
                {
                    var checkextension = Path.GetExtension(auxFile.FileName).ToLower();

                    //Se valida la extension del archivo
                    if (!allowedExtensions.Contains(checkextension))
                    {
                        //Se retorna por tipo de archivo
                        return Json(new { exito = 2, fileName = auxFile.FileName }, JsonRequestBehavior.AllowGet);
                    }

                    //Se valida si el tamaño del archivo es mayor a 5mb
                    if (auxFile.ContentLength > 5242880)
                    {
                        //Se retorna por tamaño de archivo
                        return Json(new { exito = 3, fileName = auxFile.FileName }, JsonRequestBehavior.AllowGet);
                    }
                }

            }

            int _lastElementId = 0;
            ProductCompanyFileDTO _lastFile = new ProductCompanyFileDTO();


            if (_fileList != null && _fileList.Count() > 0)
            {
                _lastFile = _fileList.LastOrDefault();
                _lastElementId = _lastFile.FileNumber + 1;
            }


            if (file != null && file.Length > 0)
            {
                //Aqui se usa la transaccionalidad para garantizar que los productos que hayan en bd esten fisicamente
                using (var tx = MDSService.BeginTx())
                {
                    try
                    {
                        //Se intentan guardar las imagenes
                        foreach (HttpPostedFileBase aux in file)
                        {
                            string ext = aux.ContentType.Substring(aux.ContentType.LastIndexOf("/") + 1).ToLower();

                            //Se guarda en base de datos
                            productCompanyFileService.Add(new ProductCompanyFileDTO
                            {
                                AlternativeName = aux.FileName,
                                CampaignID = (short)pCampaignID,
                                CompanyID = pCompanyID,
                                Extension = ext,
                                ProductID = (short)pProductID,
                                FileNumber = (short)_lastElementId
                            });

                            //Se guarda en la ruta fisica
                            string mensaje = fileManagerService.SaveFile(aux, pLeadID, (short)pCompanyID, pCampaignID, pProductID, (short)_lastElementId);

                            if(mensaje!=null && mensaje.Count()>0)
                            {
                                throw new Exception();
                            }

                            _lastElementId++;

                        }

                        //Ahora se comprueba si la cantidad de licencias en uso ya existia en base de datos, si no existe, entonces se crea, sino, se actualiza
                        ProductCompanyDTO _currentProduct = productCompanyService.GetByFullCompositeKey(pCompanyID, pProductID, pCampaignID);

                        if (_currentProduct != null)
                        {
                            _currentProduct.InstalledLicenses = pQtyInstalled;
                            _currentProduct.VLS = pQtyVLS;
                            _currentProduct.OEM = pQtyOEM;
                            _currentProduct.FPP = pQtyFPP;
                            _currentProduct.WEB = 0;

                            productCompanyService.UpdateProductcompany(_currentProduct);
                        }
                        else
                        {
                            productCompanyService.Add(new ProductCompanyDTO
                            {
                                CampaignID = pCampaignID,
                                CompanyID = pCompanyID,
                                ProductID = pProductID,
                                InstalledLicenses = pQtyInstalled,
                                VLS = pQtyVLS,
                                OEM = pQtyOEM,
                                FPP = pQtyFPP,
                                WEB = 0
                            });
                        }

                        MDSService.CommitChangesAffidavit();
                        MDSService.CommitTx(tx);
                    }
                    catch (Exception e)
                    {
                        MDSService.RollbackTx(tx);
                        //Se borran los archivos fisicos (si existe) para ese producto de esa compañia de esa campaña 
                        //empieza a contar desde el filenumber actual hasta el file.Count + filenumber
                        if (_lastFile != null && _lastFile.AlternativeName != null)
                        {
                            for (int i = (_lastFile.FileNumber + 1); i <= (file.Count() + _lastFile.FileNumber); i++)
                            {
                                DeleteProductFiles(pLeadID, (short)pCompanyID, pCampaignID, pProductID, (short)i);
                            }
                        }

                        return Json(new { exito = 0 }, JsonRequestBehavior.AllowGet);
                    }
                }
            }


            return Json(new { exito = 1 }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult LoadProductFiles(int pCompanyID, int pCampaignID, int pProductID)
        {
            IEnumerable<ProductCompanyFileDTO> _productCompanyFileList = productCompanyFileService.GetByIDAndCampaignAndProduct(pCompanyID, (short)pCampaignID, (short)pProductID);
            ProductCompanyFileViewModel _productCompanyFile = new ProductCompanyFileViewModel();
            List<ProductFileViewModel> _products = new List<ProductFileViewModel>();

            if (_productCompanyFileList != null && _productCompanyFileList.Count() > 0)
            {
                foreach (var product in _productCompanyFileList)
                {
                    _products.Add(new ProductFileViewModel
                    {
                        AlternativeName = product.AlternativeName,
                        CampaignID = product.CampaignID,
                        CompanyID = product.CompanyID,
                        Extension = product.Extension,
                        FileNumber = product.FileNumber,
                        ProductID = product.ProductID
                    });
                }
            }

            _productCompanyFile.Products = _products;
            return PartialView("_ProductCompanyFileList", _productCompanyFile);
        }

        [HttpPost]
        public JsonResult DeleteProductFiles(Guid pLeadID, short pCompanyID, short pCampaignID, short pProductID, short pFileNumber)
        {
            ProductCompanyFileDTO _productCompanyFile = productCompanyFileService.GetByFullCompositeKey(pCompanyID, pProductID, pCampaignID, pFileNumber);

            if (_productCompanyFile != null)
            {
                using (var tx = MDSService.BeginTx())
                {
                    try
                    {
                        productCompanyFileService.Remove(_productCompanyFile);
                        MDSService.CommitChangesAffidavit();

                        string mensaje = fileManagerService.DeleteProductFile(pLeadID, pCompanyID, pCampaignID, pProductID, pFileNumber);

                        if (mensaje != null && mensaje.Count() > 0)
                        {
                            throw new Exception();
                        }

                        MDSService.CommitTx(tx);

                    }
                    catch (Exception e)
                    {
                        MDSService.RollbackTx(tx);

                        return Json(new { exito = 0 }, JsonRequestBehavior.AllowGet);
                    }
                }

            }
            else
            {
                try
                {
                    string mensaje = fileManagerService.DeleteProductFile(pLeadID, pCompanyID, pCampaignID, pProductID, pFileNumber);

                    if (mensaje != null && mensaje.Count() > 0)
                    {
                        throw new Exception();
                    }
                }
                catch (Exception e)
                {
                    return Json(new { exito = 0 }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { exito = 1 }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteAllFilesByProduct(Guid pLeadID, short pCompanyID, short pCampaignID, short pProductID)
        {
            using (var tx = MDSService.BeginTx())
            {

                try
                {
                    var _allProductFiles = productCompanyFileService.GetByIDAndCampaignAndProduct(pCompanyID, pCampaignID, pProductID);

                    productCompanyService.Remove(new ProductCompanyDTO { CompanyID = pCompanyID, ProductID = pProductID, CampaignID = pCampaignID });
                    productCompanyFileService.RemoveByIDAndCampaignAndProduct(pCompanyID, pCampaignID, pProductID);

                    MDSService.CommitChangesAffidavit();
                    
                    string mensaje = fileManagerService.DeleteAllFilesByProduct(pLeadID, pCompanyID, pCampaignID, pProductID);

                    if (mensaje != null && mensaje.Count() > 0)
                    {
                        throw new Exception();
                    }

                    MDSService.CommitTx(tx);
                }
                catch (Exception e)
                {
                    MDSService.RollbackTx(tx);                    

                    return Json(new { exito = 0 }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { exito = 1 }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult FilesCountByProduct(short pCompanyID, short pCampaignID, short pProductID)
        {
            var allFilesByProduct = productCompanyFileService.GetByIDAndCampaignAndProduct(pCompanyID, pCampaignID, pProductID);

            if (allFilesByProduct != null)
            {
                return Json(new { filesAmount = allFilesByProduct.Count() }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { filesAmount = 0 }, JsonRequestBehavior.AllowGet);
        }
    }
}