using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories.Users
{
    public interface IUserRepository
    {
        /// <summary>
        /// Add an user
        /// </summary>
        void Add(Guid pId, string pUserName);
    }
}
