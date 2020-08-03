using CiSeCase.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CiSeCase.Infrastructure.Data.Config
{
    public class BasketConfiguration : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(p => p.CreatedAt).IsRequired();
            builder.Property(p => p.UpdatedAt).IsRequired();


            builder.HasOne<User>(e => e.User)
                                .WithMany(e => e.BasketItems)
                                .HasForeignKey(e => e.UserId);

            builder.HasOne<Product>(e => e.Product)
                                .WithMany(e => e.BasketItems)
                                .HasForeignKey(e => e.ProductId);
        }
    }
}