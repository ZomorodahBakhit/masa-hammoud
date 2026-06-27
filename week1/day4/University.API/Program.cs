using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using University.API.Modules;
using University.Data.Context;
using AutoWrapper;

var builder = WebApplication.CreateBuilder (args);

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

builder.Services.AddControllers ();

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