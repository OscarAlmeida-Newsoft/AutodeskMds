using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Users
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;

    /// <summary>
    /// Class for manage the user account information using and extending the Identity framework
    /// </summary>
    public class User : IdentityUser<Guid, MyLogin, UserRole, MyClaim>
    {

    }


    /// <summary>
    /// Class for manage the user login information using and extending the Identity framework
    /// </summary>
    public class MyLogin : IdentityUserLogin<Guid>
    {
    }


    /// <summary>
    /// Class for manage the user role information using and extending the Identity framework
    /// </summary>
    public class UserRole : IdentityUserRole<Guid>
    {
        /// <summary>
        /// Enum with the role status
        /// </summary>
        public enum EstadoRol
        {
            Activo = 1,
            Inactivo = 2
        }

        /// <summary>
        /// Propertie with the role status
        /// </summary>
        public EstadoRol Estado { get; set; }


        /// <summary>
        /// Class Contructor
        /// </summary>
        public UserRole()
        {

            this.Estado = EstadoRol.Activo;
        }
    }

    /// <summary>
    /// Class for manage the user claims information using and extending the Identity framework
    /// </summary>
    public class MyClaim : IdentityUserClaim<Guid>
    {
    }


    /// <summary>
    /// Class for manage the role information using and extending the Identity framework
    /// </summary>
    public class MyRole : IdentityRole<Guid, UserRole>
    {

    }
}
