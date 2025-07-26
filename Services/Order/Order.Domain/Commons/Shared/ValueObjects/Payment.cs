namespace Order.Domain.Commons.Shared.ValueObjects;

public record Payment
{
    public static Payment Empty() => new Payment(null, string.Empty, null, 0, 0);
    public static Payment New(string cardNumber, DateTime? expiration, int cVV, int paymentMethod)
        => new Payment(null, cardNumber, expiration, cVV, paymentMethod);
    public static Payment New(string? cardName, string cardNumber, DateTime? expiration, int cVV, int paymentMethod)
        => new Payment(cardName, cardNumber, expiration, cVV, paymentMethod);

    protected Payment()
    {
        
    }

    protected Payment(string? cardName, string cardNumber, DateTime? expiration, int cVV, int paymentMethod)
    {
        CardName = cardName;
        CardNumber = cardNumber;
        Expiration = expiration;
        CVV = cVV;
        PaymentMethod = paymentMethod;
    }

    public string? CardName { get;  } = default!;
    public string CardNumber { get; } = default!;
    public DateTime? Expiration { get;  } = default!;
    public int CVV { get; } = default!;

    public int PaymentMethod { get; } = default!;
}
