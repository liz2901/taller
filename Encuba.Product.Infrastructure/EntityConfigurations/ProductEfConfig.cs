using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Encuba.Product.Infrastructure.EntityConfigurations;

public class ProductEfConfig : IEntityTypeConfiguration<Domain.Entities.Product>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Product> builder)
    {
        builder.ToTable(nameof(Domain.Entities.Product));

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(t => t.Description)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(t => t.Price)
            .IsRequired();

        builder.Metadata.FindNavigation(nameof(Domain.Entities.Product.ShoppingCarts))
            ?.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}