namespace Fohjin.DDD.Commands
{
    public abstract class Command : ICommand
    {
        public Guid Id { get; init; }

        public Command(Guid id)
        {
            Id = id;
        }
    }
}