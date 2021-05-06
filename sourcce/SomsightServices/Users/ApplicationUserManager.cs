using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using SOMSightModels;
using SOMSightModels.Users;
using System;

namespace SOMSightServices
{
    public class ApplicationUserManager : UserManager<User, Guid>
    {

        public ApplicationUserManager(IUserStore<User, Guid> store, IDataProtectionProvider dataProtectionProvider) : base(store)
        {
            UserValidator = new UserValidator<User, Guid>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };


            //PasswordValidator = new PasswordValidator
            //{
            //    RequireDigit = true,
            //    RequiredLength = 6,
            //    RequireLowercase = true,
            //    RequireNonLetterOrDigit = false,
            //};

            UserLockoutEnabledByDefault = true;
            


            if (dataProtectionProvider != null)
            {
                UserTokenProvider =
                    new DataProtectorTokenProvider<User, Guid>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
        }
    }
}
