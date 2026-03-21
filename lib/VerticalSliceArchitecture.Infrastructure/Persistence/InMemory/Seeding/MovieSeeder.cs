using Bogus;
using Microsoft.EntityFrameworkCore;
using VerticalSliceArchitecture.Infrastructure.Persistence.InMemory.Entities;
using VerticalSliceArchitecture.Infrastructure.Persistence.Seeding;

namespace VerticalSliceArchitecture.Infrastructure.Persistence.InMemory.Seeding;

public sealed class MovieSeeder
    : ISeeder<InMemoryDbContext>
{
    private const byte MinMovieCount = 6;
    private const byte MaxMovieCount = 15;
    
    private IEnumerable<MovieEntity> GetMovies(byte amount)
    {
        var faker = new Faker<MovieEntity>()
            .RuleFor(static m => m.Id, faker => faker.Random.Guid())
            .RuleFor(static m => m.Title, faker => faker.Lorem.Sentence(Random.Shared.Next(1, 3)))
            .RuleFor(static m => m.Description, faker =>  faker.Lorem.Sentence(Random.Shared.Next(4, 15)))
            .RuleFor(static m => m.ReleaseDate, faker => DateOnly.FromDateTime(faker.Date.Past(Random.Shared.Next(0, 40))));
        
        return faker.GenerateLazy(amount);
    }
    
    public bool ShouldBeApplied(InMemoryDbContext context) =>
        !context.Movies.Any();

    public void Seed(InMemoryDbContext context)
    {
        context.Movies
            .AddRange(GetMovies((byte)Random.Shared.Next(MinMovieCount, MaxMovieCount)));
        
        context.SaveChanges();
    }

    public async Task<bool> ShouldBeAppliedAsync(InMemoryDbContext context, CancellationToken token) =>
        !await context.Movies.AnyAsync(token);

    public async Task SeedAsync(InMemoryDbContext context, CancellationToken token)
    {
        await context.Movies
            .AddRangeAsync(GetMovies((byte)Random.Shared.Next(MinMovieCount, MaxMovieCount)), token);
        
        await context.SaveChangesAsync(token);
    }
}