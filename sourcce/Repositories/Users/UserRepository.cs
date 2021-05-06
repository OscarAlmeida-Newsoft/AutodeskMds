using Entities.Users;
using IRepositories.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Users
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AffidavitContext context) : base(context)
        {

        }


        /// <summary>
        /// Add an user
        /// </summary>
        public void Add(Guid pId, string pUserName)
        {
            Context.Set<User>().Add(new User { Id = pId, UserName = pUserName });
        }
    }
}
