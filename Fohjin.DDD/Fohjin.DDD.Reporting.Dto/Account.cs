using System;

namespace Fohjin.DDD.Reporting.Dto
{
    public class Account
    {
        public Guid Id { get; private set; }
        public Guid ClientDetailsId { get; private set; }
        public string Name { get; private set; }

        public Account(Guid id, Guid clientDetailsId, string name)
        {
            Id = id;
            ClientDetailsId = clientDetailsId;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}