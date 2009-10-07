using System;
using System.Collections.Generic;

namespace Fohjin.DDD.Reporting.Dto
{
    public class AccountDetailsDto
    {
        public Guid Id { get; private set; }
        public Guid ClientId { get; private set; }
        public IEnumerable<LedgerDto> Ledgers { get; private set; }
        public string AccountName { get; private set; }

        public AccountDetailsDto(Guid id, Guid clientId, IEnumerable<LedgerDto> ledgers, string accountName)
        {
            Id = id;
            ClientId = clientId;
            Ledgers = ledgers;
            AccountName = accountName;
        }
    }
}