using System;
using System.Collections.Generic;
using System.Linq;
using Fohjin.DDD.Bus;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Contracts;
using Fohjin.DDD.Domain;
using Fohjin.DDD.Reporting.Dto;

namespace Fohjin.DDD.Services
{
    public interface ISendMoneyTransfer
    {
        void Send(MoneyTransfer moneyTransfer);
    }

    public class MoneyTransferService : ISendMoneyTransfer
    {
        private readonly ICommandBus _commandBus;
        private readonly IReportingRepository _reportingRepository;
        private readonly IReceiveMoneyTransfers _receiveMoneyTransfers;
        private readonly IDictionary<int, Action<MoneyTransfer>> _moneyTransferOptions;

        public MoneyTransferService(ICommandBus commandBus, IReportingRepository reportingRepository, IReceiveMoneyTransfers receiveMoneyTransfers)
        {
            _commandBus = commandBus;
            _reportingRepository = reportingRepository;
            _receiveMoneyTransfers = receiveMoneyTransfers;

            _moneyTransferOptions = new Dictionary<int, Action<MoneyTransfer>>
            {
                {0, MoneyTransferIsGoingToAnInternalAccount},
                {1, MoneyTransferIsGoingToAnInternalAccount},
                {2, MoneyTransferIsGoingToAnExternalAccount},
                {3, MoneyTransferIsGoingToAnExternalAccount},
                {4, MoneyTransferIsGoingToAnExternalNonExistingAccount},
            };
        }

        public void Send(MoneyTransfer moneyTransfer)
        {
            SystemTimer.Trigger(() => DoSend(moneyTransfer)).In(5000);
        }

        private void DoSend(MoneyTransfer moneyTransfer)
        {
            try
            {
                // I didn't want to introduce an actual external bank, so that's why you see this nice construct :)
                _moneyTransferOptions[SystemRandom.Next(0, 5)](moneyTransfer);
            }
            catch(Exception)
            {
                CompensatingActionBecauseOfFailedMoneyTransfer(moneyTransfer);
            }
        }

        private void MoneyTransferIsGoingToAnInternalAccount(MoneyTransfer moneyTransfer)
        {
            var account = _reportingRepository.GetByExample<AccountReport>(new { AccountNumber = moneyTransfer.TargetAccount }).First();
            _commandBus.Publish(new ReceiveMoneyTransferCommand(account.Id, moneyTransfer.Ammount, moneyTransfer.SourceAccount));
        }

        private void MoneyTransferIsGoingToAnExternalAccount(MoneyTransfer moneyTransfer)
        {
            _receiveMoneyTransfers.Receive(moneyTransfer);
        }

        private void MoneyTransferIsGoingToAnExternalNonExistingAccount(MoneyTransfer moneyTransfer)
        {
            _receiveMoneyTransfers.Receive(new MoneyTransfer(moneyTransfer.SourceAccount, moneyTransfer.TargetAccount.Reverse().ToString(), moneyTransfer.Ammount));
        }

        private void CompensatingActionBecauseOfFailedMoneyTransfer(MoneyTransfer moneyTransfer)
        {
            var account = _reportingRepository.GetByExample<AccountReport>(new { AccountNumber = moneyTransfer.SourceAccount }).First();
            _commandBus.Publish(new MoneyTransferFailedCommand(account.Id, moneyTransfer.Ammount, moneyTransfer.TargetAccount));
        }
    }
}