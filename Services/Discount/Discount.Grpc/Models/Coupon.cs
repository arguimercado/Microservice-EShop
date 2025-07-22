namespace Discount.Grpc.Models;

public class Coupon
{

    public Coupon()
    {
        
    }

    public Coupon(string productId, string productName, string description, float amount)
    {
        Id = Guid.NewGuid().ToString();
        ProductId = productId;
        ProductName = productName;
        Description = description;
        Amount = amount;
    }

    public string Id { get; private set; } = default!;
    public string ProductId { get; private set; } = default;
    public string ProductName { get; private set; } = default!;

    public string Description { get; private set; } = default!;

    public float Amount { get; set; }

    public void Update(float amount)
    {
        Amount = amount;
    }
}
