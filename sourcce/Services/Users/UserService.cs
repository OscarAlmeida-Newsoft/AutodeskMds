using Entities.Users;
using IRepositories.Users;
using IServices.Users;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Users
{
    public class UserService : IUserService
    {
        protected IUnitOfWork.IUnitOfWork unitOfWork;
        readonly MyApplicationUserManager userManager;
        readonly ApplicationSignInManager signInManager;
        readonly IUserRepository userRepository;

        public UserService(IUnitOfWork.IUnitOfWork pUnitOfWork, MyApplicationUserManager pUserManager, ApplicationSignInManager pSignInManager, IUserRepository pUserRespository)
        {
            unitOfWork = pUnitOfWork;
            userManager = pUserManager;
            signInManager = pSignInManager;
            userRepository = pUserRespository;
        }

        public void Create(Guid pId, string pUserName)
        {
            userRepository.Add(pId, pUserName);
            unitOfWork.Complete();
        }

        /// <summary>
        ///  Get an User by ID
        /// </summary>
        /// <param name="pId">User ID to get</param>
        /// <returns>User Object with the data</returns>
        public User GetById(Guid pId)
        {
            return userManager.FindById(pId);
        }

        public User GetByUserName(string pUserName)
        {
            return userManager.FindByName(pUserName);
        }
    }
}
