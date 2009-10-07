using System;
using System.Collections.Generic;
using Fohjin.DDD.Domain.ValueObjects;
using Fohjin.DDD.Events;

namespace Fohjin.DDD.Domain.Entities.ActiveAccountStates
{
    public abstract class ActiveAccountState : IActiveAccountState
    {
        protected readonly Func<Guid> _id;
        protected readonly Func<AccountName> _accountName;
        protected readonly Func<Balance> _balance;
        protected readonly Func<List<Ledger>> _ledgers;

        protected readonly Action<IDomainEvent> _apply;

        protected ActiveAccountState(Action<IDomainEvent> apply, Func<Guid> id, Func<AccountName> accountName, Func<Balance> balance, Func<List<Ledger>> ledgers)
        {
            _apply = apply;
            _id = id;
            _accountName = accountName;
            _balance = balance;
            _ledgers = ledgers;
        }

        public abstract void Create(Guid id, AccountName name);
        public abstract ClosedAccount Close();
        public abstract void Withdrawl(Amount amount);
        public abstract void Deposite(Amount amount);
    }
}