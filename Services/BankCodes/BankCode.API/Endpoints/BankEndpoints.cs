using BankCode.API.Infrastructures.Persistence;
using BankCode.API.Models;
using Carter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankCode.API.Endpoints;

public class BankEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {

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
    }
}
