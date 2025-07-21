using Basket.API.Commons.Extensions;
using BuildingBlocks.Commons.Exceptions;
using Catalog.Api.Commons.Extensions;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddCarter();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddOpenApi();

var connectionNpSqlString = builder.Configuration.GetConnectionString("BasketConnection");
var connectionRedisString = builder.Configuration.GetConnectionString("RedisConnection");
builder.Services.AddHealthChecks()
    .AddNpgSql(connectionNpSqlString!)
    .AddRedis(connectionRedisString!);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.UseExceptionHandler();
app.UseHttpsRedirection();
app.MapCarter();

app.UseHealthChecks("/api/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});


app.Run();
