using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using TaskManagementSystem.Data.Models;

namespace TaskManagementSystem.Data
{
    public class TMSystemDBContext: IdentityDbContext
    {
     
        public TMSystemDBContext(DbContextOptions<TMSystemDBContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Models.Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Employee>()
                .Property(o => o.Salary)
                .HasColumnType("decimal(18,2)");

            builder
                .Entity<Models.Task>()
                .HasOne(a => a.Assignee)
                .WithMany(t => t.Tasks)
                .HasForeignKey(a => a.AssigneeId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}