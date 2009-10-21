using System;

namespace Fohjin.DDD.Reporting.Dto
{
    public class Account
    {
        public Guid Id { get; private set; }
        public Guid ClientDetailsId { get; private set; }
        public string Name { get; private set; }
        public string AccountNumber { get; private set; }
        public bool Active { get; private set; }

        public Account(Guid id, Guid clientDetailsId, string name, string accountNumber, bool active)
        {
            Id = id;
            ClientDetailsId = clientDetailsId;
            Name = name;
            AccountNumber = accountNumber;
            Active = active;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}