﻿namespace BattleCards.Data
{
    using BattleCards.Models;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Card> Cards { get; set; }

        public DbSet<UserCard> UserCards { get; set; }

        public ApplicationDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(DatabaseConfiguration.ConnectionString, s => s.MigrationsAssembly("BattleCards"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserCard>()
                .HasKey(uc => new { uc.UserId, uc.CardId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
