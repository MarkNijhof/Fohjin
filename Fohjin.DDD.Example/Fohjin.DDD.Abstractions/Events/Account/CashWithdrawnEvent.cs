using System.Text.Json.Serialization;

namespace Fohjin.DDD.Events.Account;

public record CashWithdrawnEvent : DomainEvent
{
    public decimal Balance { get; set; }
    public decimal Amount { get; set; }

    [JsonConstructor]
    public CashWithdrawnEvent() { }
    public CashWithdrawnEvent(decimal balance, decimal amount)
    {
        Balance = balance;
        Amount = amount;
    }
}