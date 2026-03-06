using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VerticalSliceArchitecture.Infrastructure.Persistence.Seeding.Entities;

namespace VerticalSliceArchitecture.Infrastructure.Persistence.Seeding.Configurations;

public sealed class SongConfiguration
    : IEntityTypeConfiguration<SongEntity>
{
    public void Configure(EntityTypeBuilder<SongEntity> builder)
    {
        builder.ToTable("Songs");
        
        builder.HasKey(static s => s.Id);

        builder
            .Property(static s => s.Title)
            .IsRequired();
        builder
            .HasIndex(static s => s.Title)
            .IsUnique();
        
        builder
            .Property(static s => s.Duration)
            .IsRequired();
    }
}