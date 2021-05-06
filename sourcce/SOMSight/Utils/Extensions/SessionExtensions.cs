using SOMSight.Models.Account;
using System.Web;

namespace SOMSight.Utils.Extensions
{
    public static class SessionExtensions
    {
        private const string USUARIO_KEY = "USUARIO_KEY";




        public static void SetCurrentUser(this HttpSessionStateBase session, UserSession pUser)
        {
            session[USUARIO_KEY] = pUser;
        }

        public static UserSession GetCurrentUser(this HttpSessionStateBase session)
        {

            var data = session[USUARIO_KEY];
            if (data == null)
            {
                return null;
            }
            return (UserSession)data;
        }

        public static string GetCurrentRol(this HttpSessionStateBase session)
        {
            var _user = (UserSession)session[USUARIO_KEY];

            if (_user == null)
            {
                return string.Empty;
            }

            return _user.UserRole;

        }
    }
}