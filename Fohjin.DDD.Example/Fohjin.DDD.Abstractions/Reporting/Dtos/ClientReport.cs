namespace Fohjin.DDD.Reporting.Dtos
{
    public class ClientReport
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public ClientReport(Guid id, string name)
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