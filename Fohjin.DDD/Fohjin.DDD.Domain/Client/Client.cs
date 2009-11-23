using System;
using System.Collections.Generic;
using System.Linq;
using Fohjin.DDD.Domain.Account;
using Fohjin.DDD.Domain.Mementos;
using Fohjin.DDD.Events.Client;
using Fohjin.DDD.EventStore;
using Fohjin.DDD.EventStore.Aggregate;
using Fohjin.DDD.EventStore.Storage.Memento;

namespace Fohjin.DDD.Domain.Client
{
    public class Client : BaseAggregateRoot<IDomainEvent>, IOrginator
    {
        private PhoneNumber _phoneNumber;
        private Address _address;
        private ClientName _clientName;
        private readonly List<Guid> _accounts;
        private readonly EntityList<BankCard, IDomainEvent> _bankCards;

        public Client()
        {
            _accounts = new List<Guid>();
            _bankCards = new EntityList<BankCard, IDomainEvent>(this);

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

        public void AssignNewBankCardForAccount(Guid accountId)
        {
            IsClientCreated();

            DoesAccountBelongToClient(accountId);

            Apply(new NewBankCardForAccountAsignedEvent(Guid.NewGuid(), accountId));
        }

        public IBankCard GetBankCard(Guid bankCardId)
        {
            var bankCard = _bankCards.Where(x => x.Id == bankCardId).FirstOrDefault();
            if (bankCard == null)
                throw new NonExistingBankCardException("The requested bank card does not exist!");

            return bankCard;
        }

        private void DoesAccountBelongToClient(Guid accountId)
        {
            if (!_accounts.Contains(accountId))
                throw new NonExistingAccountException("Client does not have the requested account");
        }

        private void IsClientCreated()
        {
            if (Id == Guid.Empty)
                throw new NonExistingClientException("The Client is not created and no opperations can be executed on it");
        }

        IMemento IOrginator.CreateMemento()
        {
            var bankCardMementos = new List<IMemento>();
            _bankCards.ForEach(x => bankCardMementos.Add(((IOrginator)x).CreateMemento()));

            return new ClientMemento(Id, Version, _clientName.Name, _address.Street, _address.StreetNumber, _address.PostalCode, _address.City, _phoneNumber.Number, _accounts, bankCardMementos);
        }

        void IOrginator.SetMemento(IMemento memento)
        {
            var clientMemento = (ClientMemento) memento;
            Id = clientMemento.Id;
            Version = clientMemento.Version;
            _clientName = new ClientName(clientMemento.ClientName);
            _address = new Address(clientMemento.Street, clientMemento.StreetNumber, clientMemento.PostalCode, clientMemento.City);
            _phoneNumber = new PhoneNumber(clientMemento.PhoneNumber);
            _accounts.AddRange(clientMemento.Accounts);

            clientMemento.BankCardMementos.ForEach(x =>
            {
                var bankCard = new BankCard();
                ((IOrginator)bankCard).SetMemento(x);
                _bankCards.Add(bankCard);
            });
        }

        private void registerEvents()
        {
            RegisterEvent<ClientCreatedEvent>(onNewClientCreated);
            RegisterEvent<ClientPhoneNumberChangedEvent>(onClientPhoneNumberWasChanged);
            RegisterEvent<ClientNameChangedEvent>(onClientNameWasChanged);
            RegisterEvent<ClientMovedEvent>(onNewClientMoved);
            RegisterEvent<AccountToClientAssignedEvent>(onAccountWasAssignedToClient);
            RegisterEvent<NewBankCardForAccountAsignedEvent>(onNewBankCardForAccountAssigned);

            RegisterEvent<BankCardWasReportedStolenEvent>(onAnyEventForABankCard);
            RegisterEvent<BankCardWasCanceledByCLientEvent>(onAnyEventForABankCard);
        }

        private void onAnyEventForABankCard(IDomainEvent domainEvent)
        {
            IEntityEventProvider<IDomainEvent> bankCard;
            if (!_bankCards.TryGetValueById(domainEvent.AggregateId, out bankCard))
                throw new NonExistingBankCardException("The requested bank card does not exist!");

            bankCard.LoadFromHistory(new[] { domainEvent });
        }

        private void onNewBankCardForAccountAssigned(NewBankCardForAccountAsignedEvent newBankCardForAccountAsignedEvent)
        {
            _bankCards.Add(new BankCard(newBankCardForAccountAsignedEvent.BankCardId, newBankCardForAccountAsignedEvent.AccountId));
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