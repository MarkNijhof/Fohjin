using Fohjin.DDD.EventStore.Storage.Memento;
using System.Text.Json.Serialization;

namespace Fohjin.DDD.Domain.Mementos
{
    public class ClientMemento : IMemento
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public string ClientName { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public List<Guid> Accounts { get; set; }
        public List<IMemento> BankCardMementos { get; set; }

        [JsonConstructor]
        public ClientMemento()
        {
        }
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