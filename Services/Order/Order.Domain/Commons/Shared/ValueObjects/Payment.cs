namespace Order.Domain.Commons.Shared.ValueObjects;

public record Payment
{
    public static Payment Empty() => new Payment(null, string.Empty, null, 0, 0);
    public static Payment New(string cardNumber, DateTime? expriration, int cVV, int paymentMethod)
        => new Payment(null, cardNumber, expriration, cVV, paymentMethod);
    public static Payment New(string? cardName, string cardNumber, DateTime? expriration, int cVV, int paymentMethod)
        => new Payment(cardName, cardNumber, expriration, cVV, paymentMethod);

    protected Payment()
    {
        
    }

    protected Payment(string? cardName, string cardNumber, DateTime? expriration, int cVV, int paymentMethod)
    {
        CardName = cardName;
        CardNumber = cardNumber;
        Expriration = expriration;
        CVV = cVV;
        PaymentMethod = paymentMethod;
    }

    public string? CardName { get;  } = default!;
    public string CardNumber { get; } = default!;
    public DateTime? Expriration { get;  } = default!;
    public int CVV { get; } = default!;

    public int PaymentMethod { get; } = default!;
}
