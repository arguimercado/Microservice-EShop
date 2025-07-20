using Catalog.Api.Products.Models;

namespace Catalog.Api.Products.Dtos
{
    public class ProductDto
    {
        public ProductDto(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
            ImageFile = product.ImageFile;
            Price = product.Price;
            CreatedOn = product.CreatedOn;
            CreatedBy = product.CreatedBy;
            LastModifiedOn = product.LastModifiedOn;
            LastModifiedBy = product.LastModifiedBy;
            Categories = product.Categories;

        }
        public ProductDto(Guid id, string name, string description, string? imageFile, decimal price, DateTime createdOn, string? createdBy, DateTime lastModifiedOn, string? lastModifiedBy)
        {
            Id = id;
            Name = name;
            Description = description;
            ImageFile = imageFile;
            Price = price;
            CreatedOn = createdOn;
            CreatedBy = createdBy;
            LastModifiedOn = lastModifiedOn;
            LastModifiedBy = lastModifiedBy;
        }

        public Guid Id { get;  }
        public string Name { get;  }
        public string Description { get;  }
        public string? ImageFile { get;  }
        public decimal Price { get;  }

        public DateTime CreatedOn { get;  }

        public string? CreatedBy { get;  }
        public DateTime LastModifiedOn { get;  }

        public string? LastModifiedBy { get;  }

        public IEnumerable<string> Categories { get; set; } = new List<string>();

    }
}
