namespace Basket.API.Baskets.Dtos;

public record BasketCheckoutDto
{
    public BasketCheckoutDto(
        string userName, Guid customerId, 
        decimal totalPrice, string firstName, 
        string lastName, string emailAddress,
        string addressLine, string country, 
        string city, string state, string zipCode, 
        string cardName, string cardNumber, string expiration, 
        string cVV, int paymentMethod)
    {
        UserName = userName;
        CustomerId = customerId;
        TotalPrice = totalPrice;
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
        AddressLine = addressLine;
        Country = country;
        City = city;
        State = state;
        ZipCode = zipCode;
        CardName = cardName;
        CardNumber = cardNumber;
        Expiration = expiration;
        CVV = cVV;
        PaymentMethod = paymentMethod;
    }

    public string UserName { get;  } = default!;
    public Guid CustomerId { get;  } = default!;
    public decimal TotalPrice { get;  } = default!;

    // Shipping and BillingAddress
    public string FirstName { get;  } = default!;
    public string LastName { get;  } = default!;
    public string EmailAddress { get;  } = default!;
    public string AddressLine { get;  } = default!;
    public string Country { get;  } = default!;
    public string City { get;  } = default!;
    public string State { get;  } = default!;
    public string ZipCode { get;  } = default!;

    // Payment
    public string CardName { get;  } = default!;
    public string CardNumber { get;  } = default!;
    public string Expiration { get;  } = default!;
    public string CVV { get;  } = default!;
    public int PaymentMethod { get;  } = default!;
}
