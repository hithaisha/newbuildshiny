using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MORR.Domain.Entities;

namespace MORR.Infrastructure.Data.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(t => t.TotalPrice)
                   .HasPrecision(18, 5);

            builder.HasOne<User>(o => o.CreatedByUser)
                   .WithMany(o => o.CreatedOrder)
                   .HasForeignKey(o => o.CreatedByUserId)
                   .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne<User>(o => o.UpdatedByUser)
                   .WithMany(o => o.UpdatedOrder)
                   .HasForeignKey(o => o.UpdatedByUserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}