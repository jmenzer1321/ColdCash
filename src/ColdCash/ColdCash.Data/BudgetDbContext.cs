using Microsoft.EntityFrameworkCore;
using Models = ColdCash.Core.Models;   // alias avoids every ambiguity

namespace ColdCash.Data
{
    public class BudgetDbContext : DbContext
    {
        public DbSet<Models.User> Users => Set<Models.User>();
        public DbSet<Models.Account> Accounts => Set<Models.Account>();
        public DbSet<Models.Transaction> Transactions => Set<Models.Transaction>();
        public DbSet<Models.Envelope> Envelopes => Set<Models.Envelope>();
        public DbSet<Models.Category> Categories => Set<Models.Category>();

        public BudgetDbContext(DbContextOptions<BudgetDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder b)
        {
            b.Entity<Models.Transaction>()
              .HasOne(t => t.Account)
              .WithMany(a => a.Transactions)
              .OnDelete(DeleteBehavior.Restrict);

            b.Entity<Models.Envelope>()
              .HasOne(e => e.Category)
              .WithOne()
              .HasForeignKey<Models.Envelope>(e => e.CategoryId);

            b.Entity<Models.Category>()
              .HasIndex(c => c.Name)
              .IsUnique();
        }
    }
}