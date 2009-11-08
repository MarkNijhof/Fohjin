using System;
using Fohjin.DDD.Events.Account;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Reporting.Dto;

namespace Fohjin.DDD.EventHandlers
{
    public class ClosedAccountCreatedEventHandler : IEventHandler<ClosedAccountCreatedEvent>
    {
        private readonly IReportingRepository _reportingRepository;

        public ClosedAccountCreatedEventHandler(IReportingRepository reportingRepository)
        {
            _reportingRepository = reportingRepository;
        }

        public void Execute(ClosedAccountCreatedEvent theEvent)
        {
            var closedAccount = new ClosedAccountReport(theEvent.AccountId, theEvent.ClientId, theEvent.AccountName, theEvent.AccountNumber);
            var closedAccountDetails = new ClosedAccountDetailsReport(theEvent.AccountId, theEvent.ClientId, theEvent.AccountName, 0, theEvent.AccountNumber);

            _reportingRepository.Save(closedAccount);
            _reportingRepository.Save(closedAccountDetails);

            foreach (var ledger in theEvent.Ledgers)
            {
                var split = ledger.Value.Split(new[] { '|' });
                var amount = Convert.ToDecimal(split[0]);
                var account = split[1];
                _reportingRepository.Save(new LedgerReport(Guid.NewGuid(), theEvent.AccountId, GetDescription(ledger.Key, account), amount));
            }
        }

        private static string GetDescription(string transferType, string accountNumber)
        {
            if (transferType == "CreditMutation")
                return "Deposite";

            if (transferType == "DebitMutation")
                return "Withdrawl";

            if (transferType == "CreditTransfer")
                return string.Format("Transfer to {0}", accountNumber);

            if (transferType == "DebitTransfer")
                return string.Format("Transfer from {0}", accountNumber);

            if (transferType == "CreditTransferFailed")
                return string.Format("Transfer to {0} failed", accountNumber);

            throw new Exception(string.Format("Transfer type '{0}' is not implemented", transferType));
        }
    }
}