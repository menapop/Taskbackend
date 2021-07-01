using System;
using System.Collections.Generic;
using System.Text;
using FoodSys.Service.Helper.Password;
using Microsoft.Extensions.DependencyInjection;
using Service.Helpers.Auth;
using Service.Helpers.Email;
using Service.Helpers.Token;
using Service.RolesService;

using Service.UserService;

namespace Service.Initializer
{
    public class ServiceInitializer
    {
        public ServiceInitializer(IServiceCollection services)
        {
            services.AddTransient<ITokenHandler, TokenHandler>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddTransient<IEmailServie, EmailService>();
            services.AddTransient<IUserService, UserServicec>();
            
            services.AddTransient<IPasswordHandler, PasswordHandler>();
            services.AddTransient<IRoleService, RoleService>();
            
        }
    }
}
