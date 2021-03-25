using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BNS.Models;
using System.Linq;

namespace BNS.DataAccess
{
   public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<ProjectType> ProjectType { get; set; }
        public DbSet<Project> Project { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                    .HasMany(x => x.Users)
                    .WithOne(x => x.Supervisor)
                    .HasForeignKey(x => x.SupervisorID).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Project>()
                   .HasMany(x => x.SubProjects)
                   .WithOne(x => x.Parent)
                   .HasForeignKey(x => x.ParentID).OnDelete(DeleteBehavior.Restrict);



            base.OnModelCreating(modelBuilder);
        }
    }
    
}

