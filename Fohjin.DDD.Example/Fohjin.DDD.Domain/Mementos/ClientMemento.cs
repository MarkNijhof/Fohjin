using Fohjin.DDD.EventStore.Storage.Memento;

namespace Fohjin.DDD.Domain.Mementos
{
    [Serializable]
    public class ClientMemento : IMemento
    {
        internal Guid Id { get; init; }
        internal int Version { get; init; }
        internal string ClientName { get; init; }
        internal string Street { get; init; }
        internal string StreetNumber { get; init; }
        internal string PostalCode { get; init; }
        internal string City { get; init; }
        internal string PhoneNumber { get; init; }
        internal List<Guid> Accounts { get; init; }
        internal List<IMemento> BankCardMementos { get; init; }

        public ClientMemento(Guid id, int version, string clientName, string street, string streetNumber, string postalCode, string city, string phoneNumber, List<Guid> accounts, List<IMemento> bankCardMementos)
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
}