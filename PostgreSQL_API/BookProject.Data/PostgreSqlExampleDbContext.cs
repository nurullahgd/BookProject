using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.Extensions.Configuration;
using BookProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

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
            .HasOne(a => a.Magazine)
            .WithMany(m => m.Articles)
            .HasForeignKey(a => a.MagazineId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Article>()
                .HasOne(a => a.Author)
                .WithMany(u => u.Articles)
                .HasForeignKey(a => a.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Magazine> Magazines { get; set; }
    }
}