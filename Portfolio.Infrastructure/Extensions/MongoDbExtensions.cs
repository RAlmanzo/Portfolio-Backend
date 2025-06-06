using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Core.Interfaces.Repositories;
using Portfolio.Infrastructure.Data;
using Portfolio.Infrastructure.Repositories;
using Portfolio.Infrastructure.Settings;

namespace Portfolio.Infrastructure.Extensions
{
    public static class MongoDbExtensions
    {
        public static IServiceCollection RegisterMongoDb(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<MongoDbSettings>(config.GetSection("MongoDb"));
            services.AddSingleton<MongoDbContext>();

            return services;
        }
    }
}
