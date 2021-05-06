using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using IRepositories;
using IServices;
using Services;
using IUnitOfWork;
using UnitOfWork;
using Affidavit.Utils;
using System.Data.Entity;
using IServices.Landing;
using Services.Landing;
using IRepositories.Landing;
using IServices.SurveyQuestion;
using Services.SurveyQuestion;
using Microsoft.AspNet.Identity;
using Entities.Users;
using IRepositories.Users;
using IServices.Users;
using Services.Users;
using Affidavit.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Web;
using Owin;
using IServices.EmailQueue;
using Services.EmailQueue;
using IRepositories.EmailQueue;
using DTOs.Utils;
using Repositories;
using Repositories.Users;
using Repositories.Landing;
using Repositories.EmailQueue;
using IServices.Files;
using Services.Files;

namespace Affidavit.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            container.RegisterType<IUnitOfWork.IUnitOfWork, UnitOfWork.UnitOfWork>();
            container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<IProductFamilyService, ProductFamilyService>();
            container.RegisterType<IIndustryService, IndustryService>();
            container.RegisterType<IPartnerCapabilityService, PartnerCapabilityService>();
            container.RegisterType<IPartnerProgramService, PartnerProgramService>();
            container.RegisterType<ICompanyInfoService, CompanyInfoService>();
            container.RegisterType<IProductCompanyService, ProductCompanyService>();
            container.RegisterType<ICRMService, CRMService>();
            container.RegisterType<IQuestionxLanguageService, QuestionxLanguageService>();
            container.RegisterType<IQuestionService, QuestionService>();
            container.RegisterType<IResponseDataTypeService, ResponseDataTypeService>();
            container.RegisterType<ILanguageService, LanguageService>();
            container.RegisterType<IQuestionxProductFamilyService, QuestionxProductFamilyService>();
            container.RegisterType<ICompanyService, CompanyService>();
            container.RegisterType<ICompanyContactsService, CompanyContactsService>();
            container.RegisterType<IPartnerCapabilityCompanyService, PartnerCapabilityCompanyService>();
            container.RegisterType<IPartnerProgramCompanyService, PartnerProgramCompanyService>();
            container.RegisterType<IProductFamilyCompanyService, ProductFamilyCompanyService>();
            container.RegisterType<IAnswerCompanyService, AnswerCompanyService>();
            container.RegisterType<ICampaignService, CampaignService>();
            container.RegisterType<ICountryService, CountryService>();
            container.RegisterType<IAnswerCompanyService, AnswerCompanyService>();
            container.RegisterType<ISMTPService, SMTPService>();
            container.RegisterType<IMDSService, MDSService>();
            container.RegisterType<IEmailQueueService, EmailQueueService>();
            //Nuevos
            container.RegisterType<IProductCompanyFileService, ProductCompanyFileService>();
            container.RegisterType<IVariableService, VariableService>();
            container.RegisterType<IVariableProductService, VariableProductService>();
            container.RegisterType<IVariableProductFamilyService, VariableProductFamilyService>();
            container.RegisterType<ICompoundVariableService, CompoundVariableService>();

            //Register Services that belonged to SomSight
            container.RegisterType<ILeadService, LeadService>();
            container.RegisterType<ILandingCampaignService, LandingCampaignService>();
            container.RegisterType<ISurveyQuestionResponseService, SurveyQuestionResponseService>();
            container.RegisterType<IUserService, UserService>();

            //Servicios para subir archivos
            container.RegisterType<IFileManagerService, FileManagerService>();

            //container.RegisterType<IAffidavitContext, AffidavitContext>();
            //container.RegisterType<ICampaignRepository, CampaignRepository>();
            container.RegisterType<IProductFamilyRepository, ProductFamilyRepository>();
            container.RegisterType<IIndustryRepository, IndustryRepository>();
            container.RegisterType<IPartnerCapabilityRepository, PartnerCapabilityRepository>();
            container.RegisterType<IPartnerProgramRepository, PartnerProgramRepository>();
            container.RegisterType<ICompanyInfoRepository, CompanyInfoRepository>();
            container.RegisterType<IProductCompanyRepository, ProductCompanyRepository>();
            container.RegisterType<ICRMRepository, CRMRepository>();
            container.RegisterType<IQuestionxLanguageRepository, QuestionxLanguageRepository>();
            container.RegisterType<IQuestionRepository, QuestionRepository>();
            container.RegisterType<IResponseDataTypeRepository, ResponseDataTypeRepository>();
            container.RegisterType<ILanguageRepository, LanguageRepository>();
            container.RegisterType<IQuestionxProductFamilyRepository, QuestionxProductFamilyRepository>();
            container.RegisterType<ICompanyRepository, CompanyRepository>();
            container.RegisterType<ICompanyContactsRepository, CompanyContactsRepository>();
            container.RegisterType<IPartnerCapabilityCompanyRepository, PartnerCapabilityCompanyRepository>();
            container.RegisterType<IPartnerProgramCompanyRepository, PartnerProgramCompanyRepository>();
            container.RegisterType<IProductFamilyCompanyRepository, ProductFamilyCompanyRepository>();
            container.RegisterType<IAnswerCompanyRepository, AnswerCompanyRepository>();
            container.RegisterType<ICampaignRepository, CampaignRepository>();
            container.RegisterType<ICountryRepository, CountryRepository>();
            container.RegisterType<IAnswerCompanyRepository, AnswerCompanyRepository>();
            container.RegisterType<ISMTPRepository, SMTPRepository>();
            container.RegisterType<IEmailQueueRepository, EmailQueueRepository>();
            container.RegisterType<IProductCompanyFileRepository, ProductCompanyFileRepository>();
            container.RegisterType<IVariableRepository, VariableRepository>();
            container.RegisterType<IVariableProductRepository, VariableProductRepository>();
            container.RegisterType<IVariableProductFamilyRepository, VariableProductFamilyRepository>();
            container.RegisterType<ICompoundVariableRepository, CompoundVariableRepository>();

            //Register Repositories that belonged to SomSight
            container.RegisterType<ILeadRepository, LeadRepository>();
            container.RegisterType<ILandingCampaignRepository, LandingCampaignRepository>();
            container.RegisterType<ISurveyQuestionResponseRepository, SurveyQuestionResponseRepository>();
            container.RegisterType<IUserRepository, UserRepository>();            

            container.RegisterType<ISMTPConnectionProvider, SMTPConnectionProvider>();
            //container.RegisterType<ISOMSightProvider, SOMSightProvider>();
            container.RegisterType<ICRMConnectionProvider, CRMConnectionProvider>();

            container.RegisterType<ISharePointProvider, SharePointProvider>();
            container.RegisterType<ISAM360Provider, SAM360Provider>();
            container.RegisterType<ISAM360Service, SAM360Service>();

            container.RegisterType<IAssessmentService, AssessmenstService>();
            container.RegisterType<IAssessmentSummaryRepository, AssessmentSummaryRepository>();
            container.RegisterType<IAssessmentQuestionService, AssessmentQuestionService>();
            container.RegisterType<IAssessmentQuestionRepository, AssessmentQuestionRepository>();


            container.RegisterType<ITranslationXLanguageRepository, TranslationXLanguageRepository>();
            container.RegisterType<ITranslatorService, TranslatorService>();
            container.RegisterType<ITranslatorUtility, TranslatorUtility>(new PerRequestLifetimeManager());

            container.RegisterType<ICodeGenerator, CodeGenerator>();


            container.RegisterType<DbContext, AffidavitContext>(new PerRequestLifetimeManager());
            container.RegisterType<IUserStore<User, Guid>, ApplicationUserStore>(new PerRequestLifetimeManager());
            container.RegisterType<ApplicationUserManager>(new PerRequestLifetimeManager());
            container.RegisterType<ApplicationSignInManager>(new PerRequestLifetimeManager());
            container.RegisterType<IAuthenticationManager>(new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));
            container.RegisterType<HttpSessionStateBase>(new InjectionFactory(x =>
                new HttpSessionStateWrapper(HttpContext.Current.Session)));

            container.RegisterType<ISessionState, DefaultSessionState>(new PerRequestLifetimeManager());

            container.RegisterType<IMDSAccountService, MDSAccountService>();

            //container.Resolve<MyApplicationUserManager>();
            //container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(new HierarchicalLifetimeManager());
            
            container.RegisterType<IChooseAnActionService, ChooseAnActionService>();

            //Industry Insights
            container.RegisterType<IIndustryInsightsService, IndustryInsightsService>();
            container.RegisterType<IIndustryInsightsRepository, IndustryInsightsRepository>();
            container.RegisterType<IProcessPreLoadedRepository, ProcessPreLoadedRepository>();
            container.RegisterType<IProblemPreLoadedRepository, ProblemPreLoadedRepository>();
            container.RegisterType<IDigitalTransformationPreLoadedRepository, DigitalTransformationPreLoadedRepository>();
            container.RegisterType<IProcessRepository, ProcessRepository>();
            container.RegisterType<IProcessService, ProcessService>();
            container.RegisterType<IProblemRepository, ProblemRepository>();
            container.RegisterType<IProblemService, ProblemService>();
            container.RegisterType<IDigitalTransformationRepository, DigitalTransformationRepository>();
            container.RegisterType<IDigitalTransformationService, DigitalTransformationService>();

            container.RegisterType<IActivityService, ActivityService>();
            container.RegisterType<IActivityRepository, ActivityRepository>();
        }
    }
}
