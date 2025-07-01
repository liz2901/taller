using Encuba.Product.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Encuba.Product.Infrastructure.EntityConfigurations;

public class PublicAccessTokenEfConfig : IEntityTypeConfiguration<PublicAccessToken>
{
    public void Configure(EntityTypeBuilder<PublicAccessToken> builder)
    {
        builder.ToTable(nameof(PublicAccessToken));

        builder.HasKey(t => t.Id);

        builder.Property(t => t.AccessToken)
            .HasMaxLength(255)
            .IsRequired()
            .IsUnicode(false);

        builder.Property(t => t.RefreshToken)
            .HasMaxLength(255)
            .IsRequired()
            .IsUnicode(false);

        builder.Property(t => t.Scope)
            .HasMaxLength(20)
            .IsRequired()
            .IsUnicode(false);

        builder.Property(t => t.ExpiresIn)
            .IsRequired();

        builder.Property(t => t.CreatedAt)
            .IsRequired();

        builder.HasIndex(t => t.AccessToken)
            .IsUnique();

        builder.HasIndex(t => t.RefreshToken)
            .IsUnique();
    }
}