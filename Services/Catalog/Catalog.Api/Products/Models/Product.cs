namespace Catalog.Api.Products.Models;

public class Product
{
    public static Product Create(string name, string description,string? imageFile, decimal price)
    {
        return new Product(Guid.NewGuid(), name, description,imageFile, price);
    }
    public Product() { }

    protected Product(Guid id, string name, string description,string? imageFile, decimal price)
    {
        Id = id;
        Name = name;
        Description = description;
        ImageFile = imageFile;
        CreatedOn = DateTime.UtcNow;
        LastModifiedOn = DateTime.UtcNow;
        Price = price;
    }

    public Guid Id { get;  set; }
    public string Name { get;  set; } = default!;
    public string Description { get;  set; } = default!;
    public string? ImageFile { get;  set; }
    public decimal Price { get;  set; }

    public DateTime CreatedOn { get;  set; }

    public string? CreatedBy { get;  set; }
    public DateTime LastModifiedOn { get;  set; }

    public string? LastModifiedBy { get;  set; }


    public void SetPrice(decimal price) {
        Price = price;
    }



    public List<string> Categories { get; set; } = new();

    public void AddCategory(string category)
    {
        Categories.Add(category);
    }

    public void AddCategoryRange(IEnumerable<string> categories)
    {
        if (categories == null || !categories.Any()) return;
        
        foreach (var category in categories) {
            AddCategory(category);
        }
    }




}
