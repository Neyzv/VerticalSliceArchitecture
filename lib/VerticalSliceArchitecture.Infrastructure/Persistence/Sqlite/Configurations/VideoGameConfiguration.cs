using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VerticalSliceArchitecture.Domain.Enums;
using VerticalSliceArchitecture.Infrastructure.Persistence.EntityFramework.ValueConverters;
using VerticalSliceArchitecture.Infrastructure.Persistence.Sqlite.Entities;

namespace VerticalSliceArchitecture.Infrastructure.Persistence.Sqlite.Configurations;

public sealed class VideoGameConfiguration
    : IEntityTypeConfiguration<VideoGameEntity>
{
    public void Configure(EntityTypeBuilder<VideoGameEntity> builder)
    {
        builder.ToTable("VideoGame");
        
        builder.HasKey(static v => v.Id);
        
        builder
            .Property(static v => v.Title)
            .HasMaxLength(60)
            .IsRequired();
        builder
            .HasIndex(static v => v.Title)
            .IsUnique();

        builder
            .Property(static v => v.Genre)
            .HasConversion<EnumValueConverter<VideoGameGenre>>()
            .IsRequired();
        builder
            .HasIndex(static v => v.Genre)
            .IsUnique(false);
        
        builder
            .Property(static v => v.Description)
            .HasMaxLength(500)
            .IsRequired();
        
        builder
            .Property(static v => v.ReleaseDate)
            .HasColumnType("date")
            .IsRequired();
    }
}