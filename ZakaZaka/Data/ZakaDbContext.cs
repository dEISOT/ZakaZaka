using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZakaZaka.Models;

namespace ZakaZaka.Data
{
    public class ZakaDbContext : DbContext
    {
        public ZakaDbContext(DbContextOptions<ZakaDbContext> options) : base(options)
        {   
       
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().Property(u => u.Price).HasColumnType("money");

            modelBuilder.Entity<Assortment>()
                .HasKey(a => new { a.ShopId, a.GameId });

            modelBuilder.Entity<Assortment>()
                .HasOne(a => a.Shop)
                .WithMany(s => s.Assortments)
                .HasForeignKey(a => a.ShopId);

            modelBuilder.Entity<Assortment>()
                .HasOne(a => a.Game)
                .WithMany(g => g.Assortments)
                .HasForeignKey(a => a.GameId);

            modelBuilder.Entity<Shop>().HasData(
                new Shop[]
                {
                    new Shop{Id = 1, Name = "a"},
                    new Shop{Id = 6, Name = "a"},
                    new Shop{Id = 2, Name = "b"},
                    new Shop{Id = 3, Name = "c"},
                    new Shop{Id = 4, Name = "d"},
                    new Shop{Id = 5, Name = "e"}
                });

            modelBuilder
                .Entity<User>()
                .HasOne(u => u.Credential)
                .WithOne(c => c.User)
                .HasForeignKey<UserCredential>(c => c.UserId);

        }

        public DbSet<Game> Games { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Assortment> Assortments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TokenModel> TokenModels { get; set; }
        public DbSet<UserCredential> UserCredentials { get; set; }
    }
}
