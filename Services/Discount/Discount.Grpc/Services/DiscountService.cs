using Discount.Grpc.Data.Persistence;
using Discount.Grpc.Models;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services;

public class DiscountService
    (DiscountContext dbContext,ILogger<DiscountService> logger) 
    : DiscountProtoService.DiscountProtoServiceBase
{

    public override async Task GetAllDiscounts(GetAllDiscountsRequest request, IServerStreamWriter<CouponModel> responseStream, ServerCallContext context)
    {

        logger.LogInformation("GetAllDiscounts called");
        var coupons = await dbContext.Coupons.AsNoTracking().ToListAsync();

        var couponModels = coupons.Select(c => new CouponModel
        {
            Id = c.Id,
            ProductId = c.ProductId,
            ProductName = c.ProductName,
            Description = c.Description,
            Amount = c.Amount
        });

        foreach (var couponModel in couponModels)
        {
            await responseStream.WriteAsync(couponModel);
        }


    }

    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        logger.LogInformation("GetDiscount called with Id: {ProductName}", request.Id);
        var coupon = await dbContext.Coupons.FirstOrDefaultAsync(c => c.ProductId == request.Id);

        if (coupon == null)
            coupon = new Coupon(Guid.NewGuid().ToString(), "No Product Discount", "No Product Discount", 0f);

        var couponModel = new CouponModel
        {
            Id = coupon.Id,
            ProductId = coupon.ProductId,
            ProductName = coupon.ProductName,
            Description = coupon.Description,
            Amount = coupon.Amount
        };

        return couponModel;
    }
    
    public override async Task<RequestDiscountResponse> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = dbContext.Coupons.AsTracking().FirstOrDefault(c => c.Id == request.Id);
        if (coupon == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Discount with Id {request.Id} not found"));
        }

        coupon.Update(request.Amount);
        await dbContext.SaveChangesAsync();

        return new RequestDiscountResponse { IsSuccess = true };
    }



    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = new Coupon(request.ProductId, request.ProductName, request.Description, request.Amount);
        dbContext.Coupons.Add(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount coupon is successfully created");

        return new CouponModel
        {
            Id = coupon.Id,
            ProductId = coupon.ProductId,
            ProductName = coupon.ProductName,
            Description = coupon.Description,
            Amount = coupon.Amount
        };
    }



    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupons.FirstOrDefaultAsync(c => c.Id == request.Id);
        if (coupon == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Discount with Id {request.Id} not found"));
        }

        dbContext.Coupons.Remove(coupon);
        await dbContext.SaveChangesAsync();

        return new DeleteDiscountResponse { IsSuccess = true };
    }
}
