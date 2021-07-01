using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using Repo.shared;

namespace Repo.Repository.UserReposiory
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> CheckIfEmailExist(string Email);
    }
}
