using System;
using System.Linq;
using Fohjin.DDD.Bus;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Reporting.Dto;

namespace Fohjin.DDD.Services
{
    public interface IReceiveMoneyTransfers
    {
        void Receive(MoneyTransfer moneyTransfer);
    }

    public class MoneyReceiveService : IReceiveMoneyTransfers
    {
        private readonly IBus _bus;
        private readonly IReportingRepository _reportingRepository;

        public MoneyReceiveService(IBus bus, IReportingRepository reportingRepository)
        {
            _bus = bus;
            _reportingRepository = reportingRepository;
        }

        public void Receive(MoneyTransfer moneyTransfer)
        {
            MoneyTransferIsGoingToAnInternalAccount(moneyTransfer);
        }

        private void MoneyTransferIsGoingToAnInternalAccount(MoneyTransfer moneyTransfer)
        {
            try
            {
                var account = _reportingRepository.GetByExample<AccountReport>(new {moneyTransfer.TargetAccount}).First();
                _bus.Publish(new ReceiveMoneyTransferCommand(account.Id, moneyTransfer.Ammount, moneyTransfer.SourceAccount));
            }
            catch(Exception)
            {
                RequestedAccountDoesNotExist(moneyTransfer);
            }
        }

        private static void RequestedAccountDoesNotExist(MoneyTransfer moneyTransfer)
        {
            throw new UnknownAccountException(string.Format("The requested account '{0}' is not managed by this bank", moneyTransfer.TargetAccount));
        }
    }
}