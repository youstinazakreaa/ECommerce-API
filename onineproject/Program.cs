
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using onine.core.Entites.Identity;
using onine.core.Repositories;
using onine.Repository;
using onine.Repository.Data;
using onine.Repository.Identity;
using onineproject.Errors;
using onineproject.Extensions;
using onineproject.Helpers;
using onineproject.Middlewares;
using StackExchange.Redis;



namespace onineproject
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args); 

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddDbContext<Oninecontext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddSingleton<IConnectionMultiplexer>(option =>
            {
                var connection = builder.Configuration.GetConnectionString("RedisConnection");
                return ConnectionMultiplexer.Connect(connection);

            });
            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });


            builder.Services.AddApplicationServices();
            builder.Services.AddIdentityservice(builder.Configuration);


            var app = builder.Build();

            var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var DbContext = services.GetRequiredService<Oninecontext>();
                await DbContext.Database.MigrateAsync();
                await OnineContextSeed.SeedAsync(DbContext);


                var IdentityDbcontext = services.GetRequiredService<AppIdentityDbContext>();
                await IdentityDbcontext.Database.MigrateAsync();
                 var userManager = services.GetRequiredService<UserManager<AppUser>>();
                await AppIdentityDbContextSeed.SeedUserAsync(userManager);





            }


            catch (Exception ex) {
                 var logger = loggerFactory.CreateLogger<Program>();

                logger.LogError(ex, "An error occurred while migrating the database.");
            }



            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMiddleware<ExceptionMiddleware>();

                app.UseSwaggerMiddlewares();


            }   
            app.UseStatusCodePagesWithRedirects("/errors/{0}");
            app.UseStaticFiles();

            app.UseHttpsRedirection();
            app.UseAuthentication(); 
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
