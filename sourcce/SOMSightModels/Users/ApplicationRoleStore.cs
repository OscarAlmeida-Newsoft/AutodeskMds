using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SOMSightModels.Users
{
    public class ApplicationRoleStore : RoleStore<Role, Guid, UserRole>
    {
        public ApplicationRoleStore(DbContext context) : base(context)
        {
        }
    }
}
