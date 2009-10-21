using System;
using System.Collections.Generic;
using Fohjin.DDD.Domain.Entities.Mementos;
using Fohjin.DDD.Domain.Exceptions;
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

        public static Client CreateNew(ClientName clientName, Address address, PhoneNumber phoneNumber)
        {
            return new Client(clientName, address, phoneNumber);
        }

        public void UpdatePhoneNumber(PhoneNumber phoneNumber)
        {
            IsClientCreated();

            Apply(new ClientPhoneNumberWasChangedEvent(Id, phoneNumber.Number));
        }

        public void UpdateClientName(ClientName clientName)
        {
            IsClientCreated();

            Apply(new ClientNameWasChangedEvent(Id, clientName.Name));
        }

        public void ClientMoved(Address newAddress)
        {
            IsClientCreated();

            Apply(new ClientHasMovedEvent(Id, newAddress.Street, newAddress.StreetNumber, newAddress.PostalCode, newAddress.City));
        }

        public ActiveAccount CreateNewAccount(string accountName)
        {
            IsClientCreated();

            var activeAccount = ActiveAccount.CreateNew(Id, accountName);

            Apply(new AccountWasAssignedToClientEvent(Id, activeAccount.Id));

            return activeAccount;
        }

        private void IsClientCreated()
        {
            if (Id == new Guid())
                throw new ClientWasNotCreatedException("The Client is not created and no opperations can be executed on it");
        }

        public IMemento CreateMemento()
        {
            return new ClientMemento(Id, Version, _clientName.Name, _address.Street, _address.StreetNumber, _address.PostalCode, _address.City, _phoneNumber.Number, _accounts);
        }

        public void SetMemento(IMemento memento)
        {
            var clientMemento = (ClientMemento)memento;
            Id = clientMemento.Id;
            Version = clientMemento.Version;
            _clientName = new ClientName(clientMemento.ClientName);
            _address = new Address(clientMemento.Street, clientMemento.StreetNumber, clientMemento.PostalCode, clientMemento.City);
            _phoneNumber = new PhoneNumber(clientMemento.PhoneNumber);
            _accounts.AddRange(clientMemento.Accounts);
        }

        private void registerEvents()
        {
            RegisterEvent<NewClientCreatedEvent>(onNewClientCreated);
            RegisterEvent<ClientPhoneNumberWasChangedEvent>(onClientPhoneNumberWasChanged);
            RegisterEvent<ClientNameWasChangedEvent>(onClientNameWasChanged);
            RegisterEvent<ClientHasMovedEvent>(onNewClientMoved);
            RegisterEvent<AccountWasAssignedToClientEvent>(onAccountWasAssignedToClient);
        }

        private void onAccountWasAssignedToClient(AccountWasAssignedToClientEvent accountWasAssignedToClientEvent)
        {
            _accounts.Add(accountWasAssignedToClientEvent.AccountId);
        }

        private void onNewClientCreated(NewClientCreatedEvent newClientCreatedEvent)
        {
            Id = newClientCreatedEvent.ClientId;
            _clientName = new ClientName(newClientCreatedEvent.ClientName);
            _address = new Address(newClientCreatedEvent.Street, newClientCreatedEvent.StreetNumber, newClientCreatedEvent.PostalCode, newClientCreatedEvent.City);
            _phoneNumber = new PhoneNumber(newClientCreatedEvent.PhoneNumber);
        }

        private void onClientPhoneNumberWasChanged(ClientPhoneNumberWasChangedEvent clientPhoneNumberWasChangedEvent)
        {
            _phoneNumber = new PhoneNumber(clientPhoneNumberWasChangedEvent.PhoneNumber);
        }

        private void onClientNameWasChanged(ClientNameWasChangedEvent clientNameWasChangedEvent)
        {
            _clientName = new ClientName(clientNameWasChangedEvent.ClientName);
        }

        private void onNewClientMoved(ClientHasMovedEvent clientHasMovedEvent)
        {
            _address = new Address(clientHasMovedEvent.Street, clientHasMovedEvent.StreetNumber, clientHasMovedEvent.PostalCode, clientHasMovedEvent.City);
        }
    }
}