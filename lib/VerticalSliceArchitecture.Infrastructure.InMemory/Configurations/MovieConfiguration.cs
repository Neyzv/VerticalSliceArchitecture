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
            .IsUnique(false);
        
        builder
            .Property(static m => m.Description)
            .HasMaxLength(500)
            .IsRequired();
        
        builder
            .Property(static m => m.ReleaseDate)
            .HasColumnType("date")
            .IsRequired();
        
        builder.Property(static m => m.AuthorId)
            .IsRequired();
        
        builder.HasOne(static m => m.Author)
            .WithMany()
            .HasForeignKey(static m => m.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}