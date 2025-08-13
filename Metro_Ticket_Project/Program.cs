using Metro_Ticket_Project.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Metro_Ticket_Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container

            builder.Services.AddControllers();

            // Configure Swagger/OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new() { Title = "Metro Operations Management API", Version = "v1" });
            });

            // Add CORS policy (equivalent to @CrossOrigin in Spring Boot)
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp", policy =>
                {
                    policy.WithOrigins("http://localhost:3000", "https://localhost:3000") // React dev server
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();
                });
            });

            // Add Entity Framework DbContext 
            builder.Services.AddDbContext<MetroDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // TODO: Add your services here when converting them
            // builder.Services.AddScoped<IAuthService, AuthService>();
            // builder.Services.AddScoped<IUserService, UserService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Metro Operations Management API v1"));
            }

            app.UseHttpsRedirection();

            // Enable CORS - must be before authentication and authorization
            app.UseCors("AllowReactApp");

            // TODO: Add authentication and authorization middleware when login implemented
            // app.UseAuthentication();
            // app.UseAuthorization();

            app.MapControllers();

            Console.WriteLine("Metro Operations Management System API is running...");
            app.Run();
        }
    }
}
