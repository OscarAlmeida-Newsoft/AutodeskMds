using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SOMSightModels.Users;
using ISOMSightServices.Users;
using ISOMSightRepositories.Common;
using SOMSightModels;
using ISOMSightServices;
using AutoMapper;

namespace SOMSightServices.Users
{
    public class UserService : IUserService
    {
        
        readonly ApplicationUserManager userManager;
        readonly ApplicationSignInManager signInManager;
        readonly ApplicationRoleManager roleManager;
        //readonly IUserRepository userRepository;


        public UserService(ApplicationUserManager pUserManager
            , ApplicationSignInManager pSignInManager, ApplicationRoleManager pRoleManager)
        {
            userManager = pUserManager;
            signInManager = pSignInManager;
            roleManager = pRoleManager;

        }

        public async Task<UserRegisterDTO> Register(UserDTO pModel, string pPass)
        {
            var Id = Guid.NewGuid();


            User NewUser = new User();
            NewUser.Email = pModel.Email;
            NewUser.UserName = pModel.UserName;
            NewUser.Id = Id;
            NewUser.PhoneNumber = "000-0000";
            NewUser.ChangePassword = false;
            NewUser.CompanyName = pModel.CompanyName;
            NewUser.ContactName = pModel.ContactName;

            UserRegisterDTO _result = new UserRegisterDTO();
            _result.RegisterResult = await userManager.CreateAsync(NewUser, pPass);
            _result.UserId = NewUser.Id;

            return _result;
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(Guid pUserId)
        {
            var _code = await userManager.GenerateEmailConfirmationTokenAsync(pUserId);

            return _code;
        }

        public async Task<string> GeneratePasswordResetTokenAsync(Guid pUserId)
        {
            var _code = await userManager.GeneratePasswordResetTokenAsync(pUserId);

            return _code;
        }

        public async Task<bool> IsEmailConfirmedAsync(Guid pUserId)
        {
            var _isConfirmed = await userManager.IsEmailConfirmedAsync(pUserId);

            return _isConfirmed;
        }

        public async Task<IdentityResult> ResetPasswordAsync(Guid pUserId, string pToken, string pNewPassword)
        {
            var aux = await userManager.ResetPasswordAsync(pUserId,pToken,pNewPassword);

            return aux;
        }
        


        public async Task<IdentityResult> ConfirmEmailAsync(Guid pUserId, string pCode)
        {
            var _result = await userManager.ConfirmEmailAsync(pUserId, pCode);
            return _result;
        }

        public async Task SendEmailAsync(Guid pUserId, string pSubject,string pBody)
        {
            await userManager.SendEmailAsync(pUserId, pSubject,pBody);
        }
        

        public void SignOut(params string[] PTypes)
        {
            signInManager.AuthenticationManager.SignOut();
        }

        public async Task<SignInStatus> SingIn(string pUserName, string pPassword, bool pIsPersitent)
        {

            var _user = await userManager.FindByNameAsync(pUserName);
            if (_user == null)
            {
                return SignInStatus.Failure;
            }

            if (! await userManager.IsEmailConfirmedAsync(_user.Id))
            {
                return SignInStatus.RequiresVerification;
            }

            var _result = await signInManager.PasswordSignInAsync(pUserName, pPassword, pIsPersitent, false);

            return _result;
        }

        public UserDTO GetbyUserName(string pUserName)
        {
            UserDTO _userDTO = Mapper.Map<User, UserDTO>(userManager.FindByName(pUserName));
            return _userDTO;
        }

        public UserDTO GetbyUserId(Guid pUserId)
        {
            UserDTO _userDTO = Mapper.Map<User, UserDTO>(userManager.FindById(pUserId));
            return _userDTO;
        }
    }
}
