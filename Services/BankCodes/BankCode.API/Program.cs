using BankCode.API;
using BankCode.API.Commons.Extensions;
using BankCode.API.Infrastructures.Persistence;
using BankCode.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services
    .AddInfraService(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    await app.UseMigration();
}

app.UseHttpsRedirection();


app.MapPost("bank", async ([FromBody] BankProfileDto request, BankDbContext context) =>
{
    var bankProfile = BankProfile.NewInstance(request.AccountNumber,
        request.AccountName,
        request.BankSource,
        request.Amount
    );

    context.BankProfiles.Add(bankProfile);
    await context.SaveChangesAsync();
});

app.MapGet("bank", async (BankDbContext context) =>
{
    return await context.BankProfiles.ToListAsync();
});

app.Run();


