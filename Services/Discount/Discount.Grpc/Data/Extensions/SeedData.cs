using Discount.Grpc.Data.Persistence;
using Discount.Grpc.Models;

namespace Discount.Grpc.Data.Extensions
{
    public static class SeedData
    {
        public static async Task Seed(DiscountContext context)
        {
            if(!context.Coupons.Any())
            {
                context.Coupons.Add(new Coupon(Guid.NewGuid().ToString(), "IPhone X", "IPhone Discount", 150));
                context.Coupons.Add(new Coupon(Guid.NewGuid().ToString(), "Samsung S10", "Samsung Discount", 100));

                await context.SaveChangesAsync();
                
            }
        }
    }
}
