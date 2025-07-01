using Encuba.Product.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Encuba.Product.Infrastructure.EntityConfigurations;

public class UserPublicAccessTokenEfConfig : IEntityTypeConfiguration<UserPublicAccessToken>
{
    public void Configure(EntityTypeBuilder<UserPublicAccessToken> builder)
    {
        builder.ToTable(nameof(UserPublicAccessToken));
        builder.HasKey(t => new { t.UserId, t.PublicAccessTokenId });

    }
}