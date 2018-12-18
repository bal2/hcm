using System;
using hcm.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace hcm.Database
{
    public class HcmContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<CardAccess> CardAccesses { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupMembership> GroupMemberships { get; set; }
        public DbSet<Message> Messages { get; set; }

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

            modelBuilder.Entity<CardAccess>()
                .HasKey(c => c.AccessId);
            modelBuilder.Entity<CardAccess>()
                .Property<DateTime>(c => c.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();

            //Group
            modelBuilder.Entity<Group>()
            .HasKey(g => g.GroupId);
            modelBuilder.Entity<Group>()
                .Property(g => g.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Group>()
                .Property(g => g.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAddOrUpdate();

            //GroupMembership
            modelBuilder.Entity<GroupMembership>()
                .HasKey(m => new { m.UserId, m.GroupId });
            modelBuilder.Entity<GroupMembership>()
                .HasOne(m => m.User)
                .WithMany(u => u.GroupMemberships)
                .HasForeignKey(m => m.UserId);
            modelBuilder.Entity<GroupMembership>()
                .HasOne(m => m.Group)
                .WithMany(g => g.Members)
                .HasForeignKey(m => m.GroupId);

            //Message
            modelBuilder.Entity<Message>()
                .HasKey(m => m.MessageId);
            modelBuilder.Entity<Message>()
                .Property(m => m.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();
        }
    }
}