using System;
using System.Collections.Generic;
using Fohjin.DDD.Events;
using Fohjin.DDD.Domain.ValueObjects;
using Fohjin.DDD.Events.ActiveAccount;

namespace Fohjin.DDD.Domain.Entities.ActiveAccountStates
{
    public class InValidState : ActiveAccountState
    {
        public InValidState(Action<IDomainEvent> apply, Func<Guid> id, Func<AccountName> accountName, Func<Balance> balance, Func<List<Ledger>> ledgers) : base(apply, id, accountName, balance, ledgers) { }

        public override void Create(Guid id, AccountName name)
        {
            _apply(new AccountCreatedEvent(id, name.Name));
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