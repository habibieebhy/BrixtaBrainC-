using BrixtaOS.Domain.Events;
using BrixtaOS.Domain.Organizations;
using BrixtaOS.Domain.Roles;
using BrixtaOS.Domain.Users;
using BrixtaOS.Domain.Workflows;
using Microsoft.EntityFrameworkCore;

namespace BrixtaOS.Infrastructure.Persistence
{
    public class BrixtaDbContext : DbContext
    {
        public DbSet<Organization> Organizations { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<WorkflowDefinition> WorkflowDefinitions { get; set; } = null!;
        public DbSet<WorkflowInstance> WorkflowInstances { get; set; } = null!;
        public DbSet<EventDefinition> EventDefinitions { get; set; } = null!;
        public DbSet<EventInstance> EventInstances { get; set; } = null!;

        public BrixtaDbContext(DbContextOptions<BrixtaDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<WorkflowDefinition>()
                .Ignore(x => x.States);

            modelBuilder.Entity<WorkflowDefinition>()
                .Ignore(x => x.Transitions);
        }
    }
}