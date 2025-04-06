
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.OpenApi.Models;
using CpApi.DataBaseContext;
using CpApi.Service;
using CpApi.Interfaces;
using CpApi.Controllers;
using CpApi.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Регистрация IHttpContextAccessor
builder.Services.AddHttpContextAccessor();
builder.Services.AddSignalR();

// Настройка JWT
var jwtSettings = builder.Configuration.GetSection("Jwt");
var secretKey = jwtSettings["SecretKey"] ?? throw new InvalidOperationException("JWT secret key is not configured.");

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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});

// Настройка CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});



// Регистрация контроллеров и Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Movie Ayaz API", Version = "v1" });

    // Настройка авторизации для Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddDbContext<ContextDb>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TestDbString")), ServiceLifetime.Scoped);

builder.Services.AddScoped<IUsersLoginsService, UserLoginService>();
builder.Services.AddScoped<IFilmsGenresService, FilmGenreService>();
builder.Services.AddSingleton<JwtService>();

var app = builder.Build();

app.UseCors("AllowAllOrigins");

app.UseCors(builder =>
    builder.WithOrigins("http://localhost:5005")
           .AllowAnyMethod()
           .AllowAnyHeader()
           .AllowCredentials()
           .WithExposedHeaders("Content-Disposition"));

// Настройка Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MovieAPI");
    });
}

app.UseSwagger();   // Должно быть до UseRouting()
app.UseSwaggerUI(); // Должно быть после UseSwagger()
app.UseRouting();
// Аутентификация и авторизация
app.UseAuthentication();
app.UseAuthorization();

// Маппинг контроллеров и SignalR
app.MapControllers();
app.MapHub<MessagesHub>("/chatHub");

app.Run();