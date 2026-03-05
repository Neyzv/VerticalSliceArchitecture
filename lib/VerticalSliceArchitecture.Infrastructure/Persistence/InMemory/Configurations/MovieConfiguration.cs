using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VerticalSliceArchitecture.Infrastructure.Persistence.InMemory.Entities;

namespace VerticalSliceArchitecture.Infrastructure.Persistence.InMemory.Configurations;

public sealed class MovieConfiguration
    : IEntityTypeConfiguration<MovieEntity>
{
    public void Configure(EntityTypeBuilder<MovieEntity> builder)
    {
        builder.ToTable("Movies");
        
        builder.HasKey(m => m.Id);
        
        builder
            .Property(static m => m.Title)
            .HasMaxLength(60)
            .IsRequired();
        builder.HasIndex(static m => m.Title);
        
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