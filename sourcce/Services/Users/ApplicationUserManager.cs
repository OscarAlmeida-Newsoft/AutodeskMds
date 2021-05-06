using Entities.Users;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Users
{
    public class MyApplicationUserManager : UserManager<User, Guid>
    {
        public MyApplicationUserManager(IUserStore<User, Guid> store /*, IDataProtectionProvider dataProtectionProvider*/) : base(store)
        {
            UserValidator = new UserValidator<User, Guid>(this)
            {
                AllowOnlyAlphanumericUserNames = true,
                RequireUniqueEmail = true
            };


            PasswordValidator = new PasswordValidator
            {
                RequireDigit = false,
                RequiredLength = 3,
                RequireLowercase = false,
                RequireNonLetterOrDigit = false,
            };

            UserLockoutEnabledByDefault = false;


            /*if (dataProtectionProvider != null)
            {
                UserTokenProvider =
                    new DataProtectorTokenProvider<User, Guid>(dataProtectionProvider.Create("ASP.NET Identity"));
            }*/
        }
    }
}
