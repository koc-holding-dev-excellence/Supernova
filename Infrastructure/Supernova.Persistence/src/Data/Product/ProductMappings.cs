using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Supernova.Persistence.Data;

public class ProductMappings
{
    public static void OnModelCreating(EntityTypeBuilder<Domain.Product> builder)
    {
        builder.Property(c => c.Id).HasColumnName("SQ_APPROVAL_MTV").HasMaxLength(16).IsRequired();
        builder.Property(c => c.Name).HasColumnName("CD_MODEL").HasMaxLength(5);
        builder.HasKey(c => c.Id);
    }
}
