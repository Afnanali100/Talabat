using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Interfaces;
using Talabat.Errors;
using Talabat.Extensions;
using Talabat.Helpers;
using Talabat.Middlewares;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;

namespace Talabat
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
      
            //------------Contection To DataBase----------------------
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddSingleton<IConnectionMultiplexer>(options =>
            {
                var connection = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);

            });

            builder.Services.AddDbContext<UsersDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });
            
            
            ///----------------Extiensions------------------------
            builder.Services.AddApplicationService();
            builder.Services.UserAppExtension(builder.Configuration);
            builder.Services.SwaggerServices();
          







            var app = builder.Build();
            #region Update Database
            var scoope = app.Services.CreateScope();
            var services = scoope.ServiceProvider;
            var LogerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var dbcontext = services.GetRequiredService<StoreContext>();
                await dbcontext.Database.MigrateAsync();
                await StoreContextDataSeeding.AddSeed(dbcontext);

                var IdentityContext=services.GetRequiredService<UsersDbContext>();
                await IdentityContext.Database.MigrateAsync();

                var users = services.GetRequiredService<UserManager<UserApp>>();
                await UserAppDataSeeding.AddUserSeedingAsync(users);    
            }
            catch (Exception ex)
            {
                var log = LogerFactory.CreateLogger<Program>();
                log.LogError(ex, "Error Occured During Update Database");
            }

            #endregion


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddlware();
            }
            app.UseMiddleware<ExceptionMiddleWare>();
            app.UseStatusCodePagesWithRedirects("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseAuthentication();
            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}