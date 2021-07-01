using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repo.shared;

namespace Repo.Repository.RoleRepository
{
   public  class RoleRepository : Repository<Role> , IRoleRepository
    {
        private DbSet<Role> role;
        public RoleRepository(ApplicationContext context) : base(context)
        {
            role = context.Set<Role>();
        }
      
    }
}
