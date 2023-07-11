using Fohjin.DDD.Bus;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Common;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Reporting.Dtos;
using Fohjin.DDD.Services.Models;

namespace Fohjin.DDD.Services
{

    public class MoneyTransferService : ISendMoneyTransfer
    {
        private readonly IBus _bus;
        private readonly IReportingRepository _reportingRepository;
        private readonly IReceiveMoneyTransfers _receiveMoneyTransfers;
        private readonly IDictionary<int, Action<MoneyTransfer>> _moneyTransferOptions;
        private readonly ISystemTimer _systemTimer;
        private readonly ISystemRandom _systemRandom;

        public MoneyTransferService(
            IBus bus,
            IReportingRepository reportingRepository,
            IReceiveMoneyTransfers receiveMoneyTransfers,
            ISystemTimer systemTimer,
            ISystemRandom systemRandom
            )
        {
            _bus = bus;
            _reportingRepository = reportingRepository;
            _receiveMoneyTransfers = receiveMoneyTransfers;
            _systemTimer = systemTimer;
            _systemRandom = systemRandom;

            _moneyTransferOptions = new Dictionary<int, Action<MoneyTransfer>>
            {
                {0, MoneyTransferIsGoingToAnInternalAccount},
                {1, MoneyTransferIsGoingToAnInternalAccount},
                {2, MoneyTransferIsGoingToAnExternalAccount},
                {3, MoneyTransferIsGoingToAnExternalAccount},
                {4, MoneyTransferIsGoingToAnExternalNonExistingAccount},
                {5, MoneyTransferIsGoingToAnInternalAccount},
                {6, MoneyTransferIsGoingToAnInternalAccount},
                {7, MoneyTransferIsGoingToAnExternalAccount},
                {8, MoneyTransferIsGoingToAnExternalAccount},
            };
        }

        public void Send(MoneyTransfer moneyTransfer)
        {
            _systemTimer.Trigger(() => DoSend(moneyTransfer), @in: 5000);
        }

        private void DoSend(MoneyTransfer moneyTransfer)
        {
            try
            {
                // I didn't want to introduce an actual external bank, so that's why you see this nice construct :)
                _moneyTransferOptions[_systemRandom.Next(start: 0, end: 9)](moneyTransfer);
            }
            catch (Exception)
            {
                CompensatingActionBecauseOfFailedMoneyTransfer(moneyTransfer);
            }
        }

        private void MoneyTransferIsGoingToAnInternalAccount(MoneyTransfer moneyTransfer)
        {
            var account = _reportingRepository.GetByExample<AccountReport>(new { AccountNumber = moneyTransfer.TargetAccount }).First();
            _bus.Publish(new ReceiveMoneyTransferCommand(account.Id, moneyTransfer.Amount, moneyTransfer.SourceAccount));
            _bus.CommitAsync();
        }

        private void MoneyTransferIsGoingToAnExternalAccount(MoneyTransfer moneyTransfer)
        {
            _receiveMoneyTransfers.Receive(moneyTransfer);
        }

        private void MoneyTransferIsGoingToAnExternalNonExistingAccount(MoneyTransfer moneyTransfer)
        {
            _receiveMoneyTransfers.Receive(new MoneyTransfer(moneyTransfer.SourceAccount, moneyTransfer.TargetAccount?.Reverse().ToString(), moneyTransfer.Amount));
        }

        private void CompensatingActionBecauseOfFailedMoneyTransfer(MoneyTransfer moneyTransfer)
        {
            var account = _reportingRepository.GetByExample<AccountReport>(new { AccountNumber = moneyTransfer.SourceAccount }).First();
            _bus.Publish(new MoneyTransferFailedCompensatingCommand(account.Id, moneyTransfer.Amount, moneyTransfer.TargetAccount));
        }
    }
}