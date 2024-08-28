using Memory.Data;
using Microsoft.EntityFrameworkCore;
using Memory.Services.GameService;
using Memory.Services.JWTService;
using Memory.Services.CardService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Memory.Entities;
using Memory.Services.UserService;
using Memory.Hubs;
using Memory.Repositories;

var builder = WebApplication.CreateBuilder(args);

// CORS policy configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactLocalhost",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials(); // Allow credentials such as cookies, headers, etc.
        });
});

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
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
    };
});

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<IJWTService, JWTService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddScoped<ICardReposirory, CardRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// Use CORS policy
app.UseCors("AllowReactLocalhost");

// Redirect HTTP to HTTPS
app.UseHttpsRedirection();

// Serve static files
app.UseStaticFiles();

// JWT token handling via cookie
app.Use(async (context, next) =>
{
    if (context.Request.Cookies.ContainsKey("jwtToken"))
    {
        var token = context.Request.Cookies["jwtToken"];
        // Console.WriteLine($"Token from cookie: {token}"); 
        // var token = context.Request.Cookies["jwtToken"];
        if (!string.IsNullOrEmpty(token))
        {
            context.Request.Headers.Add("Authorization", "Bearer " + token);
        }
    }

    await next.Invoke();
});



// Authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();
app.MapHub<GameHub>("/gamehub");

app.Run();