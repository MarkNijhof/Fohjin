using System;

namespace Fohjin.DDD.Reporting.Dto
{
    public class AccountDto
    {
        public Guid Id { get; private set; }
        public Guid ClientId { get; private set; }
        public string Name { get; private set; }

        public AccountDto(Guid id, Guid clientId, string name)
        {
            Id = id;
            ClientId = clientId;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}