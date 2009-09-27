using System;
using System.Collections.Generic;
using Fohjin.DDD.Domain.Events;
using Fohjin.DDD.Domain.ValueObjects;

namespace Fohjin.DDD.Domain.Entities.ActiveAccountStates
{
    public class InValidState : ActiveAccountState
    {
        public InValidState(Action<IDomainEvent> apply, Func<Guid> id, Func<Balance> balance, Func<List<Ledger>> ledgers) : base(apply, id, balance, ledgers) { }

        public override void Create()
        {
            _apply(new AccountCreatedEvent(Guid.NewGuid()));
        }

        public override ClosedAccount Close()
        {
            throw new Exception("Cannot close an un-created ActiveAcount");
        }

        public override void Withdrawl(Amount amount)
        {
            throw new Exception("Cannot withdrawl on an un-created ActiveAcount");
        }

        public override void Deposite(Amount amount)
        {
            throw new Exception("Cannot deposite on an un-created ActiveAcount");
        }
    }
}