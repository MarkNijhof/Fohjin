using System;
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

        public MoneyTransferService(ICommandBus commandBus, IReportingRepository reportingRepository, IReceiveMoneyTransfers receiveMoneyTransfers)
        {
            _commandBus = commandBus;
            _reportingRepository = reportingRepository;
            _receiveMoneyTransfers = receiveMoneyTransfers;
        }

        public void Send(MoneyTransfer moneyTransfer)
        {
            try
            {
                // I didn't want to introduce an actual external bank, so that's why you see this nice construct :)
                if (SystemRandom.Next(0, 2) == 0)
                {
                    MoneyTransferIsGoingToAnInternalAccount(moneyTransfer);
                    return;
                }

                // Send the MoneyTransfer to an other bank here!
                // In this case here we threat the same MoneyTransfer as if it was coming from an external bank.
                _receiveMoneyTransfers.Receive(moneyTransfer);
            }
            catch(Exception)
            {
                CompensatingActionBecauseOfFailedMoneyTransfer(moneyTransfer);
            }
        }

        private void MoneyTransferIsGoingToAnInternalAccount(MoneyTransfer moneyTransfer)
        {
            var account = _reportingRepository.GetByExample<AccountReport>(new { moneyTransfer.TargetAccount }).First();
            _commandBus.Publish(new ReceiveMoneyTransferCommand(account.Id, moneyTransfer.Ammount, moneyTransfer.SourceAccount));
        }

        private void CompensatingActionBecauseOfFailedMoneyTransfer(MoneyTransfer moneyTransfer)
        {
            var account = _reportingRepository.GetByExample<AccountReport>(new { moneyTransfer.SourceAccount }).First();
            _commandBus.Publish(new MoneyTransferFailedCommand(account.Id, moneyTransfer.Ammount, moneyTransfer.TargetAccount));
        }
    }
}