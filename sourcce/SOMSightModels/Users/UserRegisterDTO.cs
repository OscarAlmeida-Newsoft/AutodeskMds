using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOMSightModels.Users
{
    public class UserRegisterDTO
    {
        public IdentityResult RegisterResult { get; set; }
        public Guid UserId { get; set; }
    }
}
