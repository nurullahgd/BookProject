using Microsoft.EntityFrameworkCore;
using BookProject.Data.Entities;

namespace BookProject.Data
{
    public class PostgreSqlExampleDbContext : DbContext
    {
        public PostgreSqlExampleDbContext(DbContextOptions<PostgreSqlExampleDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) //VERİTABANI AYAĞA KALKARKEN ÇALIŞAN BÖLÜM
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookProject.Data.PostgreSqlExampleDbContext).Assembly);
            modelBuilder.Entity<Article>()
            .Property(a => a.Id)
            .HasDefaultValueSql("gen_random_uuid()");
            modelBuilder.Entity<User>()
            .Property(a => a.Id)
            .HasDefaultValueSql("gen_random_uuid()");
            modelBuilder.Entity<Account>()
            .Property(a => a.Id)
            .HasDefaultValueSql("gen_random_uuid()");
            modelBuilder.Entity<Order>()
            .Property(a => a.Id)
            .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<Magazine>()
            .Property(a => a.Id)
            .HasDefaultValueSql("gen_random_uuid()");
            modelBuilder.Entity<Account>()
            .Property(a => a.Id)
            .HasDefaultValueSql("gen_random_uuid()");

        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Magazine> Magazines { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Account> Accounts { get; set; }

    }
}