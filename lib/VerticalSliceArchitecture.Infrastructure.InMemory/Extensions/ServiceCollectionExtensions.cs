using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VerticalSliceArchitecture.Infrastructure.InMemory.Repositories;
using VerticalSliceArchitecture.Infrastructure.InMemory.Seeding;
using VerticalSliceArchitecture.Infrastructure.Persistence.Seeding;

namespace VerticalSliceArchitecture.Infrastructure.InMemory.Extensions;

public static class ServiceCollectionExtensions
{
    extension (IServiceCollection services)
    {
        /// <summary>
        /// Register services to use <see cref="InMemoryDbContext"/>.
        /// </summary>
        /// <param name="inMemoryConfigure">The configuration lambda.</param>
        /// <returns>The app service collection.</returns>
        public IServiceCollection AddInMemoryDatabase(Action<IServiceProvider, DbContextOptionsBuilder>? inMemoryConfigure = null) =>
            services
                .AddDbContextFactory<InMemoryDbContext>((sp, o) => inMemoryConfigure?.Invoke(sp, o))
                .AddScoped<DbContext>(sp => sp.GetRequiredService<IDbContextFactory<InMemoryDbContext>>().CreateDbContext())
                .AddTransient<ISeeder<InMemoryDbContext>, MovieSeeder>()
                .AddSingleton<IMovieRepository, MovieRepository>();
    }
}