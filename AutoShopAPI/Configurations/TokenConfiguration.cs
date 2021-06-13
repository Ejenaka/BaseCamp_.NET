using AutoShop.Core.Interfaces.Configurations;
using Microsoft.Extensions.Configuration;

namespace AutoShop.API.Configurations
{
    public class TokenConfiguration : ITokenConfiguration
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int TokenExpires { get; set; }

        public TokenConfiguration(IConfiguration configuration)
        {
            IConfiguration section = configuration?.GetSection("Token");

            Secret = section.GetValue<string>(nameof(Secret));
            Issuer = section.GetValue<string>(nameof(Issuer));
            Audience = section.GetValue<string>(nameof(Audience));
            TokenExpires = section.GetValue<int>(nameof(TokenExpires));
        }
    }
}