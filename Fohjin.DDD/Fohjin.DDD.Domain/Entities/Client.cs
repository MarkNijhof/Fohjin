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
            Apply(new ClientCreatedEvent(Guid.NewGuid(), clientName.Name, address.Street, address.StreetNumber, address.PostalCode, address.City, phoneNumber.Number));
        }

        public static Client CreateNew(ClientName clientName, Address address, PhoneNumber phoneNumber)
        {
            return new Client(clientName, address, phoneNumber);
        }

        public void UpdatePhoneNumber(PhoneNumber phoneNumber)
        {
            IsClientCreated();

            Apply(new ClientPhoneNumberChangedEvent(phoneNumber.Number));
        }

        public void UpdateClientName(ClientName clientName)
        {
            IsClientCreated();

            Apply(new ClientNameChangedEvent(clientName.Name));
        }

        public void ClientMoved(Address newAddress)
        {
            IsClientCreated();

            Apply(new ClientMovedEvent(newAddress.Street, newAddress.StreetNumber, newAddress.PostalCode, newAddress.City));
        }

        public ActiveAccount CreateNewAccount(string accountName)
        {
            IsClientCreated();

            var activeAccount = ActiveAccount.CreateNew(Id, accountName);

            Apply(new AccountToClientAssignedEvent(activeAccount.Id));

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
            RegisterEvent<ClientCreatedEvent>(onNewClientCreated);
            RegisterEvent<ClientPhoneNumberChangedEvent>(onClientPhoneNumberWasChanged);
            RegisterEvent<ClientNameChangedEvent>(onClientNameWasChanged);
            RegisterEvent<ClientMovedEvent>(onNewClientMoved);
            RegisterEvent<AccountToClientAssignedEvent>(onAccountWasAssignedToClient);
        }

        private void onAccountWasAssignedToClient(AccountToClientAssignedEvent accountToClientAssignedEvent)
        {
            _accounts.Add(accountToClientAssignedEvent.AccountId);
        }

        private void onNewClientCreated(ClientCreatedEvent clientCreatedEvent)
        {
            Id = clientCreatedEvent.ClientId;
            _clientName = new ClientName(clientCreatedEvent.ClientName);
            _address = new Address(clientCreatedEvent.Street, clientCreatedEvent.StreetNumber, clientCreatedEvent.PostalCode, clientCreatedEvent.City);
            _phoneNumber = new PhoneNumber(clientCreatedEvent.PhoneNumber);
        }

        private void onClientPhoneNumberWasChanged(ClientPhoneNumberChangedEvent clientPhoneNumberChangedEvent)
        {
            _phoneNumber = new PhoneNumber(clientPhoneNumberChangedEvent.PhoneNumber);
        }

        private void onClientNameWasChanged(ClientNameChangedEvent clientNameChangedEvent)
        {
            _clientName = new ClientName(clientNameChangedEvent.ClientName);
        }

        private void onNewClientMoved(ClientMovedEvent clientMovedEvent)
        {
            _address = new Address(clientMovedEvent.Street, clientMovedEvent.StreetNumber, clientMovedEvent.PostalCode, clientMovedEvent.City);
        }
    }
}