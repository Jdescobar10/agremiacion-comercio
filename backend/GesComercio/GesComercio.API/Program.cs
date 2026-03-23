using GesComercio.Application.Interfaces;
using GesComercio.Application.Validators;
using GesComercio.Domain.Interfaces;
using GesComercio.Infrastructure.Data;
using GesComercio.Infrastructure.Repositories;
using GesComercio.Infrastructure.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// -------------------------------------------------------
// 1. DbContext
// -------------------------------------------------------
builder.Services.AddDbContext<GesComercioDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// -------------------------------------------------------
// 2. Repositories
// -------------------------------------------------------
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IComercianteRepository, ComercianteRepository>();
builder.Services.AddScoped<IMunicipioRepository, MunicipioRepository>();

// -------------------------------------------------------
// 3. Services
// -------------------------------------------------------
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();

// -------------------------------------------------------
// 4. Validators
// -------------------------------------------------------
builder.Services.AddValidatorsFromAssemblyContaining<LoginValidator>();

// -------------------------------------------------------
// 5. JWT Authentication
// -------------------------------------------------------
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"]!;

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
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(secretKey)),
        ClockSkew = TimeSpan.Zero  // Sin margen extra — expira exactamente en 1h
    };
});

builder.Services.AddAuthorization();

// -------------------------------------------------------
// 6. CORS
// -------------------------------------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins(
                builder.Configuration
                    .GetSection("Cors:AllowedOrigins")
                    .Get<string[]>()!)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// -------------------------------------------------------
// 7. Controllers
// -------------------------------------------------------
builder.Services.AddControllers();

// -------------------------------------------------------
// 8. Swagger con soporte JWT
// -------------------------------------------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "GesComercio API",
        Version = "v1",
        Description = "API para gestión de comerciantes y establecimientos"
    });

    // Configurar JWT en Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingresa el token JWT: Bearer {token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });
});

// -------------------------------------------------------
// 9. Build
// -------------------------------------------------------
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAngular");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();