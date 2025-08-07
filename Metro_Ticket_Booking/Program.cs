//using Metro_Ticket_Booking.Models;
//using Metro_Ticket_Booking.Services;
//using Microsoft.EntityFrameworkCore;
//using System.Configuration;

//namespace Metro_Ticket_Booking
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            var builder = WebApplication.CreateBuilder(args);

//            // 🔧 Add DbContext
//            var connectionString = builder.Configuration.GetConnectionString("MetroTicketConnection");
//            builder.Services.AddDbContext<MetroTicketContext>(options =>
//                options.UseSqlServer(connectionString));



//            // Add services to the container.
//            builder.Services.AddScoped<IAuthService, AuthService>();

//            // **Add CORS policy**
//            builder.Services.AddCors(options =>
//            {
//                options.AddPolicy("AllowReactApp",
//                    policy =>
//                    {
//                        policy.WithOrigins("http://localhost:3000", "http://localhost:5174/") // React app origin
//                              .AllowAnyHeader()
//                              .AllowAnyMethod();
//                    });
//            });

//            builder.Services.AddControllers();
//            builder.Services.AddEndpointsApiExplorer();
//            builder.Services.AddSwaggerGen();

//            var app = builder.Build();

//            // Configure the HTTP request pipeline.
//            if (app.Environment.IsDevelopment())
//            {
//                app.UseSwagger();
//                app.UseSwaggerUI();
//            }

//            app.UseHttpsRedirection();

//            // **Use CORS before authorization and routing**
//            app.UseCors("AllowReactApp");

//            app.UseAuthorization();

//            app.MapControllers();

//            app.Run();
//        }
//    }
//}



// Updated Program.cs - Replace your existing Program.cs
using Microsoft.EntityFrameworkCore;
using Metro_Ticket_Booking.Models;
using Metro_Ticket_Booking.Services;


namespace Metro_Ticket_Booking
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddControllers();

            // Add Entity Framework
            // In Program.cs (for .NET 6+ like .NET 8)
            builder.Services.AddDbContext<MetroTicketContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MetroTicketConnection")));

            //builder.Services.AddDbContext<MetroTicketContext>(options =>
            //    options.UseSqlServer("Data Source=Sankeerth\\SQLEXPRESS;Initial Catalog=Metro_Ticket;Integrated Security=True;Encrypt=False"));

            // Register services
            builder.Services.AddScoped<IAuthService, AuthService>();

            // Add CORS - CRITICAL for frontend communication
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp", policy =>
                {
                    policy.WithOrigins("http://localhost:3000", "https://localhost:3000", "http://localhost:5173", "https://localhost:5173")
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();
                });
            });

            // Add Swagger for API testing
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // CRITICAL: Use CORS before other middleware
            app.UseCors("AllowReactApp");

            app.UseAuthorization();

            app.MapControllers();

            // Test endpoint
            app.MapGet("/api/test", () => new
            {
                message = "Metro API is working!",
                timestamp = DateTime.UtcNow,
                database = "Connected to Metro_Ticket database"
            });

            // Test database connection endpoint
            app.MapGet("/api/test-db", async (MetroTicketContext context) =>
            {
                try
                {
                    var userCount = await context.Users.CountAsync();
                    var adminCount = await context.Admins.CountAsync();
                    var complaintCount = await context.Complaints.CountAsync();

                    return Results.Ok(new
                    {
                        message = "Database connection successful",
                        counts = new
                        {
                            users = userCount,
                            admins = adminCount,
                            complaints = complaintCount
                        },
                        connectionString = context.Database.GetConnectionString()
                    });
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new
                    {
                        message = "Database connection failed",
                        error = ex.Message
                    });
                }
            });



            app.Run();

        }
    }
}