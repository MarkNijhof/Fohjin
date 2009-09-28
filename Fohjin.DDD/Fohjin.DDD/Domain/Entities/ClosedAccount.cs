using System;
using System.Collections.Generic;

namespace Fohjin.DDD.Domain.Entities
{
    public class ClosedAccount
    {
        private readonly List<Ledger> _ledgers;
        private Guid _id;

        public ClosedAccount()
        {
            _ledgers = new List<Ledger>();
        }

        public ClosedAccount(Guid id, List<Ledger> mutations)
        {
            _id = id;
            _ledgers = mutations;
        }
    }
}