using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Properties.Services.Authentication.Interfaces;
using Properties.Services.Configuration.Models;
using System.Text;

namespace Properties.Services.Authentication.Extensions
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, ApiSettings apiSettings)
        {
            services.AddSingleton<IAuthenticationService, AuthenticationService>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = apiSettings.JwtSettings.Issuer,
                            ValidAudience = apiSettings.JwtSettings.Issuer,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(apiSettings.JwtSettings.Key))
                        };
                    }
                );

            return services;
        }
    }
}
