using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PheasantBench.Domain.Models;

namespace PheasantBench.Infrastructure
{
    public sealed class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<ForumMessage> ForumMessages { get; set; }
        public DbSet<ForumThread> ForumThreads { get; set; }
        public DbSet<Benchmark> Benchmarks { get; set; }
        public DbSet<UserUpvotes> UsersUpvotes { get; set; }
        public DbSet<User> Users { get; set; }
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
