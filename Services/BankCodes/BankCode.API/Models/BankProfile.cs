using DomainBlocks.Domains;

namespace BankCode.API.Models;

public class BankProfile : AggregateRoot<Guid>
{

    public static BankProfile NewInstance(string accountNumber,string accountName,string bankSource,decimal amount)
    {
        return new BankProfile(Guid.NewGuid(), accountNumber, accountName, bankSource, amount);
    }
    protected BankProfile(Guid id,string accountNumber, string accountName, string bankSource, decimal amount) : base(id)
    {
        
        AccountNumber = accountNumber;
        AccountName = accountName;
        BankSource = bankSource;
        Amount = amount;
        Status = "Pending";
    }

    protected BankProfile() : base(Guid.NewGuid())
    {

    }
  

    public string AccountNumber { get; private set; } = default!;
    public string AccountName { get; private set; } = default!;
    public string BankSource { get; set; } = default!;

    public string Status { get; set; } = default!;

    public decimal Amount { get; private set; }
}
