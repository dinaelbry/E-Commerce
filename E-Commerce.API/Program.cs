using Microsoft.EntityFrameworkCore;
using E_Commerce.Infrastructure.Data;
using E_Commerce.Infrastructure;
using E_Commerce.Extentions;
using E_Commerce.Application;
using Microsoft.Extensions.FileProviders;
using E_Commerce.Application.Profiles;




namespace E_Commerce
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddApplicationServices();  

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.Configure<UrlSettings>(builder.Configuration.GetSection("UrlSettings"));


            var app = builder.Build();

            await app.MigrationAndSeedAsync();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Files")),
                RequestPath = "/Files"
            });


            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
