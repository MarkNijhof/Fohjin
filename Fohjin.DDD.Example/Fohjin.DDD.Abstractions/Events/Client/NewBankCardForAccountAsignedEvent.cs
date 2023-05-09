namespace Fohjin.DDD.Events.Client
{
    public record NewBankCardForAccountAsignedEvent : DomainEvent
    {
        public Guid BankCardId { get; init; }
        public Guid AccountId { get; init; }

        public NewBankCardForAccountAsignedEvent(Guid bankCardId, Guid accountId)
        {
            BankCardId = bankCardId;
            AccountId = accountId;
        }
    }
}