using System.Text.Json.Serialization;

namespace Fohjin.DDD.Events.Client;

public record NewBankCardForAccountAsignedEvent : DomainEvent
{
    public Guid BankCardId { get; set; }
    public Guid AccountId { get; set; }

    [JsonConstructor]
    public NewBankCardForAccountAsignedEvent() { }

    public NewBankCardForAccountAsignedEvent(Guid bankCardId, Guid accountId)
    {
        BankCardId = bankCardId;
        AccountId = accountId;
    }
}