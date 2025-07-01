using Encuba.Product.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Encuba.Product.Infrastructure.EntityConfigurations;

public class UserEfConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User));

        builder.HasKey(t => t.Id);

        builder.Property(t => t.UserName)
            .HasMaxLength(64)
            .IsRequired()
            .IsUnicode(false);
        
        
        builder.Property(t => t.UserType)
            .HasMaxLength(15)
            .IsRequired()
            .IsUnicode(false);

        
        builder.Property(t => t.FirstName)
            .HasMaxLength(100)
            .IsUnicode(false);

        builder.Property(t => t.SecondName)
            .HasMaxLength(100)
            .IsUnicode(false);

        builder.Property(t => t.FirstLastName)
            .HasMaxLength(100)
            .IsUnicode(false);

        builder.Property(t => t.SecondLastName)
            .HasMaxLength(100)
            .IsUnicode(false);
        
        builder.Property(t => t.Password)
            .HasMaxLength(255)
            .IsUnicode(false);
        
        builder.Property(t => t.Email)
            .HasMaxLength(255)
            .IsRequired()
            .IsUnicode(false);

        builder.Property(t => t.Status)
            .IsRequired();
        
        builder.Property(t => t.CreatedAt)
            .IsRequired();

        builder.Property(t => t.UpdatedAt)
            .IsRequired();
        
        builder.Property(t => t.AcceptedTermsAndCondition)
            .IsRequired();
        
        builder.Property(t => t.ResetPasswordToken);
        
        builder.Property(t => t.ResetPasswordTokenExpiresIn);
        
        builder.HasOne<PublicAccessToken>()
            .WithMany()
            .HasForeignKey(t => t.PublicAccessTokenId);
        
        builder.HasIndex(t => t.Email)
            .IsUnique();
        
        builder.Metadata.FindNavigation(nameof(User.ShoppingCarts))
            ?.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}