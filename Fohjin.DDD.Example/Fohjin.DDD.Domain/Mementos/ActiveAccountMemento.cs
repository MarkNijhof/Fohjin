using Fohjin.DDD.Domain.Account;
using Fohjin.DDD.EventStore.Storage.Memento;
using System.Text.Json.Serialization;

namespace Fohjin.DDD.Domain.Mementos;

public record ActiveAccountMemento : IMemento
{
    public Guid Id { get; init; }
    public int Version { get; init; }
    public Guid ClientId { get; init; }
    public string? AccountName { get; init; }
    public string? AccountNumber { get; init; }
    public decimal Balance { get; init; }
    public bool Closed { get; init; }
    public List<KeyValuePair<string, string>> Ledgers { get; init; } = new();

    [JsonConstructor]
    public ActiveAccountMemento() { }

    public ActiveAccountMemento(
        Guid id,
        int version, 
        Guid clientId, 
        string? accountName, 
        string? accountNumber,
        decimal balance,
        List<Ledger> ledgers,
        bool closed
        )
    {
        Id = id;
        Version = version;
        ClientId = clientId;
        AccountName = accountName;
        AccountNumber = accountNumber;
        Balance = balance;
        Closed = closed;
        Ledgers = new List<KeyValuePair<string, string>>();
        ledgers.ForEach(x => Ledgers.Add(new KeyValuePair<string, string>(x.GetType().Name, string.Format("{0}|{1}", ((decimal)x.Amount), x.Account.Number))));
    }
}