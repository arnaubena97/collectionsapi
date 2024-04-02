using collectionsapi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace collectionsapi.Data
{
    public class CollectionsapiDbContext : DbContext
    {
        public CollectionsapiDbContext(DbContextOptions<CollectionsapiDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<CollectionItem> CollectionItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                        .HasMany(u => u.Collections)
                        .WithOne(c => c.User)
                        .HasForeignKey(c => c.UserId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Collection>()
                        .HasMany(c => c.CollectionItems)
                        .WithOne(ci => ci.Collection)
                        .HasForeignKey(ci => ci.CollectionId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CollectionItem>()
                        .Property(ci => ci.Price)
                        .HasColumnType("decimal(18, 2)");

        }

    }
}


