namespace Basket.API.Baskets.Models
{
    public class ShoppingCart
    {
        public static ShoppingCart Create(string username,List<ShoppingCartItem> items = null)
        {
            return new ShoppingCart(username)
            {
                Items = items ?? new List<ShoppingCartItem>()
            };
        }
        public string UserName { get; set; }
        public List<ShoppingCartItem> Items { get; set; } = new();

        public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);

        public ShoppingCart(string username)
        {
            UserName = username;
        }

        public ShoppingCart()
        {
            
        }
    }
}
