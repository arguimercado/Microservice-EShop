namespace Basket.API.Baskets.Models
{
    public class ShoppingCart
    {

        public static ShoppingCart Create(string username)
        {
            return new ShoppingCart(username);
        }

        public static ShoppingCart Create(string username,List<ShoppingCartItem>? items = null)
        {
            return new ShoppingCart(username)
            {
                Items = items ?? new List<ShoppingCartItem>()
            };
        }
        public string Id { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public List<ShoppingCartItem> Items { get; set; } = new();

        public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);

        public void UpdateItems(IEnumerable<ShoppingCartItem> items)
        {
            Items.Clear();
            Items.AddRange(items);
        }

        public ShoppingCart(string username)
        {
            UserName = username;
            Id = Guid.NewGuid().ToString();
        }

        public ShoppingCart() {
            
        }
    }
}
