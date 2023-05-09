namespace Fohjin.DDD.Reporting.Dtos
{
    public class ClientReport
    {
        public Guid Id { get; init; }
        public string Name { get; init; }

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