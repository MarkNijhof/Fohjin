using System.Linq;
using Fohjin.DDD.Bus;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Contracts;
using Fohjin.DDD.Domain;
using Fohjin.DDD.Reporting.Dto;

namespace Fohjin.DDD.Services
{
    public interface IAcceptMoneyTransfer
    {
        void Receive(MoneyTransfer moneyTransfer);
    }

    public class AcceptMoneyTransferService : IAcceptMoneyTransfer
    {
        private readonly ICommandBus _commandBus;
        private readonly IReportingRepository _reportingRepository;

        public AcceptMoneyTransferService(ICommandBus commandBus, IReportingRepository reportingRepository)
        {
            _commandBus = commandBus;
            _reportingRepository = reportingRepository;
        }

        public void Receive(MoneyTransfer moneyTransfer)
        {
            // I didn't want to introduce an actual account that didn't exists so that's why this nice construct :)
            if (SystemRandom.Next(0, 2) == 0)
            {
                MoneyTransferIsGoingToAnInternalAccount(moneyTransfer);
                return;
            }

            // The account is <quote><quote>not found<quote><quote> so we throw an exception
            RequestedAccountDoesNotExist(moneyTransfer);
        }

        private static void RequestedAccountDoesNotExist(MoneyTransfer moneyTransfer)
        {
            throw new TheAccountDoesNotBelongToThisBankException(string.Format("The requested account '{0}' is not managed by this bank", moneyTransfer.TargetAccount));
        }

        private void MoneyTransferIsGoingToAnInternalAccount(MoneyTransfer moneyTransfer)
        {
            var account = _reportingRepository.GetByExample<Account>(new { moneyTransfer.TargetAccount }).First();
            _commandBus.Publish(new TransferMoneyFromAnOtherAccountCommand(account.Id, moneyTransfer.Ammount, moneyTransfer.SourceAccount));
        }
    }
}