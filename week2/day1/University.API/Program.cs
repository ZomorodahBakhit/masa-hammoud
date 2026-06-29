using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using University.API.Modules;
using University.Data.Context;
using AutoWrapper;
using University.API.Filters;
using Serilog;

Log.Logger = new LoggerConfiguration ()
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

builder.Services.AddControllers (options =>
{
    options.Filters.Add<ApiExceptionFilter> ();
});

builder.Services.AddEndpointsApiExplorer ();
builder.Services.AddSwaggerGen ();

var app = builder.Build ();

app.UseApiResponseAndExceptionWrapper ();

if ( app.Environment.IsDevelopment () )
    {
    app.UseSwagger ();
    app.UseSwaggerUI ();
    }

app.UseHttpsRedirection ();
app.MapControllers ();

app.Run ();