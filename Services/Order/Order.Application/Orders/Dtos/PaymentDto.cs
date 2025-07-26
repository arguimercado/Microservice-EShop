using Order.Domain.Commons.Shared.ValueObjects;

namespace Order.Application.Orders.Dtos;

public record  PaymentDto
{
    public PaymentDto(string? cardName, string cardNumber, DateTime? expiration, int cVV, int paymentMethod)
    {
        CardName = cardName;
        CardNumber = cardNumber;
        Expiration = expiration;
        CVV = cVV;
        PaymentMethod = paymentMethod;
    }

    public static Payment MapToDomain(PaymentDto dto)
    {
        return Payment.New(
            cardName: dto.CardName,
            cardNumber: dto.CardNumber,
            expiration: dto.Expiration,
            cVV: dto.CVV,
            paymentMethod: dto.PaymentMethod);
    }

    public static PaymentDto MapToDto(Payment payment)
    {
        return new PaymentDto(
            cardName: payment.CardName,
            cardNumber: payment.CardNumber,
            expiration: payment.Expiration,
            cVV: payment.CVV,
            paymentMethod: payment.PaymentMethod
        );
    }

    public string? CardName { get; init; }
    public string CardNumber { get; init; }
    public DateTime? Expiration { get; init; }
    public int CVV { get; init; }
    public int PaymentMethod { get; init; }

   
}
