using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Repo.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext context;
        public UnitOfWork(ApplicationContext _context)
        {
            context = _context;
        }
        public void Commit()
        {
            context.SaveChanges();
            var changedEntriesCopy = context.ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted || e.State == EntityState.Unchanged);

            foreach (var entityEntry in changedEntriesCopy)
            {
                entityEntry.State = EntityState.Detached;
             }
        }
    }
}
