using Fohjin.DDD.Domain.Account;
using Fohjin.DDD.Domain.Mementos;
using Fohjin.DDD.Events.Client;
using Fohjin.DDD.EventStore;
using Fohjin.DDD.EventStore.Aggregate;
using Fohjin.DDD.EventStore.Storage.Memento;

namespace Fohjin.DDD.Domain.Client;

public class Client : BaseAggregateRoot<IDomainEvent>, IOriginator
{
    private PhoneNumber? _phoneNumber;
    private Address? _address;
    private ClientName? _clientName;
    private readonly List<Guid> _accounts = new();
    private readonly EntityList<BankCard, IDomainEvent> _bankCards;

    public Client()
    {
        _bankCards = new(this);

        RegisterEvents();
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

    public ActiveAccount CreateNewAccount(string? accountName, string? accountNumber)
    {
        IsClientCreated();

        var activeAccount = ActiveAccount.CreateNew(Id, accountName, accountNumber);

        Apply(new AccountToClientAssignedEvent(activeAccount.Id));

        return activeAccount;
    }

    public void AssignNewBankCardForAccount(Guid accountId)
    {
        IsClientCreated();

        DoesAccountBelongToClient(accountId);

        Apply(new NewBankCardForAccountAsignedEvent(Guid.NewGuid(), accountId));
    }

    public IBankCard GetBankCard(Guid bankCardId) =>
        _bankCards.Where(x => x.Id == bankCardId).FirstOrDefault() ??
        throw new NonExistingBankCardException("The requested bank card does not exist!");

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

    IMemento IOriginator.CreateMemento()
    {
        var bankCardMementos = new List<IMemento>();
        _bankCards.ForEach(x => bankCardMementos.Add(((IOriginator)x).CreateMemento()));

        return new ClientMemento(Id, Version, _clientName?.Name, _address?.Street, _address?.StreetNumber, _address?.PostalCode, _address?.City, _phoneNumber?.Number, _accounts, bankCardMementos);
    }

    void IOriginator.SetMemento(IMemento memento)
    {
        var clientMemento = (ClientMemento)memento;
        Id = clientMemento.Id;
        Version = clientMemento.Version;
        _clientName = new ClientName(clientMemento.ClientName);
        _address = new Address(clientMemento.Street, clientMemento.StreetNumber, clientMemento.PostalCode, clientMemento.City);
        _phoneNumber = new PhoneNumber(clientMemento.PhoneNumber);
        _accounts.AddRange(clientMemento.Accounts);

        clientMemento.BankCardMementos.ForEach(x =>
        {
            var bankCard = new BankCard();
            ((IOriginator)bankCard).SetMemento(x);
            _bankCards.Add(bankCard);
        });
    }

    private void RegisterEvents()
    {
        RegisterEvent<ClientCreatedEvent>(OnNewClientCreated);
        RegisterEvent<ClientPhoneNumberChangedEvent>(OnClientPhoneNumberWasChanged);
        RegisterEvent<ClientNameChangedEvent>(OnClientNameWasChanged);
        RegisterEvent<ClientMovedEvent>(OnNewClientMoved);
        RegisterEvent<AccountToClientAssignedEvent>(OnAccountWasAssignedToClient);
        RegisterEvent<NewBankCardForAccountAsignedEvent>(OnNewBankCardForAccountAssigned);

        RegisterEvent<BankCardWasReportedStolenEvent>(OnAnyEventForABankCard);
        RegisterEvent<BankCardWasCanceledByClientEvent>(OnAnyEventForABankCard);
    }

    private void OnAnyEventForABankCard(IDomainEvent domainEvent)
    {
        if (!_bankCards.TryGetValueById(domainEvent.AggregateId, out IEntityEventProvider<IDomainEvent>? bankCard))
            throw new NonExistingBankCardException("The requested bank card does not exist!");

        bankCard?.LoadFromHistory(new[] { domainEvent });
    }

    private void OnNewBankCardForAccountAssigned(NewBankCardForAccountAsignedEvent newBankCardForAccountAssignedEvent)
    {
        _bankCards.Add(new BankCard(newBankCardForAccountAssignedEvent.BankCardId, newBankCardForAccountAssignedEvent.AccountId));
    }

    private void OnAccountWasAssignedToClient(AccountToClientAssignedEvent accountToClientAssignedEvent)
    {
        _accounts.Add(accountToClientAssignedEvent.AccountId);
    }

    private void OnNewClientCreated(ClientCreatedEvent clientCreatedEvent)
    {
        Id = clientCreatedEvent.ClientId;
        _clientName = new ClientName(clientCreatedEvent.ClientName);
        _address = new Address(clientCreatedEvent.Street, clientCreatedEvent.StreetNumber, clientCreatedEvent.PostalCode, clientCreatedEvent.City);
        _phoneNumber = new PhoneNumber(clientCreatedEvent.PhoneNumber);
    }

    private void OnClientPhoneNumberWasChanged(ClientPhoneNumberChangedEvent clientPhoneNumberChangedEvent)
    {
        _phoneNumber = new PhoneNumber(clientPhoneNumberChangedEvent.PhoneNumber);
    }

    private void OnClientNameWasChanged(ClientNameChangedEvent clientNameChangedEvent)
    {
        _clientName = new ClientName(clientNameChangedEvent.ClientName);
    }

    private void OnNewClientMoved(ClientMovedEvent clientMovedEvent)
    {
        _address = new Address(clientMovedEvent.Street, clientMovedEvent.StreetNumber, clientMovedEvent.PostalCode, clientMovedEvent.City);
    }
}