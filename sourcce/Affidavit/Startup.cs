using Affidavit.Utils;
using IRepositories;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Practices.Unity;
using Owin;
using Repositories;

[assembly: OwinStartupAttribute(typeof(Affidavit.Startup))]
namespace Affidavit
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            //var container = new UnityContainer();
            //container.RegisterType<IDataProtectionProvider>(new InjectionFactory(c => app.GetDataProtectionProvider()));
            //container.RegisterType<IDataProtectionProvider>(c=> app.GetDataProtectionProvider(), new PerRequestLifetimeManager());
            //container.RegisterType<IDataProtectionProvider>((InjectionMember)app.GetDataProtectionProvider()).call;
            //container.RegisterType<ISMTPConnectionProvider, SMTPConnectionProvider>();
        }
    }
}
