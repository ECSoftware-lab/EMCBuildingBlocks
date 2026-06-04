using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EMC.BuildingBlocks.DependencyInjection
{
    public static class JWTAuthenticationScheme
    {
        public static IServiceCollection AddJWTAuthenticationScheme(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var key = Encoding.UTF8.GetBytes(config["Authentication:jwtKey"]!);
                string issuer = config["Authentication:Issuer"]!;

                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.FromMinutes(5),
                    RequireExpirationTime = true,

                    AudienceValidator = (audiences, token, parameters) =>
                    {
                        return audiences.Any(aud =>
                            aud.EndsWith("turneroweb.ar", StringComparison.OrdinalIgnoreCase) ||
                            aud.EndsWith("empresa1.com", StringComparison.OrdinalIgnoreCase) ||
                            aud.EndsWith("empresa2.com.ar", StringComparison.OrdinalIgnoreCase) ||
                            aud.EndsWith("ukyokonails.com.ar", StringComparison.OrdinalIgnoreCase) ||
                            aud.Contains("localhost", StringComparison.OrdinalIgnoreCase)
                        );
                    }
                };
            });

            return services;
        }
    }


    /*  public static class JWTAuthenticationScheme
      {
          public static IServiceCollection AddJWTAuthenticationScheme(this IServiceCollection services, IConfiguration config)
          {
              services.AddAuthentication(options =>
              {
                  options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
              })
      .AddJwtBearer(options =>
      {
          var key = Encoding.UTF8.GetBytes(config.GetSection("Authentication:jwtKey").Value!);
          string issuer = config.GetSection("Authentication:Issuer").Value!;
          string audience = config.GetSection("Authentication:Audience").Value!;

          options.RequireHttpsMetadata = false;
          options.SaveToken = true;
          options.TokenValidationParameters = new TokenValidationParameters
          {
              ValidateIssuer = true,
              ValidateAudience = true,
              ValidateLifetime = true,
              ValidateIssuerSigningKey = true,
              IssuerSigningKey = new SymmetricSecurityKey(key),
              ClockSkew = TimeSpan.FromMinutes(5),
              ValidIssuer = issuer,
              ValidAudience = audience,
              RequireExpirationTime = true,
              AudienceValidator = (audiences, token, parameters) =>
              {
                  return audiences.Any(aud =>
                      aud.EndsWith("turneroweb.ar", StringComparison.OrdinalIgnoreCase) ||
                      aud.Equals(" ", StringComparison.OrdinalIgnoreCase) ||
                      aud.Equals(" ", StringComparison.OrdinalIgnoreCase)
                  );
              }
          };
      });

              return services;
          }
      }*/
}

