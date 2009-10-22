using System;
using System.Collections.Generic;

namespace Fohjin.DDD.Reporting.Dto
{
    public class AccountDetails
    {
        public Guid Id { get; private set; }
        public Guid ClientId { get; private set; }
        public IEnumerable<Ledger> Ledgers { get; private set; }
        public string AccountName { get; private set; }
        public decimal Balance { get; set; }
        public string AccountNumber { get; private set; }

        public AccountDetails(Guid id, Guid clientId, string accountName, decimal balance, string accountNumber)
        {
            Id = id;
            ClientId = clientId;
            Ledgers = new List<Ledger>();
            AccountName = accountName;
            Balance = balance;
            AccountNumber = accountNumber;
        }
    }
}