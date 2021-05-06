namespace SOMSightModels.Users
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;

    public class User :IdentityUser<Guid, MyLogin, UserRole, MyClaim>
    {
        public bool ChangePassword { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public DateTime? CreatedOn { get; set; }
    }


    public class MyLogin : IdentityUserLogin<Guid>
    {
    }

    public class UserRole : IdentityUserRole<Guid>
    {
        public enum EstadoRol
        {
            Activo = 1,
            Inactivo = 2
        }

        public EstadoRol Estado { get; set; }

        public UserRole()
        {

            this.Estado = EstadoRol.Activo;
        }
    }

    public class MyClaim : IdentityUserClaim<Guid>
    {
    }

    public class Role : IdentityRole<Guid, UserRole>
    {

    }

    /// <summary>
    /// Class for manage the role information using and extending the Identity framework
    /// </summary>
    //public class MyRole : IdentityRole<Guid, UserRole>
    //{

    //}
}
