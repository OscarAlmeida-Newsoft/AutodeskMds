using System;

namespace SOMSight.Models.Account
{
    public class UserSession
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserRole { get; set; }
        public string MyProperty { get; set; }
    }
}