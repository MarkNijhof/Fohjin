using System;

namespace Fohjin.DDD.Reporting.Dto
{
    public class Client
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public Client(Guid id, string name)
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