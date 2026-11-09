using GameLibraryAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection.Emit;

namespace GameLibraryAPI.Data
{
    public class GLContext : IdentityDbContext<Gebruiker>
    {
        public GLContext(DbContextOptions<GLContext> options) : base(options) { }

        public DbSet<Game> Games { get; set; }

        public DbSet<GamePlatform> GamePlatforms { get; set; }

        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Naamgevingen
            modelBuilder.Entity<Game>().ToTable("Game");
            modelBuilder.Entity<Platform>().ToTable("Platform");
            modelBuilder.Entity<GamePlatform>().ToTable("GamePlatform");
            modelBuilder.Entity<Review>().ToTable("Review");

            modelBuilder.Entity<Game>().Property(g => g.Price).HasColumnType("decimal(7,2)");

            // To do: Relaties....






        }

    }
}
