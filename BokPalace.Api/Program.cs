using BokPalace.Api;
using BokPalace.Application;
using BokPalace.Infrastructure;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(ctx.Configuration));

builder.Services
    .AddWebApiServices()
    .AddApplicationServices()
    .AddInfrastructureServices();


var app = builder.Build();

app.UseWebApiServices();
await app.UseInfrastructureServicesAsync();

app.Run();