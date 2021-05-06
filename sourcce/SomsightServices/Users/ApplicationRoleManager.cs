using Microsoft.AspNet.Identity;
using SOMSightModels;
using SOMSightModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOMSightServices
{
    public class ApplicationRoleManager : RoleManager<Role, Guid>
    {
        public ApplicationRoleManager(IRoleStore<Role, Guid> store) : base(store)
        {
        }
    }
}
