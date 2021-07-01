using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Helpers.Auth
{
    public class AuthService : IAuthService
    {
        public Guid UserId { get ; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string[] Roles { get; set;}



    }
}
