using BuildingBlocks.Commons.Exceptions;
using Carter;
using Order.API.Commons.Extensions;
using Order.Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddOrderInfrastructure(builder.Configuration)
    .AddApiService();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    await app.InitializeDbAsync();
}

app.UseApiService();

app.Run();

