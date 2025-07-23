namespace Order.Infrastructure.Persistence.Extensions;

public static class InitialData
{
    public static IEnumerable<Customer> Customers =>
       new List<Customer>
       {
            Customer.Create(CustomerId.Of(new Guid("58c49479-ec65-4de2-86e7-033c546291aa")), "mehmet", "mehmet@gmail.com"),
            Customer.Create(CustomerId.Of(new Guid("189dc8dc-990f-48e0-a37b-e6f2b60b9d7d")), "john", "john@gmail.com")
       };


    public static IEnumerable<Product> Products =>
        new List<Product>
        {
            Product.Create(ProductId.Of(new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61")), "IPhone X", "IPhone X", 500),
            Product.Create(ProductId.Of(new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914")), "Samsung 10", "Samsung 10", 400),
            Product.Create(ProductId.Of(new Guid("4f136e9f-ff8c-4c1f-9a33-d12f689bdab8")), "Huawei Plus","Huawei Plus",650),
            Product.Create(ProductId.Of(new Guid("6ec1297b-ec0a-4aa1-be25-6726e3b51a27")), "Xiaomi Mi", "Xiaomi Mi", 450)
        };

    public static IEnumerable<SalesOrder> OrdersWithItems
    {
        get
        {
            var address1 = Address.New("mehmet", "ozkaya", "mehmet@gmail.com", "Bahcelievler No:4", "Turkey", "Istanbul","", "38050");
            var address2 = Address.New("john", "doe", "john@gmail.com", "Broadway No:1", "England", "Nottingham","", "08050");

          

            var order1 = SalesOrder.Create(
                            CustomerId.Of(new Guid("58c49479-ec65-4de2-86e7-033c546291aa")),
                            "Mehmet",
                            shippingAddress: address1,
                            billingAddress: address1);

            order1
                .AddPayment("mehmet", "5555555555554444", new DateTime(2028,12,1), 355, 1)
                .AddOrderItem(ProductId.Of(new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61")), 2, 500)
                .AddOrderItem(ProductId.Of(new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914")), 1, 400);

            var order2 = SalesOrder.Create(
                            CustomerId.Of(new Guid("189dc8dc-990f-48e0-a37b-e6f2b60b9d7d")),
                            "John",
                            shippingAddress: address2,
                            billingAddress: address2);
            order2
                .AddPayment("john", "8885555555554444", new DateTime(2028,06,01), 222, 2)
                .AddOrderItem(ProductId.Of(new Guid("4f136e9f-ff8c-4c1f-9a33-d12f689bdab8")), 1, 650)
                .AddOrderItem(ProductId.Of(new Guid("6ec1297b-ec0a-4aa1-be25-6726e3b51a27")), 2, 450);

          

            return new List<SalesOrder> { order1, order2 };
        }
    }
}
