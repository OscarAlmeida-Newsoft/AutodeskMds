using Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices.Users
{
    public interface IUserService
    {
        void Create(Guid pId, string pUserName);


        /// <summary>
        ///  Get an User by ID
        /// </summary>
        /// <param name="pId">User ID to get</param>
        /// <returns>User Object with the data</returns>
        User GetById(Guid pId);

        User GetByUserName(string pUserName);
    }
}
