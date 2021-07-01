
using Data.Entities;
using Data.EntitiesConfigurtion;


using Microsoft.EntityFrameworkCore;

namespace Repo
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.ApplyConfigurationsFromAssembly(typeof(RoleConfiguration).Assembly);
            builder.seed();  // data in tables first time 
        }
        
        
    }


}
