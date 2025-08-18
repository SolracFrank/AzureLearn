using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;


public class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK_Product");
        builder.Property(e => e.Name).HasMaxLength(50).IsRequired();
        builder.Property(e => e.Description).HasMaxLength(500);
        builder.Property(e => e.Active).IsRequired();

   builder.HasOne(p => p.Category)        
       .WithMany(c => c.Products)       
       .HasForeignKey(p => p.CategoryId) 
       .OnDelete(DeleteBehavior.Restrict);
    }
}