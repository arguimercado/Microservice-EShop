using Catalog.Api.Products.Models;
using Marten.Schema;

namespace Catalog.Api.Commons.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();

        if (await session.Query<Product>().AnyAsync()) return;

        session.Store<Product>(GetPreconfiguredProducts());

        await session.SaveChangesAsync(cancellation);

    }

    private static IEnumerable<Product> GetPreconfiguredProducts() => new List<Product>()
    {
        Product.Create(
            name: "IPhone X",
            description: "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
            imageFile: "product-1.png",
            price: 950.00M,
            categories: new List<string> { "Smart Phone" }),

        Product.Create(
            name: "Samsung 10",
            description: "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
            imageFile: "product-2.png",
            price: 840.00M,
            categories: new List<string> { "Smart Phone" }
        ),
        Product.Create(
            name: "Huawei Plus",
            description: "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
            imageFile: "product-3.png",
            price: 650.00M,
            categories: new List<string> { "White Appliances" }
        ),
        Product.Create(
            name: "Xiaomi Mi 9",
            description: "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
            imageFile: "product-4.png",
            price: 470.00M,
            categories: new List<string> { "White Appliances" }
        ),
        Product.Create(
            name: "HTC U11+ Plus",
            description: "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
            imageFile: "product-5.png",
            price: 380.00M,
            categories: new List<string> { "Smart Phone" }
        ),
        Product.Create(
            name: "LG G7 ThinQ",
            description: "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
            imageFile: "product-6.png",
            price: 240.00M,
            categories: new List<string> { "Home Kitchen" }
        ),
        Product.Create(
            name: "Panasonic Lumix",
            description: "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
            imageFile: "product-6.png",
            price: 240.00M,
            categories: new List<string> { "Camera" }
        )
    };
}
