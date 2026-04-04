using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VerticalSliceArchitecture.Infrastructure.Persistence.Seeding;
using VerticalSliceArchitecture.Infrastructure.Sqlite.Repositories;
using VerticalSliceArchitecture.Infrastructure.Sqlite.Seeding;

namespace VerticalSliceArchitecture.Infrastructure.Sqlite.Extensions;

public static class ServiceCollectionExtensions
{
    extension (IServiceCollection services)
    {
        public IServiceCollection AddSqliteDatabase(Action<IServiceProvider, DbContextOptionsBuilder>? sqliteConfigure = null) =>
            services
                .AddDbContextFactory<SqliteDbContext>((sp, o) => sqliteConfigure?.Invoke(sp, o))
                .AddScoped<DbContext>(sp => sp.GetRequiredService<IDbContextFactory<SqliteDbContext>>().CreateDbContext())
                .AddTransient<ISeeder<SqliteDbContext>, VideoGameSeeder>()
                .AddSingleton<IVideoGameRepository, VideoGameRepository>();
    }
}