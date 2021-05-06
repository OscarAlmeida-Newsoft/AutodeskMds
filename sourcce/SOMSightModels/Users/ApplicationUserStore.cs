using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOMSightModels.Users
{
    public class ApplicationUserStore : UserStore<User, Role, Guid, MyLogin, UserRole, MyClaim>
    {
        public ApplicationUserStore(DbContext context) : base(context)
        {
        }
    }
}
