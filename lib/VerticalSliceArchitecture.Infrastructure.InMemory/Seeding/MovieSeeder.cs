using Bogus;
using Microsoft.EntityFrameworkCore;
using VerticalSliceArchitecture.Infrastructure.InMemory.Entities;
using VerticalSliceArchitecture.Infrastructure.Persistence.Seeding;

namespace VerticalSliceArchitecture.Infrastructure.InMemory.Seeding;

internal sealed class MovieSeeder
    : ISeeder<InMemoryDbContext>
{
    private const byte MinMovieCount = 6;
    private const byte MaxMovieCount = 15;
    
    private static IEnumerable<MovieEntity> GetMovies(byte amount)
    {
        var authorFaker = new Faker<AuthorEntity>()
            .RuleFor(a => a.Id, faker => faker.Random.Guid())
            .RuleFor(a => a.Name,  faker => faker.Lorem.Sentence(Random.Shared.Next(1, 3)));

        var authors = authorFaker.Generate(amount / 3);
        
        var movieFaker = new Faker<MovieEntity>()
            .RuleFor(m => m.Id, faker => faker.Random.Guid())
            .RuleFor(m => m.Title, faker => faker.Lorem.Sentence(Random.Shared.Next(1, 5)))
            .RuleFor(m => m.Description, faker => faker.Lorem.Sentence(Random.Shared.Next(4, 15)))
            .RuleFor(m => m.ReleaseDate, faker => DateOnly.FromDateTime(faker.Date.Past(Random.Shared.Next(0, 40))))
            .RuleFor(m => m.Author, _ => authors.ElementAt(Random.Shared.Next(0, authors.Count)));
        
        return movieFaker.GenerateLazy(amount);
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