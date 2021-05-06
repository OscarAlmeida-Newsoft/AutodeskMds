using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SOMSightModels.Users;
using System;
using System.Threading.Tasks;

namespace ISOMSightServices.Users
{
    public interface IUserService
    {
        /// <summary>
        /// Add new user, information comes in pModel and password in pPass
        /// </summary>
        /// <param name="pModel"></param>
        /// <param name="pPass"></param>
        /// <returns></returns>
        Task<UserRegisterDTO> Register(UserDTO pModel, string pPass);


        /// <summary>
        /// Log in to the application
        /// </summary>
        /// <param name="pUserName"></param>
        /// <param name="pPassword"></param>
        /// <param name="pIsPersitent"></param>
        /// <returns></returns>
        Task<SignInStatus> SingIn(string pUserName, string pPassword, bool pIsPersitent);


        /// <summary>
        /// It's a closed session
        /// </summary>
        /// <param name="PTypes"></param>
        void SignOut(params string[] PTypes);

        Task<string> GenerateEmailConfirmationTokenAsync(Guid pUserId);

        Task<string> GeneratePasswordResetTokenAsync(Guid pUserId);

        Task<bool> IsEmailConfirmedAsync(Guid pUserId);

        Task<IdentityResult> ConfirmEmailAsync(Guid pUserId, string pCode);
        //Task<void> SendEmailAsync();

        UserDTO GetbyUserName(string pUserName);

        UserDTO GetbyUserId(Guid pUserId);

        Task SendEmailAsync(Guid pUserId, string pSubject, string pBody);
        Task<IdentityResult> ResetPasswordAsync(Guid pUserId, string pToken, string pNewPassword);
    }
}
