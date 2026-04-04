using System.Runtime.InteropServices.ComTypes;
using DorApiExplorer.Extensions;
using Microsoft.EntityFrameworkCore;
using VerticalSliceArchitecture.Infrastructure.InMemory.Extensions;
using VerticalSliceArchitecture.Infrastructure.Sqlite.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services
    .AddOpenApi()
    .AddInMemoryDatabase(static (_, o) => o.UseInMemoryDatabase("movies"))
    .AddSqliteDatabase(static (_, o) => o.UseSqlite("Data Source=app.db"))
    .AddMediaThor()
    .AddDorApiExplorer()
    .AddSwaggerGen()
    .AddEndpointsApiExplorer();

var app = builder.Build();

var migrationTasks = new List<Task>();

using (var scope = app.Services.CreateScope())
{
    migrationTasks.AddRange(
        scope.ServiceProvider
            .GetServices<DbContext>()
            .Select(db => db.Database.IsRelational() ? db.Database.MigrateAsync() : db.Database.EnsureCreatedAsync())
    );
    
    app.MapEndpoints();

    if (migrationTasks.Count > 0)
        await Task.WhenAll(migrationTasks);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();