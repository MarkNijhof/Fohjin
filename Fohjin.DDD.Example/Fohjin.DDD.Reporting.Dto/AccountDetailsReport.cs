using System;
using System.Collections.Generic;

namespace Fohjin.DDD.Reporting.Dto
{
    public class AccountDetailsReport
    {
        public Guid Id { get; private set; }
        public Guid ClientReportId { get; private set; }
        public IEnumerable<LedgerReport> Ledgers { get; private set; }
        public string AccountName { get; private set; }
        public decimal Balance { get; set; }
        public string AccountNumber { get; private set; }

        public AccountDetailsReport(Guid id, Guid clientId, string accountName, decimal balance, string accountNumber)
        {
            Id = id;
            ClientReportId = clientId;
            Ledgers = new List<LedgerReport>();
            AccountName = accountName;
            Balance = balance;
            AccountNumber = accountNumber;
        }
    }
}