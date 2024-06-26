using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Supernova.Persistence.Context;

namespace Supernova.Persistence
{
    public static class ServiceCollectionExtension
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            string connString = configuration.GetConnectionString("DefaultConnection") ?? "";

            serviceCollection.AddDbContext<MainDbContext>(options =>
            {
                options.ConfigureDatabase(connString);
            });
        }

        public static void ConfigureDatabase(this DbContextOptionsBuilder builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }
    }
}
