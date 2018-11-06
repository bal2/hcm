using System;
using hcm.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace hcm.Database
{
    public class HcmContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public HcmContext(DbContextOptions<HcmContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);
            modelBuilder.Entity<User>()
                .Property<DateTime>(u => u.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<User>()
                .Property<DateTime>(u => u.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAddOrUpdate();
        }
    }
}