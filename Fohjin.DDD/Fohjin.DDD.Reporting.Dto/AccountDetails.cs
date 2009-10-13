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

        public AccountDetails(Guid id, Guid clientId, string accountName)
        {
            Id = id;
            ClientId = clientId;
            Ledgers = new List<Ledger>();
            AccountName = accountName;
        }
    }
}