using System;

namespace Fohjin.DDD.Reporting.Dto
{
    public class ClientDto
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public ClientDto(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}