using System;
using System.Collections.Generic;
using Fohjin.DDD.Domain.Events;
using Fohjin.DDD.Domain.ValueObjects;

namespace Fohjin.DDD.Domain.Entities.ActiveAccountStates
{
    public class ClosedState : ActiveAccountState
    {
        public ClosedState(Action<IDomainEvent> apply, Func<Guid> id, Func<Balance> balance, Func<List<Ledger>> ledgers) : base(apply, id, balance, ledgers) { }

        public override void Create()
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