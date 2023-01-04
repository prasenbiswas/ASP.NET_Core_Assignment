using CoreAssignmentForRollBased.Configuration;
using CoreAssignmentForRollBased.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CoreAssignmentForRollBased.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace CoreAssignmentForRollBased.Data
{
    public class EmployeeDbContext : IdentityDbContext<ApplicationUser>
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.Entity<ApplicationUser>()
                .HasOne<Profile>(s => s.Profile)
                .WithOne(p => p.ApplicationUser)
                .HasForeignKey<Profile>(x => x.ApplicatioUserId);
        }
        public DbSet<Profile> Profiles { get; set; }
    }
}
