
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabat.API.Errors;
using Talabat.API.Extensions;
using Talabat.API.MiddleWares;
using Talabat.API.Profiles;
using Talabat.Core.Repositories;
using Talabat.Repository;
using Talabat.Repository.Data;

namespace Talabat.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            #region Configuration Services
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<G01DbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddApplicationService();
            #endregion
            var app = builder.Build();


            using var Scope = app.Services.CreateScope();
            var Service = Scope.ServiceProvider;
            var LoggerFactory = Service.GetRequiredService<ILoggerFactory>();
            try
            {
                var DbContactor = Service.GetRequiredService<G01DbContext>();
                await DbContactor.Database.MigrateAsync();
                await SeedG01DbContext.SeedAsync(DbContactor);
            }
            catch (Exception ex)
            {
                var Logger = LoggerFactory.CreateLogger<Program>();
                Logger.LogError(ex, "An error occured during applying the migration!");
            }

            #region Kestrel Pipelines
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMiddleware<ExceptionMiddleWare>();
                app.AddSwaggerMiddleware();
            }
            app.UseStatusCodePagesWithRedirects("/errors/{0}");
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}
