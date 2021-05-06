using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;

namespace Entities.Users
{
    public class ApplicationUserStore : UserStore<User, MyRole, Guid, MyLogin, UserRole, MyClaim>
    {
        public ApplicationUserStore(DbContext context) : base(context)
        {
        }
    }
}
