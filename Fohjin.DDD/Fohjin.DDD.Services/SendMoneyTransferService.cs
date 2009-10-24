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

    public class SendMoneyTransferService : ISendMoneyTransfer
    {
        private readonly ICommandBus _commandBus;
        private readonly IReportingRepository _reportingRepository;
        private readonly IAcceptMoneyTransfer _acceptMoneyTransfer;

        public SendMoneyTransferService(ICommandBus commandBus, IReportingRepository reportingRepository, IAcceptMoneyTransfer acceptMoneyTransfer)
        {
            _commandBus = commandBus;
            _reportingRepository = reportingRepository;
            _acceptMoneyTransfer = acceptMoneyTransfer;
        }

        public void Send(MoneyTransfer moneyTransfer)
        {
            // I didn't want to introduce an actual external bank, so that's why you see this nice construct :)
            if (SystemRandom.Next(0, 2) == 0)
            {
                MoneyTransferIsGoingToAnInternalAccount(moneyTransfer);
                return;
            }

            // Send the MoneyTransfer to an other bank here!
            // In this case here we threat the same MoneyTransfer as if it was coming from an external bank.
            try
            {
                _acceptMoneyTransfer.Receive(moneyTransfer);
            }
            catch(TheAccountDoesNotBelongToThisBankException)
            {
                CompensatingActionBecauseOfFailedMoneyTransfer(moneyTransfer);
            }
        }

        private void CompensatingActionBecauseOfFailedMoneyTransfer(MoneyTransfer moneyTransfer)
        {
            var account = _reportingRepository.GetByExample<Account>(new { moneyTransfer.SourceAccount }).First();
            _commandBus.Publish(new TransferMoneyToAnOtherAccountFailedCommand(account.Id, moneyTransfer.Ammount, moneyTransfer.TargetAccount));
        }

        private void MoneyTransferIsGoingToAnInternalAccount(MoneyTransfer moneyTransfer)
        {
            var account = _reportingRepository.GetByExample<Account>(new { moneyTransfer.TargetAccount }).First();
            _commandBus.Publish(new TransferMoneyFromAnOtherAccountCommand(account.Id, moneyTransfer.Ammount, moneyTransfer.SourceAccount));
        }
    }
}