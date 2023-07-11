using Fohjin.DDD.Domain.Mementos;
using Fohjin.DDD.Events.Account;
using Fohjin.DDD.EventStore;
using Fohjin.DDD.EventStore.Aggregate;
using Fohjin.DDD.EventStore.Storage.Memento;

namespace Fohjin.DDD.Domain.Account
{
    public class ClosedAccount : BaseAggregateRoot<IDomainEvent>, IOriginator
    {
        private Guid _originalAccountId;
        private Guid _clientId;
        private AccountName _accountName;
        private AccountNumber _accountNumber;
        private readonly List<Ledger> _ledgers = new();

        public ClosedAccount()
        {
            Id = Guid.Empty;
            _accountName = new(string.Empty);
            _accountNumber = new(string.Empty);
            Version = 0;
            EventVersion = 0;

            RegisterEvents();
        }

        private ClosedAccount(Guid accountId, Guid clientId, List<Ledger> ledgers, string? accountName, string? accountNumber) : this()
        {
            var Ledgers = new List<KeyValuePair<string, string>>();
            ledgers.ForEach(x => Ledgers.Add(new KeyValuePair<string, string>(x.GetType().Name, string.Format("{0}|{1}", ((decimal)x.Amount), x.Account.Number))));

            Apply(new ClosedAccountCreatedEvent(Guid.NewGuid(), accountId, clientId, Ledgers, accountName, accountNumber));
        }

        public static ClosedAccount CreateNew(Guid accountId, Guid clientId, List<Ledger> ledgers, AccountName accountName, AccountNumber accountNumber) =>
            new (accountId, clientId, ledgers, accountName.Name, accountNumber.Number);

        IMemento IOriginator.CreateMemento() =>
            new ClosedAccountMemento(Id, Version, _originalAccountId, _clientId, _accountName.Name, _accountNumber.Number, _ledgers);

        void IOriginator.SetMemento(IMemento memento)
        {
            var closedAccountMemento = (ClosedAccountMemento)memento;
            Id = closedAccountMemento.Id;
            Version = closedAccountMemento.Version;
            _originalAccountId = closedAccountMemento.OriginalAccountId;
            _clientId = closedAccountMemento.ClientId;
            _accountName = new AccountName(closedAccountMemento.AccountName);
            _accountNumber = new AccountNumber(closedAccountMemento.AccountNumber);

            foreach (var ledger in closedAccountMemento.Ledgers)
            {
                var split = ledger.Value.Split(new[] { '|' });
                var amount = new Amount(Convert.ToDecimal(split[0]));
                var account = new AccountNumber(split[1]);
                var instance = InstantiateClassFromStringValue<Ledger>(ledger.Key, amount, account);
                if (instance != null)
                    _ledgers.Add(instance);
            }
        }

        private TRequestedType? InstantiateClassFromStringValue<TRequestedType>(string className, params object[] constructorArguments)
        {
            var classType = GetType()
                .Assembly
                .GetExportedTypes()
                .Where(x => x.Name == className)
                .FirstOrDefault();

            if (classType == null)
                return default;

            return (TRequestedType?)Activator.CreateInstance(classType, constructorArguments);
        }

        private void RegisterEvents()
        {
            RegisterEvent<ClosedAccountCreatedEvent>(OnClosedAccountCreated);
        }

        private void OnClosedAccountCreated(ClosedAccountCreatedEvent closedAccountCreatedEvent)
        {
            Id = closedAccountCreatedEvent.AccountId;
            _originalAccountId = closedAccountCreatedEvent.OriginalAccountId;
            _clientId = closedAccountCreatedEvent.ClientId;
            _accountName = new AccountName(closedAccountCreatedEvent.AccountName);
            _accountNumber = new AccountNumber(closedAccountCreatedEvent.AccountNumber);

            foreach (var ledger in closedAccountCreatedEvent.Ledgers)
            {
                var split = ledger.Value.Split(new[] { '|' });
                var amount = new Amount(Convert.ToDecimal(split[0]));
                var account = new AccountNumber(split[1]);
                var instance = InstantiateClassFromStringValue<Ledger>(ledger.Key, amount, account);
                if (instance != null)
                    _ledgers.Add(instance);
            }
        }
    }
}