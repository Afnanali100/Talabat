using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Interfaces;
using Talabat.Repository.Identity;
using Talabat.Services;

namespace Talabat.Extensions
{
    public static class UserAppExtentions
    {
        public static IServiceCollection UserAppExtension(this IServiceCollection Services,IConfiguration configuration) {

            Services.AddScoped<ITokenService,TokenService>();

            Services.AddIdentity<UserApp, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
               

            }).AddEntityFrameworkStores<UsersDbContext>();


            Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
               
            }).AddJwtBearer(options =>
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = configuration["Jwt:ValidIssuer"],
                ValidateAudience = true,
                ValidAudience=configuration["Jwt:ValidAudience"],
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
            }) ;


            return Services;
        }


    }
}
