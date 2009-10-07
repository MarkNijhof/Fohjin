using System;
using System.Collections.Generic;
using Fohjin.DDD.Domain.ValueObjects;
using Fohjin.DDD.Events;

namespace Fohjin.DDD.Domain.Entities.ActiveAccountStates
{
    public class ClosedState : ActiveAccountState
    {
        public ClosedState(Action<IDomainEvent> apply, Func<Guid> id, Func<AccountName> accountName, Func<Balance> balance, Func<List<Ledger>> ledgers) : base(apply, id, accountName, balance, ledgers) { }

        public override void Create(Guid id, AccountName name)
        {
            throw new Exception("The ActiveAcount is closed and no opperations can be executed on it");
        }

        public override ClosedAccount Close()
        {
            throw new Exception("The ActiveAcount is closed and no opperations can be executed on it");
        }

        public override void Withdrawl(Amount amount)
        {
            throw new Exception("The ActiveAcount is closed and no opperations can be executed on it");
        }

        public override void Deposite(Amount amount)
        {
            throw new Exception("The ActiveAcount is closed and no opperations can be executed on it");
        }
    }
}