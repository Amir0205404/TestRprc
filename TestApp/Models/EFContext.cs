using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestApp.Models.Entities;


namespace TestApp.Models
{
    public class EFContext : IdentityDbContext<Employee, IdentityRole, string, IdentityUserClaim<string>, EmployeeRoles, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public DbSet<Position> positions { get; set; }
        
        public DbSet<EmployeeRoles> AspNetUserRoles { get; set; }
        public DbSet<EmployeePositions> employee_positions { get; set; }
        
        public DbSet<address_locality> address_locality { get; set; }
        
        public DbSet<apartment> apartments { get; set; }
        
        public DbSet<personal_account> personal_accounts { get; set; }
        
        public DbSet<resident> residents { get; set; }

        public EFContext(DbContextOptions<EFContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("DbConnectionString", EnvironmentVariableTarget.User));
        }
    }
}