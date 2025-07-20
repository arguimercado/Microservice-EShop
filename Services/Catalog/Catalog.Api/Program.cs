using BuildingBlocks.Commons.Exceptions;
using Catalog.Api.Commons.Data;
using Catalog.Api.Commons.Extensions;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
//
//
builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddCarter();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var connectionString = builder.Configuration.GetConnectionString("CatalogConnection");
builder.Services.AddHealthChecks()
    .AddNpgSql(connectionString!);

builder.Services.AddOpenApi();
if(builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CatalogInitialData>();
    
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
}
app.MapOpenApi();
app.MapScalarApiReference();
app.UseExceptionHandler();
app.UseHttpsRedirection();
app.MapCarter();

app.UseHealthChecks("/api/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});


app.Run();