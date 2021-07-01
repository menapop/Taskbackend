using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Helpers.Auth
{
   public  interface IAuthService
    {
        Guid UserId { get; set; }
        string Email { get; set; }
        string UserName { get; set; }
        string [] Roles { get; set; }

    }
}
