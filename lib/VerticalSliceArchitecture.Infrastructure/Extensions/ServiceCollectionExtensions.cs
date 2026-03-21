using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VerticalSliceArchitecture.Infrastructure.Persistence.InMemory;
using VerticalSliceArchitecture.Infrastructure.Persistence.InMemory.Seeding;
using VerticalSliceArchitecture.Infrastructure.Persistence.Seeding;
using VerticalSliceArchitecture.Infrastructure.Persistence.Sqlite;
using VerticalSliceArchitecture.Infrastructure.Persistence.Sqlite.Seeding;

namespace VerticalSliceArchitecture.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    extension (IServiceCollection services)
    {
        public IServiceCollection AddInMemoryDatabase(Action<IServiceProvider, DbContextOptionsBuilder>? inMemoryConfigure = null) =>
            services
                .AddDbContextFactory<InMemoryDbContext>((sp, o) => inMemoryConfigure?.Invoke(sp, o))
                .AddScoped(sp => sp.GetRequiredService<IDbContextFactory<InMemoryDbContext>>().CreateDbContext())
                .AddTransient<ISeeder<InMemoryDbContext>, MovieSeeder>();
        
        public IServiceCollection AddSqliteDatabase(Action<IServiceProvider, DbContextOptionsBuilder>? sqliteConfigure = null) =>
            services
                .AddDbContextFactory<SqliteDbContext>((sp, o) => sqliteConfigure?.Invoke(sp, o))
                .AddScoped(sp => sp.GetRequiredService<IDbContextFactory<SqliteDbContext>>().CreateDbContext())
                .AddTransient<ISeeder<SqliteDbContext>, VideoGameSeeder>();
    }
}