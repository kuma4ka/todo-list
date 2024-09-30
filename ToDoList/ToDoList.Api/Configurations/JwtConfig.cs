using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ToDoList.Application.Options;

namespace ToDoList.Api.Configurations;

public static class JwtConfig
{
    public static void AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtOptionsSection = configuration.GetSection("Jwt");
        services.Configure<JwtOptions>(jwtOptionsSection);

        var configKey = Environment.GetEnvironmentVariable("JWT_SECRET") ?? jwtOptionsSection["Key"];
        var configIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? jwtOptionsSection["Issuer"];
        var configAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? jwtOptionsSection["Audience"];

        if (string.IsNullOrEmpty(configKey) || string.IsNullOrEmpty(configIssuer) || string.IsNullOrEmpty(configAudience))
        {
            throw new ArgumentException("JWT configuration is missing required values (Key, Issuer, or Audience).");
        }

        if (configKey.Length < 32)
        {
            throw new ArgumentException("JWT signing key must be at least 32 characters long.");
        }

        var key = Encoding.UTF8.GetBytes(configKey);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(jwtOptions =>
        {
            jwtOptions.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = configIssuer,
                ValidAudience = configAudience,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero
            };
        });
    }
}
