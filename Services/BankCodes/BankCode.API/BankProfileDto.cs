namespace BankCode.API;

public record BankProfileDto(string AccountNumber, string AccountName, string BankSource, decimal Amount);
