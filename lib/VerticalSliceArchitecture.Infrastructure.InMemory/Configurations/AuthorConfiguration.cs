using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VerticalSliceArchitecture.Infrastructure.InMemory.Entities;

namespace VerticalSliceArchitecture.Infrastructure.InMemory.Configurations;

internal sealed class AuthorConfiguration
    : IEntityTypeConfiguration<AuthorEntity>
{
    public void Configure(EntityTypeBuilder<AuthorEntity> builder)
    {
        builder.ToTable("Authors");
        
        builder.HasKey(static x => x.Id);

        builder.Property(static x => x.Name)
            .HasMaxLength(80)
            .IsRequired();
        builder.HasIndex(static x => x.Name)
            .IsUnique(false);
    }
}