using System.Text.Json.Serialization;

namespace Fohjin.DDD.Commands;

public record MoneyTransferFailedCompensatingCommand : CommandBase
{
    public decimal Amount { get; init; }
    public string? AccountNumber { get; init; }

    [JsonConstructor]
    public MoneyTransferFailedCompensatingCommand() : base() { }
    public MoneyTransferFailedCompensatingCommand(Guid id, decimal amount, string? targetAccountNumber) : base(id)
    {
        Amount = amount;
        AccountNumber = targetAccountNumber;
    }
}