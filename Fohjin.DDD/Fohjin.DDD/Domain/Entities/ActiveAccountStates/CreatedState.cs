using System;
using System.Collections.Generic;
using Fohjin.DDD.Domain.Events;
using Fohjin.DDD.Domain.ValueObjects;

namespace Fohjin.DDD.Domain.Entities.ActiveAccountStates
{
    public class CreatedState : ActiveAccountState
    {
        public CreatedState(Action<IDomainEvent> apply, Func<Guid> id, Func<Balance> balance, Func<List<Ledger>> ledgers) : base(apply, id, balance, ledgers) { }

        public override void Create()
        {
            throw new Exception("Cannot create an already created ActiveAcount");
        }

        public override ClosedAccount Close()
        {
            var closedAccount = new ClosedAccount(_ledgers());
            _apply(new AccountClosedEvent());
            return closedAccount;
        }

        public override void Withdrawl(Amount amount)
        {
            if (_balance().WithdrawlWillResultInNegativeBalance(amount))
                throw new Exception("The amount is larger than your current balance");

            var newBalance = _balance().Withdrawl(amount);

            _apply(new WithdrawlEvent(newBalance, amount));
        }

        public override void Deposite(Amount amount)
        {
            var newBalance = _balance().Deposite(amount);

            _apply(new DepositeEvent(newBalance, amount));
        }
    }
}