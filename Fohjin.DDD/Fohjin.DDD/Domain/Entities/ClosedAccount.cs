using System;
using System.Collections.Generic;

namespace Fohjin.DDD.Domain.Entities
{
    public class ClosedAccount
    {
        private readonly List<Ledger> _mutations;
        private Guid _id;

        public ClosedAccount()
        {
            _mutations = new List<Ledger>();
        }

        public ClosedAccount(List<Ledger> mutations)
        {
            _mutations = mutations;
        }
    }
}