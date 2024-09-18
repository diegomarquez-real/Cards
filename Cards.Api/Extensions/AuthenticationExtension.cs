using Cards.Api.Services.Identity.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Cards.Api
{
    public static class AuthenticationExtension
    {
        public static void AddAuthicationUsingJWT(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.Events = new JwtBearerEvents()
                {
                    OnTokenValidated = context =>
                    {
                        var claimService = ServiceProvider.Current.GetRequiredService<IUserClaimService>();

                        // Add the access_token as a claim, as we may actually need it.
                        var accessToken = context.SecurityToken as System.IdentityModel.Tokens.Jwt.JwtSecurityToken;
                        if (accessToken != null)
                        {
                            if (context.Principal?.Identity != null && context.Principal.Identity is ClaimsIdentity identity)
                            {
                                identity.AddClaim(claimService.BuildSessionKeyClaim(accessToken.RawData));
                            }       
                        }

                        return Task.CompletedTask;
                    }
                };

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = configuration["JWTSettings:Issuer"],
                    ValidAudience = configuration["JWTSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Token"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                };
            });
        }
    }
}