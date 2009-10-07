using System;
using System.Collections.Generic;
using Fohjin.DDD.Events;
using Fohjin.DDD.Domain.ValueObjects;
using Fohjin.DDD.Events.ActiveAccount;

namespace Fohjin.DDD.Domain.Entities.ActiveAccountStates
{
    public class CreatedState : ActiveAccountState
    {
        public CreatedState(Action<IDomainEvent> apply, Func<Guid> id, Func<AccountName> accountName, Func<Balance> balance, Func<List<Ledger>> ledgers) : base(apply, id, accountName, balance, ledgers) { }

        public override void Create(Guid id, AccountName name)
        {
            throw new Exception("Cannot create an already created ActiveAcount");
        }

        public override ClosedAccount Close()
        {
            var closedAccount = new ClosedAccount(_id(), _ledgers());
            _apply(new AccountClosedEvent());
            return closedAccount;
        }

        public override void Withdrawl(Amount amount)
        {
            if (_balance().WithdrawlWillResultInNegativeBalance(amount))
                throw new Exception(string.Format("The amount {0} is larger than your current balance {1}", (decimal)amount, (decimal)_balance()));

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