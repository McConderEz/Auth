using Accounts.Domain;
using Core.DTOs.ValueObjects;
using Core.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.Shared.ValueObjects;

namespace Accounts.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.Property(u => u.SocialNetworks)
            .ValueObjectJsonConverter(
                s => new SocialNetworkDto { Url = s.Url, Title = s.Title },
                dto => SocialNetwork.Create(dto.Title, dto.Url).Value)
            .HasColumnName("social_networks");

        builder.Property(u => u.IsBanned)
            .HasColumnName("is_banned")
            .HasDefaultValue(false);

        builder.HasMany(u => u.Roles)
            .WithMany(r => r.Users);
    }
}