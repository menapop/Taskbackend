using System;
using System.Collections.Generic;
using System.Text;
using Data.Entities;
 
using Microsoft.EntityFrameworkCore;

namespace Repo
{
    public static class ModelBuilderExtensions
    {
        // data for first time 
        public static void seed(this ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Role>().HasData(new Role { Id = 1, Name = "Admin",CreationDate=DateTime.Now,IsDeleted=false });

        }
    }
}
