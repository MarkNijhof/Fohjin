using System;

namespace Fohjin.DDD.Reporting.Dto
{
    public class Account
    {
        public Guid Id { get; private set; }
        public Guid ClientDetailsId { get; private set; }
        public string Name { get; private set; }
        public string AccountNumber { get; private set; }

        public Account(Guid id, Guid clientDetailsId, string name, string accountNumber)
        {
            Id = id;
            ClientDetailsId = clientDetailsId;
            Name = name;
            AccountNumber = accountNumber;
        }

        public override string ToString()
        {
            return string.Format("{0} - ({1})", AccountNumber, Name);
        }
    }
}