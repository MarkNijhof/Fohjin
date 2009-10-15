using System;
using System.Collections.Generic;
using Fohjin.DDD.Domain.Entities.Mementos;
using Fohjin.DDD.Domain.ValueObjects;
using Fohjin.DDD.Events.Client;

namespace Fohjin.DDD.Domain.Entities
{
    public class Client : BaseAggregateRoot, IOrginator
    {
        private PhoneNumber _phoneNumber;
        private Address _address;
        private ClientName _clientName;
        private readonly List<Guid> _accounts;

        public Client()
        {
            _accounts = new List<Guid>();

            registerEvents();
        }

        private Client(ClientName clientName, Address address, PhoneNumber phoneNumber) : this()
        {
            Apply(new NewClientCreatedEvent(Guid.NewGuid(), clientName.Name, address.Street, address.StreetNumber, address.PostalCode, address.City, phoneNumber.Number));
        }

        public static Client CreateClient(ClientName clientName, Address address, PhoneNumber phoneNumber)
        {
            return new Client(clientName, address, phoneNumber);
        }

        public void UpdatePhoneNumber(PhoneNumber phoneNumber)
        {
            
        }

        public void ClientMoved(Address newAddress)
        {
            
        }

        public void AddAccount(Guid accountId)
        {
            
        }

        public void RemoveAccount(Guid accountId)
        {
            
        }

        public IMemento CreateMemento()
        {
            throw new NotImplementedException();
        }

        public void SetMemento(IMemento memento)
        {
            throw new NotImplementedException();
        }

        private void registerEvents()
        {
            RegisterEvent<NewClientCreatedEvent>(onNewClientCreated);
        }

        private void onNewClientCreated(NewClientCreatedEvent newClientCreatedEvent)
        {
            Id = newClientCreatedEvent.ClientId;
            _clientName = new ClientName(newClientCreatedEvent.ClientName);
            _address = new Address(newClientCreatedEvent.Street, newClientCreatedEvent.StreetNumber, newClientCreatedEvent.PostalCode, newClientCreatedEvent.City);
            _phoneNumber = new PhoneNumber(newClientCreatedEvent.PhoneNumber);
        }
    }
}