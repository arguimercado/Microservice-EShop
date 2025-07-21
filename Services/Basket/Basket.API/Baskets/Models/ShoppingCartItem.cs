namespace Basket.API.Baskets.Models;

public class ShoppingCartItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int Quantity { get; set; } = default!;
    public string Color { get; set; } = default!;
    public decimal Price { get; set; } = default!;

    public Guid ProductId { get; set; } = default!;
    public string ProductName { get; set; } = default!;


}
