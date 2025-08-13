using System.Text.Json.Serialization;
using Metro_Ticket_Booking.Models;
using Metro_Ticket_Booking.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Name for CORS policy
var corsPolicyName = "AllowFrontend";

// Add services to the container.

// Configure DbContext with connection string from appsettings.json
builder.Services.AddDbContext<MetroTicketContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("MetroTicketDb"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MetroTicketDb"))
    ));

// Configure Authentication Service
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IUserService, UserService>();

//payment service 
builder.Services.AddScoped<PaymentService>();


// Add Controllers support with JSON options to handle circular references
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        // or use ReferenceHandler.Preserve if you want to support reference metadata
        // options.JsonSerializerOptions.MaxDepth = 64; // optional increase max depth
    });

// Configure CORS to allow frontend origin
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicyName,
        policy =>
        {
            policy
                .WithOrigins(
                                "https://689a83dbceafad905e6e7790--metro-one.netlify.app",   // Production URL
                                "http://localhost:5173",                                     // Vite dev server
                                "http://localhost:3000"                                      // Alternative dev server
                            )
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();

        });
});

// Configure JWT authentication
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),

        ClockSkew = TimeSpan.Zero
    };
});

// Add Authorization
builder.Services.AddAuthorization();

// Enable Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware pipeline configuration
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable CORS with the named policy
app.UseCors(corsPolicyName);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Database seeding
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MetroTicketContext>();
    
    // Ensure database is created
    context.Database.EnsureCreated();
    
    // Seed stations if they don't exist
    if (!context.Stations.Any())
    {
        var stations = new List<Station>
        {
            new Station { Name = "PCMC", Address = "Pimpri Chinchwad Municipal Corporation" },
            new Station { Name = "Sant Tukaram Nagar", Address = "Sant Tukaram Nagar, Pune" },
            new Station { Name = "Bhosari (Nashik Phata)", Address = "Bhosari (Nashik Phata), Pune" },
            new Station { Name = "Kasarwadi", Address = "Kasarwadi, Pune" },
            new Station { Name = "Phugewadi", Address = "Phugewadi, Pune" },
            new Station { Name = "Dapodi", Address = "Dapodi, Pune" },
            new Station { Name = "Bopodi", Address = "Bopodi, Pune" },
            new Station { Name = "Khadki", Address = "Khadki, Pune" },
            new Station { Name = "Range Hills", Address = "Range Hills, Pune" },
            new Station { Name = "Shivajinagar", Address = "Shivajinagar, Pune" },
            new Station { Name = "Civil Court", Address = "Civil Court, Pune" },
            new Station { Name = "Budhwar Peth", Address = "Budhwar Peth, Pune" },
            new Station { Name = "Mandai", Address = "Mandai, Pune" },
            new Station { Name = "Swargate", Address = "Swargate, Pune" },
            new Station { Name = "Deccan Gymkhana", Address = "Deccan Gymkhana, Pune" },
            new Station { Name = "Garware College", Address = "Garware College, Pune" },
            new Station { Name = "Vanaz", Address = "Vanaz, Pune" },
            new Station { Name = "Anand Nagar", Address = "Anand Nagar, Pune" },
            new Station { Name = "Ideal Colony", Address = "Ideal Colony, Pune" },
            new Station { Name = "Nal Stop", Address = "Nal Stop, Pune" },
            new Station { Name = "Pune Railway Station", Address = "Pune Railway Station, Pune" }
        };
        
        context.Stations.AddRange(stations);
        await context.SaveChangesAsync();
        Console.WriteLine("✅ Stations seeded successfully");
    }
    
    // Seed metros if they don't exist
    if (!context.Metros.Any())
    {
        var metros = new List<Metro>
        {
            new Metro { Name = "Pune Metro Line 1", Capacity = 200 },
            new Metro { Name = "Pune Metro Line 2", Capacity = 200 }
        };
        
        context.Metros.AddRange(metros);
        await context.SaveChangesAsync();
        Console.WriteLine("✅ Metros seeded successfully");
    }
    
    // Seed routes if they don't exist
    if (!context.Routes.Any())
    {
        var stations = context.Stations.ToList();
        var metros = context.Metros.ToList();
        
        var routes = new List<Metro_Ticket_Booking.Models.Route>();
        
        // Create routes between consecutive stations
        for (int i = 0; i < stations.Count - 1; i++)
        {
            routes.Add(new Metro_Ticket_Booking.Models.Route
            {
                StartStationId = stations[i].StationId,
                EndStationId = stations[i + 1].StationId,
                Name = $"{stations[i].Name} to {stations[i + 1].Name}"
            });
        }
        
        context.Routes.AddRange(routes);
        await context.SaveChangesAsync();
        Console.WriteLine("✅ Routes seeded successfully");
    }
    
    // Seed admin if doesn't exist
    if (!context.Admins.Any())
    {
        var admin = new Admin
        {
            Name = "Admin",
            Email = "admin@metro.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123")
        };
        
        context.Admins.Add(admin);
        await context.SaveChangesAsync();
        Console.WriteLine("✅ Admin seeded successfully");
    }
}

app.Run();
