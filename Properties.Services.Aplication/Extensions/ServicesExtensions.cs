using Microsoft.Extensions.DependencyInjection;
using Properties.Data.Repositories.Interfaces;
using Properties.Data.Repositories.Repositories;
using Properties.Services.Application.Interfaces;
using Properties.Services.Application.Services;

namespace Properties.Services.Application.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddServicesAndDependencies(this IServiceCollection services)
        { 
            return services.AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IOwnerRepository,OwnerRepository>()
                .AddScoped<IPropertyRepository, PropertyRepository>()
                .AddScoped<IPropertyTraceRepository, PropertyTraceRepository>()
                .AddScoped<IPropertyImageRepository, PropertyImageRepository>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IOwnerService, OwnerService>()
                .AddScoped<IPropertyService, PropertyService>();
        }
    }
}
