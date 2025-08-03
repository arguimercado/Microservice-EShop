using Order.API.Commons.Extensions;
using Order.Infrastructure;
using Order.Application;
using Scalar.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddOrderApplication(builder.Configuration)
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

