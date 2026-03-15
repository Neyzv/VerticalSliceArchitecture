using System.Runtime.InteropServices.ComTypes;
using DorApiExplorer.Extensions;
using Microsoft.EntityFrameworkCore;
using VerticalSliceArchitecture.Infrastructure.Extensions;
using VerticalSliceArchitecture.Infrastructure.Persistence.InMemory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services
    .AddOpenApi()
    .AddInMemoryDatabase(static (_, o) => o.UseInMemoryDatabase("movies"))
    .AddSqliteDatabase(static (_, o) => o.UseSqlite("Data Source=app.db"))
    .AddMediaThor()
    .AddDorApiExplorer()
    .AddInMemoryDatabase();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbFactory = scope.ServiceProvider
        .GetRequiredService<IDbContextFactory<InMemoryDbContext>>();

    await using (var db = await dbFactory.CreateDbContextAsync())
    {
        var inMemoryMigrationTask = db.Database.IsRelational() ? db.Database.MigrateAsync() : db.Database.EnsureCreatedAsync();

        app.MapEndpoints();

        await inMemoryMigrationTask;
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();