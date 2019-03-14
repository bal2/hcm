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
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<RoleUser> RoleUsers { get; set; }

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

            //Permissions
            modelBuilder.Entity<Permission>()
                .HasKey(p => p.PermissionId);
            modelBuilder.Entity<Permission>()
                .HasIndex(p => p.Name)
                .IsUnique();
            modelBuilder.Entity<Role>()
                .HasKey(r => r.RoleId);
            modelBuilder.Entity<Role>()
                .HasIndex(r => r.Name)
                .IsUnique();
            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new { rp.RoleId, rp.PermissionId });
            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.Permissions)
                .HasForeignKey(rp => rp.RoleId);
            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany(p => p.Roles)
                .HasForeignKey(rp => rp.PermissionId);
            modelBuilder.Entity<RoleUser>()
               .HasKey(ru => new { ru.RoleId, ru.UserId });
            modelBuilder.Entity<RoleUser>()
                .HasOne(ru => ru.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(ru => ru.RoleId);
            modelBuilder.Entity<RoleUser>()
                .HasOne(ru => ru.User)
                .WithMany(u => u.Roles)
                .HasForeignKey(ru => ru.UserId);

            modelBuilder.Entity<Permission>()
            .HasData(
                new { PermissionId = 1L, Name = "ViewUsers", Description = "View users and user details" },
                new { PermissionId = 2L, Name = "CreateUser", Description = "Create new users" },
                new { PermissionId = 3L, Name = "UpdateUser", Description = "Update user details" },
                new { PermissionId = 4L, Name = "DeleteUser", Description = "Delete users" },
                new { PermissionId = 5L, Name = "ViewGroups", Description = "View groups and group details" },
                new { PermissionId = 6L, Name = "CreateGroup", Description = "Create new groups" },
                new { PermissionId = 7L, Name = "UpdateGroup", Description = "Update group details" },
                new { PermissionId = 8L, Name = "DeleteGroup", Description = "Delete groups" }
            );
        }
    }
}