namespace Fohjin.DDD.Commands
{
    [Serializable]
    public class Command : ICommand
    {
        public Guid Id { get; init; }

        public Command(Guid id)
        {
            Id = id;
        }
    }
}