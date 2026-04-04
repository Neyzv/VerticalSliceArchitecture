using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VerticalSliceArchitecture.Infrastructure.InMemory.Entities;

namespace VerticalSliceArchitecture.Infrastructure.InMemory.Configurations;

public sealed class MovieConfiguration
    : IEntityTypeConfiguration<MovieEntity>
{
    public void Configure(EntityTypeBuilder<MovieEntity> builder)
    {
        builder.ToTable("Movies");
        
        builder.HasKey(static m => m.Id);
        
        builder
            .Property(static m => m.Title)
            .HasMaxLength(60)
            .IsRequired();
        builder
            .HasIndex(static m => m.Title)
            .IsUnique();
        
        builder
            .Property(static m => m.Description)
            .HasMaxLength(500)
            .IsRequired();
        
        builder
            .Property(static m => m.ReleaseDate)
            .HasColumnType("date")
            .IsRequired();
    }
}