using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MORR.Domain.Entities;

namespace MORR.Infrastructure.Data.Configuration
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItem");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(t => t.TotalPrice)
                   .HasPrecision(18, 5);

            builder.HasOne<Order>(o => o.Order)
                   .WithMany(o => o.OrderItems)
                   .HasForeignKey(o => o.OrderId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Product>(o => o.Product)
                   .WithMany(o => o.OrderItems)
                   .HasForeignKey(o => o.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}