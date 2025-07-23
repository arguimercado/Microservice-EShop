namespace Order.Domain.Commons.Shared.ValueObjects;

public record Address
{
    public static Address New(string firstName, string lastName, string emailAddress, string addressLine, string city, string country, string state, string zipCode)
    {
        return new Address(firstName, lastName, emailAddress, addressLine, city, country, state, zipCode);
    }
    protected Address()
    {
        
    }
    protected Address(string firstName, string lastName, string emailAddress, string addressLine, string city, string country, string state, string zipCode)
    {
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
        AddressLine = addressLine;
        City = city;
        Country = country;
        State = state;
        ZipCode = zipCode;
    }

   
    
  

    public string FirstName { get;  } = default!;
    public string LastName { get;  } = default!;

    public string EmailAddress { get; } = default!;
    public string AddressLine { get; } = default!;
   
    public string City { get; } = default!;
    public string Country { get; } = default!;

    public string State { get; } = default!;

    public string ZipCode { get; } = default!;
}

