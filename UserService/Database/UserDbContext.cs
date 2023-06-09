﻿using Microsoft.EntityFrameworkCore;
using UserService.Interfaces.Models;

namespace UserService.Database
{
    public class UserDbContext : DbContext
    {
        public UserDbContext() { } // only for tests

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<UserDataModel>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<UserDataModel>()
                .HasOne(d => d.User)
                .WithMany(u => u.Data)
                .HasForeignKey(d => d.UserId);
        }

        public virtual DbSet<UserModel> Users { get; set; }
        public virtual DbSet<UserDataModel> UserData { get; set; }
    }
}
