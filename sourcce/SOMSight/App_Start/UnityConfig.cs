using ISOMSightRepositories;
using ISOMSightRepositories.Common;
using ISOMSightServices;
using ISOMSightServices.Users;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Practices.Unity;
using SOMSight.Utils;
using SOMSightModels.Users;
using SOMSightModels.Utils;
using SOMSightRepositories;
using SOMSightRepositories.Common;
using SOMSightServices;
using SOMSightServices.Users;
using System;
using System.Data.Entity;
using System.Web;

namespace SOMSight.App_Start
{
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
            container.RegisterType<IUnitOfWork, UnitOfWork>();

            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IEmailService, EmailService>();

            container.RegisterType<DbContext, SOMSightContext>(new PerRequestLifetimeManager());
            container.RegisterType<IUserStore<User, Guid>, ApplicationUserStore>(new PerRequestLifetimeManager());
            container.RegisterType<IRoleStore<Role, Guid>, ApplicationRoleStore>(new PerRequestLifetimeManager());
            container.RegisterType<IDataProtectionProvider>(new InjectionFactory(c => Startup.DataProtectionProvider));

            container.RegisterType<ApplicationUserManager>(new PerRequestLifetimeManager());
            container.RegisterType<ApplicationSignInManager>(new PerRequestLifetimeManager());
            container.RegisterType<IAuthenticationManager>(new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));

            container.RegisterType<ISessionState, DefaultSessionState>(new PerRequestLifetimeManager());

            container.RegisterType<ITranslatorService, TranslatorService>();
            container.RegisterType<ITranslatorUtility, TranslatorUtility>(new PerRequestLifetimeManager());

            container.RegisterType<IAssessmentService, AssessmenstService>();
            container.RegisterType<IAssessmentSummaryRepository, AssessmentSummaryRepository>();
            container.RegisterType<IAssessmentQuestionService, AssessmentQuestionService>();
            container.RegisterType<IAssessmentQuestionRepository, AssessmentQuestionRepository>();
            container.RegisterType<IChooseAnActionService, ChooseAnActionService>();

            container.RegisterType<HttpSessionStateBase>(new InjectionFactory(x =>
                    new HttpSessionStateWrapper(HttpContext.Current.Session)));
        }
    }
}