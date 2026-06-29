using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using University.API.Modules;
using University.Data.Context;
using University.Data.Entities;
using AutoWrapper;
using University.API.Filters;
using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using University.API.Configurations;
using University.API.Helpers;
using University.Core.Services;


Log.Logger = new LoggerConfiguration ()
    .MinimumLevel.Information ()
    .WriteTo.Console ()
    .WriteTo.File ("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger ();

var builder = WebApplication.CreateBuilder (args);

builder.Host.UseSerilog ();

builder.Host.UseServiceProviderFactory (
    new AutofacServiceProviderFactory ());

builder.Host.ConfigureContainer<ContainerBuilder> (container =>
{
    container.RegisterModule (new RepositoryModule ());
    container.RegisterModule (new ServiceModule ());
});

builder.Services.AddDbContext<UniversityDbContext> (options =>
{
    options.UseSqlServer (
        builder.Configuration.GetConnectionString ("DefaultConnection"));
});

builder.Services.AddIdentity<AppUser, AppRole> ()
    .AddEntityFrameworkStores<UniversityDbContext> ()
    .AddDefaultTokenProviders ();

var jwtSettings = builder.Configuration.GetSection ("JwtSettings");
builder.Services.Configure<JwtSettings> (jwtSettings);

builder.Services.AddAuthentication (options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer (options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
        {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings ["Issuer"],        
        ValidAudience = jwtSettings ["Audience"],    
        IssuerSigningKey = new SymmetricSecurityKey (
            Encoding.UTF8.GetBytes (jwtSettings ["SecretKey"]!))  
        };
});

builder.Services.AddAuthorization ();

builder.Services.AddScoped<IJwtTokenHelper, JwtTokenHelper> ();
builder.Services.AddScoped<IAuthService, University.API.Services.AuthService> ();
builder.Services.AddControllers (options =>
{
    options.Filters.Add<ApiExceptionFilter> ();
});

builder.Services.AddEndpointsApiExplorer ();
builder.Services.AddSwaggerGen (c =>
{
    c.AddSecurityDefinition ("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
        });
    c.AddSecurityRequirement (new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

var app = builder.Build ();

app.UseApiResponseAndExceptionWrapper ();

if ( app.Environment.IsDevelopment () )
    {
    app.UseSwagger ();
    app.UseSwaggerUI ();
    }

app.UseHttpsRedirection ();

app.UseAuthentication ();
app.UseAuthorization ();

app.MapControllers ();

app.Run ();