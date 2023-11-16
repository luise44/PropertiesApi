using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Properties.Services.Authentication.Interfaces;
using Properties.Services.Configuration.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ApiSettings _settings;

        public AuthenticationService(IOptions<ApiSettings> settings)
        {
            _settings = settings.Value;
        }

        public string GetJwtToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.JwtSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var Sectoken = new JwtSecurityToken(_settings.JwtSettings.Issuer,
              _settings.JwtSettings.Issuer,
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(Sectoken);
        }
    }
}
