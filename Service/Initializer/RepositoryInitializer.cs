using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Repo.Repository.RoleRepository;
using Repo.Repository.UserReposiory;
using Repo.shared;
using Repo.UnitOfWork;

namespace Service.Initializer
{
    public class RepositoryInitializer
    {
        public RepositoryInitializer(IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IRoleRepository, RoleRepository>();

            services.AddTransient<IUserRepository, UserRepository>();

          

            

        }
    }
}
