using System.Text.Json.Serialization;

namespace Fohjin.DDD.Events.Account;

public record MoneyTransferSendEvent : DomainEvent
{
    public decimal Balance { get; set; }
    public decimal Amount { get; set; }
    public string? SourceAccount { get; set; }
    public string? TargetAccount { get; set; } 

    [JsonConstructor]
    public MoneyTransferSendEvent() { }

    public MoneyTransferSendEvent(
        decimal balance,
        decimal amount,
        string? sourceAccount,
        string? targetAccount
        )
    {
        Balance = balance;
        Amount = amount;
        SourceAccount = sourceAccount;
        TargetAccount = targetAccount;
    }
}