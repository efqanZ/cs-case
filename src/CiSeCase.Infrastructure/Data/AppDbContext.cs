using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CiSeCase.Core.Models;
using CiSeCase.Core.Models.Abstract;
using CiSeCase.Infrastructure.Data.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CiSeCase.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Basket> Baskets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new BasketConfiguration());

            base.OnModelCreating(modelBuilder);

        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            CommitMethod();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            CommitMethod();
            return base.SaveChanges();
        }

        private void CommitMethod()
        {
            var modifiedEntries = ChangeTracker.Entries()
                           .Where(x => x.Entity is IAuditEntity
                               && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entry in modifiedEntries)
            {
                IAuditEntity entity = entry.Entity as IAuditEntity;
                if (entity != null)
                {
                    DateTime now = DateTime.Now;

                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedAt = now;
                    }
                    else
                    {
                        base.Entry(entity).Property(x => x.CreatedAt).IsModified = false;
                    }

                    entity.UpdatedAt = now;
                }
            }
        }
    }
}