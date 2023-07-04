using Fohjin.DDD.EventStore.Storage.Memento;
using System.Text.Json.Serialization;

namespace Fohjin.DDD.Domain.Mementos;

public record ClientMemento : IMemento
{
    public Guid Id { get; init; }
    public int Version { get; init; }
    public string? ClientName { get; init; }
    public string? Street { get; init; }
    public string? StreetNumber { get; init; }
    public string? PostalCode { get; init; }
    public string? City { get; init; }
    public string? PhoneNumber { get; init; }
    public List<Guid> Accounts { get; init; } = new();
    public List<IMemento> BankCardMementos { get; init; } = new();

    [JsonConstructor]
    public ClientMemento() { }
    public ClientMemento(
        Guid id,
        int version, 
        string? clientName, 
        string? street,
        string? streetNumber,
        string? postalCode,
        string? city,
        string? phoneNumber,
        List<Guid> accounts, 
        List<IMemento> bankCardMementos
        )
    {
        Id = id;
        Version = version;
        ClientName = clientName;
        Street = street;
        StreetNumber = streetNumber;
        PostalCode = postalCode;
        City = city;
        PhoneNumber = phoneNumber;
        Accounts = accounts;
        BankCardMementos = bankCardMementos;
    }
}