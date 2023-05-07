using LeadSoft.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace LeadSoft.Data.Context
{
    public class LeadSoftDbContext : DbContext
    {
        public LeadSoftDbContext(DbContextOptions<LeadSoftDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                         .SelectMany(e => e.GetProperties()
                             .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("VARCHAR(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LeadSoftDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
