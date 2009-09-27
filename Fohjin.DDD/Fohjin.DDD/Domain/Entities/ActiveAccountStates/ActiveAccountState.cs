using System;
using System.Collections.Generic;
using Fohjin.DDD.Domain.Events;
using Fohjin.DDD.Domain.ValueObjects;

namespace Fohjin.DDD.Domain.Entities.ActiveAccountStates
{
    public abstract class ActiveAccountState : IActiveAccountState
    {
        protected readonly Func<Guid> _id;
        protected readonly Func<Balance> _balance;
        protected readonly Func<List<Ledger>> _ledgers;

        protected readonly Action<IDomainEvent> _apply;

        protected ActiveAccountState(Action<IDomainEvent> apply, Func<Guid> id, Func<Balance> balance, Func<List<Ledger>> ledgers)
        {
            _apply = apply;
            _id = id;
            _balance = balance;
            _ledgers = ledgers;
        }

        public abstract void Create();
        public abstract ClosedAccount Close();
        public abstract void Withdrawl(Amount amount);
        public abstract void Deposite(Amount amount);
    }
}