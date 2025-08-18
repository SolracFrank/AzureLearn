using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class CategoryConfig : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(p => p.Id).HasName("pK_Category");
        
        builder.Property(e => e.Name).HasMaxLength(50).IsRequired();
        builder.Property(e => e.Active).IsRequired();
    }
}