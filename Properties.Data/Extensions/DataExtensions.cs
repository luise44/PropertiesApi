using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Properties.Services.Configuration.Models;

namespace Properties.Data.Extensions
{
    public static class DataExtensions
    {
        public static IServiceCollection AddContext(this IServiceCollection services, ApiSettings apiSettings)
        {
            return services.AddDbContext<PropertiesDbContext>(opt =>
            {
                opt.UseSqlServer(apiSettings.DatabaseSettings.ConnectionString);
            });
        }
    }
}
