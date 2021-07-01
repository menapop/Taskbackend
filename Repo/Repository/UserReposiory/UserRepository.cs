using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repo.shared;

namespace Repo.Repository.UserReposiory
{
   public class UserRepository : Repository<User> , IUserRepository
    {
        private DbSet<User> UserEntitiy;
        public UserRepository(ApplicationContext context) : base(context)
        {
            UserEntitiy = context.Set<User>();
        }

        public async Task<bool> CheckIfEmailExist(string Email)
        {
            return await UserEntitiy.AnyAsync(u => u.Email.Equals(Email));
        }
    }
}
