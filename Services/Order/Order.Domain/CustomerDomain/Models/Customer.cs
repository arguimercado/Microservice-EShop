
using Order.Domain.CustomerDomain.Types;

namespace Order.Domain.CustomerDomain.Models;

public class Customer : Entity<CustomerId>
{
    public static Customer Create(CustomerId id, string name, string email)
        => new Customer(id, name, email);
    public static Customer Create(string name, string email)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty.", nameof(name));
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email cannot be empty.", nameof(email));
        
        return new Customer(CustomerId.New(), name, email);
    }

    protected Customer() : base(CustomerId.New())
    {
       
    }

    protected Customer(CustomerId id, string name, string email) : base(id)
    {
        
        Name = name;
        Email = email;
    }

    public string Name { get; private set; } = default!;
    public string Email { get; private set; } = default!;
}
