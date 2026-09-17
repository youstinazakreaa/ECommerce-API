using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using onine.core.Entites.Identity;
using onine.Repository.Identity;
using onine.services;
using System.Text;

namespace onineproject.Extensions
{
    public static class IdentityServiceExtension
    {
      public static IServiceCollection AddIdentityservice (this IServiceCollection Services ,  IConfiguration configuration)
        {
            Services.AddScoped<ITokenService, TokenService>();

            Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>();
            Services.AddApplicationServices();
            
            
            
                Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = configuration["JWT:ValidIssuer"],

            ValidateAudience = true,
            ValidAudience = configuration["JWT:ValidAudience"],

            ValidateLifetime = true,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["JWT:Key"])
            )
        };
    });

            return Services;
        }

    }
}
