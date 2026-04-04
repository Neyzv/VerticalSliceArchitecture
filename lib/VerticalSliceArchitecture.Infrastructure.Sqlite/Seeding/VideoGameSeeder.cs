using Bogus;
using Microsoft.EntityFrameworkCore;
using VerticalSliceArchitecture.Domain.Enums;
using VerticalSliceArchitecture.Infrastructure.Persistence.Seeding;
using VerticalSliceArchitecture.Infrastructure.Sqlite.Entities;

namespace VerticalSliceArchitecture.Infrastructure.Sqlite.Seeding;

public sealed class VideoGameSeeder
    : ISeeder<SqliteDbContext>
{
    private const byte MinVideoGamesCount = 6;
    private const byte MaxVideoGameCount = 15;
    
    private IEnumerable<VideoGameEntity> GetVideoGames(byte amount)
    {
        var gameGenre = Enum.GetValues<VideoGameGenre>();
        
        var faker = new Faker<VideoGameEntity>()
            .RuleFor(static m => m.Id, faker => faker.Random.Guid())
            .RuleFor(static m => m.Title, faker => faker.Lorem.Sentence(Random.Shared.Next(1, 3)))
            .RuleFor(static m => m.Genre, faker => faker.PickRandom(gameGenre))
            .RuleFor(static m => m.Description, faker => faker.Lorem.Sentence(Random.Shared.Next(4, 15)))
            .RuleFor(static m => m.ReleaseDate, faker => DateOnly.FromDateTime(faker.Date.Past(Random.Shared.Next(0, 40))));
        
        return faker.GenerateLazy(amount);
    }
    
    public bool ShouldBeApplied(SqliteDbContext context) =>
        !context.VideoGames.Any();

    public void Seed(SqliteDbContext context)
    {
        context.VideoGames
            .AddRange(GetVideoGames((byte)Random.Shared.Next(MinVideoGamesCount, MaxVideoGameCount)));
        
        context.SaveChanges();
    }

    public async Task<bool> ShouldBeAppliedAsync(SqliteDbContext context, CancellationToken token) =>
        !await context.VideoGames.AnyAsync(token);

    public async Task SeedAsync(SqliteDbContext context, CancellationToken token)
    {
        await context.VideoGames
            .AddRangeAsync(GetVideoGames((byte)Random.Shared.Next(MinVideoGamesCount, MaxVideoGameCount)), token);
        
        await context.SaveChangesAsync(token);
    }
}