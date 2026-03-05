using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VerticalSliceArchitecture.Infrastructure.Persistence.InMemory;
using VerticalSliceArchitecture.Infrastructure.Persistence.InMemory.Seeding;
using VerticalSliceArchitecture.Infrastructure.Persistence.Seeding;

namespace VerticalSliceArchitecture.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    extension (IServiceCollection services)
    {
        public IServiceCollection AddInfrastructure(Action<IServiceProvider, DbContextOptionsBuilder>? inMemoryConfigure = null) =>
            services
                .AddDbContextFactory<InMemoryDbContext>((sp, o) => inMemoryConfigure?.Invoke(sp, o))
                .AddTransient<ISeeder, MovieSeeder>();
    }
}